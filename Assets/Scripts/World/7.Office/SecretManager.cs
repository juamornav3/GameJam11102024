
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SecretManager : MonoBehaviour
{
    public PuzzleData paintInFloor;
    public PuzzleData openBox;
    public GameObject comentary;
    public Coin coin;
    public AudioClip audioCoin;
    public AudioClip paintSound;
    public AudioClip openBoxSound;
    public PlayerData player;
    SceneController transition;
    Inventory inventory;
    GameObject contText;
    public List<GameObject> coins;

    private void Awake()
    {
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        contText = GameObject.Find("CountPlayer");
    }
    void Start()
    {
        gameObject.GetComponent<DialogueSpeaker>().Conver();
        gameObject.GetComponent<BoxCollider2D>().enabled = !coin.isRecolected;
        contText.GetComponent<TextMeshProUGUI>().text = " x " + player.cont;
        if (paintInFloor.finished)
        {
            gameObject.GetComponent<InteractionZone>().currentInformativeText = "Abrir caja fuerte";
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            comentary.GetComponent<BoxCollider2D>().enabled=true;
            if (openBox.finished)
            {
                gameObject.transform.GetChild(2).gameObject.SetActive(true);
                gameObject.GetComponent<InteractionZone>().currentInformativeText = "Conseguir recompensa";
            }
        }
    }

    void Update()
    {
        if (!coin.isRecolected)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = !DialogueManager.instance.isShowed;
        }
        
    }



    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (!paintInFloor.finished)
            {
                paintInFloor.finished = true;
                transition.ChangeSceneSound(paintSound, 14);
            }
            else if (!openBox.finished)
            {
                transition.ChangeSceneSound(openBoxSound, 15);

            }
            else if (!coin.isRecolected)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                coin.isRecolected = true;
                player.cont += 5;
                contText.GetComponent<TextMeshProUGUI>().text = " x " + player.cont;
                StartCoroutine(RecolectCoins());

            }
            
        }

    }

    IEnumerator RecolectCoins()
    {
        foreach (GameObject coin in coins)
        {
            coin.gameObject.GetComponent<Animator>().SetTrigger("Collect");
            yield return new WaitForSeconds(0.2f);
        }
        
    }

}
