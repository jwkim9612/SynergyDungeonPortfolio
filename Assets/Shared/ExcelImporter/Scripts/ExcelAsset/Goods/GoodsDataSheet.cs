using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class GoodsDataSheet : ScriptableObject, IDataSheet
{
	public List<GoodsExcelData> GoodsExcelDatas;
    private Dictionary<int, GoodsData> GoodsDatas;

    public bool TryGetGoodsData(int goodsId, out GoodsData data)
    {
        data = null;

        if (GoodsDatas.TryGetValue(goodsId, out var goodsData))
        {
            data = new GoodsData(goodsData);
            return true;
        }

        Debug.LogError($"Error TryGetGoodsData goodsId:{goodsId}");
        return false;
    }

    public void Initialize()
    {
        GenerateData();
    }

    private void GenerateData()
    {
        GoodsDatas = new Dictionary<int, GoodsData>();

        foreach (var goodsExcelData in GoodsExcelDatas)
        {
            GoodsData goodsData = new GoodsData(goodsExcelData);
            GoodsDatas.Add(goodsExcelData.Id, goodsData);
        }
    }

    public void DataValidate()
    {
        // ���̵� ������ ���� �������� Ȯ��.
        List<int> idList = new List<int>();

        foreach (var goodsExcelData in GoodsExcelDatas)
        {
            if (idList.Contains(goodsExcelData.Id))
            {
                Debug.Log($"Goods ���� ������ Id : {goodsExcelData.Id}���� ��Ĩ�ϴ�.");
            }
            else
            {
                idList.Add(goodsExcelData.Id);
            }
        }
    }
}
