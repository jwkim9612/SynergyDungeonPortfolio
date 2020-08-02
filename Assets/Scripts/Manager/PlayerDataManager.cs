using GameSparks.Api.Requests;
using GameSparks.Core;
using UnityEngine;

public class PlayerDataManager : MonoSingleton<PlayerDataManager>
{
    public delegate void OnGoldChangedDelegate();
    public delegate void OnDiamondChangedDelegate();
    public OnGoldChangedDelegate OnGoldChanged { get; set; }
    public OnDiamondChangedDelegate OnDiamondChanged { get; set; }

    // 플레이어의 데이터를 관리해주는 매니저
    public PlayerData playerData;

    // 임시 생성, 플로우가 정해지면 제거
    public void Initialize()
    {
        //playerData = new PlayerData
        //{
        //    Gold = 0,
        //    Diamond = 0,
        //    Level = 1,
        //    PlayableStage = 1,
        //};
    }

    // 게임 진입 이후 로드
    // 이후에는 재화가 소모되거나 스테이지에 입장할 때 확인하기 위해 로드하여 그 값으로 확인.
    public void LoadPlayerData()
    {
        new LogEventRequest()
            .SetEventKey("LoadPlayerData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    bool result = (bool)response.ScriptData.GetBoolean("Result");

                    if (result)
                    {
                        GSData scriptData = response.ScriptData.GetGSData("PlayerData");

                        var data = new PlayerData
                        {
                            Level = (int)scriptData.GetInt("PlayerLevel"),
                            Diamond = (int)scriptData.GetInt("PlayerDiamond"),
                            Gold = (int)scriptData.GetInt("PlayerGold"),
                            PlayableStage = (int)scriptData.GetInt("PlayerPlayableStage"),
                            TopWave = (int)scriptData.GetInt("PlayerTopWave")
                        };

                        playerData = data;
                        Debug.Log("Player Data Load Successfully !");
                        Debug.Log($"Level : {playerData.Level}, Gold : {playerData.Gold}, Diamond : {playerData.Diamond}, PlayableStage : {playerData.PlayableStage}, PlayerTopWave : {playerData.TopWave}");
                   
                        if(OnGoldChanged != null)
                        {
                            OnGoldChanged();
                        }

                        if (OnDiamondChanged != null)
                        {
                            OnDiamondChanged();
                        }
                    }
                    else
                    {
                        Debug.Log("저장된 플레이어 데이터가 없어 새로 생성합니다.");
                        InitializePlayerData();
                    }

                }
                else
                {
                    Debug.Log("Error Player Data Load");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    // 최초 게임 진입 시에 초기 값으로 해서 저장
    // 이후에는 관련된 프로퍼티 변경할 때마다 저장
    public void SavePlayerData()
    {
        new LogEventRequest()
            .SetEventKey("SavePlayerData")
            .SetEventAttribute("Level", playerData.Level)
            .SetEventAttribute("Diamond", playerData.Diamond)
            .SetEventAttribute("Gold", playerData.Gold)
            .SetEventAttribute("PlayableStage", playerData.PlayableStage)
            .SetEventAttribute("TopWave", playerData.TopWave)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success Player Data Save !");
                }
                else
                {
                    Debug.Log("Error Data Save !");
                }
            });
    }

    public void SavePlayerDataForCheat()
    {
        new LogEventRequest()
            .SetEventKey("SavePlayerData")
            .SetEventAttribute("Level", playerData.Level)
            .SetEventAttribute("Diamond", playerData.Diamond)
            .SetEventAttribute("Gold", playerData.Gold)
            .SetEventAttribute("PlayableStage", playerData.PlayableStage)
            .SetEventAttribute("TopWave", playerData.TopWave)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success Player Data Save !");
                    LoadPlayerData();
                }
                else
                {
                    Debug.Log("Error Data Save !");
                }
            });
    }

    public void InitializePlayerData()
    {
        new LogEventRequest()
            .SetEventKey("InitializePlayerData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success Initialize PlayerData !");
                    LoadPlayerData();
                }
                else
                {
                    Debug.Log("Error Initialize PlayerData !");
                }
            });
    }

    public void InitializePlayerDataAndLoadMainScene()
    {
        new LogEventRequest()
            .SetEventKey("InitializePlayerData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success Initialize InitializePlayerDataAndCheckHasInGameData !");
                    LoadPlayerData();
                    //SaveManager.Instance.CheckHasInGameData();
                    GameManager.instance.LoadGameAndLoadMainScene();
                }
                else
                {
                    Debug.Log("Error Initialize PlayerData !");
                }
            });
    }
}
