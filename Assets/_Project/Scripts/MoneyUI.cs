using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _moneyText;


    private void OnEnable()
    {
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyChanged += UpdateUI;

    }
    private void Start()
    {
        
        if (MoneyManager.Instance != null)
        {
            MoneyManager.Instance.OnMoneyChanged += UpdateUI;
           
            UpdateUI(MoneyManager.Instance.GetCurrentMoney());
        }
    }

    private void OnDisable()
    {

        if (MoneyManager.Instance != null)
            MoneyManager.Instance.OnMoneyChanged -= UpdateUI;
    }

    
    private void UpdateUI(int currentBalance)
    {
        _moneyText.text = $"$ {currentBalance}";
    }
    
}
