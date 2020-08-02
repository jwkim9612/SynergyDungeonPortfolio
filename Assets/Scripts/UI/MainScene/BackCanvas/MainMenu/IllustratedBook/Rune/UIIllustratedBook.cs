using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIllustratedBook : MonoBehaviour
{
    public UIRunePage uiRunePage = null;
    public UIObtainedRuneScreen uiObtainedRuneByCombinationScreen = null;

    /// <summary>
    /// 치트
    public Button buyButton;
    public InputField runeIdInputField;

    private void Start()
    {
        /// 치트
        buyButton.onClick.AddListener(() =>
        {
            if (runeIdInputField.text == "")
                return;

            if (IsNumber(runeIdInputField.text))
            {
                if (DataBase.Instance.runeDataSheet.TryGetRuneData(int.Parse(runeIdInputField.text), out var runeData))
                {
                    RuneManager.Instance.AddRune(int.Parse(runeIdInputField.text));
                }
            }
        });
    }

    public bool IsNumber(string me)
    {
        foreach (char ch in me)
        {
            if (!Char.IsDigit(ch))
                return false;
        }

        return true;
    }
    /// </summary>

    public void Initialize()
    {
        uiRunePage.Initialize();
    }
}
