﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        // For all squares in the column (Winner and Loser)
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
                szColText += sqTest.btn.Text;
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

        // for all squares in the row (Winner and Loser)
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
                szRowText += sqTest.btn.Text;
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
                    // There is exactly one square in column argCol that has 'ch';
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

        // Find two squares in a row|column|sector that have only two values, 
        // and the same two values. If found, mark those two values as Losers 
        // for the other squares in the row|column|sector.
        public static bool TwoPair(Square[,] myBoard, LogBox objLogBox)
        {
            bool ret = false;
            Square sqFirst, sqSecond;
            int secFirst, secSecond;

            for (int y1 = 0; y1 <= 8; y1++)
            {
                for (int x1 = 0; x1 <= 8; x1++)
                {
                    sqFirst = myBoard[x1, y1];
                    // Does our text have just two characters?
                    string szTextFirst = sqFirst.btn.Text.Replace(" ", string.Empty);
                    if (szTextFirst.Length == 2)
                    {
                        // Find another Square with the same Text.
                        for (int y2 = 0; y2 <= 8; y2++)
                        {
                            for (int x2 = 0; x2 <= 8; x2++)
                            {
                                sqSecond = myBoard[x2, y2];
                                // Speed hack: don't check squares before us.
                                if (sqSecond.btn.TabIndex > sqFirst.btn.TabIndex)
                                { 
                                    // Does our text have the same two characters?
                                    string szTextSecond = sqSecond.btn.Text.Replace(" ", string.Empty);
                                    if (szTextFirst == szTextSecond)
                                    {
                                        // Same two-char strings! 
                                        char ch1 = szTextFirst[0];
                                        char ch2 = szTextFirst[1];
                                        objLogBox.Log("TwoPair: [" + x1 + "," + y1 + "] [" + x2 + "," + y2 + "]:" + ch1 + ' ' + ch2);

                                        if (x1 == x2)
                                        {
                                            for (int y3 = 0; y3 <= 8; y3++)
                                            {
                                                if (y3 != y1 && y3 != y2)
                                                {
                                                    objLogBox.Log("TwoPair col: [" + x1 + "," + y3 + "]");
                                                    myBoard[x1, y3].Loser(ch1, Color.Red);
                                                    myBoard[x1, y3].Loser(ch2, Color.Red);
                                                }
                                            }
                                        }
                                        else if (y1 == y2)
                                        {
                                            for (int x3 = 0; x3 <= 8; x3++)
                                            {
                                                if (x3 != x1 && x3 != x2)
                                                {
                                                    objLogBox.Log("TwoPair row: [" + x3 + "," + y1 + "]");
                                                    myBoard[x3, y1].Loser(ch1, Color.Red);
                                                    myBoard[x3, y1].Loser(ch2, Color.Red);
                                                }
                                            }

                                        }

                                        secFirst = sqFirst.sector;
                                        secSecond = sqSecond.sector;

                                        // If our TwoPair are in the same sector,
                                        if (secFirst == secSecond)
                                        {
                                            // Walk every square on the board
                                            for (int y3 = 0; y3 <= 8; y3++)
                                            {
                                                for (int x3 = 0; x3 <= 8; x3++)
                                                {
                                                    // If it's in our sector ...
                                                    if (myBoard[x3,y3].sector == secFirst)
                                                    {
                                                        // But isn't either of our TwoPair squares ...
                                                        if (!(x1 == x3 && y1 == y3) &&
                                                            !(x2 == x3 && y2 == y3))
                                                        {
                                                            // It's a loser for both values.
                                                            objLogBox.Log("TwoPair sec: [" + x3 + "," + y3 + "]");
                                                            myBoard[x3, y3].Loser(ch1, Color.Red);
                                                            myBoard[x3, y3].Loser(ch2, Color.Red);
                                                        }
                                                    }
                                                }

                                            }
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
    }
}
