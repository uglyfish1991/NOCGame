using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|This script controls the <--left to right--> movement of the ROV, and the restriction     |
    //¬|of movement to screen size.                                                               |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float screenPadding = 0.5f;

    private float minX;
    private float maxX;
    private float currentVelocity = 0f;

    void Start()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Set up screen boundaries so the ROV doesn’t drift off the edge of the screen.             |
        //¬|ViewportToWorldPoint converts screen edges (0 = left, 1 = right) into world coordinates.  |
        //¬|We then apply a bit of padding so it doesn’t touch the exact edges. This could be bigger. |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        Camera cam = Camera.main;
        Vector3 screenLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 screenRight = cam.ViewportToWorldPoint(new Vector3(1, 0, cam.nearClipPlane));

        minX = screenLeft.x + screenPadding;
        maxX = screenRight.x - screenPadding;
    }

    void Update()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Get player input (A/D or left/right arrow keys) as a raw float (-1, 0, 1).                |
        //¬|Multiply this by moveSpeed to get our direction and speed (- is left, + is right)         |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        float input = Input.GetAxisRaw("Horizontal");
        float targetVelocity = input * moveSpeed;

        if (input != 0)
        {
            //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
            //¬|If we’re getting input, accelerate towards the target velocity.                           |
            //¬|If no input, decelerate smoothly down to 0.                                               |
            //¬|Mathf.MoveTowards changes a value smoothly by a fixed rate per frame.                     |
            //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
            currentVelocity = Mathf.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0, deceleration * Time.deltaTime);
        }

        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Apply the horizontal movement to the ROV using Translate.                                 |
        //¬|Velocity is multiplied by Time.deltaTime to keep movement framerate-independent.          |
        //¬|Clamp the ROV’s position so it never goes past the screen edge.                           |
        //¬|Only x-axis is affected — the ROV can't travel on the y axis                              |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|

        transform.Translate(Vector2.right * currentVelocity * Time.deltaTime);
        Vector3 clamped = transform.position;
        clamped.x = Mathf.Clamp(clamped.x, minX, maxX);
        transform.position = clamped;
    }
}
