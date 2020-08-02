using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class EnemyDataSheet : ScriptableObject, IDataSheet
{
    public List<EnemyExcelData> EnemyExcelDatas;
	private Dictionary<int, EnemyData> EnemyDatas;

	public bool TryGetEnemyData(int enemyId, out EnemyData data)
	{
		data = null;

		if (EnemyDatas.TryGetValue(enemyId, out var enemyData))
		{
			data = new EnemyData(enemyData);
			return true;
		}

		Debug.LogError($"Error TryGetEnemyData enemyId:{enemyId}");
		return false;
	}

	public void Initialize()
	{
		GenerateData();
	}

	private void GenerateData()
	{
		EnemyDatas = new Dictionary<int, EnemyData>();

		foreach (var enemyExcelData in EnemyExcelDatas)
		{
			EnemyData enemyData = new EnemyData(enemyExcelData);
			EnemyDatas.Add(enemyData.Id, enemyData);
		}
	}

    public void DataValidate()
    {
        // ���̵� ������ ���� �������� Ȯ��.
        List<int> idList = new List<int>();

        foreach (var enemyExcelData in EnemyExcelDatas)
        {
            if (idList.Contains(enemyExcelData.Id))
            {
                Debug.Log($"Enemy ���� ������ Id : {enemyExcelData.Id}���� ��Ĩ�ϴ�.");
            }
			else
			{
				idList.Add(enemyExcelData.Id);
			}
		}
    }
}
