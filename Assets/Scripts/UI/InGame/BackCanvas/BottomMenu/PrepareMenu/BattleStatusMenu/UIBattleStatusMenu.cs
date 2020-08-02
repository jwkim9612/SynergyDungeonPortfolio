using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleStatusMenu : MonoBehaviour
{
    [SerializeField] private UIPlacementStatus uiPlacementStatus = null;
    [SerializeField] private UIWave uiWave = null;
    [SerializeField] private UILevelAndExp uiLevelAndExp = null;
    [SerializeField] private UIAddExp uiAddExp = null;
    [SerializeField] private UICoin uiCoin = null;
    [SerializeField] private UIReload uiReload = null;
    [SerializeField] private UIStart uiStart = null;

    public void Initialize()
    {
        uiWave.Initialize();
        uiPlacementStatus.Initialize();
        uiLevelAndExp.Initialize();
        uiAddExp.Initialize();
        uiCoin.Initialize();
        uiReload.Initialize();
        uiStart.Initialize();
    }
}
