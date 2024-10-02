using UnityEngine;

public class ShootAbility : AbilityBase
{
    [SerializeField]
    private int _damage;

    [SerializeField]
    private GameObject _shootParticleSystemPrefab;
    [SerializeField]
    private Transform _shootPosition;

    public override void Ability()
    {
        GameObject energyBall = Instantiate(_shootParticleSystemPrefab, _shootPosition.position, _shootPosition.rotation);
        energyBall.transform.parent = transform;
        Destroy(energyBall, 4f);
        
    }

    public void SendDamage(GameObject other)
    {
        var damageable = other.GetComponent<IDamageable>();
        if (damageable == null)
            return;

        damageable.TakeDamage(_damage);
    }

    
}
