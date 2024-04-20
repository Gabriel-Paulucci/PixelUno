using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;
using PixelUno.Entities.Card;
using PixelUno.Enums;

namespace PixelUno.Entities.Deck;

public partial class Deck : Node2D
{
    [Export] public required Area2D Area { get; set; }
    [Export] public required Array<CardType> Cards { get; set; }

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
        var cards = Cards.ToArray();
        new Random().Shuffle(cards);

        _cards = new Queue<CardType>(cards);
    }

    public CardType GetNextCard()
    {
        return _cards.Dequeue();
    }
}