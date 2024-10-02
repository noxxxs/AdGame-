using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[DisallowMultipleComponent]
public class EnemyReferences : MonoBehaviour
{
    public NavMeshAgent _navMeshAgent;
    public Animator _animator;

    [Header("Stats")]
    public float _pathUpdateDelay = 0.2f;
    void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
