using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISellArea : MonoBehaviour
{
    [SerializeField] private Text price = null;

    public void UpdatePrice(CharacterInfo characterInfo)
    {
        price.text = "$" + CharacterService.GetSalePrice(characterInfo);
    }
}
