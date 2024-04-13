using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using PixelUno.Enums;

namespace PixelUno.Entities.Deck;

public partial class Deck : Node2D
{
    [Export] public required Area2D Area { get; set; }

    private Queue<CardType> _cards = [];
    private bool _hovered;

    public override void _Ready()
    {
        Area.MouseExited += AreaOnMouseExited;
        Area.MouseEntered += AreaOnMouseEntered;
    }

    private void AreaOnMouseEntered()
    {
        _hovered = true;
        Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
    }

    private void AreaOnMouseExited()
    {
        _hovered = false;
        Input.SetDefaultCursorShape();
    }

    public void Generate()
    {
        var deck = new List<CardType>();

        AddColorCard(deck, CardType.Blue0, CardType.Blue1, CardType.BluePlus2);
        AddColorCard(deck, CardType.Yellow0, CardType.Yellow1, CardType.YellowPlus2);
        AddColorCard(deck, CardType.Red0, CardType.Red1, CardType.RedPlus2);
        AddColorCard(deck, CardType.Green0, CardType.Green1, CardType.GreenPlus2);

        AddWildCard(deck, CardType.WildColor);
        AddWildCard(deck, CardType.WildPlus4);

        var cards = deck.ToArray();
        new Random().Shuffle(cards);

        _cards = new Queue<CardType>(cards);
    }

    private static void AddWildCard(List<CardType> deck, CardType wild)
    {
        var wildColor = Enumerable.Repeat((int)wild, 4)
            .Select(x => (CardType)x)
            .ToList();

        deck.AddRange(wildColor);
    }

    private static void AddColorCard(List<CardType> deck, CardType zero, CardType start, CardType end)
    {
        var colorCard = Enumerable.Range((int)start, (int)end - (int)start + 1)
            .Select(x => (CardType)x)
            .ToList();

        deck.Add(zero);
        deck.AddRange(colorCard);
        deck.AddRange(colorCard);
    }

    public CardType GetNextCard()
    {
        return _cards.Dequeue();
    }
}