using PrimeTween;
using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Cannon _cannon;
    public Transform PipeOutput;
    public Transform PipeInput;
    public float TimeToMove;


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Tween.PunchScale(PipeInput, Vector3.one * 0.3f, 0.3f);
            GameObject bullet = collision.gameObject;
            bullet.GetComponent<SphereCollider>().enabled = false;
            bullet.GetComponentInChildren<MeshRenderer>().enabled = false;
            bullet.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            _cannon.RemoveFromActiveBulletsList(bullet);
            StartCoroutine(MoveThroughPipe(bullet));
        }
      
    }

    IEnumerator MoveThroughPipe(GameObject gameObject)
    {
        yield return new WaitForSeconds(TimeToMove);
        Tween.PunchScale(PipeOutput, Vector3.one * 0.3f, 0.3f);
        gameObject.transform.position = new Vector3(PipeOutput.transform.position.x, gameObject.transform.position.y, PipeOutput.transform.position.z) + 
            new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), 0, UnityEngine.Random.Range(-0.2f, 0.2f));
        gameObject.GetComponent<SphereCollider>().enabled = true;
        gameObject.GetComponent<Bullet>().enabled = true;
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        _cannon.AddToActiveBulletsList(gameObject);
    }

}
