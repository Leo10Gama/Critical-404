using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoundManager
{
    public static int currentRound = 1;
    public static int p1roundsWon = 0;
    public static int p2roundsWon = 0;
    public static bool gameOver = false;

    public static void UpdateRound(int playerThatWon)
    {
        if (playerThatWon == 1)
        {
            p1roundsWon++;
        }
        else if (playerThatWon == 2)
        {
            p2roundsWon++;
        }
        else
        {
            throw new Exception("Bad player number passed to RoundManager");
        }
        currentRound++;
        gameOver = p1roundsWon == 2 || p2roundsWon == 2;
    }

    public static void ResetRounds()
    {
        currentRound = 1;
        p1roundsWon = 0;
        p2roundsWon = 0;
        gameOver = false;
    }
}
