using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Microsoft.AspNetCore.SignalR.Client;
using PixelUno.ViewModels;

namespace PixelUno.Adapters;

public partial class SignalRAdapter : Node
{
	private HubConnection? _connection;

	[Signal]
	public delegate void JoinPlayerEventHandler(PlayerViewModel player);

	[Signal]
	public delegate void StartEventHandler();

	[Signal]
	public delegate void AddCardEventHandler(CardViewModel card);
	
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
		
		await _connection.StartAsync();
	}

	private void OnAddCard(CardViewModel card)
	{
		CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.AddCard, card);
	}

	private void OnStart()
	{
		CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.Start);
	}

	private void OnJoinPlayer(PlayerViewModel player)
	{
		CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.JoinPlayer, player);
	}
	
	public async Task<PlayerViewModel> SetPlayerName(string name)
	{
		return await _connection!.InvokeAsync<PlayerViewModel>("SetPlayerName", name);
	}

	public async Task<string> CreateTable()
	{
		var tableId = await _connection!.InvokeAsync<string>("CreateTable");
		return tableId;
	}

	public async Task JoinTable(string tableId)
	{
		var players = await _connection!.InvokeAsync<IEnumerable<PlayerViewModel>>("JoinTable", tableId);

		foreach (var player in players)
		{
			CallDeferred(GodotObject.MethodName.EmitSignal, SignalName.JoinPlayer, player);
		}
	}

	public async Task StartGame()
	{
		await _connection!.SendAsync("StartGame");
	}

	public async Task BuyCard()
	{
		await _connection!.SendAsync("BuyCard");
	}
}