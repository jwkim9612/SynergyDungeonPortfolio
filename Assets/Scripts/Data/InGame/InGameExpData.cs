public class InGameExpData
{
    public InGameExpData(InGameExpExcelData inGameExpExcelData)
    {
        Level = inGameExpExcelData.Level;
        SatisfyExp = inGameExpExcelData.SatisfyExp;
        CumulativeExp = inGameExpExcelData.CumulativeExp;
    }

    public InGameExpData(InGameExpData inGameExpData)
    {
        Level = inGameExpData.Level;
        SatisfyExp = inGameExpData.SatisfyExp;
        CumulativeExp = inGameExpData.CumulativeExp;
    }

    public int Level;
    public int SatisfyExp;
    public int CumulativeExp;
}
