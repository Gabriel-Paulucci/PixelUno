using System;
using System.Collections.Generic;
using Godot;
using PixelUno.Entities.Card;
using PixelUno.Enums;
using PixelUno.Gui;

namespace PixelUno.Entities.Game;

public partial class Game : Node2D
{
    [Export] public required PackedScene CardScene { get; set; }
    [Export] public required CardColorSelect CardColorSelect { get; set; }
    
    private List<Card.Card> Cards { get; set; } = [];
    private Card.Card? LastCard { get; set; }
    private bool PlayerInteraction { get; set; }

    public override void _Ready()
    {
        CardColorSelect.SelectColor += CardColorSelectOnSelectColor;
    }

    private void CardColorSelectOnSelectColor(CardColor color)
    {
        if (LastCard is null)
            return;
        
        LastCard.Type.Color = color;
        LastCard.UpdateSprite();
        CardColorSelect.Hide();
        PlayerInteraction = false;
    }

    public bool CheckCard(CardType cardType)
    {
        if (PlayerInteraction)
            return false;
        
        return LastCard is null ||
               cardType.Color == CardColor.Wild ||
               cardType.Color == LastCard.Type.Color ||
               cardType.Symbol == LastCard.Type.Symbol;
    }

    public void AddFirstCard(CardType cardType)
    {
        var card = CardScene.Instantiate<Card.Card>();
        card.Type = cardType;
        AddChild(card);
        LastCard = card;
    }

    public void AddCard(CardType cardType)
    {
        var random = new Random();
        var card = CardScene.Instantiate<Card.Card>();
        card.Type = cardType;
        card.Position += new Vector2(random.Next(-10, 10), random.Next(-10, 10));
        card.Rotation += random.NextSingle() - 0.5f;

        AddChild(card);
        LastCard = card;

        if (cardType.Color != CardColor.Wild) 
            return;
        
        CardColorSelect.Show();
        PlayerInteraction = true;
    }
}