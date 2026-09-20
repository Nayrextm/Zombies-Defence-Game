using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    [Header("Health Settings")]
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    [Header("Economy")]
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

        Debug.Log($"{gameObject.name} got {damage} damage. HP left: {_currentHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeLocationalDamage(float damage, int bonusPoints)
    {

        if (_isDead) return;

        _currentHealth -= damage;

        if (bonusPoints > 0 && MoneyManager.Instance != null)
        {
            MoneyManager.Instance.AddMoney(bonusPoints);
        }

        Debug.Log($"Hit! Has been caused {damage} damage. Insta bonus: +{bonusPoints}. HP Left: {_currentHealth}");

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
            MoneyManager.Instance.AddMoney(_rewardForKill );
        }

        Debug.Log($"Zombie killed! Total kill reward: ${_rewardForKill }");

        if (TryGetComponent(out UnityEngine.AI.NavMeshAgent agent)) agent.isStopped = true;
        if (TryGetComponent(out Collider col)) col.enabled = false;

        Destroy(gameObject, 0.5f);
    }
}