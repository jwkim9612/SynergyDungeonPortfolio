using System.Collections.Generic;
using UnityEngine;

public class UIArtifactRecipeList : MonoBehaviour
{
    [SerializeField] private List<UIArtifactRecipe> uiArtifactRecipeList;

    public void SetArtifactRecipeList(int artifactPieceId)
    {
        var artifactPieceDataSheet = DataBase.Instance.artifactPieceDataSheet;
        if(artifactPieceDataSheet.TryGetCombinableArtifactIdList(artifactPieceId, out var artifactIdList))
        {
            for(int i = 0; i < uiArtifactRecipeList.Count; ++i)
            {
                if(artifactIdList.Count <= i)
                {
                    uiArtifactRecipeList[i].OnHide();
                    continue;
                }

                uiArtifactRecipeList[i].SetArtifactRecipe(artifactIdList[i]);
                uiArtifactRecipeList[i].OnShow();
            }
        }
    }
}
