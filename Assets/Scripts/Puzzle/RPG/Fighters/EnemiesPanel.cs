using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemiesPanel : MonoBehaviour
{
    public GameObject sampleButton;

    private PlayerFighter targetFighter;
    public List<Fighter> targets;

    private List<EnemyButtonUi> buttons;

    private float baseHeight;
    private RectTransform rectTransform;

    void Awake()
    {
        this.targets = new List<Fighter>();
        this.buttons = new List<EnemyButtonUi>();

        this.rectTransform = this.GetComponent<RectTransform>();
        this.baseHeight = this.rectTransform.rect.height;

        // Añadimos el botón de ejemplo como el primer botón disponible
        EnemyButtonUi btn = this.InsertNewButton(this.sampleButton, 0);
        btn.Hide();

        this.Hide();
    }

    public void OnTargetButtonClick(int index)
    {
        Fighter target = this.targets[index];
        this.targetFighter.SetTargetAndAttack(target);
    }

    public void ResetSize()
    {
        this.rectTransform.sizeDelta = new Vector2(
        this.rectTransform.rect.width,
        this.baseHeight
        );
    }
    public void Show(PlayerFighter playerFighter, Fighter[] targets)
    {
        this.targets.Clear();

        this.gameObject.SetActive(true);

        this.targetFighter = playerFighter;

        int btnIndex = 0;

        foreach (var target in targets)
        {
            this.targets.Add(target);
            EnemyButtonUi btn = this.ActivateNextButton(btnIndex);
            btn.SetText(target.idName);
            btnIndex++;
        }

        this.rectTransform.sizeDelta = new Vector2(
            this.rectTransform.rect.width,
            this.baseHeight * targets.Length
        );
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);

        foreach (var btn in this.buttons)
        {
            btn.Hide();
        }

        this.targets.Clear();
    }

    private EnemyButtonUi ActivateNextButton(int index)
    {
        foreach (var btn in this.buttons)
        {
            if (btn.index == index)
            {
                btn.Show();
                return btn;
            }
        }

        // Clonamos el botón de ejemplo
        GameObject btnGO = Instantiate(this.sampleButton);
        btnGO.transform.SetParent(this.transform);
        btnGO.transform.localScale = Vector3.one;

        // Lo añadimos como nuevo botón disponible
        EnemyButtonUi but = this.InsertNewButton(btnGO, index);
        but.Show();

        return but;
    }

    private EnemyButtonUi InsertNewButton(GameObject btnGO, int index)
    {
        EnemyButtonUi btn = new EnemyButtonUi(btnGO, index);
        btn.button.onClick.AddListener(() => { this.OnTargetButtonClick(btn.index); });

        this.buttons.Add(btn);

        return btn;
    }
}