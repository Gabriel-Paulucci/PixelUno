using Godot;

namespace PixelUno.Gui;

public partial class LocalInfo : MarginContainer
{
	[Export] public required Label TableId { get; set; }
	[Export] public required Label PlayerName { get; set; }

	public void SetInfo(string playerName, string tableId)
	{
		TableId.Text = tableId;
		PlayerName.Text = playerName;
	}
}