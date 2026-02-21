using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Налаштування")]
    [SerializeField] private float _attackDistance = 1.5f;
    [SerializeField] private float _updatePathDelay = 0.2f;

    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private float _nextUpdateTime;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        
        if (Player.Instance != null)
        {
            _playerTransform = Player.Instance.transform;
        }
    }

    void Update()
    {
        
        if (_playerTransform == null) return;

       
        if (Time.time > _nextUpdateTime)
        {
            _nextUpdateTime = Time.time + _updatePathDelay;
            MoveToPlayer();
        }

        CheckAttackDistance();
    }

    private void MoveToPlayer()
    {
       
        _agent.SetDestination(_playerTransform.position);
    }

    private void CheckAttackDistance()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (distance <= _attackDistance)
        {
            _agent.isStopped = true;
            Attack();
        }
        else
        {
           
            _agent.isStopped = false;
        }
    }

    private void Attack()
    {
        
        Debug.Log("Зомбі атакує гравця!");
    }
}