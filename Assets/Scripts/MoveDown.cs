using UnityEngine;

public class MoveDown : MonoBehaviour
{
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    //¬|This script moves objects down which move when the ROV moves, i.e. the background and     |
    //¬|samples. They have a movespeed of two, but are eased in/out based on the                  |
    //¬|MovementMultiplier set in the GameManager.                                                |         
    //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
    public float moveSpeed = 2f; // Speed at which the object moves downwards

    void Update()
    {
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        //¬|Samples have a random rotation applied at instansiation - space.World ensures they are    |
        //¬|always moving into the world's down, not the down relative to them.                       |      
        //¬|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|
        float effectiveSpeed = moveSpeed * GameManager.instance.movementMultiplier;
        transform.Translate(Vector3.down * effectiveSpeed * Time.deltaTime, Space.World);
    }

}
