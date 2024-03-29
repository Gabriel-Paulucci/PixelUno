using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using PixelUno.Enums;

namespace PixelUno.Entities.Deck;

public partial class Deck : Node2D
{
    private Queue<CardType> _cards = [];

    public void Generate()
    {
        var deck = new List<CardType>();

        AddColorCard(deck, CardType.Blue0, CardType.Blue1, CardType.BlueReverse);
        AddColorCard(deck, CardType.Yellow0, CardType.Yellow1, CardType.YellowReverse);
        AddColorCard(deck, CardType.Red0, CardType.Red1, CardType.RedReverse);
        AddColorCard(deck, CardType.Green0, CardType.Green1, CardType.GreenReverse);

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
        var colorCard = Enumerable.Range((int)start, (int)end + 1)
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