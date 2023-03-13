using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public PlayerMovement player;

    private int maxHealth;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }


    public void UpdateHealth()
    {
        healthBar.fillAmount = Math.Max(player.hp, 0) / (float)maxHealth;
    }

}
