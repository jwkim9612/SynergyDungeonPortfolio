using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameTopMenu : MonoBehaviour
{
    [SerializeField] private GameObject prepareMenu = null;
    [SerializeField] private GameObject battleMenu = null;

    private void Start()
    {
        InGameManager.instance.gameState.OnBattle += ShowBattleMenu;
        InGameManager.instance.gameState.OnPrepare += ShowPrepareMenu;
    }

    private void ShowBattleMenu()
    {
        battleMenu.SetActive(true);
        prepareMenu.SetActive(false);
    }

    private void ShowPrepareMenu()
    {
        battleMenu.SetActive(false);
        prepareMenu.SetActive(true);
    }
}
