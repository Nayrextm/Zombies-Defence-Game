using UnityEngine;
using UnityEngine.AI;
using System;

public class ZombieAI : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _attackDistance = 1.5f;
    [SerializeField] private float _attackCooldown = 1.5f;

    [Header("Move Settings")]
    [SerializeField] private float _updatePathDelay = 0.2f;


    [Header("Animations")]
    [SerializeField] private Animator _animator;

    [SerializeField] private ZombieAnimationEvents _animationEvents;

    private readonly int _isMovingHash = Animator.StringToHash("IsMoving");
    private readonly int _doAttackHash = Animator.StringToHash("DoAttack");

    private bool _wasMoving;

    private NavMeshAgent _agent;
    private Transform _playerTransform;
    private float _nextUpdateTime;
    private float _lastAttackTime;

    private float _sqrAttackDistance;

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

        _sqrAttackDistance = _attackDistance * _attackDistance;
    }

    private void OnEnable()
    {
        if (_animationEvents != null)
        {
            _animationEvents.OnAttackHitEvent += DealDamageToPlayer;
        }
    }

    private void OnDisable()
    {
        if (_animationEvents != null)
        {
            _animationEvents.OnAttackHitEvent -= DealDamageToPlayer;
        }
    }

    void Update()
    {
        if (_playerTransform == null) return;

        bool isMovingNow = _agent.velocity.sqrMagnitude > 0.05f;

        
        if (isMovingNow != _wasMoving)
        {
            if (_animator != null)
            {
                _animator.SetBool(_isMovingHash, isMovingNow);
            }
           
            _wasMoving = isMovingNow;
        }

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
        Vector3 offset = _playerTransform.position - transform.position;

        float sqrDistance = offset.sqrMagnitude;


        if (sqrDistance <= _sqrAttackDistance)
        {
            _agent.isStopped = true;

            FaceTarget(offset);

            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                Attack();
                _lastAttackTime = Time.time;
            }
        }
        else
        {
            _agent.isStopped = false;
        }
    }

    private void Attack()
    {
        if(_animator != null)
        {
            _animator.SetTrigger(_doAttackHash);
        }
    }
    private void DealDamageToPlayer()
    {
        Debug.Log("The zombie struck right on target!");

        if (_playerTransform != null && _playerTransform.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(10f); 
        }
    }


    private void FaceTarget(Vector3 direction)
    {
        direction.y = 0;

        if (direction.sqrMagnitude > 0.01f) 
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}