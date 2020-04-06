using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuForms
{
    // This class holds the functions used to solve the puzzle.
    class Techniques
    {
        // Walk every square in the board. If it's in this sector, or row, or 
        // column, but isn't us, and isn't already a winner, then keyChar is a Loser.
        // REVIEW KirkG: Why does this need to be static?
        public static bool Neighbor(Square[,] myBoard, int col, int row, char keyChar)
        {
            bool ret = false;
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
                                sqTest.Loser(keyChar - '1', keyChar, Color.Red);
                                ret = true; // We changed something.
                            }
                        }
                    }
                }
            }
            return ret;
        }

        // for each sector
        //   of the non-Winner squares in the sector
        //      for the values 1 through 9
        //        if only one square has the value, it's a Winner.
        public static bool SectorSweep(Square[,] myBoard)
        {
            bool ret = false;
            string[] mpSectorValue = { "", "", "", "", "", "", "", "", "" };
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    Square sqTest = myBoard[x, y];
                    if (sqTest.iWinner != 0)
                    {
                        mpSectorValue[sqTest.sector] += sqTest.text;
                    }
                }
            }
            // mpSectorValue contains the text strings of each sector.
            // Does any character exist in just one of them?
            string szText;
            int cchText;
            for (int s = 0; s <= 8; s++)
            {
                szText = mpSectorValue[s];
                cchText = szText.Length;
                for (char ch = '1'; ch <= '8'; ch++)
                {
                    szText = szText.Replace(ch.ToString(), string.Empty);
                    if (szText.Length + 1 == cchText)
                    {
                        // There is only one square in sector 's' that has 'ch';
                        // find the square; ch is its Winner.
                        for (int y = 0; y <= 8; y++)
                        {
                            for (int x = 0; x <= 8; x++)
                            {
                                Square sqTest = myBoard[x, y];
                                if (sqTest.iWinner != 0)
                                {
                                    if (sqTest.text.Contains(ch))
                                    {
                                        sqTest.Winner(ch - '1', ch, Color.Green);
                                    }
                                }
                            }
                        }

                    }
                }
            }

            return ret;
        }

    }
}
