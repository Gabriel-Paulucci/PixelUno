using Godot;
using PixelUno.Entities.Card;
using PixelUno.Enums;

namespace PixelUno.ViewModels;

public class CardViewModel
{
    public required string Id { get; set; }
    public required CardColor Color { get; set; }
    public required CardSymbol Symbol { get; set; }

    public static implicit operator CardViewModel(Card card)
    {
        return new CardViewModel()
        {
            Id = card.Type.Id,
            Color = card.Type.Color,
            Symbol = card.Type.Symbol
        };
    }
    
    public static implicit operator CardType(CardViewModel card)
    {
        return new CardType()
        {
            Id = card.Id,
            Color = card.Color,
            Symbol = card.Symbol
        };
    }
}