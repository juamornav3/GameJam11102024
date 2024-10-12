using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShowHint : MonoBehaviour
{
    public void OnMouseDown()
    {
        Debug.Log("DOU");
        GetComponent<SortingGroup>().sortingOrder = 1;
    }
}
