using UnityEngine;
using UnityEngine.UI;

public class UIRandomArtifactPieceGoods : UIGoods
{
    [SerializeField] private Image lockImage = null;

    public void SetUIGoods(GoodsData goodsData, int goodsId)
    {
        SetGoodsName(goodsData.Name);
        SetGoodsImage(goodsData.Image);
        SetGoodsPrice(goodsData.PurchasePrice);
        SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);

        showAskPurchaseButton.onClick.AddListener(() =>
        {
            var uiAskPurchaseForRandomArtifactPieceGoods = MainManager.instance.backCanvas.uiMainMenu.uiStore.uiAskPurchaseForRandomArtifactPieceGoods;
            uiAskPurchaseForRandomArtifactPieceGoods.SetUIAskPurchase(goodsData, goodsId);
            UIManager.Instance.ShowNew(uiAskPurchaseForRandomArtifactPieceGoods);
        });
    }

    public void Lock()
    {
        showAskPurchaseButton.interactable = false;
        lockImage.gameObject.SetActive(true);
    }
}
