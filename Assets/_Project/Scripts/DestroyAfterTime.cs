
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Tooltip("„ас об'Їкта в секундах")]
    [SerializeField] private float _lifetime = 5f;
    void Start()
    {

        Destroy(gameObject, _lifetime);
    }

    
}
