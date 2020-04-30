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
        public Square(int iTab, int iSector, 
                      int xPoint, int yPoint, int xSize, int ySize, float font, 
                      // Note: currently unused.
                      Action<object, KeyPressEventArgs> fnKeyPress)
        {
            iWinner = 0;
            chWinner = '0';
            fOriginal = false;
            sector = iSector;
            col = ((iTab - 1) % 9); // Modulo (remainder)
            row = ((iTab - 1) / 9); // Divide

            btn = new Button
            {
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Font = new Font("Microsoft Sans Serif", font, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Location = new Point(xPoint, yPoint),
                Size = new Size(xSize, ySize),
                TabIndex = iTab,
                Text = "1 2 3 4 5 6 7 8 9"
                // Don't know how to do this in here, so it's out in the for loop.
                //KeyPressEventHandler handler = fnKeyPress;
                //KeyPress += (KeyPressEventHandler)fnKeyPress;
                // Error CS0029  Cannot implicitly convert type 
                // 'System.Action<object, System.Windows.Forms.KeyPressEventArgs>' 
                // to 
                // 'System.Windows.Forms.KeyPressEventHandler'   
            };
        }

        // Reset this square to initial status.
        public void Reset()
        {
            iWinner = 0;
            chWinner = '0';
            fOriginal = false;
            btn.BackColor = Color.LightGray;
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
            btn.BackColor = Color.LightGray;
            btn.ForeColor = Color.Black;
            btn.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = argText;
        }

        public void Winner(char chValue, bool fOriginal, Color colorWinner)
        {
            // If we're already a Winner, don't do anything.
            //if (iWinner != 0)
            //{
            //    // It should be the same Winner value.
            //    Debug.Assert(iWinner == chValue - '0');
            //    return;
            //}

            iWinner = chValue - '0';
            chWinner = chValue;
            
            // We only want to set this once. Once you're Original, you stay that way.
            if (fOriginal)
            {
                this.fOriginal = fOriginal;
            }

            Color save = btn.BackColor;
            btn.Font = new Font("Microsoft Sans Serif", 40F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            btn.Text = chValue.ToString();
            btn.BackColor = Color.LightGray;
            btn.ForeColor = colorWinner;
            btn.Refresh();
            Thread.Sleep(100);
            btn.Refresh();
            btn.BackColor = save;
        }

        public bool FLoser(char chValue, Color colorLoser)
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
            btn.BackColor = colorLoser;
            btn.Refresh();
            Thread.Sleep(100);
            btn.Text = szTextNew;
            btn.Refresh();
            btn.BackColor = save;

            // If we've but one char left, it's a Winner!
            string sz = btn.Text.Replace(" ", string.Empty);
            if (sz.Length == 1)
            {
                Winner(sz[0], false, Color.DarkBlue);
            }

            return true;

        }

    }
}
