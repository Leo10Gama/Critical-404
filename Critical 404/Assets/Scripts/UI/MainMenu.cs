using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject quitButton;

    public Color deselected = new Color(0.7803922f, 0.6f, 0.8352942f);
    public Color selected = new Color(1f, 0f, 0f);

    private bool canScroll = true;
    private TextMeshProUGUI play;
    private TextMeshProUGUI settings;
    private TextMeshProUGUI quit;

    private int curr = 0;

    // Start is called before the first frame update
    void Start()
    {
        play = playButton.GetComponent<TextMeshProUGUI>();
        settings = settingsButton.GetComponent<TextMeshProUGUI>();
        quit = quitButton.GetComponent<TextMeshProUGUI>();
        Scroll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectOption()
    {
        if (curr == 0)
        {
            ClickPlay();
        }
        else if (curr == 1)
        {
            ClickSettings();
        }
        else
        {
            ClickQuit();
        }
    }

    public void ClickPlay()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ClickSettings()
    {
        // SceneManager.LoadScene("Settings");
    }

    public void ClickQuit()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ScrollDown()
    {
        if (!canScroll) return;
        curr = (curr + 1) % 3;
        Scroll();
        StartCoroutine(WaitToScroll());
    }

    public void ScrollUp()
    {
        if (!canScroll) return;
        curr = (curr + 2) % 3;
        Scroll();
        StartCoroutine(WaitToScroll());
    }

    private void Scroll()
    {
        // if (!canScroll) return;

        if (curr == 0)
        {
            play.color = selected;
            settings.color = deselected;
            quit.color = deselected;
        }
        else if (curr == 1)
        {
            settings.color = selected;
            play.color = deselected;
            quit.color = deselected;
        }
        else
        {
            quit.color = selected;
            play.color = deselected;
            settings.color = deselected;
        }
        // curr = (curr + 1) % 3;
        // StartCoroutine(WaitToScroll());
    }

    private IEnumerator WaitToScroll()
    {
        canScroll = false;
        yield return new WaitForSeconds(0.2f);  // dont accept input for this long
        canScroll = true;
    }
}
