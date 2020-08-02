using UnityEngine;
using UnityEngine.UI;

public class UIArtifactPieceInfo : UIControl
{
    [SerializeField] private Image artifactPieceImage = null;
    [SerializeField] private Text artifactPieceName = null;
    [SerializeField] private UIArtifactRecipeList uiArtifactRecipeList = null;

    public void SetArtifactPieceInfo(int artifactPieceId, bool isOwned)
    {
        var artifactPieceDataSheet = DataBase.Instance.artifactPieceDataSheet;

        if(isOwned)
        {
            if (artifactPieceDataSheet.TryGetArtifactPieceOnImage(artifactPieceId, out var image))
            {
                artifactPieceImage.sprite = image;
            }
        }
        else
        {
            if (artifactPieceDataSheet.TryGetArtifactPieceOffImage(artifactPieceId, out var image))
            {
                artifactPieceImage.sprite = image;
            }
        }

        if (artifactPieceDataSheet.TryGetArtifactPieceName(artifactPieceId, out var name))
        {
            artifactPieceName.text = name;
        }

        uiArtifactRecipeList.SetArtifactRecipeList(artifactPieceId);
    }
}
