using Godot;

namespace PixelUno.Entities.Card;

public partial class Card : Node2D
{
    [Export] public required CardType Type { get; set; }
    [Export] public required AnimatedSprite2D CardFront { get; set; }
    [Export] public required Area2D Area { get; set; }
    [Export] public required AnimationPlayer Animation { get; set; }
    [Export] public int Index { get; set; } = -1;
    
    [Signal]
    public delegate void ClickEventHandler(Card type);

    [Signal]
    public delegate void MouseEnteredEventHandler(Card zIndex);

    [Signal]
    public delegate void MouseExitedEventHandler(Card zIndex);
    
    private bool _hovered;

    public override void _Ready()
    {
        Area.MouseEntered += () => EmitSignal(SignalName.MouseEntered, this);
        Area.MouseExited += () => EmitSignal(SignalName.MouseExited, this);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("click") && _hovered)
        {
            EmitSignal(SignalName.Click, this);
        }
    }

    public override void _EnterTree()
    {
        CardFront.Frame = Type.Frame;
    }

    public void Hover(bool exit)
    {
        if (exit)
        {
            _hovered = false;
            Animation.PlayBackwards("hover");
            Input.SetDefaultCursorShape();
            ZIndex = Index;
            return;
        }
        
        _hovered = true;
        Animation.Play("hover");
        Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
        ZIndex = 1000;
    }

    public void UpdateZIndex()
    {
        if (_hovered)
            return;
        
        ZIndex = Index;
    }
}