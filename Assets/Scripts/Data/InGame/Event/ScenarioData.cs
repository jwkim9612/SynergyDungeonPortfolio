using UnityEngine;

public class ScenarioData
{
    public ScenarioData(ScenarioExcelData scenarioExcelData)
    {
        ChapterId = scenarioExcelData.ChapterId;
        StageId = scenarioExcelData.StageId;
        WaveId = scenarioExcelData.WaveId;
        ScenarioId = scenarioExcelData.ScenarioId;
        ScenarioType = scenarioExcelData.ScenarioType;
        ScenarioProbability = scenarioExcelData.ScenarioProbability;
        Description = scenarioExcelData.Description;
        CurrencyType = scenarioExcelData.CurrencyType;
        Amount = scenarioExcelData.Amount;
        RewardDescription = scenarioExcelData.RewardDescription;
        ApplyAbility = scenarioExcelData.ApplyAbility;
        ApplyPercentage = scenarioExcelData.ApplyPercentage;
        ApplyTurn = scenarioExcelData.ApplyTurn;

        Image = Resources.Load<Sprite>(scenarioExcelData.ImagePath);
    }

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
    public Sprite Image;
}
