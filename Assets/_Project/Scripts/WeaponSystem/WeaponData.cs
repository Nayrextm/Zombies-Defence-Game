
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

    
    [Header("Range & Accuracy Decay")]
    public float effectiveRange = 50f;
    public float accuracyDecayIntensity= 0.2f;

    [Header("Dynamic Spread")]
    public float moveSpreadMultiplier = 2.5f;
    public float spread = 0.05f;

    [Header("Віддача|Recoil")]
    public float recoilX = 2f;
    public float recoilY = 1.5f;
    public float recoilZ = 0.5f;
    public float snapiness = 10f;
    public float returnSpeed = 5f;

    [Header("ADS Settings")]
    public float adsZoomFov = 40f;
    public float adsSpeed = 10f;
    public float adsSpreadMultiplier = 0.1f;
    public Vector3 adsPositionOffset;


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
