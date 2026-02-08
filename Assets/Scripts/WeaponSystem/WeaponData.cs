
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon System/Weapon Data")]

public class WeaponData : ScriptableObject
{
    [Header("Назва зброї")]
    public string weaponName = "Pistol";

    [Header("Стрільба")]
    public float damage = 20f;
    public float maxDistance = 100f;
    public float fireRate = 0.2f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
