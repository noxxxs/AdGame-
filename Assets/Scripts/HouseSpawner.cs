using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _spawnEnemyEffect;

    public Transform EnemySpawnPosition;
    public Vector3 RandomSpawnOffset;
    public float SpawnRatio;
    public bool ShouldSpawnEnemy;


    private float _timeLeft;

    private void FixedUpdate()
    {
        _timeLeft += Time.deltaTime;
        if ( _timeLeft > 1 / SpawnRatio && ShouldSpawnEnemy)
        {
            SpawnEnemy();
            _timeLeft = 0;
        }
        
    }

    private void SpawnEnemy()
    {
        if (!_spawnEnemyEffect.isPlaying)
        {
            _spawnEnemyEffect.Play();
        }
        GameObject enemy = EnemyPoolObject.SharedInstance.GetPooledObject();
        if (enemy != null)
        {
            enemy.transform.position = EnemySpawnPosition.position + RandomSpawnOffset * UnityEngine.Random.Range(-0.1f, 0.1f);
            enemy.transform.rotation = enemy.transform.rotation;
            enemy.SetActive(true);

            MoveEnemyManager.MoveEnemyInstance.AddToActiveEnemyList(enemy);
            UnitCounter.Instance._unitCounter++;

        }
    }

}
