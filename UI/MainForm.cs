using System;
using System.Drawing;
using System.Windows.Forms;
using Tadmas.Latrunculi.Engine;

namespace Tadmas.Latrunculi.UI
{
    public partial class MainForm : Form
    {
        private const int BORDER_SIZE = 1;
        private const int PIECE_MARGIN = 4;

        private Board _Board = new Board();

        public MainForm()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (Brush brush = new SolidBrush(Color.Gray))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            Point[,] grid = FindGridCorners();
            Point topLeft = grid[0, 0];
            Point bottomRight = grid[_Board.Rows, _Board.Columns];
            Rectangle boardRect = Rectangle.FromLTRB(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y);

            using (Brush brush = new SolidBrush(Color.Khaki))
            {
                e.Graphics.FillRectangle(brush, boardRect);
            }

            using (Pen pen = new Pen(Color.DarkKhaki))
            {
                for (int row = 0; row <= _Board.Rows; row++)
                {
                    e.Graphics.DrawLine(pen, grid[row, 0], grid[row, _Board.Columns]);
                }
                for (int col = 0; col <= _Board.Columns; col++)
                {
                    e.Graphics.DrawLine(pen, grid[0, col], grid[_Board.Rows, col]);
                }
            }

            using (Brush whiteBrush = new SolidBrush(Color.White))
            using (Brush blackBrush = new SolidBrush(Color.Black))
            using (Pen grayPen = new Pen(Color.Gray))
            using (Pen darkGrayPen = new Pen(Color.DarkGray))
            {
                for (int row = 0; row < _Board.Rows; row++)
                {
                    for (int col = 0; col < _Board.Columns; col++)
                    {
                        if (_Board[row, col] != null)
                        {
                            Rectangle pieceRect = Rectangle.FromLTRB(grid[row, col].X, grid[row, col].Y, grid[row + 1, col + 1].X, grid[row + 1, col + 1].Y);
                            pieceRect.Inflate(-PIECE_MARGIN, -PIECE_MARGIN);
                            Brush brush = _Board[row, col].Player == Player.White ? whiteBrush : blackBrush;
                            Pen pen = _Board[row, col].Player == Player.White ? darkGrayPen : grayPen;
                            switch (_Board[row, col].Type)
                            {
                                case PieceType.Pawn:
                                    e.Graphics.FillEllipse(brush, pieceRect);
                                    e.Graphics.DrawEllipse(pen, pieceRect);
                                    break;
                                case PieceType.King:
                                    e.Graphics.FillRectangle(brush, pieceRect);
                                    e.Graphics.DrawRectangle(pen, pieceRect);
                                    e.Graphics.DrawLine(pen, pieceRect.Left, pieceRect.Top, pieceRect.Right, pieceRect.Bottom);
                                    e.Graphics.DrawLine(pen, pieceRect.Right, pieceRect.Top, pieceRect.Left, pieceRect.Bottom);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private Point[,] FindGridCorners()
        {
            Point[,] grid = new Point[_Board.Rows + 1, _Board.Columns + 1];

            int gridSize = Math.Min((this.ClientRectangle.Width - BORDER_SIZE) / _Board.Columns,
                                    (this.ClientRectangle.Height - BORDER_SIZE) / _Board.Rows);

            Size boardSize = new Size(_Board.Columns * gridSize + BORDER_SIZE,
                                      _Board.Rows * gridSize + BORDER_SIZE);

            Rectangle boardRect = CenterInRect(this.ClientRectangle, boardSize);

            for (int row = 0; row <= _Board.Rows; row++)
            {
                for (int col = 0; col <= _Board.Columns; col++)
                {
                    grid[row, col] = new Point(boardRect.X + col * gridSize,
                                               boardRect.Y + row * gridSize);
                }
            }

            return grid;
        }

        private Rectangle CenterInRect(Rectangle container, Size childSize)
        {
            Point origin = new Point(container.X + (container.Width - childSize.Width) / 2,
                                     container.Y + (container.Height - childSize.Height) / 2);

            return new Rectangle(origin, childSize);
        }
    }
}
