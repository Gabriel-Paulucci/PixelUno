using Godot;

namespace PixelUno.ViewModels;

public partial class PlayerViewModel : GodotObject
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}