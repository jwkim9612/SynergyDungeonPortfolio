using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ArtifactDataSheet : ScriptableObject, IDataSheet
{
	public List<ArtifactExcelData> ArtifactExcelDatas;
    private Dictionary<int, ArtifactData> ArtifactDatas;

    public void Initialize()
    {
        GenerateData();
    }

    private void GenerateData()
    {
        ArtifactDatas = new Dictionary<int, ArtifactData>();

        foreach (var ArtifactCombinationExcelData in ArtifactExcelDatas)
        {
            ArtifactData artifactCombinationData = new ArtifactData(ArtifactCombinationExcelData);
            ArtifactDatas.Add(artifactCombinationData.Id, artifactCombinationData);
        }
    }

    public bool TryGetArtifactData(int artifactId, out ArtifactData data)
    {
        data = null;
        if(ArtifactDatas.TryGetValue(artifactId, out data))
        {
            return true;
        }

        Debug.Log($"Error TryGetArtifactData artifactId = {artifactId}");
        return false;
    }

    public bool TryGetArtifactDatas(out Dictionary<int, ArtifactData> artifactCombinationDatas)
    {
        artifactCombinationDatas = new Dictionary<int, ArtifactData>(ArtifactDatas);
        if (artifactCombinationDatas != null)
        {
            return true;
        }

        Debug.LogError("Error TryGetArtifactDatas");
        return false;
    }

    public void DataValidate()
    {
        // ���̵� ������ ���� �������� Ȯ��.
        List<int> idList = new List<int>();

        foreach (var artifactExcelData in ArtifactExcelDatas)
        {
            if (idList.Contains(artifactExcelData.Id))
            {
                Debug.Log($"Artifact ���� ������ Id : {artifactExcelData.Id}���� ��Ĩ�ϴ�.");
            }
            else
            {
                idList.Add(artifactExcelData.Id);
            }
        }
    }
}
