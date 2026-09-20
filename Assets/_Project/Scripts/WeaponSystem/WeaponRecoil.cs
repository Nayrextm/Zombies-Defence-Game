using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    private const float RecoilThreshold = 0.01f;

    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    [SerializeField] private WeaponData _weaponData;

    void Update()
    {
       
        if (_targetRotation == Vector3.zero && _currentRotation == Vector3.zero) return;
       
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, Time.deltaTime * _weaponData.returnSpeed);
        _currentRotation = Vector3.Lerp(_currentRotation, _targetRotation, Time.deltaTime * _weaponData.snapiness);

        transform.localRotation = Quaternion.Euler(_currentRotation);
       
        if (_currentRotation.sqrMagnitude < RecoilThreshold)
        {
            _currentRotation = Vector3.zero;
            _targetRotation = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }

    public void RecoilFire()
    {
        _targetRotation += new Vector3(-_weaponData.recoilX,
            Random.Range(-_weaponData.recoilY, _weaponData.recoilY),
            Random.Range(-_weaponData.recoilZ, _weaponData.recoilZ));
    }
}