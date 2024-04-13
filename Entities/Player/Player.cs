using System.Collections.Generic;
using System.Linq;
using Godot;
using PixelUno.Enums;

namespace PixelUno.Entities.Player;

public partial class Player : Node2D
{
    [Export] public required bool Self { get; set; }
    [Export] public required string UserName { get; set; }
    [Export] public required PackedScene CardScene { get; set; }

    private List<Card.Card> Cards { get; set; } = [];

    public void AddCard(CardType newCard)
    {
        var card = CardScene.Instantiate<Card.Card>();
        card.Type = newCard;
        card.Position = new Vector2(700, 0);

        AddChild(card);
        
        Cards.Add(card);
    }

    public override void _Ready()
    {
        GetViewport().PhysicsObjectPicking = true;
        GetViewport().PhysicsObjectPickingSort = true;
    }

    public override void _PhysicsProcess(double delta)
    {
        var space = 1000.0f / (Cards.Count + 1);

        var position = -500 + space;
        var index = 0;
        foreach (var card in Cards)
        {
            card.Position = card.Position.MoveToward(new Vector2(position, 0), (float)delta * 2000);
            card.ZIndex = index++;
            position += space;
        }
    }
}