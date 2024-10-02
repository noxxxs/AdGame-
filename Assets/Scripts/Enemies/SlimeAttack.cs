using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlimeAttack : MonoBehaviour
{
    [SerializeField] private float _raycastAttackDistance;
    [SerializeField] private LayerMask _attackLayer;

    SkinnedMeshRenderer[] _skinnedMeshRenderer;

    private void Update()
    {
        Vector3 direction = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        Debug.DrawRay(transform.position, direction * _raycastAttackDistance, Color.red);

    }
    public void Attack()
    {
        Vector3 direction = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, _raycastAttackDistance, _attackLayer))
        {
            if (hit.transform.TryGetComponent<Enemy>(out Enemy T))
            {
                T.TakeDamage(20);
            }
        }
        

    }
}
