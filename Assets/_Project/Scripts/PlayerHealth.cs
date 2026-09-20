using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 100f;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        Debug.Log($"The player is in pain! Remaining HP is: { _health}");

        if (_health <= 0)
        {
            Debug.Log("Game Over!");
        }
    }
}