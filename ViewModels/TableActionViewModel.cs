using PixelUno.Enums;

namespace PixelUno.ViewModels;

public class TableActionViewModel
{
    public required TableAction Action { get; set; }
    public required PlayerViewModel Player { get; set; }
}