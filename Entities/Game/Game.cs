using System.Collections.Generic;
using System.Linq;
using Godot;
using PixelUno.Enums;

namespace PixelUno.Entities.Game;

public partial class Game : Node2D
{
    private List<Card.Card> Cards { get; set; } = [];

    public bool CheckCard(CardSymbol cardSymbol)
    {
        return true;
    }
}