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
            // If we aren't currently clicked on a Winner square, then do nothing.
            if (myBoard[col,row].iWinner != -1)
            {
                for (int y = 0; y <= 8; y++)
                {
                    for (int x = 0; x <= 8; x++)
                    {
                        Square sqTest = myBoard[x, y];
                        if (sqTest.iWinner == -1)
                        {
                            if (x == col ||
                                y == row ||
                                sqTest.sector == myBoard[col, row].sector
                                )
                            {
                                if (!(x == col && y == row))
                                {
                                    sqTest.Loser(keyChar, Color.Red);
                                    ret = true; // We changed something.
                                }
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
            Square sqTest;
            string[] mpSectorText = { "", "", "", "", "", "", "", "", "" };
            for (int y = 0; y <= 8; y++)
            {
                for (int x = 0; x <= 8; x++)
                {
                    sqTest = myBoard[x, y];
                    if (sqTest.iWinner == -1)
                    {
                        mpSectorText[sqTest.sector] += sqTest.btn.Text;
                    }
                }
            }
            // mpSectorText contains the text strings of each sector.
            // Does any character value exist in just one of them?
            string szText;
            int cchText;
            for (int s = 0; s <= 8; s++)
            {
                szText = mpSectorText[s];
                for (char ch = '1'; ch <= '9'; ch++)
                {
                    cchText = szText.Length;
                    szText = szText.Replace(ch.ToString(), string.Empty);
                    if (szText.Length + 1 == cchText)
                    {
                        // There is only one square in sector 's' that has 'ch';
                        // find the square; ch is its Winner.
                        for (int y = 0; y <= 8; y++)
                        {
                            for (int x = 0; x <= 8; x++)
                            {
                                sqTest = myBoard[x, y];
                                if (sqTest.sector == s)
                                {
                                    if (sqTest.iWinner == -1)
                                    {
                                        if (sqTest.btn.Text.Contains(ch))
                                        {
                                            sqTest.Winner(ch, Color.Green);

                                            // After we've marked someone a Winner, we need to erase their
                                            // neighboring little numbers.
                                            Neighbor(myBoard, x, y, ch);

                                            ret = true; // We changed something.
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ret;
        }

        // for the non-Winner squares in the column
        //   for the values 1 through 9
        //     if only one square has the value, it's a Winner.
        public static bool ColumnSweep(Square[,] myBoard, int argCol)
        {
            bool ret = false;
            Square sqTest;
            string szColText = "";
            for (int y = 0; y <= 8; y++)
            {
                sqTest = myBoard[argCol, y];
                if (sqTest.iWinner == -1)
                {
                    szColText += sqTest.btn.Text;
                }
            }
            // szColText contains the text strings of all squares in the column.
            // Does any character value exist just once?
            int cchText;
            for (char ch = '1'; ch <= '9'; ch++)
            {
                cchText = szColText.Length;
                szColText = szColText.Replace(ch.ToString(), string.Empty);
                if (szColText.Length + 1 == cchText)
                {
                    // There is only one square in column argCol that has 'ch';
                    // find the square; ch is its Winner.
                    for (int y = 0; y <= 8; y++)
                    {
                        sqTest = myBoard[argCol, y];
                        if (sqTest.iWinner == -1)
                        {
                            if (sqTest.btn.Text.Contains(ch))
                            {
                                sqTest.Winner(ch, Color.Green);

                                // After we've marked someone a Winner, we need to erase their
                                // neighboring little numbers.
                                Neighbor(myBoard, argCol, y, ch);

                                ret = true; // We changed something.
                            }
                        }
                    }

                }
            }

            return ret;
        }

        // for the non-Winner squares in the row
        //   for the values 1 through 9
        //     if only one square has the value, it's a Winner.
        public static bool RowSweep(Square[,] myBoard, int argRow)
        {
            bool ret = false;
            Square sqTest;
            string szRowText = "";
            for (int x = 0; x <= 8; x++)
            {
                sqTest = myBoard[x, argRow];
                if (sqTest.iWinner == -1)
                {
                    szRowText += sqTest.btn.Text;
                }
            }
            // szRowText contains the text strings of all squares in the row.
            // Does any character value exist just once?
            int cchText;
            for (char ch = '1'; ch <= '9'; ch++)
            {
                cchText = szRowText.Length;
                szRowText = szRowText.Replace(ch.ToString(), string.Empty);
                if (szRowText.Length + 1 == cchText)
                {
                    // There is only one square in column argCol that has 'ch';
                    // find the square; ch is its Winner.
                    for (int x = 0; x <= 8; x++)
                    {
                        sqTest = myBoard[x, argRow];
                        if (sqTest.iWinner == -1)
                        {
                            if (sqTest.btn.Text.Contains(ch))
                            {
                                sqTest.Winner(ch, Color.Green);

                                // After we've marked someone a Winner, we need to erase their
                                // neighboring little numbers.
                                Neighbor(myBoard, x, argRow, ch);

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
