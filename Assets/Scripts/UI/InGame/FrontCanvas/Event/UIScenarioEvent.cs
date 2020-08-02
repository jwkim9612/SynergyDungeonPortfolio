using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScenarioEvent : MonoBehaviour
{
    [SerializeField] private GameObject main = null;
    [SerializeField] private GameObject scenario = null;
    [SerializeField] private Text titleText = null;
    [SerializeField] private List<UIScenarioEventButton> selectButtonList = null;
    public ScenarioEvent scenarioEvent;

    public void Initialize()
    {
        scenarioEvent = new ScenarioEvent();
        scenarioEvent.Initialize();

        foreach (var selectButton in selectButtonList)
        {
            selectButton.scenarioEvent = this.scenarioEvent;
        }

        InGameManager.instance.gameState.OnPrepare += CheckScenarioDataAndSetScenarioEvent;
    }

    private void CheckScenarioDataAndSetScenarioEvent()
    {
        OnShow();
        main.SetActive(false);

        scenarioEvent.UpdateStageData();
        if (scenarioEvent.HasScenarioData())
        {
            OnShow();
            StartCoroutine(Co_CheckScenarioDataAndSetScenarioEvent());
        }
    }

    private IEnumerator Co_CheckScenarioDataAndSetScenarioEvent()
    {
        var animationTime = InGameManager.instance.frontCanvas.uiEventOccurred.PlayAnimation();
        yield return new WaitForSeconds(animationTime);
        
        var activatedSelectButtonList = SetSelectButtonListAndGetSelectButtonList();
        OnShowMainAndScenario();

        ////////////////////////
        titleText.text = "";

        var description = scenarioEvent.GetTitleText();
        foreach (var letter in description)
        {
            titleText.text += letter;
            yield return new WaitForSeconds(InGameService.TITLE_READ_SPEED);
        }
        ////////////////////////////

        foreach (var selectButton in activatedSelectButtonList)
        {
            selectButton.OnVisible();
        }
    }

    public List<UIScenarioEventButton> SetSelectButtonListAndGetSelectButtonList()
    {
        List<UIScenarioEventButton> activatedSelectButtonList = new List<UIScenarioEventButton>(); 

        for (int scenarioId = 1; scenarioId <= selectButtonList.Count; scenarioId++)
        {
            var scenarioData = scenarioEvent.GetScenarioDataByScenarioId(scenarioId);
            if (scenarioData != null)
            {
                selectButtonList[scenarioId - 1].SetButton(scenarioData);
                selectButtonList[scenarioId - 1].OnInvisible();
                selectButtonList[scenarioId - 1].OnShow();
                selectButtonList[scenarioId - 1].SetInteractable();
                activatedSelectButtonList.Add(selectButtonList[scenarioId - 1]);
            }
            else
            {
                selectButtonList[scenarioId - 1].OnHide();
            }
        }

        return activatedSelectButtonList;
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnShowMainAndScenario()
    {
        main.SetActive(true);
        scenario.SetActive(true);
    }
}
