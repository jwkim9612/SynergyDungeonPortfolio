using UnityEngine;

public class InGameBackCanvas : MonoBehaviour
{
    public UIInGameMainMenu uiMainMenu;
    public UIBottomMenu uiBottomMenu;

    public void Initialize()
    {
        uiMainMenu.Initialize();
        uiBottomMenu.Initialize();
    }
}
