using Enums;

public static class StringHelper 
{
    public static string GetCardType(CardType type)
    {

        string typeAsString = "No Type";
        switch (type)
        {
            case CardType.NORMAL:
                typeAsString = "Normal";
                break;
            case CardType.CHAMPION:
                typeAsString = "Champion";
                break;
            case CardType.TERRAIN:
                typeAsString = "Terrain";
                break;
            case CardType.SPELL:
                typeAsString = "Spell";
                break;
            default:
                break;
        }

        return typeAsString;
    }
}
