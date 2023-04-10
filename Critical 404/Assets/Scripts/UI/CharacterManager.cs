using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManager
{
    public static GameObject p1Character = null;
    public static GameObject p2Character = null;

    public static void SetCharacter(int playerId, GameObject characterPrefab)
    {
        if (playerId == 1)
        {
            p1Character = characterPrefab;
        }
        else if (playerId == 2)
        {
            p2Character = characterPrefab;
        }
    }
}
