using Godot;
using PixelUno.Enums;

namespace PixelUno.Entities.Card;

[GlobalClass]
public partial class CardType : Resource
{
    [Export] public CardSymbol Symbol { get; set; }
    [Export] public CardColor Color { get; set; }
    public required string Id { get; set; }

    public int Frame
    {
        get
        {
            var symbolFrame = Symbol switch
            {
                CardSymbol.Zero => 0,
                CardSymbol.One => 1,
                CardSymbol.Two => 2,
                CardSymbol.Three => 3,
                CardSymbol.Four => 4,
                CardSymbol.Five => 5,
                CardSymbol.Six => 6,
                CardSymbol.Seven => 7,
                CardSymbol.Eight => 8,
                CardSymbol.Nine => 9,
                CardSymbol.Block => 10,
                CardSymbol.Reverse => 11,
                CardSymbol.Plus2 => 12,
                CardSymbol.Color => 13,
                CardSymbol.Plus4 => 14,
                _ => 0
            };

            return symbolFrame - Color switch
            {
                CardColor.Blue => 0,
                CardColor.Yellow => 0,
                CardColor.Red => 0,
                CardColor.Green => 0,
                CardColor.Wild => 13,
                _ => 0
            };
        }
    }

    public int Order
    {
        get
        {
            var symbolFrame = Symbol switch
            {
                CardSymbol.Zero => 0,
                CardSymbol.One => 1,
                CardSymbol.Two => 2,
                CardSymbol.Three => 3,
                CardSymbol.Four => 4,
                CardSymbol.Five => 5,
                CardSymbol.Six => 6,
                CardSymbol.Seven => 7,
                CardSymbol.Eight => 8,
                CardSymbol.Nine => 9,
                CardSymbol.Block => 10,
                CardSymbol.Reverse => 11,
                CardSymbol.Plus2 => 12,
                CardSymbol.Color => 13,
                CardSymbol.Plus4 => 14,
                _ => 0
            };

            return symbolFrame + Color switch
            {
                CardColor.Blue => 0,
                CardColor.Yellow => 15,
                CardColor.Red => 30,
                CardColor.Green => 45,
                CardColor.Wild => 60,
                _ => 0
            };
        }
    }
}