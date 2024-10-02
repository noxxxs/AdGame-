using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeSpawner : MonoBehaviour
{
    public bool _canSpawn = false;
    public GameObject _prefab;

    private float _timer = 0f;
    public float spawnInterval = 1f;

    [Range(0f,50f)]
    public float _torquePower = 10f;

    [Range(0f, 50f)]
    public float _upForcePower = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_canSpawn)
            {
                _canSpawn = false;
            } else
            {
                _canSpawn = true;
            }
        }

        _timer += Time.deltaTime;

        if (_canSpawn && _timer >= spawnInterval)
        {
            SpawnSlime();
            _timer = 0f; 
        }
        
    }

    void SpawnSlime()
    {
        // Спавн об'єкта
        GameObject obj = Instantiate(_prefab, transform.position, transform.rotation);
        obj.GetComponent<NavMeshAgent>().enabled = false;
        obj.GetComponent<EnemyBrain_Stupid>().enabled = false;

        // Генерація рандомного вектора для обертання
        Vector3 randomTorque = Random.onUnitSphere * _torquePower;

        obj.GetComponent<Rigidbody>().freezeRotation = false;
        // Застосування обертання
        obj.GetComponent<Rigidbody>().AddTorque(randomTorque, ForceMode.Impulse);
        obj.GetComponent<Rigidbody>().AddForce(Vector3.up* _upForcePower *10, ForceMode.Impulse);
    }
}
