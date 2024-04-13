using Godot;

namespace PixelUno.Entities.Table;

public partial class Table : Node2D
{
    [Export] public required Deck.Deck Deck { get; set; }
    [Export] public required Player.Player CurrentPlayer { get; set; }

    public override void _Ready()
    {
        Deck.Generate();

        for (var i = 0; i < 20; i++)
        {
            CurrentPlayer.AddCard(Deck.GetNextCard());
        }
    }
}
