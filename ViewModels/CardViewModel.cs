using Godot;
using PixelUno.Enums;

namespace PixelUno.ViewModels;

public partial class CardViewModel : GodotObject
{
    public required CardColor Color { get; set; }
    public required CardSymbol Symbol { get; set; }
}