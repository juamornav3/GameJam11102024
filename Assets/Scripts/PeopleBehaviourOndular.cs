using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleBehaviourOndular : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 startPosition;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public bool mirrored = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Set Rigidbody2D to Kinematic
        startPosition = rb.position;
    }

    void Update()
    {
        float newX;
        float newY;
        if (!mirrored)
        {
            newX = startPosition.x + Mathf.Cos(Time.time * frequency) * amplitude;
            newY = startPosition.y + Mathf.Cos(Time.time * frequency) * amplitude;
        }
        else
        {
            newX = startPosition.x + Mathf.Sin(Time.time * frequency) * amplitude;
            newY = startPosition.y - Mathf.Sin(Time.time * frequency) * amplitude;
        }
        
        rb.MovePosition(new Vector2(newX, newY));
    }
    
}
