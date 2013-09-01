using System;
using System.Collections.Generic;

namespace Tadmas.Latrunculi.Engine
{
    public enum PieceType
    {
        Pawn,
        King
    }

    public class Piece
    {
        public PieceType Type { get; set; }
        public Player Player { get; set; }
    }
}
