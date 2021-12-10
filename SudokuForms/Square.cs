using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

// This is for Flavor flav. I don't understand it.
using static SudokuForms.Game;

namespace SudokuForms
{
    public class Square
    {
        public Game objGame;
        public int iBoard { get; } // index into the objBoard.rgSquare array.
        public int iWinner { get; set; } // When there's only one left.
        public char chWinner { get; set; } // When there's only one left.
        public bool fOriginal { get; set; } // Is this one of the starting squares?
        public int row { get; set; }    // What row we're in.
        public int col { get; set; }    // What column we're in.
        public int sector { get; set; } // What sector we're in.
        public Button btn { get; set; }

        // Constructor
        public Square(Game argGame,
                      int iTab, int iSector, 
                      int xPoint, int yPoint, int xSize, int ySize, float font, 
                      KeyPressEventHandler fnKeyPress,
                      KeyEventHandler fnKeyDown,
                      EventHandler fnClick
                      )
        {
            objGame = argGame;
            iBoard = iTab - 1;
            iWinner = -1;
            chWinner = 'X';
            fOriginal = false;
            sector = iSector;
            col = ((iTab - 1) % objGame.cDimension); // Modulo (remainder)
            row = ((iTab - 1) / objGame.cDimension); // Divide

            btn = new Button
            {
                BackColor = MyBackColor(),
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", font, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(xPoint, yPoint),
                Size = new Size(xSize, ySize),
                TabIndex = iTab,
                Text = objGame.szAll
            };

            btn.KeyPress += fnKeyPress;
            btn.KeyDown += fnKeyDown;
            btn.Click += fnClick;

            objGame.Controls.Add(btn);
        }

        public void Delete() {
            objGame.Controls.Remove(btn);
        }

        // Sudoku and SuperSudoku have one color per sector, alternating. So we map _sector_ to color.
        readonly Color[] mpSectorColor = {
            Color.AliceBlue,   Color.FloralWhite, Color.AliceBlue,
            Color.FloralWhite, Color.AliceBlue,   Color.FloralWhite,
            Color.AliceBlue,   Color.FloralWhite, Color.AliceBlue
        };
        readonly Color[] mpSectorColorSuper = {
            Color.AliceBlue,   Color.FloralWhite, Color.AliceBlue,   Color.FloralWhite,
            Color.FloralWhite, Color.AliceBlue,   Color.FloralWhite, Color.AliceBlue,
            Color.AliceBlue,   Color.FloralWhite, Color.AliceBlue,   Color.FloralWhite,
            Color.FloralWhite, Color.AliceBlue,   Color.FloralWhite, Color.AliceBlue
        };

        // HyperSudoku has three colors, with the inset sectors having the 3rd color, so we map _square_ to color.
        readonly Color[] mpSquareColorHyper = {
            Color.AliceBlue,   Color.AliceBlue,   Color.AliceBlue,   Color.FloralWhite, Color.FloralWhite, Color.FloralWhite, Color.AliceBlue,   Color.AliceBlue,   Color.AliceBlue,
            Color.AliceBlue,   Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.FloralWhite, Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.AliceBlue,
            Color.AliceBlue,   Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.FloralWhite, Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.AliceBlue,
            Color.FloralWhite, Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.AliceBlue,   Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.FloralWhite,
            Color.FloralWhite, Color.FloralWhite, Color.FloralWhite, Color.AliceBlue,   Color.AliceBlue,   Color.AliceBlue,   Color.FloralWhite, Color.FloralWhite, Color.FloralWhite,
            Color.FloralWhite, Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.AliceBlue,   Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.FloralWhite,
            Color.AliceBlue,   Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.FloralWhite, Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.AliceBlue,
            Color.AliceBlue,   Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.FloralWhite, Color.MistyRose,   Color.MistyRose,   Color.MistyRose,   Color.AliceBlue,
            Color.AliceBlue,   Color.AliceBlue,   Color.AliceBlue,   Color.FloralWhite, Color.FloralWhite, Color.FloralWhite, Color.AliceBlue,   Color.AliceBlue,   Color.AliceBlue
        };
        public Color MyBackColor()
        {
            switch (objGame.curFlavor)
            {
                case Flavor.Sudoku:
                    return mpSectorColor[sector];
                case Flavor.SuperSudoku:
                    return mpSectorColorSuper[sector];
                default: // case Flavor.HyperSudoku:
                    return mpSquareColorHyper[iBoard];
            }
        }

        public Color MyLoserColor()
        {
            return Color.Yellow;
        }

        // Reset this square to initial status.
        public void Reset()
        {
            CouldBes(objGame.szAll);
        }

        // Set this square to an 'intermediate' status, partly evaluated.
        public void CouldBes(string argText)
        {
            iWinner = -1;
            chWinner = 'X';
            fOriginal = false;
            btn.BackColor = MyBackColor();
            btn.ForeColor = Color.Black;
            btn.Font = new Font("Microsoft Sans Serif", objGame.font, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = argText;
        }

        public void Winner(char chValue, bool fOriginal, Color colorWinner, Board objBoard)
        {
            // For char values '0' through '9', iWinner is 0 to 9.
            // For char values 'A' through 'F', iWinner is 10 to 15.
            if (chValue >= '0' && chValue <= '9')
            {
                iWinner = chValue - '0';
            }
            else if (chValue >= 'A' && chValue <= 'F')
            {
                iWinner = chValue - 'A' + 10;
            } else {
                objBoard.objLogBox.Log("Error: bogus 'Winner' char of " + chValue);
            }

            chWinner = chValue;
            Color colorSquare = colorWinner;
            
            // We only want to set this once. Once you're Original, you stay that way.
            if (fOriginal)
            {
                this.fOriginal = fOriginal;
            }

            // This square has been declared to be a winner. But if its value is 
            // anywhere else in the row/column/sector, then the puzzle is broken.
            // This doesn't get specifically recorded in the saved XML file. Should it?
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
            btn.Font = new Font("Microsoft Sans Serif", objBoard.objGame.emSizeWinner, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = chValue.ToString() + " ";
            btn.BackColor = MyBackColor();
            btn.ForeColor = colorSquare;
            btn.Refresh();
            Thread.Sleep(10);
            btn.Refresh();
            btn.BackColor = save;
        }

        public bool FLoser(char chValue, Board objBoard)
        {
            // If we're already a Winner, don't do anything.
            if (iWinner != -1)
            {
                return false;
            }

            // If the Text doesn't change, don't do anything.
            string szTextNew = btn.Text.Replace(chValue + " ", null);
            if (btn.Text.Equals(szTextNew))
            {
                return false;
            }

            Color save = btn.BackColor;
            btn.BackColor = MyLoserColor();
            btn.Refresh();
            Thread.Sleep(10);
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
