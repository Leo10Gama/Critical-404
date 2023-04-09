using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public TMP_Text countdownText;
    public GameObject fightManagerObject;

    private float currentTime = 99f;  // time that starts on the counter
    private FightManager fm;

    private bool timeHasExpired = false;

    void Start()
    {
        fm = fightManagerObject.GetComponent<FightManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeHasExpired) return;

        currentTime -= 1 * Time.deltaTime; // subtracts 1 sec from the starting time foir
        countdownText.text = currentTime.ToString ("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            timeHasExpired = true;
            fm.TimeUp();
        }

        if (currentTime < 10f)
        {
            countdownText.color = Color.red;
        }

    }
}
