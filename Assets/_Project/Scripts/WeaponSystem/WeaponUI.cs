using UnityEngine;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public static WeaponUI Instance;

    [SerializeField] private TextMeshProUGUI _ammoText;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateAmmoText(int current, int reserve)
    {
        if (_ammoText != null)
        {
            _ammoText.text = $"{current}/{reserve}";
        }
    }
}
