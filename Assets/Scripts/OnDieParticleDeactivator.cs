using UnityEngine;

public class OnDieParticleDeactivator : MonoBehaviour
{
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        gameObject.SetActive(false);
    }
}
