using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonSuitCase : MonoBehaviour
{
    public GameObject suitcaseIconObject;
    private Image suitcaseIcon;
    private Vector3 originalScale;

    private void Start()
    {
        suitcaseIcon = suitcaseIconObject.GetComponent<Image>();
        originalScale = suitcaseIcon.transform.localScale;
    }

    public void PointerEnter()
    {
        suitcaseIcon.transform.localScale = originalScale * 1.1f; 
    }

    public void PointerExit()
    {
        suitcaseIcon.transform.localScale = originalScale; 
    }
}
