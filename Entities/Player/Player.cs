using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using PixelUno.Adapters;
using PixelUno.Entities.Card;
using PixelUno.Enums;
using PixelUno.Gui;
using PixelUno.ViewModels;

namespace PixelUno.Entities.Player;

public partial class Player : Node2D
{
    [Export] public required bool Self { get; set; }
    [Export] public required string UserName { get; set; }
    [Export] public required PackedScene CardScene { get; set; }
    [Export] public required CardColorSelect CardColorSelect { get; set; }

    private List<Card.Card> Cards { get; set; } = [];
    private HashSet<Card.Card> CardHovers { get; set; } = [];
    private SignalRAdapter? _signalR;

    public override void _Ready()
    {
        _signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");
        CardColorSelect.SelectColor += CardColorSelectOnSelectColor;
    }

    private async void CardColorSelectOnSelectColor(Card.Card card)
    {
        await PlatingCard(card);
    }

    private async Task PlatingCard(Card.Card card)
    {
        await _signalR!.PlayingCard(new CardViewModel()
        {
            Color = card.Type.Color,
            Symbol = card.Type.Symbol
        });

        RemoveCard(card);
        OrderCards();
    }

    public void AddCard(CardType newCard)
    {
        var card = CardScene.Instantiate<Card.Card>();
        card.Type = newCard;
        card.Position = new Vector2(700, 0);
        card.Click += CardOnClick;
        card.MouseEntered += CardOnMouseEntered;
        card.MouseExited += CardOnMouseExited;

        AddChild(card);

        Cards.Add(card);
        OrderCards();
    }

    private async void CardOnClick(Card.Card card)
    {
        var topCard = CardHovers.MaxBy(x => x.Index);

        if (topCard != card)
            return;

        if (!await _signalR!.CanPlay())
            return;
        
        if (!await _signalR!.CheckCard(new CardViewModel()
            {
                Color = card.Type.Color,
                Symbol = card.Type.Symbol
            }))
            return;

        if (card.Type.Color == CardColor.Wild)
        {
            CardColorSelect.Color(card);
            return;
        }

        await PlatingCard(card);
    }

    private void CardOnMouseEntered(Card.Card card)
    {
        var topCard = CardHovers.MaxBy(x => x.Index);

        if (topCard is null)
        {
            card.Hover(false);
            CardHovers.Add(card);
            return;
        }

        if (topCard == card)
            return;

        CardHovers.Add(card);

        if (topCard.Index >= card.Index)
            return;

        topCard.Hover(true);
        card.Hover(false);
    }

    private void CardOnMouseExited(Card.Card card)
    {
        var topCard = CardHovers.MaxBy(x => x.Index);

        if (topCard is null)
            return;

        if (topCard != card)
        {
            CardHovers.Remove(card);
            return;
        }

        topCard.Hover(true);
        CardHovers.Remove(card);

        var newHover = CardHovers.MaxBy(x => x.Index);
        newHover?.Hover(false);
    }

    public override void _PhysicsProcess(double delta)
    {
        var space = 1000.0f / (Cards.Count + 1);

        var position = -500 + space;
        var index = 0;
        foreach (var card in Cards)
        {
            card.Position = card.Position.MoveToward(new Vector2(position, 0), (float)delta * 1500);
            card.Index = index++;
            card.UpdateZIndex();
            position += space;
        }
    }

    private void RemoveCard(Card.Card card)
    {
        Cards.Remove(card);
        OrderCards();
        CardHovers.Remove(card);
        card.QueueFree();
    }

    private void OrderCards()
    {
        Cards.Sort((x, y) => x.Type.Order - y.Type.Order);
    }
}