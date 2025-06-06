using UnityEngine;

public class FishMoveDown : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|Sets a variable (moveSpeed) which defines how fast a fish moves. This is public, as each  |
    //¬|fish will have its own speed really. 2 is the minimum to not look like they're going      |
    //¬|backwards against the background image.                                                   |
    //¬|On each frame, the transform is moved "down". Time.deltaTime evens out framerate issues.  |
    //¬|This is attached to every fish prefab and customised.                                     |
    //¬|This is for environment.                                                                  |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    public float moveSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); 
    }
}
