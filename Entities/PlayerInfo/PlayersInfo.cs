using Godot;
using PixelUno.ViewModels;

namespace PixelUno.Entities.PlayerInfo;

public partial class PlayersInfo : MarginContainer
{
	[Export] public required Node List { get; set; }
	[Export] public required PackedScene PlayerInfo { get; set; }

	public void AddPlayer(PlayerViewModel player)
	{
		var playerInfo = PlayerInfo.Instantiate<PixelUno.Entities.PlayerInfo.PlayerInfo>();
		playerInfo.SetPlayer(player);
		List.AddChild(playerInfo);
	}
}