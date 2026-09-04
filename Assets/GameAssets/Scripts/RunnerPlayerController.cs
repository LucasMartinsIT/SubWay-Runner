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

}
