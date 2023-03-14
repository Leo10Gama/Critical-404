using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayAgainMenu : MonoBehaviour
{

    public GameObject playAgainButton;
    public GameObject quitButton;

    public Color deselected = new Color(1f, 1f, 1f);
    public Color selected = new Color(1f, 173/255f, 76/255f);

    private TextMeshProUGUI playAgain;
    private TextMeshProUGUI quit;

    private int curr = 0;

    // Start is called before the first frame update
    void Start()
    {
        playAgain = playAgainButton.GetComponent<TextMeshProUGUI>();
        quit = quitButton.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectOption()
    {
        if (curr == 0)
        {
            ClickPlayAgain();
        }
        else
        {
            ClickQuit();
        }
    }

    public void ClickPlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickQuit()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void Scroll()
    {
        Debug.Log(quit);
        if (curr == 0)
        {
            quit.color = selected;
            playAgain.color = deselected;
        }
        else
        {
            playAgain.color = selected;
            quit.color = deselected;
        }
        curr = (curr + 1) % 2;
    }
}
