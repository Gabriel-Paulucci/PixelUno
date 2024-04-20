using Godot;
using PixelUno.Entities.Card;

namespace PixelUno.Entities.Table;

public partial class Table : Node2D
{
    [Export] public required Deck.Deck Deck { get; set; }
    [Export] public required Player.Player CurrentPlayer { get; set; }
    [Export] public required Game.Game Game { get; set; }

    public override void _Ready()
    {
        Deck.Generate();

        for (var i = 0; i < 20; i++)
        {
            CurrentPlayer.AddCard(Deck.GetNextCard());
        }
        
        Game.AddCard(Deck.GetNextCard());
        
        CurrentPlayer.SelectedCard += CurrentPlayerOnSelectedCard;
    }

    private void CurrentPlayerOnSelectedCard(Card.Card card)
    {
        if (!Game.CheckCard(card.Type)) 
            return;
        
        Game.AddCard(card.Type);
        CurrentPlayer.RemoveCard(card);
    }
}
