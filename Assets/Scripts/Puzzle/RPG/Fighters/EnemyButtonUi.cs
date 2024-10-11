using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyButtonUi
{
    public int index { protected set; get; }
    public Button button { protected set; get; }

    private TextMeshProUGUI label;
    private GameObject btn;

    public EnemyButtonUi(GameObject btn, int index)
    {
        this.index = index;
        this.btn = btn;

        this.label = btn.GetComponentInChildren<TextMeshProUGUI>();
        this.button = btn.GetComponent<Button>();
    }

    public void Show()
    {
        btn.SetActive(true);
    }

    public void Hide()
    {
        btn.SetActive(false);
    }

    public void SetText(string text)
    {
        label.text = text;
    }

}
