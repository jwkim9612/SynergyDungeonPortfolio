using System.Collections.Generic;
using UnityEngine;

public class ArtifactData
{
    public ArtifactData(ArtifactExcelData artifactExcelData)
    {
        Id = artifactExcelData.Id;
        Name = artifactExcelData.Name;
        ArtifactPieceIds = artifactExcelData.ArtifactPieceIds;
        InitializeEnemyIds();

        AbilityData = new AbilityData();
        AbilityData.SetAbilityData(artifactExcelData);

        Image = Resources.Load<Sprite>(artifactExcelData.ImagePath);
    }

    private void InitializeEnemyIds()
    {
        ArtifactPieceIdList = new List<int>();

        string[] ArtifactPieceIdsStr = ArtifactPieceIds.Split(',');
        foreach (var ArtifactPieceId in ArtifactPieceIdsStr)
        {
            ArtifactPieceIdList.Add(int.Parse(ArtifactPieceId));
        }
    }

    public int Id;
    public string Name;
    public string ArtifactPieceIds;
    public List<int> ArtifactPieceIdList;
    public AbilityData AbilityData;
    public Sprite Image;
}
