using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class ChapterInfoDataSheet : ScriptableObject, IDataSheet
{
	public List<ChapterInfoExcelData> ChapterInfoExcelDatas;

	private Dictionary<int, ChapterInfoData> ChapterInfoDatas;
	private Dictionary<int, Dictionary<int, ChapterInfoData>> AllChapterInfoDatas;

	public bool TryGetChapterInfoExpAmount(int chapterId, int waveId, out int amount)
	{
		amount = 0;

		if (TryGetChapterInfoData(chapterId, waveId, out var chapterInfoData))
		{
			amount = chapterInfoData.ExpAmount;
			return true;
		}

		Debug.LogWarning($"Error TryGetChapterInfoExpAmount chapterId:{chapterId} waveId:{waveId}");
		return false;
	}

	public bool TryGetChapterInfoGoldAmount(int chapterId, int waveId, out int amount)
	{
		amount = 0;

		if(TryGetChapterInfoData(chapterId, waveId, out var chapterInfoData))
		{
			amount = chapterInfoData.GoldAmount;
			return true;
		}

		Debug.LogWarning($"Error TryGetChapterInfoGoldAmount chapterId:{chapterId} waveId:{waveId}");
		return false;
	}

	public bool TryGetChapterInfoData(int chapterId, int waveId, out ChapterInfoData data)
	{
		data = null;

		if (TryGetChapterInfoDatas(chapterId, out var chapterInfoDatas))
		{
			if(chapterInfoDatas.TryGetValue(waveId, out data))
			{
				return true;
			}
		}

		Debug.LogWarning($"Error TryGetChapterInfoData chapterId:{chapterId} waveId:{waveId}");
		return false;
	}

	public bool TryGetChapterInfoDatas(int chapterId, out Dictionary<int, ChapterInfoData> datas)
	{
		if (AllChapterInfoDatas.TryGetValue(chapterId, out datas))
		{
			return true;
		}

		Debug.LogWarning($"Error TryGetChapterInfoDatas chapterId:{chapterId}");
		return false;
	}

	public void Initialize()
	{
		AllChapterInfoDatas = new Dictionary<int, Dictionary<int, ChapterInfoData>>();

		GenerateData();
		//InitializeEnemyIds();
		//InitializeFrontIds();
		//InitializeBackIds();
	}

	private void GenerateData()
	{
		ChapterInfoDatas = new Dictionary<int, ChapterInfoData>();

		foreach (var chapterInfoExcelData in ChapterInfoExcelDatas)
		{
			ChapterInfoData chapterInfoData = new ChapterInfoData(chapterInfoExcelData);
			ChapterInfoDatas.Add(chapterInfoData.WaveId, chapterInfoData);
		}

		AllChapterInfoDatas.Add(ChapterInfoExcelDatas[0].ChapterId, ChapterInfoDatas);
	}
    //private void InitializeEnemyIds()
    //{
    //	foreach (var ChapterInfoData in ChapterInfoDatas)
    //	{
    //		ChapterInfoData.EnemyIdList.Clear();

    //		string[] enemyIdsStr = ChapterInfoData.EnemyIds.Split(',');
    //		foreach (var enemyId in enemyIdsStr)
    //		{
    //			ChapterInfoData.EnemyIdList.Add(enemyId[0] - '0');
    //		}
    //	}
    //}

    //private void InitializeFrontIds()
    //{
    //	foreach (var ChapterInfoData in ChapterInfoDatas)
    //	{
    //		ChapterInfoData.FrontIdList.Clear();

    //		if (ChapterInfoData.FrontIds == "")
    //			continue;

    //		string[] frontIdsStr = ChapterInfoData.FrontIds.Split(',');
    //		foreach (var frontId in frontIdsStr)
    //		{
    //			ChapterInfoData.FrontIdList.Add(frontId[0] - '0');
    //		}
    //	}
    //}

    //private void InitializeBackIds()
    //{
    //	foreach (var ChapterInfoData in ChapterInfoDatas)
    //	{
    //		ChapterInfoData.BackIdList.Clear();

    //		if (ChapterInfoData.BackIds == "")
    //			continue;

    //		string[] backIdsStr = ChapterInfoData.BackIds.Split(',');
    //		foreach (var backId in backIdsStr)
    //		{
    //			ChapterInfoData.BackIdList.Add(backId[0] - '0');
    //		}
    //	}
    //}

    public void DataValidate()
    {
		// 아이디가 고유한 값을 가지는지 확인.
		List<int> idList = new List<int>();

        foreach (var chapterInfoExcelData in ChapterInfoExcelDatas)
        {
            if (idList.Contains(chapterInfoExcelData.WaveId))
            {
                Debug.Log($"ChapterInfo 엑셀 데이터 WaveId : {chapterInfoExcelData.WaveId}값이 겹칩니다.");
            }
			else
			{
				idList.Add(chapterInfoExcelData.WaveId);
			}
		}
    }
}
