using UnityEngine;
using UnityEngine.UI;

public class UIStart : MonoBehaviour
{
    [SerializeField] private Button startButton = null;

    public void Initialize()
    {
        SetStartButton();
    }

    private void SetStartButton()
    {
        startButton.onClick.AddListener(() => {

            var uiCharacterArea = InGameManager.instance.draggableCentral.uiCharacterArea;
            var uiCanNotStart = InGameManager.instance.frontCanvas.uiCanNotStart;

            if (uiCharacterArea.IsEmpty())
            {
                uiCanNotStart.PlayShowCanNotStart();
            }
            else
            {
                uiCanNotStart.HideCanNotStart();

                var gameState = InGameManager.instance.gameState;
                gameState.SetInGameState(InGameState.Battle);
            }
        });
    }
}
