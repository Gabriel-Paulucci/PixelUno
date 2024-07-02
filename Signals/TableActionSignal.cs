using Godot;
using PixelUno.Enums;
using PixelUno.ViewModels;

namespace PixelUno.Signals;

public partial class TableActionSignal : GodotObject
{
    public required TableAction Action { get; set; }
    public required PlayerViewModel Player { get; set; }
}