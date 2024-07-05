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
        _signalR.Clear += SignalROnClear;

        Menu.EnterInGame += MenuOnEnterInGame;
        EndGame.GoHome += EndGameOnGoHome;
    }

    private void SignalROnClear()
    {
        ClearGame();
    }

    private void SignalROnEndGame(PlayerSignal player)
    {
        EndGame.Show();
        EndGame.SetWinnerName(player);
    }

    private async void EndGameOnGoHome()
    {
        await _signalR!.Leave();
        ClearGame();
    }

    private void ClearGame()
    {
        _table?.Free();
        _table = null;
        EndGame.Hide();
        Menu.ClearData();
        Menu.Show();
    }

    private async void MenuOnEnterInGame()
    {
        Menu.Hide();
        EndGame.Hide();

        _table = TableScene.Instantiate<Table>();
        _table.SetInfo(Menu.PlayerName.Text, Menu.TableId.Text);
        AddChild(_table);

        var alreadyStarted = await _signalR!.AlreadyStarted();

        if (alreadyStarted)
        {
            await _table.Started();
        }
    }
}