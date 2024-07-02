using Godot;
using PixelUno.ViewModels;

namespace PixelUno.Gui;

public partial class EndGame : Control
{
	[Export] public required Label PlayerName { get; set; }
	[Export] public required Button Home { get; set; }

	[Signal]
	public delegate void GoHomeEventHandler();
	
	public override void _Ready()
	{
		
		Home.Pressed += HomeOnPressed;
	}

	public void SetWinnerName(PlayerViewModel player)
	{
		PlayerName.Text = player.Name;
	}
	
	private void HomeOnPressed()
	{
		EmitSignal(SignalName.GoHome);
	}
}