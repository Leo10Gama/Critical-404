using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public PlayerMovement player;

    public Toggle toggle1;
    public Toggle toggle2;

    private int maxHealth;

    void Start()
    {
    }

    void Update()
    {

        if (healthBar.fillAmount <= 0)
        {
            // activates the toggle component
            toggle1.isOn = true;
        }

        if (healthBar.fillAmount <= 0)
        {
            toggle2.isOn = true;
        }
        
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
