using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float screenPadding = 0.5f; // Optional: how far from edge player can go

    private float minX;
    private float maxX;

    void Start()
    {
        // Get screen bounds in world space
        Camera cam = Camera.main;
        Vector3 screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));

        minX = screenLeft.x + screenPadding;
        maxX = screenRight.x - screenPadding;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector2 move = new Vector2(horizontalInput * moveSpeed, 0);
        transform.Translate(move * Time.deltaTime);

        // Clamp position so player stays on screen
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = clampedPosition;
    }
}
