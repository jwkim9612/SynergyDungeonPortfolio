using UnityEngine;

public class UIBattleArea : MonoBehaviour
{
    [SerializeField] private UICharacterArea uiCharacterArea = null;
    [SerializeField] private UIEnemyArea uiEnemyArea = null;
    public BattleStatus battleStatus = null;

    private bool isFirstTime;

    public void Initialize()
    {
        // uiCharacterArea는 DraggableCentral에서 초기화해줌.
        uiEnemyArea.Initialize();

        isFirstTime = true;

        InGameManager.instance.gameState.OnBattle += BattleStart;
        InGameManager.instance.gameState.OnBattle += SpaceExpansion;
        InGameManager.instance.gameState.OnPrepare += SpaceReduction;
    }

    private void SpaceExpansion()
    {
        RectTransform rec = transform as RectTransform;
        rec.Translate(new Vector3(0.0f, -InGameService.DISTANCE_TO_MOVE_AT_START_OF_BATTLE, 0.0f));
    }

    private void SpaceReduction()
    {
        if (isFirstTime)
        {
            isFirstTime = false;
            return;
        }

        RectTransform rec = transform as RectTransform;
        rec.Translate(new Vector3(0.0f, InGameService.DISTANCE_TO_MOVE_AT_START_OF_BATTLE, 0.0f));
    }

    private void BattleStart()
    {
        battleStatus.characters = uiCharacterArea.GetCharacterList();
        battleStatus.enemies = uiEnemyArea.GetEnemyList();
        battleStatus.BattleStart();
    }
}
