using System;
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

    private SignalRAdapter? _signalR;

    public override void _Ready()
    {
        _signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");

        TablePhase.Hide();
        
        Login.Pressed += LoginOnPressed;
        JoinTable.Pressed += JoinTableOnPressed;
        CreateTable.Pressed += CreateTableOnPressed;
    }

    private async void CreateTableOnPressed()
    {
        var tableId = await _signalR?.CreateTable()!;
        TableId.Text = tableId;
    }

    private void JoinTableOnPressed()
    {
        throw new NotImplementedException();
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