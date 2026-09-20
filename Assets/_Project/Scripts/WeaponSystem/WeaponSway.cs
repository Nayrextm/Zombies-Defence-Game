
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Inertia settings")]
    [SerializeField] private float _smooth = 8f;
    [SerializeField] private float _swayMultiplier = 2f;

    [SerializeField] private float _adsSwayReduction = 0.2f;

    private StarterAssets.StarterAssetsInputs _input;
    private void Awake()
    {
        _input = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }

    void Update()
    {
        float currentSwayMultiplier = _input.ads ? (_swayMultiplier * _adsSwayReduction) : _swayMultiplier;

        float mouseX = _input.look.x * currentSwayMultiplier;
        float mouseY = _input.look.y * currentSwayMultiplier;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * _smooth);
    }
}
