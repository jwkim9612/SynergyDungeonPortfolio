using System;

[Serializable]
public class PlayerData
{
    public int Level { get; set; }
    public int Gold { get; set; }
    public int Diamond { get; set; }
    public int PlayableStage { get; set; }
    public int TopWave { get; set; }

    public void IncreasePlayableStage()
    {
        ++PlayableStage;
    }

    public void InitializeTopWave()
    {
        TopWave = 0;
    }
}