using GameSparks.Api.Requests;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactManager : MonoSingleton<ArtifactManager>
{
    public delegate void OnArtifactPieceChangedDelegate();
    public delegate void OnAddArtifactPieceDelegate(int artifactPieceId);
    public OnArtifactPieceChangedDelegate OnArtifactPieceChanged { get; set; }
    public OnAddArtifactPieceDelegate OnAddArtifactPiece { get; set; }
    public List<int> ownedPieceIdList { get; set; }
    public int pieceTotalNumber { get; set; }

    public void Initialize()
    {
        InitializePieceTotalNumber();
        LoadOwnedArtifactPieceData();
    }

    private void InitializePieceTotalNumber()
    {
        var artifactPieceDataSheet = DataBase.Instance.artifactPieceDataSheet;
        if (artifactPieceDataSheet.TryGetArtifactPieceTotalNumber(out var totalNumber))
        {
            pieceTotalNumber = totalNumber;
        }
    }

    public void AddArtifactPiece(int artifactPieceId)
    {
        new LogEventRequest()
            .SetEventKey("AddArtifactPiece")
            .SetEventAttribute("ArtifactPieceId", artifactPieceId)
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    AddIdToOwnedPieceIdList(artifactPieceId);
                    Debug.Log("Success Add ArtifactPiece!");
                }
                else
                {
                    Debug.Log("Error Add ArtifactPiece!");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    private void AddIdToOwnedPieceIdList(int id)
    {
        if(ownedPieceIdList.Contains(id))
        {
            Debug.Log("Error 중복된 값이 들어왔습니다.");
            return;
        }

        ownedPieceIdList.Add(id);
        OnAddArtifactPiece(id);

        OnArtifactPieceChanged?.Invoke();
    }

    public void LoadOwnedArtifactPieceData()
    {
        new LogEventRequest()
            .SetEventKey("LoadOwnedArtifactPieceData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    bool result = (bool)response.ScriptData.GetBoolean("Result");
                    if (result)
                    {
                        ownedPieceIdList = new List<int>();
                        var ownedArtifactPieceData = response.ScriptData.GetIntList("OwnedArtifactPieceData");

                        foreach (var ownedArtifactId in ownedArtifactPieceData)
                        {
                            ownedPieceIdList.Add(ownedArtifactId);
                        }

                        OnArtifactPieceChanged?.Invoke();
                    }
                    else
                    {
                        InitializeOwnedArtifactPieceData();
                    }
                }
                else
                {
                    Debug.Log("Error LoadOwnedRunes");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public void InitializeOwnedArtifactPieceData()
    {
        new LogEventRequest()
            .SetEventKey("InitializeOwnedArtifactPieceData")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Success Initialize OwnedArtifactPieceData !");
                    LoadOwnedArtifactPieceData();
                }
                else
                {
                    Debug.Log("Error Initialize OwnedArtifactPieceData !");
                    Debug.Log(response.Errors.JSON);
                }
            });
    }

    public bool IsOwnedArtifact(int artifactId)
    {
        if(ownedPieceIdList.Contains(artifactId))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
