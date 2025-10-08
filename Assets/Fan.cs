using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    public float FanForce;

    public void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            collider.transform.Translate(transform.up * -1 / 10);
            _cannon.RemoveFromActiveBulletsList(collider.gameObject);
        } 
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            _cannon.AddToActiveBulletsList(collider.gameObject);
        }
    }

}
