using System.Collections.Generic;
using UnityEngine;


public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _leftPoint;
    [SerializeField] private Transform _rightPoint;
    private List<GameObject> _activeBulletsList;

    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _shootParticles;

    private float _timeLeft;

    public float CannonSpeed;
    public float ShootForce;
    public float ShootRatio;


    public Transform BulletSpawnPosition;
    [Tooltip("should be > (1,1,1)")]
    public Vector3 RandomSpawnOffset;


    private void Start()
    {
        _activeBulletsList = new List<GameObject>();
    }

    void FixedUpdate()
    {
        MoveBullets();
        _timeLeft += Time.deltaTime;

        if (_timeLeft > 1 / ShootRatio)
        {
            if (InputManager.Instance.GetAction("Fire").ReadValue<float>() > 0)
            {
                Shoot();
                if (!_shootParticles.isPlaying)
                _shootParticles.Play();
                _animator.SetBool("isShooting", true);
                _timeLeft = 0;
            } else
            {
                _shootParticles.Stop();
                _animator.SetBool("isShooting", false);

            }
           
        }

       


       if (InputManager.Instance.GetAction("Move").ReadValue<Vector2>().x == -1 &&
            transform.position.x >= _leftPoint.position.x)
        {
            MoveCanon(Vector3.left);
            _animator.SetBool("isMovingLeft", true);
        }
        else if (InputManager.Instance.GetAction("Move").ReadValue<Vector2>().x == 1 &&
            transform.position.x <= _rightPoint.position.x)
        {
            MoveCanon(Vector3.right);
            _animator.SetBool("isMovingRight", true);
        } else if ((Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(_rightPoint.position.x, 0, 0)) < .5f
            || Vector3.Distance(new Vector3(transform.position.x, 0, 0), new Vector3(_leftPoint.position.x, 0, 0)) < .5f) 
            || InputManager.Instance.GetAction("Move").ReadValue<Vector2>().x == 0)
        {
            _animator.SetBool("isMovingLeft", false);
            _animator.SetBool("isMovingRight", false);
        }

        
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPoolBullet.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = BulletSpawnPosition.position + RandomSpawnOffset * UnityEngine.Random.Range(-0.1f, 0.1f);
            bullet.transform.rotation = bullet.transform.rotation;
            bullet.SetActive(true);

            _activeBulletsList.Add(bullet);
            UnitCounter.Instance._unitCounter++;
        }
    }

    private void MoveBullets()
    {
        foreach (var bullet in _activeBulletsList)
        {
            bullet.GetComponent<Rigidbody>().linearVelocity = transform.forward * ShootForce * 5;
        }  
    }
 
    private void MoveCanon(Vector3 direction)
    {
        transform.Translate(direction * CannonSpeed / 100);
    }

    public List<GameObject> GetActiveBulletsList()
    {
        return _activeBulletsList;
    }

    public void RemoveFromActiveBulletsList(GameObject gameObject)
    {
        _activeBulletsList.Remove(gameObject);
    }

    public void AddToActiveBulletsList(GameObject gameObject)
    {
        _activeBulletsList.Add(gameObject);
    }
}
