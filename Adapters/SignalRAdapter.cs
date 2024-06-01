using System;
using System.Threading.Tasks;
using Godot;
using Microsoft.AspNetCore.SignalR.Client;
using PixelUno.Exceptions;

namespace PixelUno.Adapters;

public partial class SignalRAdapter : Node
{
	private HubConnection? _connection;

	public async Task Connect(string url)
	{
		var uri = new Uri(url);
		var hubUrl = $"{uri.Scheme}://{uri.Authority}/hubs/game";
		
		_connection = new HubConnectionBuilder()
			.WithUrl(hubUrl)
			.WithAutomaticReconnect()
			.Build();

		await _connection.StartAsync();
	}

	public async Task SetPlayerName(string name)
	{
		if (_connection is null)
			throw new GameException("Inicie a conexão com o servidor");
		
		await _connection.SendAsync("SetPlayerName", name);
	}

	public async Task<string> CreateTable()
	{
		if (_connection is null)
			throw new GameException("Inicie a conexão com o servidor");

		var tableId = await _connection.InvokeAsync<string>("CreateTable");
		return tableId;
	}
}