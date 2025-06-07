using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 10f; // How quickly to accelerate
    public float deceleration = 10f; // How quickly to decelerate
    public float screenPadding = 0.5f;

    private float minX;
    private float maxX;
    private float currentVelocity = 0f;

    void Start()
    {
        Camera cam = Camera.main;
        Vector3 screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));

        minX = screenLeft.x + screenPadding;
        maxX = screenRight.x - screenPadding;
    }

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        // Determine target velocity
        float targetVelocity = input * moveSpeed;

        // Apply smoothing based on acceleration/deceleration
        if (input != 0)
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0, deceleration * Time.deltaTime);
        }

        // Apply movement
        transform.Translate(Vector2.right * currentVelocity * Time.deltaTime);

        // Clamp position
        Vector3 clamped = transform.position;
        clamped.x = Mathf.Clamp(clamped.x, minX, maxX);
        transform.position = clamped;
    }
}
