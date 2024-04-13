using System;
using System.Linq;
using Godot;
using PixelUno.Enums;

namespace PixelUno.Entities.Card;

public partial class Card : Node2D
{
    [Export] public required CardType Type { get; set; }
    [Export] public required AnimatedSprite2D CardFront { get; set; }
    [Export] public required Area2D Area { get; set; }
    [Export] public required AnimationPlayer Animation { get; set; }
    [Export] public required bool Lock { get; set; }

    [Signal]
    public delegate void ClickEventHandler();

    private bool _hovered;

    public override void _Ready()
    {
        Area.MouseEntered += AreaOnMouseEntered;
        Area.MouseExited += AreaOnMouseExited;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("click") && _hovered)
        {
            EmitSignal(SignalName.Click);
        }
    }

    private void AreaOnMouseEntered()
    {
        if (Lock)
            return;
        
        _hovered = true;
        Animation.Play("hover");
        Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
    }

    private void AreaOnMouseExited()
    {
        if (Lock)
            return;

        _hovered = false;
        Animation.PlayBackwards("hover");
        Input.SetDefaultCursorShape();
    }

    public override void _EnterTree()
    {
        CardFront.Frame = (int)Type;
    }
}