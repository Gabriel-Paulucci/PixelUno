using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using PixelUno.Adapters;
using PixelUno.Entities.Card;
using PixelUno.Entities.PlayerInfo;
using PixelUno.Signals;
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
    [Export] public required PlayersInfo PlayersInfo { get; set; }

    private SignalRAdapter? _signalR;

    public override async void _Ready()
    {
        _signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");
        _signalR.JoinPlayer += SignalROnJoinPlayer;
        _signalR.Start += SignalROnStart;
        _signalR.AddCard += SignalROnAddCard;
        _signalR.PlayCard += SignalROnPlayCard;
        
        Deck.BuyCard += DeckOnBuyCard;
        Start.Pressed += StartOnPressed;
        
        await LoadPlayers();
    }

    private async Task LoadPlayers()
    {
        var players = await _signalR!.GetPlayers();

        foreach (var player in players)
        {
            PlayersInfo.AddPlayer(player);
        }
    }
    
    private void SignalROnPlayCard(CardSignal card)
    {
        Game.AddCard(card);
    }

    private void SignalROnAddCard(CardSignal card)
    {
        CurrentPlayer.AddCard(card);
    }

    private void SignalROnStart()
    {
        Start.Hide();
    }

    private async void StartOnPressed()
    {
        await _signalR!.StartGame();
    }

    private void SignalROnJoinPlayer(PlayerSignal player)
    {
        PlayersInfo.AddPlayer(player);
    }

    private async void DeckOnBuyCard()
    {
        await _signalR!.BuyCard();
    }
}