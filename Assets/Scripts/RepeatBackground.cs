using UnityEngine;
//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
//¬|The ROV only moves <--left to right--> - the background scrolls down to give the illusion |
//¬|of movement.                                                                              |
//¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   
public class RepeatBackground : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|Get the starting position of the background and get its height / 2                        |
    //¬|When the background gets to halfway through its scroll height, jump it back to the        |
    //¬|original. This currently isn't working - but I think that's more that I've not made the   |
    //¬|image properly seamless. The code USED to work on the old background. 
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   
    private Vector3 startPosition;
    private float repeatHeight;

    void Start()
    {
        startPosition = transform.position;
        repeatHeight = GetComponent<SpriteRenderer>().size.y / 2;
    }

    void Update()
    {
        if (transform.position.y < startPosition.y - repeatHeight)
        {
            transform.position = startPosition;
        }
    }
}
