using System;
using Godot;

namespace PixelUno.Entities.Card;

public partial class CardEntity : Node2D
{
	[Export] public required CardType Type { get; set; }
	[Export] public required AnimatedSprite2D CardFront { get; set; }
	[Export] public required Area2D Area { get; set; }
	[Export] public required AnimationPlayer Animation { get; set; }
	[Export] public required bool Lock { get; set; }

	public override void _Ready()
	{
		Area.MouseEntered += AreaOnMouseEntered;
		Area.MouseExited += AreaOnMouseExited;
	}

	private void AreaOnMouseEntered()
	{
		if (Lock)
			return;
		
		Animation.Play("hover");
	}

	private void AreaOnMouseExited()
	{
		if (Lock)
			return;
		
		Animation.PlayBackwards("hover");
	}

	public override void _EnterTree()
	{
		CardFront.Frame = new Random().Next(0, 43);
	}
}