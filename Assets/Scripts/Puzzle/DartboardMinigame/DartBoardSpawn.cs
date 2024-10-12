using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;



public class DartBoardSpawn : MonoBehaviour
{
    private float  maxX, maxY;

    [SerializeField] private Transform[] points;

    [SerializeField] private GameObject[] dartboards;

    [SerializeField] private float dartboardTime;

    public Transform dartboardsParent;
    private float nextDartboardTime;
    private float nextVelocity;

    private void Start()
    {
        maxX = points.Max(point => point.position.x);
        maxY = points.Max(point => point.position.y);
    }

    private void Update()
    {
        nextDartboardTime += Time.deltaTime;
        nextVelocity += Time.deltaTime;

        if (nextDartboardTime >= dartboardTime)
        {
            nextDartboardTime = 0;
            CreateDartboard();

        }
        if (nextVelocity >= 30)
        {
            nextVelocity = 0;
            dartboardTime = Math.Max(5,dartboardTime - 3);
        }
    }

    private void CreateDartboard()
    {
        int dartboardNum = 0;
        Vector2 randomPosition = new Vector2(maxX, maxY);
        GameObject dartboard = Instantiate(dartboards[dartboardNum], randomPosition, Quaternion.identity);
        dartboard.transform.SetParent(dartboardsParent);
    }

}
