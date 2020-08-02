using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIArtifactPieceList : MonoBehaviour
{
    [SerializeField] private UIArtifactPiece uiArtifactPiece = null;
    [SerializeField] private GridLayoutGroup gridLayout = null;
    private List<UIArtifactPiece> uiArtifactPieceList;

    public void Initialize()
    {
        InitializeArtifactPieceList();

        ArtifactManager.Instance.OnAddArtifactPiece += UpdateArtifactPiece;
    }

    private void InitializeArtifactPieceList()
    {
        uiArtifactPieceList = new List<UIArtifactPiece>();

        var artifactPieceDataSheet = DataBase.Instance.artifactPieceDataSheet;
        if (artifactPieceDataSheet.TryGetArtifactPieceDatas(out var artifactPieceDatas))
        {
            foreach (var artifactPiece in artifactPieceDatas)
            {
                var uiArtifactPiece = Instantiate(this.uiArtifactPiece, gridLayout.transform);

                bool isOwnedArtifact = false;
                var ownedArtifactPieceIdList = ArtifactManager.Instance.ownedPieceIdList;
                if (ownedArtifactPieceIdList.Contains(artifactPiece.Value.Id))
                {
                    isOwnedArtifact = true;
                }
                uiArtifactPiece.SetArtifactPiece(artifactPiece.Value.Id, isOwnedArtifact);
                uiArtifactPieceList.Add(uiArtifactPiece);
            }
        }

        uiArtifactPiece.OnHide();
        Sort();
    }

    public void UpdateArtifactPiece(int id)
    {
        var ownedArtifactPieceIdList = ArtifactManager.Instance.ownedPieceIdList;
        if (ownedArtifactPieceIdList.Contains(id))
        {
            var uiArtifactPiece = uiArtifactPieceList.Find(x => x.id == id);
            if(uiArtifactPiece != null)
            {
                uiArtifactPiece.SetOn();
                Sort();
            }
        }
    }

    private void Sort()
    {
        uiArtifactPieceList = uiArtifactPieceList.OrderByDescending(x => x.isOwned).ThenBy(x => x.id).ToList();

        for (int i = 0; i < uiArtifactPieceList.Count; i++)
        {
            uiArtifactPieceList[i].gameObject.transform.SetSiblingIndex(i);
        }
    }
}
