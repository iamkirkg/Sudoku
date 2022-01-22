using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is for Flavor flav. I don't understand it.
using static SudokuForms.Game;

namespace SudokuForms
{
    public partial class TryEmBoards : Form
    {
        public TryEmBoards(Board objBoard, LogBox objLogBox, int col, int row, char ch)
        {
            InitializeComponent();
        }
    }
}
