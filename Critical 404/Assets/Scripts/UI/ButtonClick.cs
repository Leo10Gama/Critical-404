using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Image Player1pick;
    public Image Player2pick;

    public TMP_Text Player1Pick;
    public TMP_Text Player2Pick;

    public string characterName;

    public void OnClick(int playerNum)
    {
        Image sourceImage = GetComponent<Image>();

        if (playerNum == 1)
        {
            Player1pick.sprite = sourceImage.sprite;
            Player1Pick.text = characterName;
        }
        else if (playerNum == 2)
        {
            Player2pick.sprite = sourceImage.sprite;
            Player2Pick.text = characterName;
        }
    }
}
