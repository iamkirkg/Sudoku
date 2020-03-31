using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameEngine;

namespace SudokuForms
{
    public partial class Form3 : Form
    {
        public BoardLayout.Square[,] myGameBoard;

        public Form3()
        {
            InitializeComponent();

            myGameBoard = new BoardLayout.Square[9, 9]
            {
                { new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(2), new BoardLayout.Square(2), new BoardLayout.Square(2) },
                { new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(2), new BoardLayout.Square(2), new BoardLayout.Square(2) },
                { new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(0), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(1), new BoardLayout.Square(2), new BoardLayout.Square(2), new BoardLayout.Square(2) },
                { new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(5), new BoardLayout.Square(5), new BoardLayout.Square(5) },
                { new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(5), new BoardLayout.Square(5), new BoardLayout.Square(5) },
                { new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(3), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(4), new BoardLayout.Square(5), new BoardLayout.Square(5), new BoardLayout.Square(5) },
                { new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(8), new BoardLayout.Square(8), new BoardLayout.Square(8) },
                { new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(8), new BoardLayout.Square(8), new BoardLayout.Square(8) },
                { new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(6), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(7), new BoardLayout.Square(8), new BoardLayout.Square(8), new BoardLayout.Square(8) }
            };
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
        }

        private void Winner(int row, int col, Button btn, char keyChar)
        {
            if (keyChar >= '1' && keyChar <= '9')
            {
                btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btn.Text = keyChar.ToString();
                BoardLayout.WinnerWinner(myGameBoard[row, col], keyChar - '1');

                // Walk every square in the board. If it's in this sector, or row, or 
                // column, but isn't us, then keyChar is a Loser.
                for (int x = 0; x <= 8; x++)
                {
                    for (int y = 0; y <= 8; y++)
                    {
                        if (x == row ||
                            y == col ||
                            myGameBoard[x, y].sector == myGameBoard[row, col].sector
                            )
                        {
                            if (!(x == row && y == col))
                            {
                                BoardLayout.Loser(myGameBoard[x, y], keyChar - '1', keyChar);
                            }
                        }
                    }
                }
            }
        }
    }
}
