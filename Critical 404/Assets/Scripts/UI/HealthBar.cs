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
    public Toggle round2Toggle;

    public int numRound = 2;

    private int currentRound = 0;

    private int maxHealth;

    void Start()
    {
        StartRound();
    }


    void StartRound()
    {
        currentRound++;

        //SetMaxHealth(currentRound);


        if (healthBar.fillAmount <= 0)
        {
            //activates the toggle component
            r1Toggle.isOn = true;
            round2Toggle.isOn = false;
            OnRoundComplete();

        }

    }

    void OnRoundComplete()
    {
        // If the current round is the final round, end the game
        if (currentRound == numRound)
        {
            EndGame();
        }
        else
        {
            // Otherwise, start a new round
            StartRound();


        }
    }

    void EndGame()
    {

        // Reset the round count and start a new game
        currentRound = 0;
        StartRound();
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
