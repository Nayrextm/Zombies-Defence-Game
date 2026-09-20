using UnityEngine;
using DG.Tweening;

public class WeaponCollision : MonoBehaviour
{
    [SerializeField] private WeaponData _data;
    [SerializeField] private LayerMask _hitMask;
    [SerializeField] private float _smoothSpeed = 12f;
    [SerializeField] private float _maxTiltAngle = 15f;

    private float _currentPushZ;
    private float _currentTilt; 

    private Camera _mainCam;
    private WeaponController _controller;

    private void Awake()
    {
        _mainCam = Camera.main;
        _controller = GetComponentInParent<WeaponController>();
    }

    void Update()
    {
        if (_controller != null && _controller.IsReloading) return;

        RaycastHit hit;
        Vector3 camPos = _mainCam.transform.position;
        Vector3 camForward = _mainCam.transform.forward;

        float targetPush = 0f;
        float targetTilt = 0f; 

        if (Physics.Raycast(camPos, camForward, out hit, _data.wallCheckDistance, _hitMask))
        {
            float ratio = 1f - (hit.distance / _data.wallCheckDistance);
            targetPush = -ratio * _data.maxPushBack;
            targetTilt = ratio * _maxTiltAngle; 
        }

        
        _currentPushZ = Mathf.Lerp(_currentPushZ, targetPush, Time.deltaTime * _smoothSpeed);
        _currentTilt = Mathf.Lerp(_currentTilt, targetTilt, Time.deltaTime * _smoothSpeed); 
        
        if (Mathf.Abs(transform.localPosition.z - _currentPushZ) > 0.0001f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, _currentPushZ);
        }

        transform.localRotation = Quaternion.Euler(-_currentTilt, 0, 0);
    }
}