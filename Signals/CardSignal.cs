using Godot;
using PixelUno.Enums;
using PixelUno.ViewModels;

namespace PixelUno.Signals;

public partial class CardSignal : GodotObject
{
    public required CardColor Color { get; set; }
    public required CardSymbol Symbol { get; set; }

    public static implicit operator CardSignal(CardViewModel card)
    {
        return new CardSignal()
        {
            Color = card.Color,
            Symbol = card.Symbol
        };
    }
}