using System.Collections.Generic;
using System.Linq;
using Godot;
using PixelUno.Entities.Card;
using PixelUno.Enums;

namespace PixelUno.Entities.Player;

public partial class Player : Node2D
{
    [Export] public required bool Self { get; set; }
    [Export] public required string UserName { get; set; }
    [Export] public required PackedScene CardScene { get; set; }

    private List<Card.Card> Cards { get; set; } = [];
    private HashSet<int> CardHovers { get; set; } = [];

    public void AddCard(CardType newCard)
    {
        var card = CardScene.Instantiate<Card.Card>();
        card.Type = newCard;
        card.Position = new Vector2(700, 0);
        card.MouseEntered += CardOnMouseEntered;
        card.MouseExited += CardOnMouseExited;

        AddChild(card);
        
        Cards.Add(card);
        Cards.Sort((x, y) => x.Type.Frame - y.Type.Frame);
    }

    private void CardOnMouseEntered(int zIndex)
    {
        if (CardHovers.Count == 0)
        {
            CardHovers.Add(zIndex);
            Cards.ElementAtOrDefault(zIndex)?.Hover(false);
            return;
        }
        
        var oldTopZIndex = CardHovers.Max();
        CardHovers.Add(zIndex);
        var newTopZIndex = CardHovers.Max();
        
        if (oldTopZIndex == newTopZIndex)
            return;

        Cards.ElementAtOrDefault(oldTopZIndex)?.Hover(true);
        Cards.ElementAtOrDefault(newTopZIndex)?.Hover(false);
    }

    private void CardOnMouseExited(int zIndex)
    {
        if (CardHovers.Count == 1)
        {
            CardHovers.Remove(zIndex);
            Cards.ElementAtOrDefault(zIndex)?.Hover(true);
            return;
        }
        
        var oldTopZIndex = CardHovers.Max();
        CardHovers.Remove(zIndex);
        var newTopZIndex = CardHovers.Max();
        
        if (oldTopZIndex == newTopZIndex)
            return;
        
        Cards.ElementAtOrDefault(oldTopZIndex)?.Hover(true);
        Cards.ElementAtOrDefault(newTopZIndex)?.Hover(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        var space = 1000.0f / (Cards.Count + 1);

        var position = -500 + space;
        var index = 0;
        foreach (var card in Cards)
        {
            card.Position = card.Position.MoveToward(new Vector2(position, 0), (float)delta * 2000);
            card.Index = index++;
            card.UpdateZIndex();
            position += space;
        }
    }
}