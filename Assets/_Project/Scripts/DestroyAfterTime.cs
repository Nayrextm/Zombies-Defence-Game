using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [Tooltip("Object time in seconds")]
    [SerializeField] private float _lifetime = 5f;
    void Start()
    {

        Destroy(gameObject, _lifetime);
    }

    
}
