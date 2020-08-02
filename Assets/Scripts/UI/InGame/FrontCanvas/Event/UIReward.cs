using UnityEngine;
using UnityEngine.UI;

public class UIReward : MonoBehaviour
{
    [SerializeField] private Image rewardImage = null;
    [SerializeField] private Text rewardText = null;
    [SerializeField] private Button button = null;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            OnHide();
            transform.parent.gameObject.SetActive(false);
        });
    }
    
    public void SetReward(ScenarioData scenarioData)
    {
        switch (scenarioData.CurrencyType)
        {
            case RewardCurrency.None:
                break;
            case RewardCurrency.Gold:
                break;
            case RewardCurrency.Rune:
                break;
            case RewardCurrency.RandomRune:
                break;
            case RewardCurrency.RandomPotion:
                break;
            case RewardCurrency.Relic:
                break;
            case RewardCurrency.Artifact:
                break;
            case RewardCurrency.Coin:
                rewardImage.sprite = InGameService.COIN_IMAGE;
                break;
            case RewardCurrency.Status:
                break;
            case RewardCurrency.RandomArtifactPiece:
                break;
            case RewardCurrency.Nothing:
                break;
            default:
                break;
        }


        SetText(scenarioData.RewardDescription);
    }

    

    private void SetText(string text)
    {
        rewardText.text = text;
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
