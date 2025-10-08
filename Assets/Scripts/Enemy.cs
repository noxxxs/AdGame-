using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public void Damage()
    {
        MoveEnemyManager.MoveEnemyInstance.RemoveFromActiveEnemysList(gameObject);
        gameObject.SetActive(false);
        UnitCounter.Instance._unitCounter--;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Damage();
        }
    }

}
