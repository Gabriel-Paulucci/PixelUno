using Godot;
using PixelUno.Gui;

namespace PixelUno.Entities.Table;

public partial class Table : Node2D
{
    [Export] public required Deck.Deck Deck { get; set; }
    [Export] public required Player.Player CurrentPlayer { get; set; }
    [Export] public required Game.Game Game { get; set; }
    [Export] public required Timer Timer { get; set; }
    [Export] public required int MaxPlayerDelay { get; set; }
    [Export] public required int StartPlayerCards { get; set; }

    public override void _Ready()
    {
        Deck.BuyCard += DeckOnBuyCard;
        CurrentPlayer.SelectedCard += CurrentPlayerOnSelectedCard;
        Timer.Timeout += TimerOnTimeout;

        Deck.Generate();

        for (var i = 0; i < StartPlayerCards; i++)
        {
            CurrentPlayer.AddCard(Deck.GetNextCard());
        }

        Game.AddCard(Deck.GetNextCard());
        ResetTimer();
    }

    private void TimerOnTimeout()
    {
        CurrentPlayer.AddCard(Deck.GetNextCard());
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
        ResetTimer();
    }

    private void CurrentPlayerOnSelectedCard(Card.Card card)
    {
        if (!Game.CheckCard(card.Type))
            return;

        Game.AddCard(card.Type);
        CurrentPlayer.RemoveCard(card);
        ResetTimer();
    }

    private void ResetTimer()
    {
        Timer.Start(MaxPlayerDelay);
    }
}