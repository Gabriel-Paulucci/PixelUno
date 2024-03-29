using Godot;

namespace PixelUno.Entities.Table;

public partial class Table : Node2D
{
    [Export] public required Deck.Deck Deck { get; set; }

    public override void _Ready()
    {
        Deck.Generate();
    }
}