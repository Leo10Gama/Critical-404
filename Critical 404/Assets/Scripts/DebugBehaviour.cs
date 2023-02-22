using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBehaviour : MonoBehaviour
{

    bool AT_SLOW_SPEED = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            if (AT_SLOW_SPEED)
            {
                Time.timeScale = 0.25f;
            }
            else
            {
                Time.timeScale = 1.00f;
            }
            AT_SLOW_SPEED = !AT_SLOW_SPEED;
        }
    }
}
