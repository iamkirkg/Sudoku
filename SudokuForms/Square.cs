using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SudokuForms
{
    public class Square
    {
        public int iWinner { get; set; } // When there's only one left.
        public char chWinner { get; set; }
        public bool fOriginal { get; set; } // Is this one of the starting squares?
        public int row { get; set; }    // What row we're in.
        public int col { get; set; }    // What column we're in.
        public int sector { get; set; } // What sector we're in.
        public Button btn { get; set; }

        // Constructor
        public Square(Game objGame,
                      int iTab, int iSector, 
                      int xPoint, int yPoint, int xSize, int ySize, float font, 
                      KeyPressEventHandler fnKeyPress,
                      KeyEventHandler fnKeyDown,
                      EventHandler fnClick
                      )
        {
            iWinner = 0;
            chWinner = '0';
            fOriginal = false;
            sector = iSector;
            col = ((iTab - 1) % 9); // Modulo (remainder)
            row = ((iTab - 1) / 9); // Divide

            btn = new Button
            {
                BackColor = MyBackColor(),
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", font, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(xPoint, yPoint),
                Size = new Size(xSize, ySize),
                TabIndex = iTab,
                Text = "1 2 3 4 5 6 7 8 9"
            };

            btn.KeyPress += fnKeyPress;
            btn.KeyDown += fnKeyDown;
            btn.Click += fnClick;

            objGame.Controls.Add(btn);
        }

        // Alternate background colors for sectors.
        public Color MyBackColor()
        {
            if ((sector % 2) == 1)
            {
                return Color.FloralWhite;
            }
            else
            {
                return Color.AliceBlue;
            }
        }

        public Color MyLoserColor()
        {
            return Color.Yellow;
        }

        // Reset this square to initial status.
        public void Reset()
        {
            iWinner = 0;
            chWinner = '0';
            fOriginal = false;
            btn.BackColor = MyBackColor();
            btn.ForeColor = Color.Black;
            btn.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = "1 2 3 4 5 6 7 8 9";
        }

        // Set this square to an 'intermediate' status, partly evaluated.
        public void CouldBes(string argText)
        {
            iWinner = 0;
            chWinner = '0';
            fOriginal = false;
            btn.BackColor = MyBackColor();
            btn.ForeColor = Color.Black;
            btn.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = argText;
        }

        public void Winner(char chValue, bool fOriginal, Color colorWinner, Board objBoard)
        {
            iWinner = chValue - '0';
            chWinner = chValue;
            Color colorSquare = colorWinner;
            
            // We only want to set this once. Once you're Original, you stay that way.
            if (fOriginal)
            {
                this.fOriginal = fOriginal;
            }

            // This square has been declared to be a winner. But if its value is 
            // anywhere else in the row/column/sector, then the puzzle is broken.
            foreach (Square sq in objBoard.rgSquare)
            {
                if ((sq.chWinner == chValue) &&
                    (sq.btn.TabIndex != btn.TabIndex) &&
                    ((sq.row == row) || (sq.col == col) || (sq.sector == sector))
                   )
                {
                    objBoard.objLogBox.Log("Error: r" + sq.row + "c" + sq.col + ":r" + row + "c" + col + ":" + chValue);
                    colorSquare = Color.Red;
                }
            }

            Color save = btn.BackColor;
            btn.Font = new Font("Microsoft Sans Serif", 40F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = chValue.ToString();
            btn.BackColor = MyBackColor();
            btn.ForeColor = colorSquare;
            btn.Refresh();
            Thread.Sleep(50);
            btn.Refresh();
            btn.BackColor = save;
        }

        public bool FLoser(char chValue, Board objBoard)
        {
            // If we're already a Winner, don't do anything.
            if (iWinner != 0)
            {
                return false;
            }

            // If the Text doesn't change, don't do anything.
            string szTextNew = btn.Text.Replace(chValue, ' ').Replace("  ", " ");
            if (btn.Text.Equals(szTextNew))
            {
                return false;
            }

            Color save = btn.BackColor;
            btn.BackColor = MyLoserColor();
            btn.Refresh();
            Thread.Sleep(50);
            btn.Text = szTextNew;
            btn.Refresh();
            btn.BackColor = save;

            // If we've but one char left, it's a Winner!
            string sz = btn.Text.Replace(" ", string.Empty);
            if (sz.Length == 1)
            {
                Winner(sz[0], false, Color.DarkBlue, objBoard);
            }

            return true;

        }

    }
}
