using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|This script manages the picking up of samples, and the triggering of the SampleSlides.    |
    //¬|It is attached to the ROV gameobject.                                                     |
    //!|This script needs work - sometimes collisions just don't trigger!                         |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    private GameObject currentCollectable;

    void Update()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|At every frame, check three things:                                                       |
        //¬|If the rov is not moving + if we've collided with something + if we've pressed x          |
        //¬|NB: currentCollectable gets given a value OnTriggerEnter!!!!                              |
        //¬|If those three conditions are met:                                                        |
        //¬|Get the CollectableItem component from the object we've just bumped into, and store it in | 
        //¬|a variable called item. The CheckCollected() function is called, passing over the info    |
        //¬|from the CollectableItem (which is a container for the Scriptable Object "Sample Data")   |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        if (Input.GetKeyDown(KeyCode.X) && currentCollectable != null && !GameManager.instance.rovIsMoving)
        {
            CollectableItem item = currentCollectable.GetComponent<CollectableItem>();
            if (item != null)
            {
                //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                //¬|CheckCollected() checks if the item we picked up was one of our targets.                  |
                //¬|FFOBT<SampleSlideUP> looks in the scene to find the first object with a SampleSlideUI     |
                //¬|component attached (which SHOULD only be the SampleUIManager) and call the ShowSlide      |
                //¬|method. The data shown comes from the item.data.                                          |
                //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
                GameManager.instance.CheckCollected(item.data.sampleName);
                FindFirstObjectByType<SampleSlideUI>().ShowSlide(item.data);
            }
            //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
            //¬|Once that is done, we destroy that sample, and then set the variable to null so it's      |
            //¬|clean for the next collision.                                                             |
            //!|THIS NEEDS CHANGING! Items which are "put back" should not be destroyed. Also, if you     |
            //!|collide with two items, it doesn't always reset properly - fix this!                      |
            //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
            Destroy(currentCollectable);
            currentCollectable = null;
        }
    }

    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|OnTriggerEnter is called when something else with a collider (like a sample) comes into   |
    //¬|contact with this (the ROV) objects collider.                                             |
    //¬|"other" references the "other" collider belonging to the sample, and has properties like  |
    //¬|other.name.                                                                               |
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    void OnTriggerEnter2D(Collider2D other)
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|If what we collided with has the tag of "collectable"                                     |
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        if (other.CompareTag("Collectable"))
        {
            //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
            //¬|Make it the currentCollectable                                                            |
            //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
            currentCollectable = other.gameObject;
        }
    }

    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|When we leave the trigger zone, if what we leave had a "Collectable" tag and was the      |
    //¬|object we were picking up, set currentCollectable to nothing.
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Collectable") && other.gameObject == currentCollectable)
        {
            currentCollectable = null;
        }
    }
}
