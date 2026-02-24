
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    [Header("Налаштування здоров'я")]
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    [Header("Економіка")]
    [SerializeField] private int _rewardForKill = 100;

    private bool _isDead = false;

    private void Awake()
    {
       
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isDead) return;

        
        _currentHealth -= damage;

        Debug.Log($"{gameObject.name} отримав {damage} шкоди. Залишилось: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
        if (_isDead) return;
        _isDead = true;

       
        if (MoneyManager.Instance != null)
        {
           
            MoneyManager.Instance.AddMoney(_rewardForKill);
        }

        Debug.Log($"Зомбі вбито! Нагорода: ${_rewardForKill}");

        
        if (TryGetComponent(out UnityEngine.AI.NavMeshAgent agent)) agent.isStopped = true;
        if (TryGetComponent(out Collider col)) col.enabled = false;

        
        Destroy(gameObject, 0.5f);
    }
}