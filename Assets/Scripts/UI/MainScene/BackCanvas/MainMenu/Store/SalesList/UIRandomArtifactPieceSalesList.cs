using UnityEngine;

public class UIRandomArtifactPieceSalesList : MonoBehaviour
{
    private UIRandomArtifactPieceGoods uiRandomArtifactPieceGoods;

    public void Initialize()
    {
        SetUIRandomArtifactPieceGoods();
        UpdateGoods();

        ArtifactManager.Instance.OnArtifactPieceChanged += UpdateGoods;
    }

    private void SetUIRandomArtifactPieceGoods()
    {
        uiRandomArtifactPieceGoods = GetComponentInChildren<UIRandomArtifactPieceGoods>();

        var randomArtifactPieceSalesId = GoodsService.RANDOM_ARTIFACTPIECE_SALES_ID;

        var goodsDataSheet = DataBase.Instance.goodsDataSheet;
        if (goodsDataSheet.TryGetGoodsData(randomArtifactPieceSalesId, out var goodsData))
        {
            uiRandomArtifactPieceGoods.SetUIGoods(goodsData, randomArtifactPieceSalesId);
        }
    }

    public void UpdateGoods()
    {
        var pieceTotalNumber = ArtifactManager.Instance.pieceTotalNumber;
        var unlockedArtifactPieceNum = ArtifactManager.Instance.ownedPieceIdList.Count;

        if (pieceTotalNumber == unlockedArtifactPieceNum)
        {
            uiRandomArtifactPieceGoods.Lock();
        }
    }

    private void OnDestroy()
    {
        if(ArtifactManager.IsAlive)
        {
            ArtifactManager.Instance.OnArtifactPieceChanged -= UpdateGoods;
        }
    }
}
