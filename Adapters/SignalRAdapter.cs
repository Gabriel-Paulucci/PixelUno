using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Microsoft.AspNetCore.SignalR.Client;
using PixelUno.Signals;
using PixelUno.ViewModels;

namespace PixelUno.Adapters;

public partial class SignalRAdapter : Node
{
    private HubConnection? _connection;

    [Signal]
    public delegate void JoinPlayerEventHandler(PlayerSignal player);

    [Signal]
    public delegate void StartEventHandler();

    [Signal]
    public delegate void AddCardEventHandler(CardSignal card);

    [Signal]
    public delegate void PlayCardEventHandler(CardSignal card);

    [Signal]
    public delegate void UpdatePlayerInfoEventHandler(PlayerSignal player);

    [Signal]
    public delegate void TableNextStepsEventHandler(int action, PlayerSignal player);

    [Signal]
    public delegate void EndGameEventHandler(PlayerSignal player);

    [Signal]
    public delegate void ClearEventHandler();

    public async Task Connect(string url)
    {
        var uri = new Uri(url);
        var hubUrl = $"{uri.Scheme}://{uri.Authority}/hubs/game";

        _connection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .WithAutomaticReconnect()
            .Build();

        _connection.On<PlayerViewModel>("JoinPlayer", OnJoinPlayer);
        _connection.On("Start", OnStart);
        _connection.On<CardViewModel>("AddCard", OnAddCard);
        _connection.On<CardViewModel>("PlayCard", OnPlayCard);
        _connection.On<PlayerViewModel>("UpdatePlayerInfo", OnUpdatePlayerInfo);
        _connection.On<IEnumerable<TableActionViewModel>>("TableNextSteps", OnTableNextSteps);
        _connection.On<PlayerViewModel>("EndGame", OnEndGame);
        _connection.On("Clear", OnClear);

        await _connection.StartAsync();
    }

    private void OnClear()
    {
        CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.Clear);
    }

    private void OnEndGame(PlayerViewModel player)
    {
        CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.EndGame, (PlayerSignal)player);
    }

    private void OnTableNextSteps(IEnumerable<TableActionViewModel> actions)
    {
        foreach (var action in actions)
        {
            CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.TableNextSteps, (int)action.Action,
                (PlayerSignal)action.Player);
        }
    }

    private void OnPlayCard(CardViewModel card)
    {
        CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.PlayCard, (CardSignal)card);
    }

    private void OnAddCard(CardViewModel card)
    {
        CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.AddCard, (CardSignal)card);
    }

    private void OnStart()
    {
        CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.Start);
    }

    private void OnJoinPlayer(PlayerViewModel player)
    {
        CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.JoinPlayer, (PlayerSignal)player);
    }

    private void OnUpdatePlayerInfo(PlayerViewModel player)
    {
        CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.UpdatePlayerInfo, (PlayerSignal)player);
    }

    public async Task<PlayerViewModel> SetPlayerName(string name)
    {
        return await _connection!.InvokeAsync<PlayerViewModel>(nameof(SetPlayerName), name);
    }

    public async Task<string> CreateTable()
    {
        var tableId = await _connection!.InvokeAsync<string>(nameof(CreateTable));
        return tableId;
    }

    public async Task JoinTable(string tableId)
    {
        await _connection!.SendAsync(nameof(JoinTable), tableId);
    }

    public async Task StartGame()
    {
        await _connection!.SendAsync(nameof(StartGame));
    }

    public async Task BuyCard()
    {
        await _connection!.SendAsync(nameof(BuyCard));
    }

    public async Task<bool> CheckCard(CardViewModel card)
    {
        return await _connection!.InvokeAsync<bool>(nameof(CheckCard), card);
    }

    public async Task PlayingCard(CardViewModel card)
    {
        await _connection!.SendAsync(nameof(PlayingCard), card);
    }

    public async Task<IEnumerable<PlayerViewModel>> GetPlayers()
    {
        return await _connection!.InvokeAsync<IEnumerable<PlayerViewModel>>(nameof(GetPlayers));
    }

    public async Task Leave()
    {
        await _connection!.SendAsync("Leave");
    }

    public async Task GetMyCards()
    {
        var cards = await _connection!.InvokeAsync<IEnumerable<CardViewModel>>(nameof(GetMyCards));

        foreach (var card in cards)
        {
            CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.AddCard, (CardSignal)card);
        }
    }

    public async Task<bool> AlreadyStarted()
    {
        return await _connection!.InvokeAsync<bool>(nameof(AlreadyStarted));
    }

    public async Task<CardViewModel?> GetTableCard()
    {
        return await _connection!.InvokeAsync<CardViewModel?>(nameof(GetTableCard));
    }
}