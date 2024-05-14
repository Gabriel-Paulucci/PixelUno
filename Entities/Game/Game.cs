using System;
using Godot;
using PixelUno.Entities.Card;
using PixelUno.Enums;
using PixelUno.Gui;

namespace PixelUno.Entities.Game;

public partial class Game : Node2D
{
    [Export] public required PackedScene CardScene { get; set; }
    [Export] public required CardColorSelect CardColorSelect { get; set; }
    [Export] public int CardsToBuy { get; private set; }
    
    private Card.Card? _lastCard;
    private bool _waitPlayerInteraction;
    private bool _firstCard = true;

    public override void _Ready()
    {
        CardColorSelect.SelectColor += CardColorSelectOnSelectColor;
    }

    private void CardColorSelectOnSelectColor(CardColor color)
    {
        if (_lastCard is null)
            return;
        
        _lastCard.Type.Color = color;
        _lastCard.UpdateSprite();
        CardColorSelect.Hide();
        _waitPlayerInteraction = false;
    }

    public bool CheckCard(CardType cardType)
    {
        if (_waitPlayerInteraction)
            return false;

        if (CardsToBuy > 0)
        {
            return cardType.Symbol switch
            {
                CardSymbol.Plus4 => true,
                CardSymbol.Plus2 when _lastCard?.Type.Symbol == CardSymbol.Plus2 => true,
                _ => false
            };
        }
        
        return _lastCard is null ||
               cardType.Color == CardColor.Wild ||
               cardType.Color == _lastCard.Type.Color ||
               cardType.Symbol == _lastCard.Type.Symbol;
    }

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
        _lastCard = card;

        switch (cardType)
        {
            case { Symbol: CardSymbol.Plus2 }:
                CardsToBuy += 2;
                break;
            case { Symbol: CardSymbol.Plus4 }:
                CardsToBuy += 4;
                break;
        }
        
        if (cardType.Color != CardColor.Wild) 
            return;
        
        CardColorSelect.Show();
        _waitPlayerInteraction = true;
    }

    public void ClearCardsToBuy()
    {
        CardsToBuy = 0;
    }
}