using Godot;
using PixelUno.Adapters;
using PixelUno.Signals;
using PixelUno.ViewModels;

namespace PixelUno.Entities.PlayerInfo;

public partial class PlayerInfo : Control
{
	[Export] public required Label PlayerName { get; set; }
	[Export] public required Label Cards { get; set; }
	[Export] public required string DefaultName { get; set; }

	private PlayerViewModel? _player;	
	private SignalRAdapter? _signalR;

	public override void _Ready()
	{
		_signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");
		_signalR.UpdatePlayerInfo += SignalROnUpdatePlayerInfo;
	}

	public override void _Process(double delta)
	{
		PlayerName.Text = _player?.Name ?? DefaultName;
		Cards.Text = _player?.CardAmount.ToString();
	}

	private void SignalROnUpdatePlayerInfo(PlayerSignal player)
	{
		if (player.Id != _player?.Id)
			return;

		_player = player;
	}

	public void SetPlayer(PlayerViewModel player)
	{
		_player = player;
	}
}