using Godot;
using PixelUno.Adapters;

namespace PixelUno.Gui;

public partial class Menu : Control
{
    [Export] public required Control LoginPhase { get; set; }
    [Export] public required LineEdit Host { get; set; }
    [Export] public required LineEdit PlayerName { get; set; }
    [Export] public required Button Login { get; set; }
    [Export] public required Control TablePhase { get; set; }
    [Export] public required LineEdit TableId { get; set; }
    [Export] public required Button JoinTable { get; set; }
    [Export] public required Button CreateTable { get; set; }
    [Export] public required Button Playing { get; set; }

    private SignalRAdapter? _signalR;

    [Signal]
    public delegate void EnterInGameEventHandler();

    public override void _Ready()
    {
        _signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");

        TablePhase.Hide();
        Playing.Hide();
        
        Login.Pressed += LoginOnPressed;
        JoinTable.Pressed += JoinTableOnPressed;
        CreateTable.Pressed += CreateTableOnPressed;
        Playing.Pressed += PlayingOnPressed;
    }

    private void PlayingOnPressed()
    {
        EmitSignal(SignalName.EnterInGame);
    }

    private async void CreateTableOnPressed()
    {
        var tableId = await _signalR?.CreateTable()!;
        TableId.Text = tableId;
    }

    private async void JoinTableOnPressed()
    {
        await _signalR?.JoinTable(TableId.Text)!;
        Playing.Show();
    }

    private async void LoginOnPressed()
    {
        if (_signalR is null)
            return;

        await _signalR.Connect(Host.Text);
        await _signalR.SetPlayerName(PlayerName.Text);
        LoginPhase.Hide();
        TablePhase.Show();
    }
}