using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    
    float currentTime = 0f;
    float startingTime = 15f; //starting time

    public TMP_Text countdownText;


    void Start()
    {
       currentTime = startingTime; 
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime; // subtracts 1 sec from the starting time foir
        countdownText.text = currentTime.ToString ("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }

        if (currentTime < 10f)
        {
            countdownText.color = Color.red;
        }

    }
}
