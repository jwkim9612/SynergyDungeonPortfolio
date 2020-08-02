using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ProbabilityDataSheet : ScriptableObject, IDataSheet
{
	public List<ProbabilityExcelData> ProbabilityExcelDatas;
    private Dictionary<int, ProbabilityData> ProbabilityDatas;

    public bool TryGetProbabilityData(int level, out ProbabilityData data)
    {
        data = null;

        if (ProbabilityDatas.TryGetValue(level, out var probabilityData))
        {
            data = new ProbabilityData(probabilityData);
            return true;
        }

        Debug.LogError($"Error TryGetProbabilityData level:{level}");
        return false;
    }

    public void Initialize()
    {
        GenerateData();
    }

    private void GenerateData()
    {
        ProbabilityDatas = new Dictionary<int, ProbabilityData>();

        foreach (var probabilityExcelData in ProbabilityExcelDatas)
        {
            ProbabilityData probabilityData = new ProbabilityData(probabilityExcelData);
            ProbabilityDatas.Add(probabilityData.Level, probabilityData);
        }
    }

    public void DataValidate()
    {
        // ������ ������ ���� �������� Ȯ��.
        List<int> levelList = new List<int>();

        foreach (var probabilityExcelData in ProbabilityExcelDatas)
        {
            if (levelList.Contains(probabilityExcelData.Level))
            {
                Debug.Log($"Probability ���� ������ Level : {probabilityExcelData.Level}���� ��Ĩ�ϴ�.");
            }
            else
            {
                levelList.Add(probabilityExcelData.Level);
            }
        }
    }
}
