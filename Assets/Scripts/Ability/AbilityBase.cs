using System.Collections;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{

    [Header("Ability Info")]
    public string _title;
    public Sprite _icon;
    public float _cooldownTime = 1;
    private bool _canUse = true;


    public void TriggerAbility()
    {
        if (_canUse)
        {
            Ability();
            StartCooldown();
        }

    }
    public abstract void Ability();
    void StartCooldown()
    {
        StartCoroutine(Cooldown());
        IEnumerator Cooldown()
        {
            _canUse = false;
            yield return new WaitForSeconds(_cooldownTime);
            _canUse = true;
        }
    }
}
