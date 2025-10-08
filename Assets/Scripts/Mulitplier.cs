using System.Collections.Generic;
using TMPro;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;

public class Mulitplier : MonoBehaviour
{
    [SerializeField] private int _multiplierAmount;
    [SerializeField] private TextMeshProUGUI _multiplierText;
    [SerializeField] private ParticleSystem _multiplyEffect;
    private Cannon _cannon;

    public int RandomizeSpawnPos;

    private void Start()
    {
        _cannon = GameObject.FindWithTag("Cannon").GetComponent<Cannon>();
    }

    private void Update()
    {
        _multiplierText.text = "x" + _multiplierAmount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (!_multiplyEffect.isPlaying)
            {
                _multiplyEffect.Play();
            }
            MultiplyObjects(other.gameObject);
        }
    }

    /*void MultiplyObjects(GameObject bullet)
    {
        List<GameObject> multipliedList = bullet.GetComponent<Bullet>().GetMultipliedByList();

        if (multipliedList.Contains(gameObject))
        {
            return;
        }
        else
        {
            bullet.GetComponent<Bullet>().SetMultiplierToList(gameObject);
            for (int i = 0; i < _multiplierAmount - 1; i++)
            {
                GameObject multipiedBullet = ObjectPoolBullet.SharedInstance.GetPooledObject();
                if (multipiedBullet != null)
                {
                    multipiedBullet.GetComponent<Bullet>().SetMultiplierToList(gameObject);
                    multipiedBullet.transform.position = bullet.transform.position + new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), 0, UnityEngine.Random.Range(-0.1f, 0.1f)) * RandomizeSpawnPos;
                    multipiedBullet.transform.rotation = multipiedBullet.transform.rotation;
                    multipiedBullet.SetActive(true);

                    _cannon.GetActiveBulletsList().Add(multipiedBullet);
                    UnitCounter.Instance._unitCounter++;
                }
            }
        }
    }*/

    private void MultiplyObjects(GameObject bullet)
    {
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        List<GameObject> multipliedList = bulletComponent.GetMultipliedByList();

        if (multipliedList.Contains(gameObject)) return;

        bulletComponent.SetMultiplierToList(gameObject);

        int multiplierCount = _multiplierAmount - 1;
        if (multiplierCount <= 0) return;

        NativeArray<Vector3> spawnPositions = new NativeArray<Vector3>(multiplierCount, Allocator.TempJob);

        uint randomSeed = (uint)UnityEngine.Random.Range(1, 100000);

        BulletSpawnJob spawnJob = new BulletSpawnJob
        {
            originalPosition = bullet.transform.position,
            randomRange = 0.1f * RandomizeSpawnPos,
            spawnPositions = spawnPositions,
            randomSeed = randomSeed
        };

        JobHandle jobHandle = spawnJob.Schedule(multiplierCount, 1);
        jobHandle.Complete();

        for (int i = 0; i < multiplierCount; i++)
        {
            GameObject multipiedBullet = ObjectPoolBullet.SharedInstance.GetPooledObject();
            if (multipiedBullet != null)
            {
                multipiedBullet.GetComponent<Bullet>().SetMultiplierToList(gameObject);
                multipiedBullet.transform.position = spawnPositions[i];
                multipiedBullet.transform.rotation = bullet.transform.rotation;
                multipiedBullet.SetActive(true);

                _cannon.GetActiveBulletsList().Add(multipiedBullet);
                UnitCounter.Instance._unitCounter++;
            }
        }

        spawnPositions.Dispose(); 
    }

    [BurstCompile]
    struct BulletSpawnJob : IJobParallelFor
    {
        public Vector3 originalPosition;
        public float randomRange;
        public NativeArray<Vector3> spawnPositions;
        public uint randomSeed; 

        public void Execute(int index)
        {
            Unity.Mathematics.Random random = new Unity.Mathematics.Random(randomSeed + (uint)index);

            spawnPositions[index] = originalPosition + new Vector3(
                random.NextFloat(-randomRange, randomRange),
                0,
                random.NextFloat(-randomRange, randomRange)
            );
        }
    }

}
