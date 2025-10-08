using PrimeTween;
using TMPro;
using UnityEngine;

public class DestroyableHouse : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private GameObject _onDieHouseEffect;
    [SerializeField] private GameObject _onDieMainHouseEffect;


    // DeactiveWhenDie
    [SerializeField] BoxCollider _collider;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] HouseSpawner _houseSpawnerScript;
    [SerializeField] ParticleSystem _spawnEffect;
    [SerializeField] TextMeshProUGUI _hpText;

    public float ScaleAnimationStrength;
    public bool _isMainHouse;

    private void Update()
    {
        _healthText.text = "" + _health;
    }
    public void Damage()
    {
        _health -= 1;

        if (_health <= 0)
        {
            DieHouse(_isMainHouse);
            Destroy(gameObject, _onDieHouseEffect.GetComponent<ParticleSystem>().main.duration);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Tween.PunchScale(transform, Vector3.one * ScaleAnimationStrength, 0.2f);
            Damage();
        }
    }

    private void DieHouse(bool isMainHouse)
    {
        if (isMainHouse) 
        {
            _onDieHouseEffect.SetActive(true);
            _onDieMainHouseEffect.SetActive(true);
            _collider.enabled = false;
            _meshRenderer.enabled = false;
            _houseSpawnerScript.enabled = false;
            _hpText.enabled = false;
            _spawnEffect.Stop();
            Invoke("RestartCurrentLevel", 1.5f);
        }
        else
        {
            _onDieHouseEffect.SetActive(true);
            _collider.enabled = false;
            _meshRenderer.enabled = false;
            _houseSpawnerScript.enabled = false;
            _hpText.enabled = false;
            _spawnEffect.Stop();
        }
        
    }

    void RestartCurrentLevel()
    {
        GameManager.Instance.RestartLevel();
    }
}
