using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|Sets a variable (lowerBound) which defines how far an item can move down the y axis before|
    //¬|it needs to be destroyed.                                                                 |
    //¬|On each frame, the y position is checked to see if it dips below the bound.               |
    //¬|This is attached to every game object with any movedown script on, bar the background.    |
    //¬|This keeps the game running efficiently.                                                  |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|

    private float lowerBound = -10;
    void Update()
    {
        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
    }
}
