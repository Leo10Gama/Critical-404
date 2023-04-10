using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSelectCursor : MonoBehaviour
{
    public int cursorId;

    private bool canScroll = true;
    private int curr = 0;

    private CharacterSelectScreen css;
    private RectTransform rt;
    private InputActionAsset inputAsset;
    private InputActionMap player;

    void Awake()
    {
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");

        rt = this.GetComponent<RectTransform>();
        css = transform.Find("/Canvas").GetComponent<CharacterSelectScreen>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }

    public void OnSelect()
    {
        css.SelectCharacter(curr, cursorId);
        Destroy(gameObject);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        float dirX = context.ReadValue<float>();
        if (dirX > 0.1f) ScrollRight();
        else if (dirX < -0.1f) ScrollLeft();
    }

    public void ScrollLeft()
    {
        if (!canScroll) return;
        curr = (curr + 1) % 2;
        Scroll();
        StartCoroutine(WaitToScroll());
    }

    public void ScrollRight()
    {
        if (!canScroll) return;
        curr = (curr + 1) % 2;
        Scroll();
        StartCoroutine(WaitToScroll());
    }

    private void Scroll()
    {
        if (curr == 0)
        {
            rt.anchoredPosition = new Vector3(-780 + ((cursorId - 1) * 135), 470, 0);
        }
        else if (curr == 1)
        {
            rt.anchoredPosition = new Vector3(-430 + ((cursorId - 1) * 135), 470, 0);
        }
    }

    private IEnumerator WaitToScroll()
    {
        canScroll = false;
        yield return new WaitForSeconds(0.2f);  // dont accept input for this long
        canScroll = true;
    }
}
