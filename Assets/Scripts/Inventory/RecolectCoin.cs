using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecolectCoin : MonoBehaviour
{
    public Coin coin;
    public PlayerData playerData;
    SceneController transition;
    Inventory inventory;
    GameObject contText;

    private void Awake()
    {
        transition = GameObject.Find("Transition").GetComponent<SceneController>();
        inventory = GameObject.Find("Scripts").GetComponent(typeof(Inventory)) as Inventory;
        contText = GameObject.Find("CountPlayer");
    }

    private void Start()
    {
        contText.GetComponent<TextMeshProUGUI>().text = " x " + playerData.cont;
    }
    private void OnMouseDown()
    {
        if (!inventory.GetComponent<Inventory>().inventoryEnabled && !transition.GetComponent<SceneController>().itsChanging && !DialogueManager.instance.isShowed)
        {
            if (!coin.isRecolected)
            {
                coin.isRecolected = true;
                gameObject.GetComponent<Animator>().SetTrigger("Collect");
                playerData.cont++;
                contText.GetComponent<TextMeshProUGUI>().text = " x " + playerData.cont;
            }
        }
       
    }
}
