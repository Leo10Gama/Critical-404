using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Toggle toggle;

    void Start()
    {
        // Attach a listener to the toggle component to handle toggle events
        toggle.onValueChanged.AddListener(ToggleGameObject);
    }

    void ToggleGameObject(bool isOn)
    {
        // Check if the toggle is on and enable/disable the game object accordingly
        gameObject.SetActive(isOn);
    }

    void Update()
    {
        // Check if the "L" key is pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Toggle the state of the toggle component
            toggle.isOn = !toggle.isOn;
        }
    }
}
