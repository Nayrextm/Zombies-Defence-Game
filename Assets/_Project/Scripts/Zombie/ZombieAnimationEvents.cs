using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieAnimationEvents : MonoBehaviour
{
    public event Action OnAttackHitEvent;

    public void TriggerAttackHit()
    {
        
        OnAttackHitEvent?.Invoke();
    }
}
