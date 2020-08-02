﻿using System.Collections.Generic;

public class AbilityEffect
{
    public Ability ability;
    public int remainingTurn;
    private WayOfCalculate wayOfCalculate;
    public int effectValue;
    public string description;

    public AbilityEffectData abilityEffectData;
    public List<int> dataIdList;

    public AbilityEffect(PotionData potionData)
    {
        ability = potionData.Ability;
        remainingTurn = -1;
        wayOfCalculate = potionData.WayOfIncrease;
        effectValue = potionData.IncreaseValue;
        abilityEffectData = AbilityEffectData.Potion;
        description = potionData.Description;

        dataIdList = new List<int>(); 
        dataIdList.Add(potionData.Id);
    }

    public AbilityEffect(ScenarioData scenarioData)
    {
        ability = scenarioData.ApplyAbility;
        remainingTurn = scenarioData.ApplyTurn;
        wayOfCalculate = WayOfCalculate.Percentage;
        effectValue = scenarioData.ApplyPercentage;
        abilityEffectData = AbilityEffectData.Scenario;
        description = scenarioData.RewardDescription;

        dataIdList = new List<int>();
        dataIdList.Add(scenarioData.ChapterId);
        dataIdList.Add(scenarioData.WaveId);
        dataIdList.Add(scenarioData.ScenarioId);
    }

    // 능력치 효과의 남은 턴을 하나 감소
    public void DecreaseRemainingTurn()
    {
        if (remainingTurn != AbilityEffectService.NUM_OF_INFINITY)
            --remainingTurn;
    }

    // 능력치 효과의 남은 턴이 없으면 true 있으면 false를 반환
    public bool IsOver()
    {
        if (remainingTurn == 0)
            return true;

        return false;
    }
}
