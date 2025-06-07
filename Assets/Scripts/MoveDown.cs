using UnityEngine;

public class MoveDown : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the object moves downwards

    void Update()
{
    float effectiveSpeed = moveSpeed * GameManager.instance.movementMultiplier;
    transform.Translate(Vector3.down * effectiveSpeed * Time.deltaTime, Space.World);
}

}
