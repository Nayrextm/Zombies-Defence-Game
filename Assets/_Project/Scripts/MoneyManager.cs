using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [Header("Fine-tuning the economy")]
    [SerializeField] private int _startingMoney = 500;

    private int _currentMoney;

    public event Action<int> OnMoneyChanged;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _currentMoney = _startingMoney;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        OnMoneyChanged?.Invoke(_currentMoney);
    }

    public void AddMoney(int amount)
    {
        _currentMoney += amount;
        OnMoneyChanged?.Invoke(_currentMoney);
    }
    public bool TrySpendMoney(int amount)
    {
        if (_currentMoney >= amount)
        {
            _currentMoney -= amount;
            OnMoneyChanged?.Invoke(_currentMoney);
            return true;
        }

        Debug.Log("Insufficient funds for the purchase!");
        return false;
    }

    public int GetCurrentMoney()
    {
        return _currentMoney;
    }

}
