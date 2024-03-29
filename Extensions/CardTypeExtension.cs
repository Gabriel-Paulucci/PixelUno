using PixelUno.Enums;

namespace PixelUno.Extensions;

public static class CardTypeExtension
{
    public static bool IsBlock(this CardType cardType)
    {
        return cardType switch
        {
            CardType.BlueBlock or CardType.YellowBlock or CardType.RedBlock or CardType.GreenBlock => true,
            _ => false
        };
    }

    public static bool IsBlue(this CardType cardType)
    {
        return cardType switch
        {
            CardType.Blue0 or CardType.Blue1 or CardType.Blue2 or CardType.Blue3 or CardType.Blue4 or CardType.Blue5
                or CardType.Blue6 or CardType.Blue7 or CardType.Blue8 or CardType.Blue9 or CardType.BlueBlock
                or CardType.BlueReverse or CardType.BluePlus2 => true,
            _ => false
        };
    }

    public static bool IsYellow(this CardType cardType)
    {
        return cardType switch
        {
            CardType.Yellow0 or CardType.Yellow1 or CardType.Yellow2 or CardType.Yellow3 or CardType.Yellow4
                or CardType.Yellow5 or CardType.Yellow6 or CardType.Yellow7 or CardType.Yellow8 or CardType.Yellow9
                or CardType.YellowBlock or CardType.YellowReverse or CardType.YellowPlus2 => true,
            _ => false
        };
    }

    public static bool IsRed(this CardType cardType)
    {
        return cardType switch
        {
            CardType.Red0 or CardType.Red1 or CardType.Red2 or CardType.Red3 or CardType.Red4 or CardType.Red5
                or CardType.Red6 or CardType.Red7 or CardType.Red8 or CardType.Red9 or CardType.RedBlock
                or CardType.RedReverse or CardType.RedPlus2 => true,
            _ => false
        };
    }

    public static bool IsGreen(this CardType cardType)
    {
        return cardType switch
        {
            CardType.Green0 or CardType.Green1 or CardType.Green2 or CardType.Green3 or CardType.Green4
                or CardType.Green5 or CardType.Green6 or CardType.Green7 or CardType.Green8 or CardType.Green9
                or CardType.GreenBlock or CardType.GreenReverse or CardType.GreenPlus2 => true,
            _ => false
        };
    }

    public static bool IsWild(this CardType cardType)
    {
        return cardType switch
        {
            CardType.WildColor or CardType.WildPlus4 => true,
            _ => false
        };
    }
}