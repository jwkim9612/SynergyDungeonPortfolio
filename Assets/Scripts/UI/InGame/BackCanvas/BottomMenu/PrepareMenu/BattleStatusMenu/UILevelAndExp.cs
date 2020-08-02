using UnityEngine;
using UnityEngine.UI;

public class UILevelAndExp : MonoBehaviour
{
    [SerializeField] private Text levelText = null;
    [SerializeField] private Text expText = null;

    public void Initialize()
    {
        UpdateExpText();
        UpdateLevelText();

        InGameManager.instance.playerState.OnExpChanged += UpdateExpText;
        InGameManager.instance.playerState.OnLevelUp += UpdateLevelText;
    }

    public void UpdateLevelText()
    {
        int level = InGameManager.instance.playerState.level;

        levelText.text = "Lv" + level;
    }

    public void UpdateExpText()
    {
        var playerState = InGameManager.instance.playerState;

        if (playerState.IsMaxLevel())
        {
            expText.text = "Max";
        }
        else
        {
            int exp = playerState.exp;
            int satisfyExp = playerState.SatisfyExp;

            expText.text = exp + "/" + satisfyExp;
        }
    }
}
