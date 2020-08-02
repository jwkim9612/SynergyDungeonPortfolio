using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameCharacterInfo : MonoBehaviour
{
    [SerializeField] private Image characterImage = null;
    [SerializeField] private Text nameText = null;
    [SerializeField] private Image tribeImage = null;
    [SerializeField] private Image originImage = null;

    [SerializeField] private Text textAttack = null;
    [SerializeField] private Text textMagicalAttack = null;
    [SerializeField] private Text textHealth = null;
    [SerializeField] private Text textDefence = null;
    [SerializeField] private Text textMagicDefence = null;
    [SerializeField] private Text textShield = null;
    [SerializeField] private Text textAccuracy = null;
    [SerializeField] private Text textEvasion = null;
    [SerializeField] private Text textCritical = null;
    [SerializeField] private Text textAttackSpeed = null;

    public void SetInGameCharacterInfo(UICharacter uiCharacter)
    {
        var characterDataSheet = DataBase.Instance.characterDataSheet;
        if(characterDataSheet.TryGetCharacterData(uiCharacter.characterInfo.id, out var characterData))
        {
            // 후에 변경 필요
            if(characterData.HeadImage == null)
            {
                SetCharacterImage(characterData.Image);
            }
            else
            {
                SetCharacterImage(characterData.HeadImage);
            }

            SetNameText(characterData.Name);
            SetTribeImage(characterData.TribeData.Image);
            SetOriginImage(characterData.OriginData.Image);
            SetCharacterAbilityText(uiCharacter.character.ability);
        }
    }

    private void SetCharacterAbilityText(AbilityData abilityData)
    {
        textAttack.text = abilityData.Attack.ToString();
        textMagicalAttack.text = abilityData.MagicalAttack.ToString();
        textHealth.text = abilityData.Health.ToString();
        textDefence.text = abilityData.Defence.ToString();
        textMagicDefence.text = abilityData.MagicDefence.ToString();
        textShield.text = abilityData.Shield.ToString();
        textAccuracy.text = abilityData.Accuracy.ToString();
        textEvasion.text = abilityData.Evasion.ToString();
        textCritical.text = abilityData.Critical.ToString();
        textAttackSpeed.text = abilityData.AttackSpeed.ToString();
    }

    private void SetCharacterImage(Sprite sprite)
    {
        if(sprite == null)
        {
            Debug.LogWarning("Error SetCharacterImage");
            return;
        }

        characterImage.sprite = sprite;
    }

    private void SetNameText(string name)
    {
        nameText.text = name;
    }

    private void SetTribeImage(Sprite sprite)
    {
        tribeImage.sprite = sprite;
    }

    private void SetOriginImage(Sprite sprite)
    {
        originImage.sprite = sprite;
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
