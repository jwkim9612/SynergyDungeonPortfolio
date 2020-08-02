using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public SimpleScrollSnap simpleScrollSnap;

    public UICharacterAndArtifact uiCharacterAndArtifact;
    public UIIllustratedBook uiIllustratedBook;
    public UIBattlefield uiBattlefield;
    public UIStore uiStore;

    public void Initialize()
    {
        uiCharacterAndArtifact.Initialize();
        uiIllustratedBook.Initialize();
        uiStore.Initialize();
        uiBattlefield.Initialize();
    }
}
