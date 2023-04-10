using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectScreen : MonoBehaviour
{
    public GameObject p1Cursor;
    public GameObject p2Cursor;

    public GameObject milaImage;
    public GameObject spreadImage;

    public GameObject milaPrefab;
    public GameObject spreadPrefab;

    private bool[] selectedCharacter = new bool[] {false, false};

    private CharacterSelectCursor p1c;
    private CharacterSelectCursor p2c;
    private int[] selectIndex = new int[] {0, 0};

    // Start is called before the first frame update
    void Start()
    {
        p1c = p1Cursor.GetComponent<CharacterSelectCursor>();
        p2c = p2Cursor.GetComponent<CharacterSelectCursor>();
        
        p1c.cursorId = 1;
        p2c.cursorId = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCharacter(int index, int cursorId)
    {
        if (index == 0)         // mila
        {
            milaImage.GetComponent<ButtonClick>().OnClick(cursorId);
            CharacterManager.SetCharacter(cursorId, milaPrefab);
        }
        else if (index == 1)    // spread
        {
            spreadImage.GetComponent<ButtonClick>().OnClick(cursorId);
            CharacterManager.SetCharacter(cursorId, spreadPrefab);
        }

        selectedCharacter[cursorId - 1] = true;

        if (selectedCharacter[0] && selectedCharacter[1])
        {
            StartCoroutine(LoadFight());
        }
    }

    private IEnumerator LoadFight()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }
}
