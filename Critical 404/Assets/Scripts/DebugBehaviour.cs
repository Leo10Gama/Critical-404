using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehaviour : MonoBehaviour
{

    private enum DEBUG_MODE
    {
        NORM,   // 0
        SLOW,   // 1
        STEP    // 2
    }
    private int TOTAL_DEBUG_OPTIONS = Enum.GetNames(typeof(DEBUG_MODE)).Length;
    private DEBUG_MODE currentDebugMode = DEBUG_MODE.NORM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            currentDebugMode = (DEBUG_MODE)(((int)(currentDebugMode) + 1) % TOTAL_DEBUG_OPTIONS);
            Debug.Log("Debug mode changed: " + currentDebugMode);
            switch (currentDebugMode)
            {
                case DEBUG_MODE.NORM:
                    Time.timeScale = 1.00f;
                    break;
                case DEBUG_MODE.SLOW:
                    Time.timeScale = 0.25f;
                    break;
                case DEBUG_MODE.STEP:
                    Time.timeScale = 0.00f;
                    break;
            }
        }

        if (Input.GetKeyDown("p") && currentDebugMode == DEBUG_MODE.STEP)
        {
            StartCoroutine(NextFrame());
        }
    }

    private IEnumerator NextFrame()
    {
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(1f / 60f);
        Time.timeScale = 0f;
    }
}
