using System;

[Serializable]
public class ScenarioExcelData
{
    public int ChapterId;
    public int StageId;
    public int WaveId;
    public int ScenarioId;
    public string ScenarioType;
    public int ScenarioProbability;
    public string Description;
    public RewardCurrency CurrencyType;
    public int Amount;
    public string RewardDescription;
    public Ability ApplyAbility;
    public int ApplyPercentage;
    public int ApplyTurn;
    public string ImagePath;
}
