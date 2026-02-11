
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Налаштування інерції")]
    [SerializeField] private float _smooth = 8f;
    [SerializeField] private float _swayMultipliyer = 2f;

    private StarterAssets.StarterAssetsInputs _input;
    private void Awake()
    {
        _input = GetComponentInParent<StarterAssets.StarterAssetsInputs>();
    }
    
    void Update()
    {
        float mouseX = _input.look.x * _swayMultipliyer;
        float mouseY = _input.look.y * _swayMultipliyer;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * _smooth);
    }
}
