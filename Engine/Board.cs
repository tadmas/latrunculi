using System;
using System.Collections.Generic;

namespace Tadmas.Latrunculi.Engine
{
    public sealed class Board
    {
        private const int ROWS = 8;
        private const int COLUMNS = 12;

        private Piece[,] _Pieces = new Piece[ROWS,COLUMNS];

        public Board()
        {
            for (int column = 0; column < COLUMNS; column++)
            {
                _Pieces[0, column] = new Piece { Player = Player.Black, Type = PieceType.Pawn };
                _Pieces[ROWS - 1, column] = new Piece { Player = Player.White, Type = PieceType.Pawn };
            }
            int midPoint = (COLUMNS / 2); // just to the right of the midpoint line
            _Pieces[1, midPoint - 1] = new Piece { Player = Player.Black, Type = PieceType.King };
            _Pieces[ROWS - 2, midPoint] = new Piece { Player = Player.White, Type = PieceType.King };
        }

        public Piece this[int row, int column]
        {
            get { return _Pieces[row, column]; }
        }

        public int Rows
        {
            get { return ROWS; }
        }

        public int Columns
        {
            get { return COLUMNS; }
        }
    }
}
