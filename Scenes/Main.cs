using Godot;
using PixelUno.Adapters;
using PixelUno.Entities.Table;
using PixelUno.Gui;
using PixelUno.Signals;

namespace PixelUno.Scenes;

public partial class Main : Node2D
{
    [Export] public required PackedScene TableScene { get; set; }
    [Export] public required Menu Menu { get; set; }
    [Export] public required EndGame EndGame { get; set; }

    private SignalRAdapter? _signalR;
    private Table? _table;

    public override void _Ready()
    {
        _signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");
        _signalR.EndGame += SignalROnEndGame;
        
        Menu.EnterInGame += MenuOnEnterInGame;
        EndGame.GoHome += EndGameOnGoHome;
    }

    private void SignalROnEndGame(PlayerSignal player)
    {
        
        EndGame.Show();
        EndGame.SetWinnerName(player);
    }

    private void EndGameOnGoHome()
    {
        _table?.QueueFree();
        EndGame.Hide();
        Menu.Show();
    }

    private void MenuOnEnterInGame()
    {
        Menu.Hide();
        EndGame.Hide();

        _table = TableScene.Instantiate<Table>();
        _table.SetInfo(Menu.PlayerName.Text, Menu.TableId.Text);
        AddChild(_table);
    }
}