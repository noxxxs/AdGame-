using UnityEngine;
using PrimeTween;
using TMPro;

public class DestroyableObject : MonoBehaviour, IDamageable
{

    //Stats
    [SerializeField] private int _health;
    [SerializeField] private TextMeshProUGUI _healthText;

    public float ScaleAnimationStrength;

    private void Update()
    {
        _healthText.text = "" + _health;
    }
    public void Damage()
    {
        _health -= 1;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null) {
            Tween.PunchScale(transform, Vector3.one * ScaleAnimationStrength, 0.2f);
            Damage();
        }
    }

}
