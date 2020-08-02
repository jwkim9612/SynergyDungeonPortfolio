using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnemyArea : MonoBehaviour
{
    public UIEnemyPlacementArea frontArea;
    public UIEnemyPlacementArea backArea;

    public void Initialize()
    {
        InGameManager.instance.gameState.OnBattle += InitializeEnemyPositions;
        InGameManager.instance.gameState.OnPrepare += CreateEnemies;

        frontArea.Initialize();
        backArea.Initialize();
    }

    // 2~3프레임 쉬지않으면 이상한 위치에 생성되어 코루틴을 사용
    public void CreateEnemies()
    {
        StartCoroutine(Co_CreateEnemies());
    }

    IEnumerator Co_CreateEnemies()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        var currentWaveData = StageManager.Instance.currentChapterData.chapterInfoDatas[StageManager.Instance.currentWave];
        var frontIdList = currentWaveData.FrontIdList;
        var backIdList = currentWaveData.BackIdList;

        var enemyDataSheet = DataBase.Instance.enemyDataSheet;
        if (enemyDataSheet == null)
        {
            Debug.LogError("Error enemyDataSheet is null");
            yield break;
        }

        int currentEnemyIndex = 0;

        if (frontIdList != null)
        {
            for (int frontIndex = 0; frontIndex < frontIdList.Count; ++frontIndex)
            {
                int enemyId = currentWaveData.EnemyIdList[currentEnemyIndex];
                if (enemyDataSheet.TryGetEnemyData(enemyId, out var enemyData))
                {
                    frontArea.uiEnemies[currentWaveData.FrontIdList[frontIndex]].SetEnemy(enemyData);
                }

                ++currentEnemyIndex;
            }
        }

        if (backIdList != null)
        {
            for (int backIndex = 0; backIndex < backIdList.Count; ++backIndex)
            {
                int enemyId = currentWaveData.EnemyIdList[currentEnemyIndex];
                if (enemyDataSheet.TryGetEnemyData(enemyId, out var enemyData))
                {
                    backArea.uiEnemies[currentWaveData.BackIdList[backIndex]].SetEnemy(enemyData);
                }

                ++currentEnemyIndex;
            }
        }
    }

    public List<Enemy> GetEnemyList()
    {
        List<Enemy> enemies = new List<Enemy>();
        enemies.AddRange(backArea.GetEnemyList());
        enemies.AddRange(frontArea.GetEnemyList());

        return enemies;
    }

    public void InitializeEnemyPositions()
    {
        backArea.InitializeEnemyPositions();
        frontArea.InitializeEnemyPositions();
    }
}
