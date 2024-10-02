using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _health;

   
    public void TakeDamage(int damageAdmount)
    {
        _health -= damageAdmount;

        if (_health <= 0)
        {
            DieAction();
        }

        Debug.Log(gameObject.name + $"health: {_health} ");
    }

  

    void DieAction()
    {
        Destroy(gameObject);
    }


}
