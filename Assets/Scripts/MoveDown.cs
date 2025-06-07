using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the object moves downwards

    void Update()
    {
        if (GameManager.instance.rovIsMoving){
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World); // move down in world space otherwise they go off on whatever their rotation is
        }
    }
}
