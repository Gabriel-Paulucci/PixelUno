using Godot;

namespace PixelUno.Entities.Card;

public partial class CardEntity : Node2D
{
	[Export] public required CardType Type { get; set; }
	[Export] public required AnimatedSprite2D CardFront { get; set; }

	public override void _EnterTree()
	{
		CardFront.Frame = (int)Type;
	}
}