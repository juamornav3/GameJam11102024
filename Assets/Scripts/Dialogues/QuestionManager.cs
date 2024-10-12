using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPref;

    [SerializeField]
    private TextMeshProUGUI questText;
    [SerializeField]
    private Transform choicesContainer;
    private List<Button> poolButtons = new List<Button>();

    public void ActiveButtons (int quantity, string title, Choices[] choices)
    {
        questText.text = title;
        if(poolButtons.Count>= quantity)
        {
            for (int i=0; i < poolButtons.Count; i++)
            {
                if (i < quantity)
                {
                    poolButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].choice;
                    poolButtons[i].onClick.RemoveAllListeners();
                    Conversation co = choices[i].result;
                    poolButtons[i].onClick.AddListener(() => GuiveFunctionButtons(co));
                    poolButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    poolButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            int remaining=(quantity-poolButtons.Count);
            for(int i=0; i < remaining; i++)
            {
                var newButton=Instantiate(buttonPref,choicesContainer).GetComponent<Button>();
                newButton.gameObject.SetActive(true);
                poolButtons.Add(newButton);
            }
            ActiveButtons(quantity, title, choices);
        }
    }

    public void GuiveFunctionButtons(Conversation conv)
    {
        DialogueManager.instance.SetConversation(conv, null);
    }
    
}
