using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoorManager : MonoBehaviour
{
    SceneController transition;
    GameObject background;
    Inventory inventory;
    public Sprite open;
    public PuzzleData puzzle;
    public AudioClip papersound;
    public GameObject arrow;

    private void Awake()
    {
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
        background = GameObject.Find("Background");
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
    }
    void Start()
    {
        if (puzzle.finished)
        {
            background.GetComponent<SpriteRenderer>().sprite = open;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<InteractionZone>().enabled = false;
            arrow.GetComponent<Arrow>().isFree = true;
        }

    }

    private void OnMouseDown()
    {
        if(!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            puzzle.finished = true;
            transition.ChangeSceneSound(papersound, 8);
        }

    }



}
