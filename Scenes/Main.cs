using Godot;
using PixelUno.Entities.Table;
using PixelUno.Gui;

namespace PixelUno.Scenes;

public partial class Main : Node2D
{
    [Export] public required PackedScene TableScene { get; set; }
    [Export] public required Menu Menu { get; set; }

    private Table? _table;

    public override void _Ready()
    {
        Menu.EnterInGame += MenuOnEnterInGame;
    }

    private void MenuOnEnterInGame()
    {
        Menu.Hide();

        _table = TableScene.Instantiate<Table>();
        AddChild(_table);
    }
}