using UnityEngine;

public class CardService
{
    public const int NUM_OF_CARDS = 29;
    public const int MAX_NUM_OF_CARDS_PER_CHARACTER = 9;
    public const int RELOAD_PRICE = 2;
    public const int ADDEXP_PRICE = 1;

    public const string DEFAULT_IMAGE_NAME = "Empty";

    public static Color GetColorByTier(Tier tier)
    {
        Color color = new Color();
        switch (tier)
        {
            case Tier.One:
                color = Color.gray;
                break;
            case Tier.Two:
                color = Color.green;
                break;
            case Tier.Three:
                color = Color.blue;
                break;
            case Tier.Four:
                color = Color.red;
                break;
            case Tier.Five:
                color = Color.yellow;
                break;
            default:
                Debug.Log("Error GetColorByTier");
                break;
        }
        return color;
    }

    public static int GetPriceByTier(Tier tier)
    {
        int price = 0;
        switch (tier)
        {
            case Tier.One:
                price = 1;
                break;
            case Tier.Two:
                price = 2;
                break;
            case Tier.Three:
                price = 3;
                break;
            case Tier.Four:
                price = 4;
                break;
            case Tier.Five:
                price = 5;
                break;
            default:
                Debug.Log("Error GetPriceByTier");
                break;
        }
        return price;
    }


}