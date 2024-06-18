using System;
using Godot;
using PixelUno.Entities.Card;

namespace PixelUno.Entities.Game;

public partial class Game : Node2D
{
    [Export] public required PackedScene CardScene { get; set; }

    private bool _firstCard = true;

    public void AddCard(CardType cardType)
    {
        var card = CardScene.Instantiate<Card.Card>();
        card.Type = cardType;

        if (!_firstCard)
        {
            var random = new Random();
            card.Position += new Vector2(random.Next(-10, 10), random.Next(-10, 10));
            card.Rotation += random.NextSingle() - 0.5f;
        }
        else
        {
            _firstCard = false;
        }

        AddChild(card);
    }
}