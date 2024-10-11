using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatusPanel : StatusPanel
{
    public List<Fighter> bossParts;
    public string bossName;
    public string level;
    public float maxHealth;
    public float currentHealth;

    public void Start()
    {
        nameLabel.text = bossName;
        levelLabel.text = level;
        maxHealth = 0;
        foreach (var bossPart in bossParts)
        {
            maxHealth += bossPart.GetStats().maxHealth;
        }

        this.SetHealth(maxHealth,maxHealth);
    }
    void Update()
    {
        currentHealth = 0;
        foreach (var bossPart in bossParts)
        {
            currentHealth += bossPart.GetStats().health;
        }
        this.SetHealth(currentHealth, maxHealth);
    }

}
