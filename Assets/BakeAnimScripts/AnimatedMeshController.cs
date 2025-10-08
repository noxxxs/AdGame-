using UnityEngine;

public class AnimatedMeshController : MonoBehaviour
{
    [SerializeField]
    private AnimatedMesh Animator;

    private void Start()
    {
        ActivateAll();
    }

    public void DeactivateAll()
    {
         Animator.enabled = false;
         Animator.GetComponentInChildren<MeshRenderer>().enabled = false;

    }

    public void ActivateAll()
    {

        Animator.enabled = true;
        Animator.GetComponentInChildren<MeshRenderer>().enabled = true;
        Animator.Play("RunBulletMan");
    
    }
}
