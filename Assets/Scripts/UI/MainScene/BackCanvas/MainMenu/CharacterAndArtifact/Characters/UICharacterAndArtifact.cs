using UnityEngine;

public class UICharacterAndArtifact : MonoBehaviour
{
    public UIDetailedSettings uiDetailedSettings;
    public UICharacterList uiCharacterList;
    public UIArtifactPieceStatus uiArtifactPieceStatus;
    public UIArtifactPieceList uiArtifactPieceList;
    public UIArtifactPieceInfo uiArtifactPieceInfo;

    public void Initialize()
    {
        uiDetailedSettings.Initialize();
        uiCharacterList.Initialize();
        uiArtifactPieceStatus.Initialize();
        uiArtifactPieceList.Initialize();
    }
}
