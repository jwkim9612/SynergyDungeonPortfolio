using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlacementStatus : MonoBehaviour
{
    [SerializeField] private Text placementStatus = null;

    public void Initialize()
    {
        UpdatePlacementStatus();

        InGameManager.instance.draggableCentral.uiCharacterArea.OnPlacementChanged += UpdatePlacementStatus;
        InGameManager.instance.playerState.OnLevelUp += UpdatePlacementStatus;
    }

    public void UpdatePlacementStatus()
    {
        int numOfCurrentPlacedCharacters = InGameManager.instance.draggableCentral.uiCharacterArea.numOfCurrentPlacedCharacters;
        int numOfCanPlacedInBattleArea = InGameManager.instance.playerState.numOfCanPlacedInBattleArea;

        placementStatus.text = numOfCurrentPlacedCharacters + "/" + numOfCanPlacedInBattleArea;
    }
}
