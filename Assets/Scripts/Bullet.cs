using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour, IDamageable
{
    private Cannon _cannon;
    private List<GameObject> _multipliedByList;

    void Awake()
    {
        _cannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<Cannon>();
        _multipliedByList = new List<GameObject>();
    }
    public void Damage()
    {
        GameObject particle = ParticlesPoolObject.SharedInstance.GetPooledObject();
        particle.transform.position = transform.position;
        particle.SetActive(true);

        _cannon.RemoveFromActiveBulletsList(gameObject);
        gameObject.SetActive(false);
        _multipliedByList.Clear();
        UnitCounter.Instance._unitCounter--;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<DestroyableObject>() != null || collision.gameObject.GetComponent<Enemy>() != null || collision.gameObject.GetComponent<DestroyableHouse>())
        {
            Damage();
        }
    }

    public List<GameObject> GetMultipliedByList()
    {
        return _multipliedByList;
    }

    public void SetMultiplierToList(GameObject gameObject)
    {
        _multipliedByList.Add(gameObject);
    }



}
