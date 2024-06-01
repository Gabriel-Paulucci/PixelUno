using System.Threading.Tasks;
using Godot;
using PixelUno.Adapters;
using PixelUno.Gui;

namespace PixelUno.Entities.Table;

public partial class Table : Node2D
{
    [Export] public required Deck.Deck Deck { get; set; }
    [Export] public required Player.Player CurrentPlayer { get; set; }
    [Export] public required Game.Game Game { get; set; }
    [Export] public required int MaxPlayerDelay { get; set; }
    [Export] public required int StartPlayerCards { get; set; }

    private SignalRAdapter? _signalR;

    public override void _Ready()
    {
        _signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");
        
        Deck.BuyCard += DeckOnBuyCard;
        CurrentPlayer.SelectedCard += CurrentPlayerOnSelectedCard;
        
        // Deck.Generate();
        //
        // for (var i = 0; i < StartPlayerCards; i++)
        // {
        //     CurrentPlayer.AddCard(Deck.GetNextCard());
        // }
        //
        // Game.AddCard(Deck.GetNextCard());
    }

    private void DeckOnBuyCard()
    {
        if (Game.CardsToBuy > 0)
        {
            for (var i = 0; i < Game.CardsToBuy; i++)
            {
                CurrentPlayer.AddCard(Deck.GetNextCard());
            }

            Game.ClearCardsToBuy();
            return;
        }

        CurrentPlayer.AddCard(Deck.GetNextCard());
    }

    private void CurrentPlayerOnSelectedCard(Card.Card card)
    {
        if (!Game.CheckCard(card.Type))
            return;

        Game.AddCard(card.Type);
        CurrentPlayer.RemoveCard(card);
    }
}