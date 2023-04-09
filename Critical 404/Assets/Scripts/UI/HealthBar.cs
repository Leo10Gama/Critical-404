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

    public Toggle r1Toggle;
    public Toggle r2Toggle;

    private int maxHealth;

    void Start()
    {
    }


    void Update()
    {
    }

    public void UpdateRoundsWon(int roundNumber)
    {
        if (roundNumber >= 2)
        {
            r1Toggle.isOn = true;
            r2Toggle.isOn = true;
        }
        else if (roundNumber >= 1)
        {
            r1Toggle.isOn = true;
            r2Toggle.isOn = false;
        }
        else
        {
            r1Toggle.isOn = false;
            r2Toggle.isOn = false;
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
