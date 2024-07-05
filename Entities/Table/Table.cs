using System.Threading.Tasks;
using Godot;
using PixelUno.Adapters;
using PixelUno.Entities.PlayerInfo;
using PixelUno.Gui;
using PixelUno.Signals;

namespace PixelUno.Entities.Table;

public partial class Table : Node2D
{
    [Export] public required Deck.Deck Deck { get; set; }
    [Export] public required Player.Player CurrentPlayer { get; set; }
    [Export] public required Game.Game Game { get; set; }
    [Export] public required Button Start { get; set; }
    [Export] public required PlayersInfo PlayersInfo { get; set; }
    [Export] public required LocalInfo LocalInfo { get; set; }

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

    public override void _ExitTree()
    {
        _signalR!.JoinPlayer -= SignalROnJoinPlayer;
        _signalR.Start -= SignalROnStart;
        _signalR.AddCard -= SignalROnAddCard;
        _signalR.PlayCard -= SignalROnPlayCard;
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

    public void SetInfo(string playerName, string tableId)
    {
        LocalInfo.SetInfo(playerName, tableId);
    }

    private void SignalROnJoinPlayer(PlayerSignal player)
    {
        PlayersInfo.AddPlayer(player);
    }

    private async void DeckOnBuyCard()
    {
        await _signalR!.BuyCard();
    }

    public async Task Started()
    {
        Start.Hide();
        await _signalR!.GetMyCards();
    }
}