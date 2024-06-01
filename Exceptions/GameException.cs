using System;

namespace PixelUno.Exceptions;

public class GameException(string message) : Exception(message)
{
}