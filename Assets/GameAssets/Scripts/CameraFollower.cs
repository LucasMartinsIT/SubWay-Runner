using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [Header("Player Characters")]
    public Transform[] players;

    [Header("Offset Settings")]
    public Vector3 offset = new Vector3(0, 2.6f, -5f);

    [Header("Follow Settings")]
    public float followSmoothness = 0.1f;
    public float rotationSmoothness = 5f;
    private Transform currentTarget;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        UpdateTarget();

        if(currentTarget == null) return;

        Vector3 desiredPosition = currentTarget.position + offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            followSmoothness
            );

        Quaternion desiredRotation = Quaternion.LookRotation(currentTarget.forward);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            desiredRotation,
            rotationSmoothness * Time.deltaTime
            );
    }

    void UpdateTarget()
    {
        foreach(Transform player in players)
        {
            if(player != null && player.gameObject.activeInHierarchy)
            {
                currentTarget = player;
                return;
            }
        }
        currentTarget = null;
    }
}
