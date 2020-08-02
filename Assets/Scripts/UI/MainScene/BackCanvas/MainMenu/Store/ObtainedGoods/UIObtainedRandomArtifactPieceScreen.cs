using UnityEngine;
using UnityEngine.UI;

public class UIObtainedRandomArtifactPieceScreen : UIControl
{
    [SerializeField] private Image artifactPieceImage = null;
    [SerializeField] private Text artifactPieceText = null;

    public void SetUIObtainedScreen(int artifactPieceId)
    {
        var artifactPieceDataSheet = DataBase.Instance.artifactPieceDataSheet;
        if (artifactPieceDataSheet.TryGetArtifactPieceData(artifactPieceId, out var artifactPieceData))
        {
            SetGoodsImage(artifactPieceData.OnImage);
            SetGoodsName(artifactPieceData.Name);
        }
    }

    private void SetGoodsImage(Sprite image)
    {
        artifactPieceImage.sprite = image;
    }

    private void SetGoodsName(string name)
    {
        artifactPieceText.text = name;
    }
}
