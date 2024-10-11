using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Moving : MonoBehaviour
{
    public float velocity;
    public List<string> pointsS;
    public bool flip;
    List<Transform> points = new List<Transform>();
    public int step;
    public float minDistance;

    private void Awake()
    {
        for (int i = 0; i < pointsS.Count; i++)
        {
            points.Add(GameObject.Find(pointsS[i]).GetComponent<Transform>());
        }
    }
    private void Start()
    {
        if (flip)
        {
            gameObject.transform.Rotate(0, 180, 0);
        }
    }

    private void Update()
    {
        if (step<points.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, points[step].position, velocity * Time.deltaTime);
            if (Vector2.Distance(transform.position, points[step].position) < minDistance)
            {
                step += 1;
                if (step >= points.Count)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Destroy");
                    Destroy(gameObject, 0.5f);
                }

            }
        }
       
    }
}
