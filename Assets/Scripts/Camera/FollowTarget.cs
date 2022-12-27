using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        // Calculate target position
        Vector3 targetPosition = target.position + offset;

        // Calculate interpolation factor
        float interpolationFactor = Time.deltaTime / (Time.deltaTime + smoothTime);

        // Interpolate between current position and target position

        transform.position = Vector3.Lerp(transform.position, targetPosition, interpolationFactor);
    }
}
