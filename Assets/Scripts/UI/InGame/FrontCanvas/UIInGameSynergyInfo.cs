using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameSynergyInfo : MonoBehaviour
{
    [SerializeField] private Image synergyImage = null;
    [SerializeField] private Text synergyNameText = null;
    [SerializeField] private Text synergyInfoText = null;

    [SerializeField] private GameObject inBattleParent = null;
    [SerializeField] private GameObject inPrepareParent = null;

    public void Initialize()
    {
        InGameManager.instance.gameState.OnBattle += MoveForBattle;
        InGameManager.instance.gameState.OnPrepare += MoveForPrepare;
    }

    private void MoveForBattle()
    {
        TransformService.SetParentAndMoveRelativeToParent(this.transform, inBattleParent);
    }

    private void MoveForPrepare()
    {
        TransformService.SetParentAndMoveRelativeToParent(this.transform, inPrepareParent);
    }

    public void SetSynergyInfo(Tribe tribe)
    {
        var tribeDataSheet = DataBase.Instance.tribeDataSheet;
        if(tribeDataSheet == null)
        {
            Debug.LogError("tribeDataSheet is null!");
            return;
        }

        if(tribeDataSheet.TryGetTribeData(tribe, out var tribeData))
        {
            SetSynergyImage(tribeData.Image);
            SetSynergyNameText(SynergyService.GetNameByTribe(tribeData.Tribe));
            SetSynergyInfoText(tribeData.Description);
        }
    }

    public void SetSynergyInfo(Origin origin)
    {
        var originDataSheet = DataBase.Instance.originDataSheet;
        if (originDataSheet == null)
        {
            Debug.LogError("originDataSheet is null!");
            return;
        }

        if (originDataSheet.TryGetOriginData(origin, out var originData))
        {
            SetSynergyImage(originData.Image);
            SetSynergyNameText(SynergyService.GetNameByOrigin(originData.Origin));
            SetSynergyInfoText(originData.Description);
        }
    }

    private void SetSynergyImage(Sprite sprite)
    {
        synergyImage.sprite = sprite;
    }

    private void SetSynergyNameText(string synergyName)
    {
        synergyNameText.text = synergyName;
    }

    private void SetSynergyInfoText(string synergyInfo)
    {
        synergyInfoText.text = synergyInfo;
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
