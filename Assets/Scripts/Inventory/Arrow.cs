using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    SceneController transition;
    public AudioClip sound;
    public bool isFree = true;
    public int id;
    private void Awake()
    {
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
    }

    void Start()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnMouseDown()
    {
            transition.ChangeSceneSound(sound, id);
            
    }
}
