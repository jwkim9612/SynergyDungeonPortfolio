using UnityEngine;
using UnityEngine.UI;

public class UIArtifactPiece : MonoBehaviour
{
    [SerializeField] private Button showInfoButton;
    [SerializeField] private Image artifactImage;
    public int id;
    public bool isOwned;

    public void SetArtifactPiece(int id, bool isOwned)
    {
        this.id = id;
        this.isOwned = isOwned;

        var artifactPieceDataSheet = DataBase.Instance.artifactPieceDataSheet;

        if(isOwned)
        {
            if (artifactPieceDataSheet.TryGetArtifactPieceOnImage(id, out var image))
            {
                artifactImage.sprite = image;
            }
        }
        else
        {
            if (artifactPieceDataSheet.TryGetArtifactPieceOffImage(id, out var image))
            {
                artifactImage.sprite = image;
            }
        }

        SetShowInfoButton();
    }

    private void SetShowInfoButton()
    {
        showInfoButton.onClick.RemoveAllListeners();
        showInfoButton.onClick.AddListener(() =>
        {
            var uiArtifactPieceInfo = MainManager.instance.backCanvas.uiMainMenu.uiCharacterAndArtifact.uiArtifactPieceInfo;
            uiArtifactPieceInfo.SetArtifactPieceInfo(id, isOwned);

            UIManager.Instance.ShowNew(uiArtifactPieceInfo);
        });
    }

    public void SetOn()
    {
        isOwned = true;

        var artifactPieceDataSheet = DataBase.Instance.artifactPieceDataSheet;
        if (artifactPieceDataSheet.TryGetArtifactPieceOnImage(id, out var image))
        {
            artifactImage.sprite = image;
        }
    }

    public void OnHide()
    {
        gameObject.SetActive(false);
    }
}
