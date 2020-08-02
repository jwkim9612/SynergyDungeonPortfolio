public class ArtifactService
{
    public static int NUMBER_OF_ALL_ARTIFACTS;

    public static void Initialize()
    {
        InitializeNumberOfAllArtifacts();
    }

    private static void InitializeNumberOfAllArtifacts()
    {
        var ArtifactPieceExcelDatas = DataBase.Instance.artifactPieceDataSheet.ArtifactPieceExcelDatas;

        NUMBER_OF_ALL_ARTIFACTS = ArtifactPieceExcelDatas.Count;
    }
}
