using UnityEngine;
using System.Collections;

public class RunnerPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 8f;
    public float laneDistance = 2f;
    public float horizontalSmooth = 10f;

    public float jumpForce = 6f;
    public float gravity = -22f;
    public float slideDuration = 0.68f;

    [Header("Reference")]

    public Animator animator;
    public CharacterController controller;

    private int desiredLane = 1;
    private float verticalVelocity = 0f;
    private bool isSliding = false;

    private void Update()
    {

        //Player Movement
        PlayerMove();
        PcControls();
    }

    public void PlayerMove()

    {
        //HorizontalMovement
        Vector3 move = Vector3.forward * forwardSpeed;
        float targetX = (desiredLane - 1) * laneDistance;
        float newX = Mathf.Lerp(transform.position.x, targetX, horizontalSmooth * Time.deltaTime);
        move.x = (newX - transform.position.x) / Time.deltaTime;
        //VerticalMovement
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -1f;
            animator.SetBool("IsJumping", false);

        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    public void PcControls()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            desiredLane = Mathf.Min(desiredLane + 1, 2);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            desiredLane = Mathf.Max(desiredLane -1, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Slide();
        }

#endif


    }

    public void Jump()
    {
        if (!controller.isGrounded) return;

        verticalVelocity = jumpForce;

        animator.SetBool("IsJumping", true);
    }

    public void Slide()
    {
        if(!isSliding && controller.isGrounded)
        {
            StartCoroutine(DoSlide());
        }
    }
    private IEnumerator DoSlide()
    {
        isSliding = true;
        animator.SetBool("IsSliding", true);

        float origH = controller.height;
        Vector3 origC = controller.center;

        controller.height = origH / 2f;
        controller.center = new Vector3(origC.x, origC.y / 2f, origC.z);

        yield return new WaitForSeconds(slideDuration);

        controller.height = origH;
        controller.center = origC;

        animator.SetBool("IsSliding", false);
        isSliding = false;
    }
}
