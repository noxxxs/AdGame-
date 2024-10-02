using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAbilityCollision : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _shootParticleSystem;
    [SerializeField]
    private ShootAbility _ShootAbilityScript;
    private List<ParticleCollisionEvent> _particleCollisionEvents;


    private void Start()
    {
        _shootParticleSystem = GetComponent<ParticleSystem>();
        _particleCollisionEvents = new List<ParticleCollisionEvent>();
        _ShootAbilityScript = gameObject.GetComponentInParent<ShootAbility>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(_shootParticleSystem, other, _particleCollisionEvents);

        for (int i = 0; i < _particleCollisionEvents.Count; i++)
        {
            _ShootAbilityScript.SendDamage(other);
        }
    }
}
