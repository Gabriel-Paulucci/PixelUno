using Godot;
using PixelUno.Enums;
using PixelUno.ViewModels;

namespace PixelUno.Signals;

public partial class PlayerSignal : GodotObject
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public int CardAmount { get; set; }
    public TableAction Action { get; set; }

    public static implicit operator PlayerSignal(PlayerViewModel player)
    {
        return new PlayerSignal()
        {
            Id = player.Id,
            Name = player.Name,
            CardAmount = player.CardAmount,
            Action = player.Action
        };
    }
    
    public static implicit operator PlayerViewModel(PlayerSignal playerSignal)
    {
        return new PlayerViewModel()
        {
            Id = playerSignal.Id,
            Name = playerSignal.Name,
            CardAmount = playerSignal.CardAmount,
            Action = playerSignal.Action
        };
    }
}