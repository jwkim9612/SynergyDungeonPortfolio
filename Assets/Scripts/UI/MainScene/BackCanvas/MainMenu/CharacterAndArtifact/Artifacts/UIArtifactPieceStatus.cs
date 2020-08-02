using UnityEngine;
using UnityEngine.UI;

public class UIArtifactPieceStatus : MonoBehaviour
{
    [SerializeField] private Text artifactPieceNumText = null;
    [SerializeField] private Text unlockedArtifactPieceNumText = null;

    public void Initialize()
    {
        SetArtifactsNumText();
        UpdateUnlockedArtifactPieceNumText();

        ArtifactManager.Instance.OnArtifactPieceChanged += UpdateUnlockedArtifactPieceNumText;
    }

    private void SetArtifactsNumText()
    {
        artifactPieceNumText.text = $"총 아티팩트 조각 갯수 : {ArtifactService.NUMBER_OF_ALL_ARTIFACTS}";
    }

    public void UpdateUnlockedArtifactPieceNumText()
    {
        var unlockedArtifactPieceNum = ArtifactManager.Instance.ownedPieceIdList.Count;
        unlockedArtifactPieceNumText.text = $"해금된 아티팩트 조각 갯수 : {unlockedArtifactPieceNum}";
    }

    private void OnDestroy()
    {
        if (ArtifactManager.IsAlive)
        {
            ArtifactManager.Instance.OnArtifactPieceChanged -= UpdateUnlockedArtifactPieceNumText;
        }
    }
}
