using Godot;
using PixelUno.Enums;

namespace PixelUno.ViewModels;

public class CardViewModel
{
    public required CardColor Color { get; set; }
    public required CardSymbol Symbol { get; set; }
}