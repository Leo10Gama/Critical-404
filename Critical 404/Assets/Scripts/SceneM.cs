using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneM : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    private GameObject p1;
    private GameObject p2;
    private PlayerMovement p1script;
    private PlayerMovement p2script;
    private GameObject turningPoint = null;

    void Awake()
    {
        p1 = (GameObject)Instantiate(player1, new Vector3(-6f, 0f, 0f), Quaternion.identity);
        p2 = (GameObject)Instantiate(player2, new Vector3(6f, 0f, 0f), Quaternion.identity);
        p1script = p1.GetComponent<PlayerMovement>();
        p2script = p2.GetComponent<PlayerMovement>();
        turningPoint = transform.Find("TurningPoint").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = 0f;
        float p1x = p1.transform.position.x;
        float p2x = p2.transform.position.x;
        if (p1x > p2x)
        {
            newPos = ((p1x - p2x) / 2f) + p2x;
        }
        else
        {
            newPos = ((p2x - p1x) / 2f) + p1x;
        }
        p1script.SetTurningPoint(newPos);
        p2script.SetTurningPoint(newPos);
        turningPoint.transform.position = new Vector3(newPos, 0f, 0f);
    }
}
