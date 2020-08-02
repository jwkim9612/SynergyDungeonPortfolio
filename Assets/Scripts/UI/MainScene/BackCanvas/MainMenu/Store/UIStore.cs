using UnityEngine;
using UnityEngine.UI;

public class UIStore : MonoBehaviour
{
    [SerializeField] private GameObject beingPurchase = null;
    [SerializeField] private UIFloatingText purchaseCompletedFloatingText = null;
    [SerializeField] private UIFloatingText soldOutFloatingText = null;

    public UIRandomRuneSalesList uiRandomRuneSalesList;
    public UIRandomPotionSalesList uiRandomPotionSalesList;
    public UIRuneOnSalesList uiRuneOnSalesList;
    public UIRandomArtifactPieceSalesList uiRandomArtifactPieceSalesList;

    public UIObtainedRunesScreen uiObtainedRunesScreen;
    public UIObtainedRuneScreen uiObtainedRuneScreen;
    public UIObtainedPotionScreen uiObtainedPotionScreen;
    public UIObtainedRandomArtifactPieceScreen uiObtainedRandomArtifactPieceScreen;

    public UIAskPurchaseForAmountRequiredGoods uiAskPurchaseForAmountRequiredGoods;
    public UIAskPurchaseForRandomPotionGoods uiAskPurchaseForRandomPotionGoods;
    public UIAskPurchaseForRandomRuneGoods uiAskPurchaseForRandomRuneGoods;
    public UIAskPurchaseForRuneGoods uiAskPurchaseForRuneGoods;
    public UIAskPurchaseForRandomArtifactPieceGoods uiAskPurchaseForRandomArtifactPieceGoods;

    public PotentialDraggableScrollView scrollView;

    [SerializeField] private Button cheatGoldButton = null;
    [SerializeField] private Button cheatDiamondButton = null;

    public void Initialize()
    {
        uiRuneOnSalesList.Initialize();
        uiRandomRuneSalesList.Initialize();
        uiRandomPotionSalesList.Initialize();
        uiRandomArtifactPieceSalesList.Initialize();

        uiObtainedRunesScreen.Initialize();
        purchaseCompletedFloatingText.Initialize();
        soldOutFloatingText.Initialize();

        uiAskPurchaseForAmountRequiredGoods.Initialize();
        uiAskPurchaseForRandomPotionGoods.Initialize();
        uiAskPurchaseForRandomRuneGoods.Initialize();
        uiAskPurchaseForRuneGoods.Initialize();
        uiAskPurchaseForRandomArtifactPieceGoods.Initialize();

        SetCheatOnGoldButton();
        SetCheatOnDiamondButton();
    }

    private void SetCheatOnGoldButton()
    {
        cheatGoldButton.onClick.AddListener(() =>
        {
            PlayerDataManager.Instance.playerData.Gold += 1000;
            PlayerDataManager.Instance.SavePlayerDataForCheat();
        });
    }

    private void SetCheatOnDiamondButton()
    {
        cheatDiamondButton.onClick.AddListener(() =>
        {
            PlayerDataManager.Instance.playerData.Diamond += 1000;
            PlayerDataManager.Instance.SavePlayerDataForCheat();
        });
    }

    public void ShowBeingPurchase()
    {
        beingPurchase.SetActive(true);
    }

    public void HideBeginPurchase()
    {
        beingPurchase.SetActive(false);
    }

    public void PlayPurchaseCompletedFloatingText()
    {
        purchaseCompletedFloatingText.Play();
    }

    public void PlaySoldOutFloatingText()
    {
        soldOutFloatingText.Play();
    }
}
