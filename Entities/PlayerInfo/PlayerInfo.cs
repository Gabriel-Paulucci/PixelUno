using Godot;
using PixelUno.ViewModels;

namespace PixelUno.Entities.PlayerInfo;

public partial class PlayerInfo : Control
{
	[Export] public required Label PlayerName { get; set; }
	[Export] public required Label Cards { get; set; }
	[Export] public required string DefaultName { get; set; }

	private PlayerViewModel? _player;

	public override void _Process(double delta)
	{
		PlayerName.Text = _player?.Name ?? DefaultName;
	}

	public void SetPlayer(PlayerViewModel player)
	{
		_player = player;
	}
}