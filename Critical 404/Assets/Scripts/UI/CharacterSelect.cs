using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject character1Info;
    public GameObject character2Info;
    public Image characterPortrait;
    public Text characterName;
    public Text characterDescription;

    public Image selectedCharacterImage;

    private bool selectingCharacter1 = true;

    private void Start()
    {
        SelectCharacter1();
    }

    public void SelectCharacter1()
    {
        selectingCharacter1 = true;
        character1Info.SetActive(true);
        character2Info.SetActive(false);
        UpdateCharacterUI();
    }

    public void SelectCharacter2()
    {
        selectingCharacter1 = false;
        character1Info.SetActive(false);
        character2Info.SetActive(true);
        UpdateCharacterUI();
    }

    private void UpdateCharacterUI()
    {
        if (selectingCharacter1)
        {
            characterPortrait.sprite = character1Info.GetComponent<CharacterInfo>().portrait;
            characterName.text = character1Info.GetComponent<CharacterInfo>().name;
            characterDescription.text = character1Info.GetComponent<CharacterInfo>().description;
            selectedCharacterImage.sprite = character1Info.GetComponent<CharacterInfo>().portrait;
        }
        else
        {
            characterPortrait.sprite = character2Info.GetComponent<CharacterInfo>().portrait;
            characterName.text = character2Info.GetComponent<CharacterInfo>().name;
            characterDescription.text = character2Info.GetComponent<CharacterInfo>().description;
            selectedCharacterImage.sprite = character2Info.GetComponent<CharacterInfo>().portrait;

            if (selectingCharacter1)
            {
                // ...
                Debug.Log("Updating character UI for Character 1");
            }
            else
            {
                // ...
                Debug.Log("Updating character UI for Character 2");
            }
        }
    }
}
