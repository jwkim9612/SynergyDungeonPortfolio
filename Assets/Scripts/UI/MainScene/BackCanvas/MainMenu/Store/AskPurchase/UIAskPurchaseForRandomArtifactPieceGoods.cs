public class UIAskPurchaseForRandomArtifactPieceGoods : UIAskPurchase
{
    public override void Initialize()
    {
        SetPurchaseButton();
    }

    private void SetPurchaseButton()
    {
        purchaseButton.onClick.AddListener(() =>
        {
            UIManager.Instance.HideAndShowPreview();

            var artifactPieceTotalNumber = ArtifactManager.Instance.pieceTotalNumber;
            GoodsManager.Instance.PurchaseRandomArtifactPiece(goodsId, artifactPieceTotalNumber);
        });
    }

    public void SetUIAskPurchase(GoodsData goodsData, int goodsId)
    {
        SetAskPurchaseText(goodsData.Name);
        SetGoodsImage(goodsData.Image);
        SetGoodsPrice(goodsData.PurchasePrice, goodsData.PurchaseCurrency);
        SetPurchaseCurrencyImage(goodsData.PurchaseCurrency);

        this.goodsId = goodsId;
    }
}
