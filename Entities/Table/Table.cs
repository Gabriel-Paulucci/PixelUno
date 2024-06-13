using System;
using System.Collections.Generic;
using Godot;
using PixelUno.Adapters;
using PixelUno.Entities.Card;
using PixelUno.ViewModels;

namespace PixelUno.Entities.Table;

public partial class Table : Node2D
{
    [Export] public required Deck.Deck Deck { get; set; }
    [Export] public required Player.Player CurrentPlayer { get; set; }
    [Export] public required Game.Game Game { get; set; }
    [Export] public required int MaxPlayerDelay { get; set; }
    [Export] public required int StartPlayerCards { get; set; }
    [Export] public required Button Start { get; set; }

    private SignalRAdapter? _signalR;
    private List<PlayerViewModel> _otherPlayers = [];

    public override void _Ready()
    {
        _signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");
        _signalR.JoinPlayer += SignalROnJoinPlayer;
        _signalR.Start += SignalROnStart;
        _signalR.AddCard += SignalROnAddCard;
        
        Deck.BuyCard += DeckOnBuyCard;
        CurrentPlayer.SelectedCard += CurrentPlayerOnSelectedCard;
        Start.Pressed += StartOnPressed;
    }

    private void SignalROnAddCard(CardViewModel card)
    {
        CurrentPlayer.AddCard(new CardType()
        {
            Color = card.Color,
            Symbol = card.Symbol
        });
    }

    private void SignalROnStart()
    {
        Start.Hide();
    }

    private async void StartOnPressed()
    {
        await _signalR!.StartGame();
    }

    private void SignalROnJoinPlayer(PlayerViewModel player)
    {
        _otherPlayers.Add(player);
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