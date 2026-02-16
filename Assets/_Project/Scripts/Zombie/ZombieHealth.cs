
using UnityEngine;

public class ZombieHealth : MonoBehaviour, IDamageable
{
    [Header("Параметри")]
    [SerializeField] private float _maxHealth = 100f;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    
    public void TakeDamage(float damage)
    {
        if (_currentHealth <= 0) return;
        _currentHealth -= damage;
        Debug.Log($"{gameObject.name} отримав {damage} шкоди. Залишилось: {_currentHealth}");


        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    
    private void Die()
    {
        Debug.Log("Зомбі загинув!");


        Destroy(gameObject);
    }
}
