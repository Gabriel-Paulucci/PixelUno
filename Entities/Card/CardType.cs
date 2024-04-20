using Godot;
using PixelUno.Enums;

namespace PixelUno.Entities.Card;

[GlobalClass]
public partial class CardType : Resource
{
    [Export] public CardSymbol Symbol { get; set; }
    [Export] public CardColor Color { get; set; }
    [Export] public int Frame { get; set; }
}