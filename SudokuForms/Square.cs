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
        // How long do we pause, to see the progress.
        private int msecSleep = 50;
        public int iBoard { get; } // index into the objBoard.rgSquare array.
        public int iWinner { get; set; } // When there's only one left.
        public char chWinner { get; set; } // When there's only one left.
        public bool fOriginal { get; set; } // Is this one of the starting squares?
        public int row { get; }    // What row we're in.
        public int col { get; }    // What column we're in.
        public int sector { get; set; } // What sector we're in.
        public int hypersector { get; set; } // What hypersector we're in; for HyperSudoku only of course.
        public Button btn { get; set; }

        // I don't understand whether these should be static, or const, or readonly.
        public readonly Color colorRange = Color.BurlyWood;
        public readonly Color colorNSome = Color.Gold;
        public readonly Color colorError = Color.Red;
        public readonly Color colorLoser = Color.Yellow;
        public readonly Color colorOriginal = Color.DarkGreen;
        public readonly Color colorWinner = Color.DarkBlue;
        public readonly static Color colorSector1 = Color.AliceBlue;
        public readonly static Color colorSector2 = Color.FloralWhite;
        public readonly static Color colorSectorH = Color.MistyRose;

        // Constructor
        public Square(Game argGame,
                      int iTab, int iSector, int iHyperSector,
                      Point objPoint, Size objSize, float font, 
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
            hypersector = iHyperSector;
            col = ((iTab - 1) % objGame.cDimension); // Modulo (remainder)
            row = ((iTab - 1) / objGame.cDimension); // Divide

            btn = new Button
            {
                BackColor = MyBackColor(),
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", font, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Location = objPoint,
                Size = objSize,
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

        // Palette:
        //   https://www.rapidtables.com/web/color/html-color-codes.html
        //   https://www.html.am/html-codes/color/color-scheme.cfm

        // Sudoku and SuperSudoku have one color per sector, alternating. So we map _sector_ to color.
        readonly Color[] mpSectorColor = {
            colorSector1, colorSector2, colorSector1,
            colorSector2, colorSector1, colorSector2,
            colorSector1, colorSector2, colorSector1
        };
        readonly Color[] mpSectorColorSuper = {
            colorSector1, colorSector2, colorSector1, colorSector2,
            colorSector2, colorSector1, colorSector2, colorSector1,
            colorSector1, colorSector2, colorSector1, colorSector2,
            colorSector2, colorSector1, colorSector2, colorSector1
        };

        // HyperSudoku has three colors, with the inset sectors having the 3rd color, so we map _square_ to color.
        readonly Color[] mpSquarecolorSectorH = {
            colorSector1, colorSector1, colorSector1, colorSector2, colorSector2, colorSector2, colorSector1, colorSector1, colorSector1,
            colorSector1, colorSectorH, colorSectorH, colorSectorH, colorSector2, colorSectorH, colorSectorH, colorSectorH, colorSector1,
            colorSector1, colorSectorH, colorSectorH, colorSectorH, colorSector2, colorSectorH, colorSectorH, colorSectorH, colorSector1,
            colorSector2, colorSectorH, colorSectorH, colorSectorH, colorSector1, colorSectorH, colorSectorH, colorSectorH, colorSector2,
            colorSector2, colorSector2, colorSector2, colorSector1, colorSector1, colorSector1, colorSector2, colorSector2, colorSector2,
            colorSector2, colorSectorH, colorSectorH, colorSectorH, colorSector1, colorSectorH, colorSectorH, colorSectorH, colorSector2,
            colorSector1, colorSectorH, colorSectorH, colorSectorH, colorSector2, colorSectorH, colorSectorH, colorSectorH, colorSector1,
            colorSector1, colorSectorH, colorSectorH, colorSectorH, colorSector2, colorSectorH, colorSectorH, colorSectorH, colorSector1,
            colorSector1, colorSector1, colorSector1, colorSector2, colorSector2, colorSector2, colorSector1, colorSector1, colorSector1
        };

        public void SetBackColor(Color c) {
            btn.BackColor = c;
            btn.Refresh();
            Thread.Sleep(msecSleep);
        }

        public Color MyBackColor()
        {
            switch (objGame.gameFlav)
            {
                case Flavor.Sudoku:
                    return mpSectorColor[sector];
                case Flavor.SuperSudoku:
                    return mpSectorColorSuper[sector];
                default: // case Flavor.HyperSudoku:
                    return mpSquarecolorSectorH[iBoard];
            }
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
            btn.Font = new Font("Microsoft Sans Serif", objGame.objBoard.font, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = argText;
        }

        public void Winner(char chValue, bool fOriginal, Board objBoard)
        {
            // For char values '0' through '9', iWinner is 0 to 9.
            // For char values 'A' through 'F', iWinner is 10 to 15.
            if (chValue >= '0' && chValue <= '9') {
                iWinner = chValue - '0';
            } else if (chValue >= 'A' && chValue <= 'F') {
                iWinner = chValue - 'A' + 10;
            } else {
                objGame.objLogBox.Log("Error: bogus 'Winner' char of " + chValue);
            }

            chWinner = chValue;
            Color colorFore = fOriginal ? colorOriginal : colorWinner;
            
            // We only want to set this once. Once you're Original, you stay that way.
            if (fOriginal) {
                this.fOriginal = fOriginal;
            }

            // This square has been declared to be a winner. But if its value is 
            // anywhere else in the row/column/sector, then the puzzle is broken.
            // This doesn't get specifically recorded in the saved XML file. Should it?
            foreach (Square sq in objBoard.rgSquare) {
                if ((sq.chWinner == chValue) &&
                    (sq.btn.TabIndex != btn.TabIndex) &&
                    ((sq.row == row) || (sq.col == col) || (sq.sector == sector))
                   ) {
                    objGame.objLogBox.Log("Error: r" + sq.row + "c" + sq.col + ":r" + row + "c" + col + ":" + chValue);
                    colorFore = colorError;
                }
            }

            btn.Font = new Font("Microsoft Sans Serif", objGame.objBoard.emSizeWinner, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = chValue.ToString() + " ";
            btn.ForeColor = colorFore;
            SetBackColor(MyBackColor());
        }

        // Look to see if this Square would change.
        // This function is a logical subset of FLoser(), below.
        public bool FLoserTest(char chValue)
        {
            if (iWinner != -1)
            {
                return false;
            }
            return btn.Text.Contains(chValue + "");
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

            SetBackColor(colorLoser); // show yellow with old text for a moment
            btn.Text = szTextNew;
            btn.Refresh();
            Thread.Sleep(msecSleep);  // show yellow with new text for a moment

            // If we've but one char left, it's a Winner!
            string sz = btn.Text.Replace(" ", string.Empty);
            if (sz.Length == 1)
            {
                Winner(sz[0], false, objBoard);
            }

            return true;
        }

    }
}
