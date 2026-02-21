using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 100f;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        Debug.Log($"Гравцеві боляче! Залишилось: {_health}");

        if (_health <= 0)
        {
            Debug.Log("Гра закінчена!");
            
        }
    }
}