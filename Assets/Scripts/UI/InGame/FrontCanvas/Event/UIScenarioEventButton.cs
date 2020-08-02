using GooglePlayGames.BasicApi.Multiplayer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScenarioEventButton : MonoBehaviour
{
    [SerializeField] private Button button = null;
    [SerializeField] private Text descriptionText = null;
    [SerializeField] private UIReward uiReward = null;
    private ScenarioData scenarioData;
    public ScenarioEvent scenarioEvent;

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            scenarioEvent.IncreaseProbability(scenarioData);

            if(EventProcessing())
            {
                SetRewardAndShowReward();
            }
            else
            {
                GetComponentInParent<UIScenarioEvent>().gameObject.SetActive(false);
            }
        });
    }

    public void SetButton(ScenarioData data)
    {
        SetScenarioData(data);
        SetText(data.Description);
    }

    public void SetText(string text)
    {
        this.descriptionText.text = text;
    }

    public void SetScenarioData(ScenarioData data)
    {
        scenarioData = data;
    }

    public void SetInteractable()
    {
        if(scenarioData.CurrencyType == RewardCurrency.Coin)
        {
            var playerCoin = InGameManager.instance.playerState.coin;

            if(playerCoin + scenarioData.Amount < 0)
            {
                button.interactable = false;
                return;
            }
        }

        button.interactable = true;
    }

    public void OnInvisible()
    {
        GetComponent<RectTransform>().localScale = Vector3.zero;
    }

    public void OnVisible()
    {
        GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }

    public void SetRewardAndShowReward()
    {
        uiReward.SetReward(scenarioData);
        uiReward.OnShow();
    }

    /// <summary>
    /// 이벤트 처리
    /// </summary>
    /// <returns>이벤트 보상이 있는가</returns>
    private bool EventProcessing()
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
                InGameManager.instance.playerState.IncreaseCoin(scenarioData.Amount);
                break;
            case RewardCurrency.Status:
                InGameManager.instance.backCanvas.uiMainMenu.uiAbilityEffectList.AddAbilityEffect(scenarioData);
                break;
            case RewardCurrency.RandomArtifactPiece:
                break;
            case RewardCurrency.Nothing:
                return false;
            default:
                break;
        }

        return true;
    }


}
