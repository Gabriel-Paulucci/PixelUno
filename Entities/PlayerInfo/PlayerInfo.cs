using Godot;
using PixelUno.Adapters;
using PixelUno.Enums;
using PixelUno.Signals;
using PixelUno.ViewModels;

namespace PixelUno.Entities.PlayerInfo;

public partial class PlayerInfo : PanelContainer
{
    [Export] public required Label PlayerName { get; set; }
    [Export] public required Label Cards { get; set; }
    [Export] public required string DefaultName { get; set; }

    [Export] public required StyleBoxFlat StyleIdle { get; set; }
    [Export] public required StyleBoxFlat StylePlaying { get; set; }
    [Export] public required StyleBoxFlat StyleBlock { get; set; }
    [Export] public required StyleBoxFlat StyleNext { get; set; }

    private PlayerViewModel? _player;
    private SignalRAdapter? _signalR;

    public override void _Ready()
    {
        _signalR = GetNode<SignalRAdapter>("/root/SignalRAdapter");
        _signalR.UpdatePlayerInfo += SignalROnUpdatePlayerInfo;
    }

    public override void _ExitTree()
    {
        _signalR!.UpdatePlayerInfo -= SignalROnUpdatePlayerInfo;
    }

    private void SignalROnUpdatePlayerInfo(PlayerSignal player)
    {
        if (player.Id != _player?.Id)
            return;
        
        _player = player;
        UpdateData();
    }

    public void SetPlayer(PlayerViewModel player)
    {
        _player = player;
        UpdateData();
    }

    private void UpdateData()
    {
        PlayerName.Text = _player?.Name ?? DefaultName;
        Cards.Text = _player?.CardAmount.ToString();
        var style = _player?.Action switch
        {
            TableAction.Idle => StyleIdle,
            TableAction.Playing => StylePlaying,
            TableAction.Block => StyleBlock,
            TableAction.Next => StyleNext,
            _ => StyleIdle
        };
        
        RemoveThemeStyleboxOverride("panel");
        AddThemeStyleboxOverride("panel", style);
    }
}