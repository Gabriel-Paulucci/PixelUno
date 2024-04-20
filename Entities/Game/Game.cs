using System;
using System.Collections.Generic;
using Godot;
using PixelUno.Entities.Card;
using PixelUno.Enums;

namespace PixelUno.Entities.Game;

public partial class Game : Node2D
{
    [Export] public required PackedScene CardScene { get; set; }
    
    private List<Card.Card> Cards { get; set; } = [];
    private CardType? LastCard { get; set; }

    public bool CheckCard(CardType cardType)
    {
        return LastCard is null ||
               cardType.Color == CardColor.Wild ||
               cardType.Color == LastCard.Color ||
               cardType.Symbol == LastCard.Symbol;
    }

    public void AddCard(CardType cardType)
    {
        var random = new Random();
        var card = CardScene.Instantiate<Card.Card>();
        card.Type = cardType;
        card.Position += new Vector2(random.Next(-10, 10), random.Next(-10, 10));
        card.Rotation += random.NextSingle() - 0.5f;
        
        AddChild(card);
        LastCard = cardType;
    }
}