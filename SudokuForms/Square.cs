using System;
using System.Windows.Forms;

namespace SudokuForms
{
    public class Square
    {
        public int iWinner { get; set; } // When there's only one left.
        public int sector { get; }       // What sector we're in.
        public bool[] rgf { get; set; }  // 'true' means it could be, 'false' means it can't be.
        public string text { get; set; } // '1 2 3 4 5 6 7 8 9', getting replaced by spaces.
        public Button btn { get; set; }

        // Constructor
        public Square(int iTab, int iSector, 
                      int xPoint, int yPoint, int xSize, int ySize, float font, 
                      // Note: currently unused.
                      Action<object, KeyPressEventArgs> fnKeyPress)
        {
            sector = iSector;

            btn = new Button
            {
                Font = new System.Drawing.Font("Microsoft Sans Serif", font, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(xPoint, yPoint),
                Size = new System.Drawing.Size(xSize, ySize),
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

            iWinner = 0;
            rgf = new bool[] { true, true, true, true, true, true, true, true, true };
            text = "1 2 3 4 5 6 7 8 9";
        }

        public void WinnerWinner(int iArg)
        {
            iWinner = iArg;
            for (int i = 0; i <= 8; i++)
            {
                rgf[i] = false;
            }
            rgf[iWinner] = true;
        }

        public void Loser(int iLoser, char chLoser)
        {
            rgf[iLoser] = false;
            btn.Text = btn.Text.Replace(chLoser, ' ');
        }

    }
}
