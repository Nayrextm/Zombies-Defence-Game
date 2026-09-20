
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon System/Weapon Data")]

public class WeaponData : ScriptableObject
{
    [Header("Weapon Name")]
    public string weaponName = "Pistol";

    [Header("Shooting")]
    public float damage = 20f;
    public float maxDistance = 100f;
    public float fireRate = 0.2f;

    
    [Header("Range & Accuracy Decay")]
    public float effectiveRange = 50f;
    public float accuracyDecayIntensity= 0.2f;

    [Header("Dynamic Spread")]
    public float moveSpreadMultiplier = 2.5f;
    public float spread = 0.05f;

    [Header("Recoil")]
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

    [Header("Ammo Settings")]
    public int magSize = 12;
    public int maxReserveAmmo = 64;
    public float reloadTime = 1.5f;

    [Header("Reload time")]
    public float reloadPartialTime = 1.5f;
    public float reloadEmptyTime = 2.2f;

    [Header("Sound Settings")]
    public AudioClip shootSound;
    public AudioClip dryFireSound; 
    [Range(0, 1)] public float shootVolume = 0.5f;
    public float dryFireVolume = 0.4f;

    [Header("Reload Sounds")] 
    public AudioClip reloadPartialSound;
    public AudioClip reloadEmptySound;
    [Range(0, 1)] public float reloadVolume = 0.7f;

    [Header("Physical Senses")]
    public float wallCheckDistance = 0.5f;
    public float maxPushBack = 0.35f;

    [Header("Hit Effects")]
    [Tooltip("Instant effect: sparks, dust, blood splatters")]
    public GameObject impactVFXprefab;

    [Tooltip("Permanent effect: bullet hole (decal)")]
    public GameObject bulletHolePrefab;
}
