using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ScreenShakeController : MonoBehaviour
{
 
    public static ScreenShakeController instance;
    public GameObject cam;
    public float rotationMultiplier = 15f;

    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    private Vector3 initialPosition;
 
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        initialPosition = new Vector3(
            cam.transform.position.x, cam.transform.position.y, cam.transform.position.z
        );
    }
 
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.K))
        // {
        //     StartShake(0.5f, 1f);
        // }
    }
 
    private void LateUpdate()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;
 
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;
 
            Vector3 newPosition = new Vector3(xAmount, yAmount, 0f);
            cam.transform.position = initialPosition + newPosition;
 
            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }
        else
        {
            cam.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            cam.transform.position = initialPosition;
        }
    }
 
    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length / 60f;
        shakePower = power;
 
        shakeFadeTime = power / length;
 
        shakeRotation = power * rotationMultiplier;
    }
}