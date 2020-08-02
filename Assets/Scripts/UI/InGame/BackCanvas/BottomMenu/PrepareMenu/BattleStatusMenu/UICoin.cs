using UnityEngine;
using UnityEngine.UI;

public class UICoin : MonoBehaviour
{
    [SerializeField] private Text coinText = null;

    public void Initialize()
    {
        UpdateCoinText();

        InGameManager.instance.playerState.OnCoinChanged += UpdateCoinText;
    }

    public void UpdateCoinText()
    {
        coinText.text = InGameManager.instance.playerState.coin + "";
    }
}
