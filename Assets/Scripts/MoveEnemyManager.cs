using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyManager : MonoBehaviour
{
    public static MoveEnemyManager MoveEnemyInstance;
    [SerializeField] public List<GameObject> _activeEnemyList;

    public float EnemyMoveSpeed;

    void Awake()
    {
        MoveEnemyInstance = this;
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        foreach (var enemy in _activeEnemyList)
        {
            enemy.GetComponent<Rigidbody>().linearVelocity = transform.forward * -1 * EnemyMoveSpeed * 5;
        }
    }

    public void AddToActiveEnemyList(GameObject gameObject)
    {
        _activeEnemyList.Add(gameObject);
    }

    public void RemoveFromActiveEnemysList(GameObject gameObject)
    {
        _activeEnemyList.Remove(gameObject);
    }

}
