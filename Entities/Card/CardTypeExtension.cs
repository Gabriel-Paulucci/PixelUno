namespace PixelUno.Entities.Card;

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
}