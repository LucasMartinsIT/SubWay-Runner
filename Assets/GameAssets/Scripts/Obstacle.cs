using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            Animator animator = col.GetComponent<Animator>();
            RunnerPlayerController controller = col.GetComponent<RunnerPlayerController>();

            if(animator != null)
            {
                animator.SetTrigger("Die");
            }

            if(controller != null)
            {
                controller.enabled = false;
            }

        }
    }
}
