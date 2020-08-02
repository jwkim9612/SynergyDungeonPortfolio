using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using geniikw.DataSheetLab;

public class UICharacterSlotToShow : MonoBehaviour
{
    [SerializeField] private Text characterName = null;
    [SerializeField] private Image image = null;
    [SerializeField] private Image costBorder = null;

    public CharacterData characterData { get; set; } = null;

    public void SetCharacterData(CharacterData newCharacterData)
    {
        characterData = newCharacterData;

        SetName(characterData.Name);
        SetImage(characterData.Image);
        SetCostBorder(characterData.Tier);
    }

    public void SetName(string name)
    {
        if (name != null)
        {
            characterName.text = name;
        }
        else
        {
            Debug.Log("No Character Name");
        }
    }

    public void SetImage(Sprite sprite)
    {
        if (sprite != null)
        {
            image.sprite = sprite;
        }
        else
        {
            Debug.Log("No Character Image");
        }
    }

    public void SetCostBorder(Tier tier)
    {
        switch (tier)
        {
            case Tier.One:
                costBorder.color = Color.gray;
                break;
            case Tier.Two:
                costBorder.color = Color.green;
                break;
            case Tier.Three:
                costBorder.color = Color.blue;
                break;
            case Tier.Four:
                costBorder.color = Color.red;
                break;
            default:
                Debug.Log("Error SetCostBorder");
                break;
        }
    }
}
