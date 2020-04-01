using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SudokuForms
{
    public class Square
    {
        public int iWinner { get; set; } // When there's only one left.
        public int sector { get; }       // What sector we're in.
        public bool[] rgf { get; set; }  // 'true' means it could be, 'false' means it can't be.
        public String text { get; set; } // '1 2 3 4 5 6 7 8 9', getting replaced by spaces.
        public Button btn { get; set; }

        // Constructor
        public Square(int argSector, Button argBtn)
        {
            sector = argSector;
            Button btn = null;
            if (argBtn != null)
            {
                btn = argBtn;
            }
            iWinner = 0;
            rgf = new bool[] { true, true, true, true, true, true, true, true, true };
            text = "1 2 3 4 5 6 7 8 9";
        }

        public void WinnerWinner(int iWinner)
        {
            this.iWinner = iWinner;
            for (int i = 0; i <= 8; i++)
            {
                this.rgf[i] = false;
            }
            this.rgf[iWinner] = true;
        }

        public void Loser(int iLoser, char chLoser)
        {
            this.rgf[iLoser] = false;
            this.text = text.Replace(chLoser, ' ');

            // This test will not be necessary when we fill out the whole array.
            if (btn != null)
            {
                btn.Text = btn.Text.Replace(chLoser, ' ');
            }
        }

        /*
        public Square[,] unusedGameBoard = new Square[9, 9]
        {
            { new Square(0), new Square(0), new Square(0), new Square(1), new Square(1), new Square(1), new Square(2), new Square(2), new Square(2) },
            { new Square(0), new Square(0), new Square(0), new Square(1), new Square(1), new Square(1), new Square(2), new Square(2), new Square(2) },
            { new Square(0), new Square(0), new Square(0), new Square(1), new Square(1), new Square(1), new Square(2), new Square(2), new Square(2) },
            { new Square(3), new Square(3), new Square(3), new Square(4), new Square(4), new Square(4), new Square(5), new Square(5), new Square(5) },
            { new Square(3), new Square(3), new Square(3), new Square(4), new Square(4), new Square(4), new Square(5), new Square(5), new Square(5) },
            { new Square(3), new Square(3), new Square(3), new Square(4), new Square(4), new Square(4), new Square(5), new Square(5), new Square(5) },
            { new Square(6), new Square(6), new Square(6), new Square(7), new Square(7), new Square(7), new Square(8), new Square(8), new Square(8) },
            { new Square(6), new Square(6), new Square(6), new Square(7), new Square(7), new Square(7), new Square(8), new Square(8), new Square(8) },
            { new Square(6), new Square(6), new Square(6), new Square(7), new Square(7), new Square(7), new Square(8), new Square(8), new Square(8) }
        };
        */

    }
}
