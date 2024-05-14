using Godot;
using PixelUno.Enums;

namespace PixelUno.Gui;

public partial class CardColorSelect : Panel
{
    [Export] public required BaseButton BlueButton { get; set; }
    [Export] public required BaseButton YellowButton { get; set; }
    [Export] public required BaseButton RedButton { get; set; }
    [Export] public required BaseButton GreenButton { get; set; }

    [Signal]
    public delegate void SelectColorEventHandler(CardColor color);

    public override void _Ready()
    {
        BlueButton.ButtonUp += () => ButtonOnPressed(CardColor.Blue);
        YellowButton.ButtonUp += () => ButtonOnPressed(CardColor.Yellow);
        RedButton.ButtonUp += () => ButtonOnPressed(CardColor.Red);
        GreenButton.ButtonUp += () => ButtonOnPressed(CardColor.Green);
    }

    private void ButtonOnPressed(CardColor color)
    {
        EmitSignal(SignalName.SelectColor, (int)color);
    }
}