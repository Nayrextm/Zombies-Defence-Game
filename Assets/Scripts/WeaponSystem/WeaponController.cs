using System.Collections;
using StarterAssets;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private WeaponRecoil _recoilWeapon;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private Transform _shootingPoint;

    private float _nextFireTime;
    private StarterAssetsInputs _input;

    private void Awake()
    {
        _input = GetComponentInParent<StarterAssetsInputs>();
    }



    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (_input.shoot && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + _weaponData.fireRate;

            Shoot();
            _input.shoot = false;
        }
    }

    private void Shoot()
    {

        Ray cameraRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;

        if (Physics.Raycast(cameraRay, out RaycastHit hit, _weaponData.maxDistance))
            targetPoint = hit.point;
        else
            targetPoint = cameraRay.GetPoint(_weaponData.maxDistance);

        Vector3 direction = targetPoint - _shootingPoint.position;


        _recoilWeapon.RecoilFire();




        if (Physics.Raycast(_shootingPoint.position, direction, out RaycastHit finalHit, _weaponData.maxDistance))
        {
            Debug.Log($"Влучання: {finalHit.transform.name}");

            if (_weaponData.impactVFXprefab != null)
            {
                Instantiate(_weaponData.impactVFXprefab, finalHit.point, Quaternion.LookRotation(finalHit.normal));
            }

            if (_weaponData.bulletHolePrefab != null)
            {
               
                Vector3 spawnPosition = finalHit.point + (finalHit.normal * 0.02f);

                
                Quaternion holeRotation = Quaternion.LookRotation(finalHit.normal);

                GameObject hole = Instantiate(_weaponData.bulletHolePrefab, spawnPosition, holeRotation);

               
                hole.transform.Rotate(0, 180, 0);

                
                hole.transform.SetParent(finalHit.transform);

                
                hole.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            }





            if (finalHit.transform.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.TakeDamage(_weaponData.damage);
            }
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