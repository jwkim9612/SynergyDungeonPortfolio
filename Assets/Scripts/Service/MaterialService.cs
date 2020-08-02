using UnityEngine;

public class MaterialService : MonoBehaviour
{
    public const string RED_MATERIAL_PATH = "Material/Red";
    public const string WHITE_MATERIAL_PATH = "Material/White";

    public static Material redMaterial = Resources.Load<Material>(RED_MATERIAL_PATH);
    public static Material whiteMaterial = Resources.Load<Material>(WHITE_MATERIAL_PATH);
}
