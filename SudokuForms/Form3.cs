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
            WWinner(myGameBoard[4, 5], 3);
        }

        private void WWinner(BoardLayout.Square sq, int iWinner)
        {
            BoardLayout.WinnerWinner(sq, iWinner);
        }
    }
}
