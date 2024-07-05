using Godot;
using PixelUno.Enums;

namespace PixelUno.ViewModels;

public class PlayerViewModel
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public int CardAmount { get; set; }
    public TableAction Action { get; set; }
}