using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPosition;
    private float repeatHeight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        repeatHeight = GetComponent<SpriteRenderer>().size.y/2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < startPosition.y - repeatHeight)
        {
            transform.position = startPosition;
        }
    }
}
