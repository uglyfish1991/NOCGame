using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private GameObject currentCollectable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && currentCollectable != null && !GameManager.instance.rovIsMoving)
        {
            // Check for sample name
            CollectableItem item = currentCollectable.GetComponent<CollectableItem>();
            if (item != null)
            {
                GameManager.instance.CheckCollected(item.sampleName);
            }

            Destroy(currentCollectable);
            currentCollectable = null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectable"))
        {
            currentCollectable = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Collectable") && other.gameObject == currentCollectable)
        {
            currentCollectable = null;
        }
    }
}
