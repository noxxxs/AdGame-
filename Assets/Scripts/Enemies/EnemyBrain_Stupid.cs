using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.LightAnchor;

public class EnemyBrain_Stupid : MonoBehaviour
{
    public Transform _target;
   
    private EnemyReferences _enemyReferences;

    private float _pathUpdateDeadline;

    private Rigidbody _rb;


    public float _chaseDistance;

    private void Awake()
    {
        _enemyReferences = GetComponent<EnemyReferences>();
        _rb = GetComponent<Rigidbody>();
        _target = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }


    private void Start()
    {
        _enemyReferences._navMeshAgent.updateUpAxis = false;
    }


    private void Update()
    {
        if (_target != null)
        {
            bool inRange = Vector3.Distance(transform.position, _target.position) <= _chaseDistance;
            bool inAttackRange = Vector3.Distance(transform.position, _target.position) <= _enemyReferences._navMeshAgent.stoppingDistance;

            if (inRange && !inAttackRange)
            {
                UpdatePath();
                LookAtTarget();
                _enemyReferences._animator.SetBool("isAttacking", false);
            }
              else if (inAttackRange)
            {
                LookAtTarget();
                _enemyReferences._animator.SetBool("isAttacking", true);
                _enemyReferences._navMeshAgent.ResetPath();
            }



            if (_enemyReferences._navMeshAgent.desiredVelocity.sqrMagnitude > 0 && !inAttackRange)
            {
                _enemyReferences._animator.SetBool("isWalking", true);
            }
            else if (!inAttackRange)
            {
                _enemyReferences._animator.SetBool("isWalking", false);
            }


        }
    }


    void LookAtTarget()
    {
        Vector3 lookPos = _target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }


    void UpdatePath()
    {
        if (Time.time >= _pathUpdateDeadline)
        {
            _pathUpdateDeadline = Time.time + _enemyReferences._pathUpdateDelay;
            _enemyReferences._navMeshAgent.SetDestination(_target.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _chaseDistance);
    }


}











