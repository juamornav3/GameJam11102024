using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    public TextMeshProUGUI nameLabel;
    public TextMeshProUGUI levelLabel;

    public Slider healthSlider;
    public Image healthSliderBar;
    public TextMeshProUGUI healthLabel;

    public void SetStats(string name, Stats stats)
    {
        nameLabel.text = name;
        levelLabel.text = $"N. {stats.level}";
        this.SetHealth(stats.health, stats.maxHealth);
    }

    public void SetHealth(float health, float maxHealth)
    {
        healthLabel.text = $"{Mathf.RoundToInt(health)}/{Mathf.RoundToInt(maxHealth)}";
        float percentage = health / maxHealth;
        healthSlider.value = percentage;

        if(percentage <= 0.5f && percentage >= 0.33f)
        {
            healthSliderBar.color = Color.yellow;
        } else if (percentage < 0.33f)
        {
            healthSliderBar.color = Color.red;
        }
        else
        {
            healthSliderBar.color = Color.green;
        }

    }
}
