using System.Collections.Generic;
using Godot;
using PixelUno.Enums;

namespace PixelUno.Entities.Player;

public partial class Player : Node2D
{
	[Export] public required bool Self { get; set; }
	[Export] public required string UserName { get; set; }
	[Export] public required PackedScene CardScene { get; set; }
	
	public List<Card.Card> Cards { get; set; } = [];

	public void AddCard(CardType newCard)
	{
		var card = CardScene.Instantiate<Card.Card>();
		card.Type = newCard;
		
		AddChild(card);
	}

}