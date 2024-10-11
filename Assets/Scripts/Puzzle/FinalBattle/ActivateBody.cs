using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBody : MonoBehaviour
{
    GameObject body;
    GameObject leftArm;
    GameObject rightArm;
    bool activate = true;
    void Start()
    {
        body = gameObject.transform.GetChild(0).gameObject;
        leftArm = gameObject.transform.GetChild(1).GetChild(0).gameObject;
        rightArm = gameObject.transform.GetChild(2).GetChild(0).gameObject;
    }

    void Update()
    {
        if (leftArm.GetComponent<HighLight>().finish && rightArm.GetComponent<HighLight>().finish && activate)
        {
            activate = false;
            body.GetComponent<HighLight>().finish = false;
        }
    }
}
