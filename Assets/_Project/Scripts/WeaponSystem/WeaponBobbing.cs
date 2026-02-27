using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class WeaponBobbing : MonoBehaviour
{
    [Header("BobbingSettings")]
    [SerializeField] private float _bobAmount = 0.05f;
    [SerializeField] private float _bobSpeed = 10f;
    [SerializeField] private float _smooth = 10f;

    [SerializeField] private float _adsReduction = 0.3f;

    private StarterAssets.StarterAssetsInputs _inputs;
    private CharacterController _controller;
    private Vector3 _startLocalPos;
    private float _timer = 0f;



    private void Awake()
    {
        _inputs = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
        _controller = GetComponentInParent<CharacterController>();
        _startLocalPos = transform.localPosition;
    }
    //void Update()
    //{
    //    Vector3 horizontalVelocity = new Vector3(_controller.velocity.x, 0, _controller.velocity.z);
    //    float currentSpeed = horizontalVelocity.magnitude;


    //    if (currentSpeed > 0.1f)
    //    {
    //        _timer *= Time.deltaTime * _bobSpeed * currentSpeed;


    //        float multiplier = _inputs.ads ? _adsReduction : 0.1f;
    //        float moveX = Mathf.Cos(_timer * 0.5f) * _bobAmount * multiplier;
    //        float moveY = Mathf.Sin(_timer) * _bobAmount * multiplier;

    //        Vector3 targetPos = _startLocalPos + new Vector3(moveX, moveY, 0);
    //        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * _smooth);

    //    }
    //    else
    //    {
    //        _timer = 0f;
    //        transform.localPosition = Vector3.Lerp(transform.localPosition, _startLocalPos, Time.deltaTime * _smooth);
    //    }
    //}
    void Update()
    {
        
        float inputMagnitude = _inputs.move.magnitude;

      
        if (inputMagnitude > 0.1f && !(_inputs.ads && _adsReduction <= 0f))
        {
            
            _timer += Time.deltaTime * _bobSpeed * (inputMagnitude * 0.5f);

            float multiplier = _inputs.ads ? _adsReduction : 1f;

            float moveX = Mathf.Sin(_timer * 0.5f) * _bobAmount * multiplier;
            float moveY = Mathf.Sin(_timer) * (_bobAmount * 1.5f) * multiplier;
            float tiltZ = Mathf.Sin(_timer * 0.5f) * (_bobAmount * 50f) * multiplier;

            Vector3 targetPos = _startLocalPos + new Vector3(moveX, moveY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * _smooth);

            Quaternion targetRot = Quaternion.Euler(0, 0, tiltZ);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * _smooth);
        }
        else
        {
           
            _timer = Mathf.Lerp(_timer, 0, Time.deltaTime * _smooth);
            transform.localPosition = Vector3.Lerp(transform.localPosition, _startLocalPos, Time.deltaTime * _smooth);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * _smooth);
        }
    }
}
