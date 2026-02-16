using System.Collections;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using DG.Tweening;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponRecoil _recoilWeapon;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform _shootingPoint;

    [SerializeField] private StarterAssets.FirstPersonController _playerController;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private float _defaultFOV;
    private Vector3 _hipPosition;

    private float _nextFireTime;
    private StarterAssetsInputs _input;
    private CharacterController _characterController;

    private Tween _adsTween;
    private Tween _fovTween;



    private void Awake()
    {
        _input = GetComponentInParent<StarterAssetsInputs>();
        if (_playerController != null)
        {
            _characterController = _playerController.GetComponent<CharacterController>();
        }
    }

    private void Start()
    {
        _defaultFOV = _virtualCamera.m_Lens.FieldOfView;
        _hipPosition = _shootingPoint.parent.localPosition;
    }

    private void Update()
    {
        HandleShooting();
        CheckInputForADS();
    }

    private bool _lastAdsState;
    private void CheckInputForADS()
    {
        if (_input != null && _input.ads != _lastAdsState)
        {
            _lastAdsState = _input.ads;
            PlayADSTween(_lastAdsState);
        }
    }

    private void PlayADSTween(bool isAiming)
    {
        float targetFOV = isAiming ? _weaponData.adsZoomFov : _defaultFOV;
        Vector3 targetPos = isAiming ? _weaponData.adsPositionOffset : _hipPosition;

        _adsTween?.Kill();
        _fovTween?.Kill();



        //_virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(_virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * _weaponData.adsSpeed);
        //_shootingPoint.parent.localPosition = Vector3.Lerp(_shootingPoint.parent.localPosition, targetPos, Time.deltaTime * _weaponData.adsSpeed);


        _adsTween = _shootingPoint.parent.DOLocalMove(targetPos, _weaponData.adsSpeed)
            .SetEase(Ease.OutBack)
            .SetUpdate(UpdateType.Normal, true);


        _fovTween = DOTween.To(() => _virtualCamera.m_Lens.FieldOfView,
            x => _virtualCamera.m_Lens.FieldOfView = x, targetFOV, _weaponData.adsSpeed)
            .SetEase(Ease.InOutSine);
    }

    private void HandleShooting()
    {
        if (_input != null && _input.shoot && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + _weaponData.fireRate;

            Shoot();
            _input.shoot = false;
        }
    }


    private void Shoot()
    {

        float currentSpread = _weaponData.spread;

        if (_input.ads)
        {

            currentSpread *= _weaponData.adsSpreadMultiplier;
        }
        else if (_characterController != null && _characterController.velocity.magnitude > 0.1f)
        {

            currentSpread *= _weaponData.moveSpreadMultiplier;
        }


        Ray cameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;

        if (Physics.Raycast(cameraRay, out RaycastHit hit, _weaponData.maxDistance))
            targetPoint = hit.point;
        else
            targetPoint = cameraRay.GetPoint(_weaponData.maxDistance);


        float distanceToTarget = Vector3.Distance(_shootingPoint.position, targetPoint);

        if (distanceToTarget > _weaponData.effectiveRange)
        {

            float extraDistance = distanceToTarget - _weaponData.effectiveRange;
            currentSpread += extraDistance * _weaponData.accuracyDecayIntensity;
        }


        Vector3 direction = targetPoint - _shootingPoint.position;


        Vector2 spreadOffset = Random.insideUnitCircle * currentSpread;

        Vector3 finalDirection = direction.normalized +
            (_shootingPoint.right * spreadOffset.x) +
            (_shootingPoint.up * spreadOffset.y);

        if (_recoilWeapon != null) _recoilWeapon.RecoilFire();

        _virtualCamera.transform.DOShakePosition(0.1f, 0.2f, 10, 90f);

        if (Physics.Raycast(_shootingPoint.position, finalDirection, out RaycastHit finalHit, _weaponData.maxDistance))
        {
            Debug.DrawRay(_shootingPoint.position, finalDirection * _weaponData.maxDistance, Color.red, 2f);
            DoImpactEffects(finalHit);
        }
    }

    private void DoImpactEffects(RaycastHit hit)
    {
        if (_weaponData.impactVFXprefab != null)
        {
            Instantiate(_weaponData.impactVFXprefab, hit.point, Quaternion.LookRotation(hit.normal));
        }

        if (_weaponData.bulletHolePrefab != null)
        {
            Vector3 spawnPosition = hit.point + (hit.normal * 0.02f);
            GameObject hole = Instantiate(_weaponData.bulletHolePrefab, spawnPosition, Quaternion.LookRotation(hit.normal));
            hole.transform.Rotate(0, 180, 0);
            hole.transform.SetParent(hit.transform);
            hole.transform.localScale = Vector3.one * 0.05f;
        }

        if (hit.transform.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(_weaponData.damage);
        }

    }

}

//if (_weaponData.bulletHolePrefab != null)
//{
//    Vector3 spawnPosition = finalHit.point + (finalHit.normal * 0.01f);

//    Quaternion holeRotation = Quaternion.LookRotation(finalHit.normal);

//    GameObject hole = Instantiate(_weaponData.bulletHolePrefab, spawnPosition, holeRotation);

//    hole.transform.SetParent(finalHit.transform);
//}