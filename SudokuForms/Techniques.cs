using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuForms
{
    // This class holds the functions used to solve the puzzle.
    class Techniques
    {
        // REVIEW KirkG: Why does this need to be static?
        public static bool Neighbor(Square[,] myBoard, int col, int row, char keyChar)
        {
            bool ret = false;
            // Walk every square in the board. If it's in this sector, or row, or 
            // column, but isn't us, and isn't already a winner, then keyChar is a Loser.
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    Square sqTest = myBoard[x, y];
                    if (sqTest.iWinner == 0)
                    {
                        if (x == col ||
                            y == row ||
                            sqTest.sector == myBoard[col, row].sector
                            )
                        {
                            if (!(x == col && y == row))
                            {
                                sqTest.Loser(keyChar - '1', keyChar);
                                ret = true; // We changed something.
                            }
                        }
                    }
                }
            }
            return ret;
        }
    }
}
