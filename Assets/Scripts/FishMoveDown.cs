using UnityEngine;

public class FishMoveDown : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the object moves downwards

    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); // Move down
    
    }
}
