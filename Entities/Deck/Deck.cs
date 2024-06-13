using Godot;
using Godot.Collections;
using PixelUno.Entities.Card;

namespace PixelUno.Entities.Deck;

public partial class Deck : Node2D
{
    [Export] public required Area2D Area { get; set; }
    [Export] public required Array<CardType> Cards { get; set; }
    [Export] public required AnimationPlayer Animation { get; set; }

    private bool _hovered;
    
    [Signal]
    public delegate void BuyCardEventHandler();

    public override void _Ready()
    {
        Area.MouseExited += AreaOnMouseExited;
        Area.MouseEntered += AreaOnMouseEntered;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("click") && _hovered)
        {
            EmitSignal(SignalName.BuyCard);
        }
    }

    private void AreaOnMouseEntered()
    {
        _hovered = true;
        Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
        Animation.Play("hover");
    }

    private void AreaOnMouseExited()
    {
        _hovered = false;
        Input.SetDefaultCursorShape();
        Animation.PlayBackwards("hover");
    }
}