using System.Collections.Generic;
using UnityEngine;

public class ArtifactPieceData
{
    public ArtifactPieceData(ArtifactPieceExcelData artifactPieceExcelData)
    {
        Id = artifactPieceExcelData.Id;
        Name = artifactPieceExcelData.Name;
        DropProbability = artifactPieceExcelData.DropProbability;

        OnImage = Resources.Load<Sprite>(artifactPieceExcelData.OnImagePath);
        OffImage = Resources.Load<Sprite>(artifactPieceExcelData.OffImagePath);

        InitializeCombinableArtifactsList();
    }

    public ArtifactPieceData(ArtifactPieceData artifactPieceData)
    {
        Id = artifactPieceData.Id;
        Name = artifactPieceData.Name;
        DropProbability = artifactPieceData.DropProbability;
        OnImage = artifactPieceData.OnImage;
        OffImage = artifactPieceData.OffImage;
        CombinableArtifactList = artifactPieceData.CombinableArtifactList;
    }

    private void InitializeCombinableArtifactsList()
    {
        CombinableArtifactList = new List<int>();

        var artifactDataSheet = DataBase.Instance.artifactDataSheet;
        if (artifactDataSheet.TryGetArtifactDatas(out var artifactDatas))
        {
            foreach (var artifactData in artifactDatas)
            {
                if (artifactData.Value.ArtifactPieceIdList.Contains(this.Id))
                {
                    CombinableArtifactList.Add(artifactData.Value.Id);
                }
            }
        }
    }

    public int Id;
    public string Name;
    public int DropProbability;
    public Sprite OffImage;
    public Sprite OnImage;
    public List<int> CombinableArtifactList;
}
