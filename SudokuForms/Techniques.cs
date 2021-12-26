using System;
using System.Linq;

// This is for Flavor flav. I don't understand it.
using static SudokuForms.Game;
using static SudokuForms.SortBitmask;

namespace SudokuForms
{
    // This class holds the functions used to solve the puzzle.
    class Techniques
    {

        // ==================================================================
        //
        // Call for every Row and Column and Sector.
        public static bool AllRanges(Board objBoard, LogBox objLogBox)
        {
            bool ret = false;

            for (int i = 0; i < objBoard.objGame.cDimension; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Row, i);
                ret |= RangeCheck(objBoard, objRange, objLogBox);
            }
            for (int i = 0; i < objBoard.objGame.cDimension; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Col, i);
                ret |= RangeCheck(objBoard, objRange, objLogBox);
            }
            for (int i = 0; i < objBoard.objGame.cSector; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Sec, i);
                ret |= RangeCheck(objBoard, objRange, objLogBox);
            }

            if (ret)
            {
                objLogBox.Log("AllRanges");
            }
            return ret;
        }

        // Examine a Range of the board.
        //
        // Note that objBoard.objGame.bitCount is 9 for Sudoku and HyperSudoku, 16 for SuperSudoku.
        //
        // There are nine(16) Squares in a Range. We want to examine all 512 (2^9)
        // or 65536 (2^16)
        // combinations (not permuations) of those Squares. For each combination,
        // concatenate-sort-uniq their Text strings (both Winner and CouldBes).
        // If the length matches the count of Squares, then we have an Nsome: Twosome or
        // Threesome or Foursome ... Eightsome Ninesome.
        //
        // The first test, with value 0, is binary 000000000 (or 0000000000000000). It represents
        // looking at no Squares in the Range. This is a degenerative case. 
        // It looks at no Squares, has no values, has length 0, matching the 
        // bitcount of 0, matching the Square count of 0. It's a 'Zerosome', 
        // and we would call 'Loser' on no values.
        //
        // The last test, with value 511, is binary 111111111 (or 1111111111111111). It represents
        // looking at every Square in the Range. This is a degenerative case. 
        // Every (legit) Range, whether just starting, partially or fully solved, 
        // will have all nine(16) values somewhere, thus its length of nine(16) matches its
        // bit count, its Square count. Thus, every Range has a 'Ninesome'
        // with all nine Squares. It would call 'Loser' on all nine values, but 
        // (amusingly) only on the *other* Squares in the Range, and here there are
        // none.
        //
        // Consider this Range:
        //
        //     sq0   sq1   sq2   sq3   sq4   sq5   sq6   sq7   sq8
        //   +=====+=====+=====+=====+=====+=====+=====+=====+=====+
        //   | 2 4 |     |     |     |     | 2 6 |     | 1 4 | 4 6 |
        //   |  6  | 2 4 |  9  | 1 7 |  5  |  8  |  3  | 7 8 |  8  |
        //   |     |     |     |     |     |     |     |     |     |
        //   +=====+=====+=====+=====+=====+=====+=====+=====+=====+
        //      1     1     0     0     0     1     0     0     1
        //
        // When we get to 291, binary 100100011 (low bit first, on the left 
        // Square), we combine 246+24+268+468 -> 2468. Four characters, four
        // ON bits, four Squares: it's a Foursome. On the five squares that
        // _aren't_ in that Foursome (2-3-4-6-7), call Loser('2'), Loser('4'),
        // Loser('6'), Loser('8'). That makes sq7's '1478' into '17'.
        //
        // We used to iterate through the bitmasks, 000000000 to 111111111. But 
        // that had us finding long Nsomes (like 6char), when shorter Nsomes
        // (like 2char) would have triggered too. Shorter is what the brain sees
        // first, which we want to replicate.  So iterate the bitmasks, first the
        // 1bit, then 2bit, 3bit, on up.

        public static bool RangeCheck(Board objBoard, Range objRange, LogBox objLogBox)
        {
            bool ret = false;
            string szLog = "Range " + objRange.type + objRange.i.ToString("X"); // "Col7" or "SecD"

            // all bitmasks: from 000000000 through 111111111 (or 1111111111111111)

            // This one works for Sudoku, HyperSudoku, and SuperSudoku.
            for (int bitmask = 0; bitmask < Math.Pow(2, objBoard.objGame.bitCount); bitmask++)
            // This one works for Sudoku and HyperSudoku, not SuperSudoku.
            // This one, it seems, isn't any faster.  Waste of effort?
            //foreach (int bitmask in rgBitmask)
            {
                int bitshift = bitmask;
                int bitcount = 0;
                string szTuple = "";
                bool fHighlight = false; // This is a latch: have we yet highlighted this Range?

                // Calculate bitcount, as the number of ON bits in the current bitmask.
                // Calculate szTuple ('1478') of the Squares in the Range's bitmask.
                for (int ibit = 0; ((ibit < objBoard.objGame.bitCount) && (bitshift != 0)); ibit++)
                {
                    // If the low bit is set, we want the chars of that Square.
                    if ((bitshift % 2) == 1)
                    {
                        bitcount++;
                        foreach (char ch in objRange.rgSquare[ibit].btn.Text)
                        {
                            if (!szTuple.Contains(ch) && ch != ' ')
                            {
                                szTuple += ch;
                            }
                        }
                    }
                    bitshift = (bitshift / 2);
                }

                // Does this sub-Range match the number of ON bits in the bitmask?
                if (szTuple.Length == bitcount)
                {
                    // We have found an Nsome.

                    bool ret2 = false;

                    // For all the Squares of Range that _aren't_ in bitmask, 
                    //   call Loser for all the values of szTuple.
                    bitshift = bitmask;
                    for (int ibit = 0; ibit < objBoard.objGame.bitCount; ibit++) {
                        // If the low bit is NOT set, we want that Square.
                        if ((bitshift % 2) == 0) {
                            foreach (char ch in szTuple) {
                                if (!fHighlight && objRange.rgSquare[ibit].FLoserTest(ch)) {
                                    // We have found the first Square that's
                                    // actually going to change. Highlight the Range.
                                    int bitshiftHighlight = bitmask;
                                    //if (bitcount >= 4) { ret2 = ret2; } // just for breakpoints.
                                    for (int ibitHighlight = 0; ibitHighlight < objBoard.objGame.bitCount; ibitHighlight++) {
                                        // low bit means it's part of the NSome, otherwise part of the Range.
                                        if ((bitshiftHighlight % 2) == 0) {
                                            //objRange.rgSquare[ibitHighlight].SetBackColor(objRange.rgSquare[ibitHighlight].colorRange);
                                        } else {
                                            objRange.rgSquare[ibitHighlight].SetBackColor(objRange.rgSquare[ibitHighlight].colorNSome);
                                        }
                                        bitshiftHighlight = (bitshiftHighlight / 2);
                                    }

                                    fHighlight = true; // drop the latch. We only do this once per Range.
                                }
                                ret2 |= objRange.rgSquare[ibit].FLoser(ch, objBoard);
                            }
                        }
                        bitshift = (bitshift / 2);
                    }

                    objRange.SetRangeColor(false);

                    if (ret2)
                    {
                        szLog += " '" + szTuple + "'";
                    }
                    ret |= ret2;
                }
            }

            if (ret) {
                objLogBox.Log(szLog);
            }

            // For all the Squares of Range, reset the backcolor.
            foreach (Square sq in objRange.rgSquare)
            {
                sq.SetBackColor(sq.MyBackColor());
            }

            return ret;
        }

        // ==================================================================

        /*

        Below is our sector 's'.
        Note we ignore squares with iWinner: those that are done.
        We can observe that only column 1 has a 1, 
          Therefore other squares in that column have Loser(1).
        We can observe that only row 2 has any 7s.
          Therefore other squares in that row have Loser(7).
        ------------------------------
        |         |        |         |
        |    2    |   1 3  |    5    | mpsLineText[s].row[0] = "13"
        |         |        |         |
        ------------------------------
        |         |        |         |
        |  3 6 9  |  1 3 9 |    4    | mpsLineText[s].row[1] = "369139"
        |         |        |         |
        ------------------------------
        |         |        |         |
        | 3 6 7 9 |    8   |  3 6 7  | mpsLineText[s].row[2] = "3679367"
        |         |        |         |
        ------------------------------
           ||         ||         ||
           \/         \/         \/
       mpsLineText  mpsLineText  mpsLineText
       [s].col[0]   [s].col[1]   [s].col[2]
       = "3693679"  = "13139"    = "367"

        */

        public class LineText
        {
            public LineText()
            {
                row = new string[] { "", "", "" };
                col = new string[] { "", "", "" };
            }
            public string[] row { get; set; }
            public string[] col { get; set; }
        }

        public static bool FLineFind(Board objBoard, LogBox objLogBox)
        {
            // BUGBUG This routine needs work for SuperSudoku.
            // BUGBUG We need to check foursies, not threesies.
            // BUGBUG We need to walk ch 0..F for Super, and 1..9 for Normal.

            bool ret = false;

            // REVIEW KirkG: surely this isn't right, doing 'new' both in the
            // decl and the initialization loop.  Am I leaking memory?
            LineText[] mpsLineText = new LineText[objBoard.objGame.cSector];
            for (int s = 0; s < objBoard.objGame.cSector; s++)
            {
                mpsLineText[s] = new LineText();
            }

            // Walk the board, concatenating all our Text strings into our array.
            foreach (Square sq in objBoard.rgSquare)
            {
                mpsLineText[sq.sector].row[sq.row % 3] += sq.btn.Text.Replace(" ", string.Empty);
                mpsLineText[sq.sector].col[sq.col % 3] += sq.btn.Text.Replace(" ", string.Empty);
                if (sq.hypersector != -1) {
                    mpsLineText[sq.hypersector].row[sq.row % 3] += sq.btn.Text.Replace(" ", string.Empty);
                    mpsLineText[sq.hypersector].col[sq.col % 3] += sq.btn.Text.Replace(" ", string.Empty);
                }
            }

            // Within a sector (or hypersector), find a value that is present
            // in just one row, or just one column.

            for (int s = 0; s < objBoard.objGame.cSector; s++)
            {
                for (char ch = '1'; ch <= '9'; ch++)
                {
                    if (
                        ( mpsLineText[s].row[0].Contains(ch)) &&
                        (!mpsLineText[s].row[1].Contains(ch)) &&
                        (!mpsLineText[s].row[2].Contains(ch))
                        )
                    {
                        ret |= FRowLoser(objBoard, s, 0, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].row[0].Contains(ch)) &&
                        ( mpsLineText[s].row[1].Contains(ch)) &&
                        (!mpsLineText[s].row[2].Contains(ch))
                        )
                    {
                        ret |= FRowLoser(objBoard, s, 1, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].row[0].Contains(ch)) &&
                        (!mpsLineText[s].row[1].Contains(ch)) &&
                        ( mpsLineText[s].row[2].Contains(ch))
                        )
                    {
                        ret |= FRowLoser(objBoard, s, 2, ch, objLogBox);
                    }

                    if (
                        ( mpsLineText[s].col[0].Contains(ch)) &&
                        (!mpsLineText[s].col[1].Contains(ch)) &&
                        (!mpsLineText[s].col[2].Contains(ch))
                        )
                    {
                        ret |= FColLoser(objBoard, s, 0, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].col[0].Contains(ch)) &&
                        ( mpsLineText[s].col[1].Contains(ch)) &&
                        (!mpsLineText[s].col[2].Contains(ch))
                        )
                    {
                        ret |= FColLoser(objBoard, s, 1, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].col[0].Contains(ch)) &&
                        (!mpsLineText[s].col[1].Contains(ch)) &&
                        ( mpsLineText[s].col[2].Contains(ch))
                        )
                    {
                        ret |= FColLoser(objBoard, s, 2, ch, objLogBox);
                    }
                }
             }
            if (ret)
            {
                objLogBox.Log("FLineFind");
            }
            return ret;
        }

        // For reference, our sector map:
        //  0 0 0 1 1 1 2 2 2
        //  0 0 0 1 1 1 2 2 2
        //  0 0 0 1 1 1 2 2 2
        //  3 3 3 4 4 4 5 5 5
        //  3 3 3 4 4 4 5 5 5
        //  3 3 3 4 4 4 5 5 5
        //  6 6 6 7 7 7 8 8 8
        //  6 6 6 7 7 7 8 8 8
        //  6 6 6 7 7 7 8 8 8

        private static bool FRowLoser(Board objBoard, int s, int rs, char ch, LogBox objLogBox)
        {
            // BUGBUG This routine needs work for SuperSudoku.
            // BUGBUG Whatever this crazy row-mapping calculation, SuperSize it.
            // BUGBUG Just change the 3s to 4s?

            // BUGBUG No way this is right for Hyper.  This mapping from sector
            // to row isn't going to work.

            // For squares in the same row, but not the same sector, ch is a Loser.
            // The trick is, the row value is 0-1-2, relative to the sector. We have
            // to map it to row [0-8] of the board.
            //
            // s  s/3 (s/3)*3 ((s/3)*3)+row
            // 0   0     0        0 + rs
            // 1   0     0        0 + rs
            // 2   0     0        0 + rs
            // 3   1     3        3 + rs
            // 4   1     3        3 + rs
            // 5   1     3        3 + rs
            // 6   2     6        6 + rs
            // 7   2     6        6 + rs
            // 8   2     6        6 + rs

            // If there's actually a square that changes (a real Loser), then:
            //   - Set the bg of all the matching squares IN the sector.
            //   - Call FLoser on all the matching squares NOT IN the sector.
            //   - Reset the entire row.

            bool ret = false;
            int row = ((s / 3) * 3) + rs;
            Square sq;

            // Do we have any actual Losers?
            for (int x = 0; x < objBoard.objGame.cDimension; x++)
            {
                sq = objBoard.rgSquare[x, row];
                if (sq.sector != s)
                {
                    ret |= sq.FLoserTest(ch);
                }
            }
            if (!ret) { return false; }

            // Highlight the squares in the sector, in the row, that have our character.
            // They're the reason we're doing this.
            for (int x = 0; x < objBoard.objGame.cDimension; x++)
            {
                sq = objBoard.rgSquare[x, row];
                if ((sq.sector == s) && (sq.btn.Text.Contains(ch)))
                {
                    sq.SetBackColor(sq.colorNSome);
                }
            }

            // Update the squares in the row, not in the sector, that are Losers.
            for (int x = 0; x < objBoard.objGame.cDimension; x++)
            {
                sq = objBoard.rgSquare[x, row];
                if (sq.sector != s)
                {
                    ret |= sq.FLoser(ch, objBoard);
                }
            }

            // Reset the colors for the row.
            for (int x = 0; x < objBoard.objGame.cDimension; x++)
            {
                sq = objBoard.rgSquare[x, row];
                sq.btn.BackColor = sq.MyBackColor();
                sq.btn.Refresh();
            }

            if (ret)
            {
                //objLogBox.Log("LineFind: in sector " + s + ", only row " + row + " has char " + ch);
            }

            return ret;
        }

        private static bool FColLoser(Board objBoard, int s, int cs, char ch, LogBox objLogBox)
        {
            // For squares in the same column, but not the same sector, ch is a Loser.
            // The trick is, the col value is 0-1-2, relative to the sector. We have
            // to map it to col [0-8] of the board.
            //
            // s  s%3 (s%3)*3 ((s%3)*3)+col
            // 0   0     0       0 + cs
            // 1   1     3       3 + cs
            // 2   2     6       6 + cs
            // 3   0     0       0 + cs
            // 4   1     3       3 + cs
            // 5   2     6       6 + cs
            // 6   0     0       0 + cs
            // 7   1     3       3 + cs
            // 8   2     6       6 + cs

            bool ret = false;
            int col = ((s % 3) * 3) + cs;
            Square sq;

            // Do we have any actual Losers?
            for (int y = 0; y < objBoard.objGame.cDimension; y++)
            {
                sq = objBoard.rgSquare[col, y];
                if (sq.sector != s)
                {
                    ret |= sq.FLoserTest(ch);
                }
            }
            if (!ret) { return false; }

            // Highlight the squares in the sector, in the col, that have our char.
            // They're the reason we're doing this.
            for (int y = 0; y < objBoard.objGame.cDimension; y++)
            {
                sq = objBoard.rgSquare[col, y];
                if ((sq.sector == s) && (sq.btn.Text.Contains(ch)))
                {
                    sq.SetBackColor(sq.colorNSome);
                }
            }

            // Update the squares in the col, not in the sector, that are Losers.
            for (int y = 0; y < objBoard.objGame.cDimension; y++)
            {
                sq = objBoard.rgSquare[col, y];
                if (sq.sector != s)
                {
                    ret |= sq.FLoser(ch, objBoard);
                }
            }

            // Reset the colors for the row.
            for (int y = 0; y < objBoard.objGame.cDimension; y++)
            {
                sq = objBoard.rgSquare[col, y];
                sq.btn.BackColor = sq.MyBackColor();
                sq.btn.Refresh();
            }

            if (ret)
            {
                //objLogBox.Log("LineFind: in sector " + s + ", only col " + col + " has char " + ch);
            }

            return ret;
        }

        // ==================================================================
        //
        // This is the flipside version of FLineFind, above.
        //
        // For each row and column
        //   For ch 1 through 9
        //     If ch is found in only one sector of the row|column
        //       Call FLoser(ch) on the squares of the sector, not in our row|column.

        public static bool FSectorsFind(Board objBoard, LogBox objLogBox)
        {
            bool ret = false;

            for (int i = 0; i < objBoard.objGame.cDimension; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Col, i);
                ret |= FSectorFind(objBoard, objRange, objLogBox);
            }
            for (int i = 0; i < objBoard.objGame.cDimension; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Row, i);
                ret |= FSectorFind(objBoard, objRange, objLogBox);
            }

            return ret;
        }

        public static bool FSectorFind(Board objBoard, Range objRange, LogBox objLogBox)
        {
            // BUGBUG: needs work for SuperSudoku.

            bool ret = false;
            bool ret2;

            // What sector(s) we've seen for each charater, across all the squares of
            // this range.
            //   -1 : no sectors
            //   0 .. 9 : the only sector seen by that char
            //   -2 : multiple sectors

            int[] mpchs = new int[objBoard.objGame.cDimension];
            foreach (int ichs in mpchs) { mpchs[ichs] = -1;  }

            foreach (Square sq in objRange.rgSquare)
            {
                for (char ch = '1'; ch <= '9'; ch++)
                {
                    int ich = ch - '1';
                    if (sq.btn.Text.Contains(ch))
                    {
                        switch (mpchs[ich])
                        {
                            case -1:
                                mpchs[ich] = sq.sector;
                                break;
                            // 0 .. 9
                            default:
                                if (mpchs[ich] != sq.sector)
                                {
                                    mpchs[ich] = -2;
                                }
                                break;
                            case -2:
                                break;
                        }
                    }
                }
            }

            /*
            String szLog = "FSectorFind ";
            if (objRange.type == Range.Type.Col)
            {
                szLog += "col" + objRange.i + ":";
            }
            else
            {
                szLog += "row" + objRange.i + ":";
            }
            for (int ich = 0; ich <= 8; ich++)
            {
                szLog += " " + mpchs[ich];
            }
            objLogBox.Log(szLog);
            */

            for (int ich = 0; ich < objBoard.objGame.cDimension; ich++)
            {
                // BUGBUG Fix for SuperSudoku.
                char ch = (char)(ich + '1');
                int sec = mpchs[ich];

                foreach (Square sq in objBoard.rgSquare)
                {
                    if (sq.sector == sec)
                    {
                        // The value ch was found (in our row|column) _only_ in
                        // this sector. If this square is not in the row|column
                        // of our Range, then ch is a Loser.
                        switch (objRange.type)
                        {
                            case Range.Type.Row:
                                if (sq.row != objRange.i)
                                {
                                    ret2 = sq.FLoser(ch, objBoard);
                                    if (ret2)
                                    {
                                        objLogBox.Log("FSectorFind row s" + sec + " r" + sq.row + " c" + sq.col + " ch" + ch);
                                        ret = ret2;
                                    }
                                }
                                break;
                            case Range.Type.Col:
                                if (sq.col != objRange.i)
                                {
                                    ret2 = sq.FLoser(ch, objBoard);
                                    if (ret2)
                                    {
                                        objLogBox.Log("FSectorFind col s" + sec + " r" + sq.row + " c" + sq.col + " ch" + ch);
                                        ret = ret2;
                                    }
                                }
                                break;
                            case Range.Type.Sec:
                                // This shouldn't happen.
                                break;
                        }
                    }
                }
            }

            return ret;
        }

    }
}
