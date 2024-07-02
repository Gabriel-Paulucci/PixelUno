using Godot;
using Microsoft.VisualBasic;
using PixelUno.Entities.Card;
using PixelUno.Enums;
using PixelUno.ViewModels;

namespace PixelUno.Signals;

public partial class CardSignal : GodotObject
{
    public required string Id { get; set; }
    public required CardColor Color { get; set; }
    public required CardSymbol Symbol { get; set; }

    public static implicit operator CardSignal(CardViewModel card)
    {
        return new CardSignal()
        {
            Id = card.Id,
            Color = card.Color,
            Symbol = card.Symbol
        };
    }

    public static implicit operator CardType(CardSignal card)
    {
        return new CardType()
        {
            Id = card.Id,
            Color = card.Color,
            Symbol = card.Symbol
        };
    }
}