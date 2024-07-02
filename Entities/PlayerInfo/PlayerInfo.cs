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
        _signalR.TableNextSteps += SignalROnTableNextSteps;
    }

    private void SignalROnTableNextSteps(int action, PlayerSignal player)
    {
        if (player.Id != _player?.Id)
            return;

        var style = ((TableAction)action) switch
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

    public override void _Process(double delta)
    {
        PlayerName.Text = _player?.Name ?? DefaultName;
        Cards.Text = _player?.CardAmount.ToString();
    }

    private void SignalROnUpdatePlayerInfo(PlayerSignal player)
    {
        if (player.Id != _player?.Id)
            return;

        _player = player;
    }

    public void SetPlayer(PlayerViewModel player)
    {
        _player = player;
    }
}