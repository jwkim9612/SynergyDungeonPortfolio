using UnityEngine;

public class InGamePlayerState : MonoBehaviour
{
    public delegate void OnCoinChangedDelegate();
    public delegate void OnExpChangedDelegate();
    public delegate void OnLevelUpDelegate();
    public OnCoinChangedDelegate OnCoinChanged { get; set; }
    public OnExpChangedDelegate OnExpChanged { get; set; }
    public OnLevelUpDelegate OnLevelUp { get; set; }
    
    public int coin { get; set; }
    public int level;
    public int exp;
    public int SatisfyExp;
    public int numOfCanPlacedInBattleArea;

    private InGameExpDataSheet inGameExpDataSheet;

    public void Initialize()
    {
        inGameExpDataSheet = DataBase.Instance.inGameExpDataSheet;

        if (SaveManager.Instance.IsLoadedData)
        {
            InitializeByInGameSaveData();
        }
        else
        {
            InitializeByDefault();
        }

        InGameManager.instance.gameState.OnPrepare += IncreaseCoinByPrepare;
        InGameManager.instance.gameState.OnComplete += IncreaseExpByBattleWin;
    }

    // 인게임 세이브데이터로 초기화
    private void InitializeByInGameSaveData()
    {
        coin = SaveManager.Instance.inGameSaveData.Coin;
        level = SaveManager.Instance.inGameSaveData.Level;

        if (inGameExpDataSheet.TryGetSatisfyExp(level, out var satisfyExp))
        {
            SatisfyExp = satisfyExp;
        }

        exp = SaveManager.Instance.inGameSaveData.Exp;
        numOfCanPlacedInBattleArea = level;
    }

    // 기본 값으로 초기화
    private void InitializeByDefault()
    {
        coin = InGameService.DEFAULT_COIN;
        level = InGameService.DEFAULT_LEVEL;

        if (inGameExpDataSheet.TryGetSatisfyExp(level, out var satisfyExp))
        {
            SatisfyExp = satisfyExp;
        }

        exp = InGameService.DEFAULT_EXP;
        numOfCanPlacedInBattleArea = InGameService.DEFAULT_NUM_OF_CAN_PLACED_IN_BATTLEAREA;
    }

    // 코인 증가
    public void IncreaseCoin(int increaseValue)
    {
        coin = Mathf.Clamp(coin + increaseValue, InGameService.MIN_NUMBER_OF_COIN, InGameService.MAX_NUMBER_OF_COIN);

        // OnCoinChanged가 비어있는지 확인 후 실행
        OnCoinChanged?.Invoke();
    }

    // 준비 상태에 의한 코인 증가
    public void IncreaseCoinByPrepare()
    {
        int currentChapter = StageManager.Instance.currentChapter;
        int currentWave = StageManager.Instance.currentWave;

        var chapterInfoDataSheet = DataBase.Instance.chapterInfoDataSheet;
        if(chapterInfoDataSheet.TryGetChapterInfoGoldAmount(currentChapter, currentWave, out var goldAmount))
        {
            IncreaseCoin(goldAmount);
        }
    }

    // 코인 사용
    public void UseCoin(int usedValue)
    {
        coin = Mathf.Clamp(coin - usedValue, InGameService.MIN_NUMBER_OF_COIN, coin);
        OnCoinChanged();
    }

    // 경험치 증가
    public void IncreaseExp(int increaseValue)
    {
        if (IsMaxLevel())
            return;

        exp += increaseValue;

        if(exp >= SatisfyExp)
        {
            level += 1;
            exp -= SatisfyExp;
            
            if (inGameExpDataSheet.TryGetSatisfyExp(level, out var satisfyExp))
            {
                SatisfyExp = satisfyExp;
            }

            IncreaseNumOfCanPlacedInBattleArea();
            OnLevelUp();
        }

        OnExpChanged();
    }

    // 경험치 구매에 의한 경험치 증가
    public void IncreaseExpByAddExp()
    {
        UseCoin(InGameService.CAN_BUY_EXP);
        IncreaseExp(InGameService.CAN_BUY_EXP);
    }

    // 배틀 승리에 의한 경험치 증가
    public void IncreaseExpByBattleWin()
    {
        int currentChapter = StageManager.Instance.currentChapter;
        int currentWave = StageManager.Instance.currentWave;

        var chapterInfoDateSheet = DataBase.Instance.chapterInfoDataSheet;
        if (chapterInfoDateSheet.TryGetChapterInfoExpAmount(currentChapter, currentWave, out var expAmount))
        {
            IncreaseExp(expAmount);
        }
    }

    // 배틀공간에 배치할 수 있는 수를 증가
    public void IncreaseNumOfCanPlacedInBattleArea()
    {
        Mathf.Clamp(++numOfCanPlacedInBattleArea, InGameService.MIN_NUMBER_OF_CAN_PLACED, InGameService.MAX_NUMBER_OF_CAN_PLACED);
    }

    // 현재 최고레벨인지를 반환
    public bool IsMaxLevel()
    {
        int satisfyExp = InGameManager.instance.playerState.SatisfyExp;
        int maxLevelOfSatisfyExp = PlayerService.MAX_LEVEL_OF_SATISFY_EXP;

        return satisfyExp == maxLevelOfSatisfyExp ? true : false;
    }
}
