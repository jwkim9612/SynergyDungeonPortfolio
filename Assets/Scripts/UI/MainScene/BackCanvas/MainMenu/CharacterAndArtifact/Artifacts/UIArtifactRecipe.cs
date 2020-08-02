using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIArtifactRecipe : MonoBehaviour
{
    [SerializeField] private Image artifactImage;
    [SerializeField] private List<Image> artifactPieceImageList;
 
    public void SetArtifactRecipe(int artifactId)
    {
        var artifactDataSheet = DataBase.Instance.artifactDataSheet;
        if(artifactDataSheet.TryGetArtifactData(artifactId, out var artifactData))
        {
            artifactImage.sprite = artifactData.Image;

            var artifactPieceIdList = artifactData.ArtifactPieceIdList;
            SetArtifactPieceImageList(artifactPieceIdList);
        }
    }

    private void SetArtifactPieceImageList(List<int> artifactPieceIdList)
    {
        for (int i = 0; i < artifactPieceImageList.Count; ++i)
        {
            var artifactPieceId = artifactPieceIdList[i];
            var artifactPieceDataSheet = DataBase.Instance.artifactPieceDataSheet;
            if (ArtifactManager.Instance.IsOwnedArtifact(artifactPieceId))
            {
                if (artifactPieceDataSheet.TryGetArtifactPieceOnImage(artifactPieceId, out var image))
                {
                    artifactPieceImageList[i].sprite = image;
                }
            }
            else
            {
                if (artifactPieceDataSheet.TryGetArtifactPieceOffImage(artifactPieceId, out var image))
                {
                    artifactPieceImageList[i].sprite = image;
                }
            }
        }
    }

    public void OnShow()
    {
        gameObject.SetActive(true);
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
