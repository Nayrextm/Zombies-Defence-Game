using System.Collections;
using StarterAssets;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform shootingPoint;

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
            _nextFireTime = Time.time + weaponData.fireRate;

            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Постріл з " + weaponData.weaponName);

        RaycastHit hit;
        
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out hit, weaponData.maxDistance))
        {
            Debug.Log("Влучання в " + hit.transform.name);
        }
    }
}
