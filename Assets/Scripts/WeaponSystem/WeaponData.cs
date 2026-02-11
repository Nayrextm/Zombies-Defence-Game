
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

    [Header("Віддача|Recoil")]
    public float recoilX = 2f;
    public float recoilY = 1.5f;
    public float recoilZ = 0.5f;
    public float snapiness = 10f;
    public float returnSpeed = 5f;


    [Header("Ефекти Влучання")]
    [Tooltip("Миттєвий ефект: іскри, пил, бризки крові")]
    public GameObject impactVFXprefab;

    [Tooltip("Постійний ефект: дірка від кулі (деколь)")]
    public GameObject bulletHolePrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
