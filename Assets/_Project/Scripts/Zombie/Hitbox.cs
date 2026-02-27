
using UnityEngine;

public class Hitbox : MonoBehaviour, IDamageable
{

    [Header("Зв'язок з мозком")]
    [SerializeField] private ZombieHealth _mainHealth;

    [Header("Налаштування зони")]
    [SerializeField] private float _damageMultiplier = 1f;
    [SerializeField] private int _bonusPoints = 0;



    public void TakeDamage(float damage)
    {
        if (_mainHealth != null)
        {
            float finalDamage = damage * _damageMultiplier;

            _mainHealth.TakeLocationalDamage(finalDamage, _bonusPoints);
        }
    }
}
