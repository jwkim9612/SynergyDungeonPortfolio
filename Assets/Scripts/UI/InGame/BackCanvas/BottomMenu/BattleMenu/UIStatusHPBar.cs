﻿using UnityEngine;
using UnityEngine.UI;

public class UIStatusHPBar : MonoBehaviour
{
    [SerializeField] private Slider hpSlider = null;
    private Character controllingPawn = null;

    public void Initialize()
    {
        InGameManager.instance.gameState.OnBattle += UpdateHPBar;
    }

    public void SetControllingPawn(Character character)
    {
        if (character != null)
        {
            controllingPawn = character;
            controllingPawn.OnHit += UpdateHPBar;
        }
    }

    public void UpdateHPBar()
    {
        if (controllingPawn != null)
        {
            hpSlider.value = controllingPawn.GetHPRatio();
        }
        else
        {
            Debug.Log("Error Update HP Bar. ControllingPawn is null");
        }
    }
}
