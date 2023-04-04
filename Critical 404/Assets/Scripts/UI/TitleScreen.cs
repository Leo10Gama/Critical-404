using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleScreen : MonoBehaviour
{
    public TextMeshProUGUI startText;

    void Start()
    {
        startText = GameObject.Find("Canvas/MainMenu/StartButtonText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    public void Mainmenu()
    {
        startText.text = "Loading...";
        //yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainMenu");
    }

    public void setting()
    {
        SceneManager.LoadScene("Settings");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Title()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}