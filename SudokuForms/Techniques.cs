using System;
using System.Linq;

namespace SudokuForms
{
    // This class holds the functions used to solve the puzzle.
    class Techniques
    {

        // ==================================================================
        //
        // Keep calling all the Techniques, until they all crap out.
        public static bool Go(Board objBoard, LogBox objLogBox)
        {
            bool fProgress = false; // Did we do make any progress?
            bool fContinue = true;  // Should we keep going?
            while (fContinue)
            {
                fContinue = false;
                while (AllRanges(objBoard, objLogBox))    { fContinue = true; fProgress = true;  }
                while (FLineFind(objBoard, objLogBox))    { fContinue = true; fProgress = true; }
                while (FSectorsFind(objBoard, objLogBox)) { fContinue = true; fProgress = true; }
                while (XWing(objBoard, objLogBox))        { fContinue = true; fProgress = true; }
            }
            return fProgress;
        }

        // ==================================================================
        //
        // Call for every Row and Column and Sector.
        public static bool AllRanges(Board objBoard, LogBox objLogBox)
        {
            bool ret = false;

            for (int i = 0; i < objBoard.cDimension; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Row, i);
                ret |= RangeCheck(objBoard, objRange, objLogBox);
            }
            for (int i = 0; i < objBoard.cDimension; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Col, i);
                ret |= RangeCheck(objBoard, objRange, objLogBox);
            }
            for (int i = 0; i < objBoard.cSector; i++)
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
        // Note that objBoard.bitCount is 9 for Sudoku and HyperSudoku, 16 for SuperSudoku.
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

        // -----------------------------------------------------------------
        //
        // We are implementing a speed hack. Checking every bitmask for an Nsome,
        // especially in a near-finished board, and especially in SuperSudoku,
        // takes _forever_. It's likely some horrible O(n^5).
        //
        // Consider this nearly-finished row:
        //    [14] [14] [3] [6] [7] [8] [2] [5] [9]
        // The only Nsome your brain would care about is '14', which (still) 
        // produces nothing.  But the full bitmask sweep will match on 143, 146,
        // 147, ... 149, and then 1436, 1437, ... 1439, all the way out to 
        // the 8some 14367825. All are Nsomes, none produces any change.
        //
        // Every finished row is even more absurd:
        //    [1] [4] [3] [6] [7] [8] [2] [5] [9]
        // Here, _every_ bitmask produces an Nsome, and yet we (of course) get
        // no change. The mask 000000111 produces '259', a 3some, so we call
        // Loser(2) Loser(5) Loser(9), but of course nothing changes.
        //
        // The speed hack is as follows:
        //   If the bitmask is of length 1, use it.
        //     [8] - call Loser(8) on the rest of the row
        //   Else if the bitmask has any 'Winner' squares (length 1), skip it.
        //     [14] [14] [3] - letting [3] through (above rule) will do the same.
        //     [1] [123] [123] - let [1] through, shrink this to [23] [23].
        //   Otherwise, go for it:
        //     [14] [14] - call Loser(1) and Loser(4) elsewhere on the row.
        //     [12] [123] [13] - call Loser(1,2,3) elsewhere on the row.
        // -----------------------------------------------------------------

        // This is an alternate method, via SortBitmask.cs.  We are not currently 
        // using it, if for no other reason than SuperSudoku. The (generated) 
        // rgBitmask array of ints swells from (2^9) 512 ints to (2^16) 64K!
        //   We used to iterate through the bitmasks, 000000000 to 111111111. But 
        //   that had us finding long Nsomes (like 6char), when shorter Nsomes
        //   (like 2char) would have triggered too. Shorter is what the brain sees
        //   first, which we want to replicate.  So iterate the bitmasks, first the
        //   1bit, then 2bit, 3bit, on up.

        private static bool RangeCheck(Board objBoard, Range objRange, LogBox objLogBox)
        {
            bool ret = false;
            string szLog = "Range " + objRange.type + objRange.i.ToString("X"); // "Col7" or "SecD"

            // all bitmasks: from 000000000 through 111111111 (or 1111111111111111)
            // This one works for Sudoku, HyperSudoku, and SuperSudoku.
            for (int bitmask = 0; bitmask < Math.Pow(2, objBoard.bitCount); bitmask++)
            // This one works for Sudoku and HyperSudoku, not SuperSudoku.
            // This one, it seems, isn't any faster.  Waste of effort?
            //foreach (int bitmask in rgBitmask)
            {
                int bitshift = bitmask;
                int bitcount = 0;
                string szTuple = "";
                bool fWinner = false; // This is a latch: are any of our squares Winners?
                bool fHighlight = false; // This is a latch: have we yet highlighted this Range?

                // Calculate bitcount, as the number of ON bits in the current bitmask.
                // Calculate szTuple ('1478') of the Squares in the Range's bitmask.
                for (int ibit = 0; ((ibit < objBoard.bitCount) && (bitshift != 0)); ibit++)
                {
                    // If the low bit is set, we want the chars of that Square.
                    if ((bitshift % 2) == 1)
                    {
                        bitcount++;
                        foreach (char ch in objRange.rgSquare[ibit].btn.Text)
                        {
                            if (objRange.rgSquare[ibit].iWinner != -1)
                            {
                                fWinner = true; // There is at least one single-char square in our tuple.
                            }
                            if (!szTuple.Contains(ch) && ch != ' ')
                            {
                                szTuple += ch;
                            }
                        }
                    }
                    bitshift = (bitshift / 2);
                }

                if (
                    // 1. Does this sub-Range match the number of ON bits in the bitmask?
                    (szTuple.Length == bitcount) &&
                    // 2. Apply our above speed-hack: continue if length=1 or no winners found.
                    (bitcount == 1 || !fWinner)
                   )
                {
                    // We have found an Nsome.

                    bool ret2 = false;

                    // For all the Squares of Range that _aren't_ in bitmask, 
                    //   call Loser for all the values of szTuple.
                    bitshift = bitmask;
                    for (int ibit = 0; ibit < objBoard.bitCount; ibit++) {
                        // If the low bit is NOT set, we want that Square.
                        if ((bitshift % 2) == 0) {
                            foreach (char ch in szTuple) {
                                if (!fHighlight && objRange.rgSquare[ibit].FLoserTest(ch)) {
                                    // We have found the first Square that's
                                    // actually going to change. Highlight the Range.
                                    int bitshiftHighlight = bitmask;
                                    for (int ibitHighlight = 0; ibitHighlight < objBoard.bitCount; ibitHighlight++) {
                                        // low bit means it's part of the NSome, otherwise part of the Range.
                                        if ((bitshiftHighlight % 2) != 0) {
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

                    objRange.ResetRangeColor();

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

            return ret;
        }

        // ==================================================================

        /*

        Each sector (in mpsLineText) has a LineText object, containing 
        six strings:
            Three for the sector's rows: the string is the text of the entire row
            Three for the sector's cols: the string is the text of the entire col

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
                // For Sudoku+Hyper, we need three; for Super we need four.
                row = new string[] { "", "", "", "" };
                col = new string[] { "", "", "", "" };
            }
            public string[] row { get; set; }
            public string[] col { get; set; }
        }

        private static int[] mpTabHyperSR =
        {
            -1, -1,-1,-1, -1, -1,-1,-1, -1,
            -1, 00,00,00, -1, 00,00,00, -1,
            -1, 01,01,01, -1, 01,01,01, -1,
            -1, 02,02,02, -1, 02,02,02, -1,
            -1, -1,-1,-1, -1, -1,-1,-1, -1,
            -1, 00,00,00, -1, 00,00,00, -1,
            -1, 01,01,01, -1, 01,01,01, -1,
            -1, 02,02,02, -1, 02,02,02, -1,
            -1, -1,-1,-1, -1, -1,-1,-1, -1
        };
        private static int[] mpTabHyperSC =
        {
            -1, -1,-1,-1, -1, -1,-1,-1, -1,
            -1, 00,01,02, -1, 00,01,02, -1,
            -1, 00,01,02, -1, 00,01,02, -1,
            -1, 00,01,02, -1, 00,01,02, -1,
            -1, -1,-1,-1, -1, -1,-1,-1, -1,
            -1, 00,01,02, -1, 00,01,02, -1,
            -1, 00,01,02, -1, 00,01,02, -1,
            -1, 00,01,02, -1, 00,01,02, -1,
            -1, -1,-1,-1, -1, -1,-1,-1, -1
        };

        public static bool FLineFind(Board objBoard, LogBox objLogBox)
        {
            bool ret;

            // REVIEW KirkG: surely this isn't right, doing 'new' both in the
            // decl and the initialization loop.  Am I leaking memory?
            LineText[] mpsLineText = new LineText[objBoard.cSector];
            for (int s = 0; s < objBoard.cSector; s++)
            {
                mpsLineText[s] = new LineText();
            }

            // Walk the board, concatenating all our Text strings into our array.
            foreach (Square sq in objBoard.rgSquare)
            {
                if (objBoard.fSuper) {
                    mpsLineText[sq.sector].row[sq.row % 4] += sq.btn.Text.Replace(" ", string.Empty);
                    mpsLineText[sq.sector].col[sq.col % 4] += sq.btn.Text.Replace(" ", string.Empty);
                } else {
                    mpsLineText[sq.sector].row[sq.row % 3] += sq.btn.Text.Replace(" ", string.Empty);
                    mpsLineText[sq.sector].col[sq.col % 3] += sq.btn.Text.Replace(" ", string.Empty);
                }
                if (sq.hypersector != -1) {
                    mpsLineText[sq.hypersector].row[mpTabHyperSR[sq.iBoard]] += sq.btn.Text.Replace(" ", string.Empty);
                    mpsLineText[sq.hypersector].col[mpTabHyperSC[sq.iBoard]] += sq.btn.Text.Replace(" ", string.Empty);
                }
            }

            // Within a sector (or hypersector), find a value that is present
            // in just one row, or just one column.

            if (objBoard.fSuper) {
                ret = SectorTestSuper(objBoard, objLogBox, mpsLineText);
            }  else {
                ret = SectorTest(objBoard, objLogBox, mpsLineText);
            }

            if (ret)
            {
                objLogBox.Log("FLineFind");
            }
            return ret;
        }

        private static char[] rgchSudoku = {'1','2','3','4','5','6','7','8','9'};

        private static bool SectorTest (Board objBoard, LogBox objLogBox, LineText[] mpsLineText)
        {
            bool ret = false;
            for (int s = 0; s < objBoard.cSector; s++)
            {
                foreach (char ch in rgchSudoku)
                {
                    if (
                        (mpsLineText[s].row[0].Contains(ch)) &&
                        (!mpsLineText[s].row[1].Contains(ch)) &&
                        (!mpsLineText[s].row[2].Contains(ch))
                        )
                    {
                        ret |= FRowLoser(objBoard, s, 0, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].row[0].Contains(ch)) &&
                        (mpsLineText[s].row[1].Contains(ch)) &&
                        (!mpsLineText[s].row[2].Contains(ch))
                        )
                    {
                        ret |= FRowLoser(objBoard, s, 1, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].row[0].Contains(ch)) &&
                        (!mpsLineText[s].row[1].Contains(ch)) &&
                        (mpsLineText[s].row[2].Contains(ch))
                        )
                    {
                        ret |= FRowLoser(objBoard, s, 2, ch, objLogBox);
                    }

                    if (
                        (mpsLineText[s].col[0].Contains(ch)) &&
                        (!mpsLineText[s].col[1].Contains(ch)) &&
                        (!mpsLineText[s].col[2].Contains(ch))
                        )
                    {
                        ret |= FColLoser(objBoard, s, 0, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].col[0].Contains(ch)) &&
                        (mpsLineText[s].col[1].Contains(ch)) &&
                        (!mpsLineText[s].col[2].Contains(ch))
                        )
                    {
                        ret |= FColLoser(objBoard, s, 1, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].col[0].Contains(ch)) &&
                        (!mpsLineText[s].col[1].Contains(ch)) &&
                        (mpsLineText[s].col[2].Contains(ch))
                        )
                    {
                        ret |= FColLoser(objBoard, s, 2, ch, objLogBox);
                    }
                }
            }
            return ret;
        }

        private static char[] rgchSuper = { '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F' };

        private static bool SectorTestSuper (Board objBoard, LogBox objLogBox, LineText[] mpsLineText)
        {
            bool ret = false;
            for (int s = 0; s < objBoard.cSector; s++)
            {
                foreach (char ch in rgchSuper)
                {
                    if (
                        ( mpsLineText[s].row[0].Contains(ch)) &&
                        (!mpsLineText[s].row[1].Contains(ch)) &&
                        (!mpsLineText[s].row[2].Contains(ch)) &&
                        (!mpsLineText[s].row[3].Contains(ch))
                    )
                    {
                        ret |= FRowLoser(objBoard, s, 0, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].row[0].Contains(ch)) &&
                        ( mpsLineText[s].row[1].Contains(ch)) &&
                        (!mpsLineText[s].row[2].Contains(ch)) &&
                        (!mpsLineText[s].row[3].Contains(ch))
                    )
                    {
                        ret |= FRowLoser(objBoard, s, 1, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].row[0].Contains(ch)) &&
                        (!mpsLineText[s].row[1].Contains(ch)) &&
                        ( mpsLineText[s].row[2].Contains(ch)) &&
                        (!mpsLineText[s].row[3].Contains(ch))
                    )
                    {
                        ret |= FRowLoser(objBoard, s, 2, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].row[0].Contains(ch)) &&
                        (!mpsLineText[s].row[1].Contains(ch)) &&
                        (!mpsLineText[s].row[2].Contains(ch)) &&
                        ( mpsLineText[s].row[3].Contains(ch))
                    )
                    {
                        ret |= FRowLoser(objBoard, s, 3, ch, objLogBox);
                    }
                    // --------------------
                    if (
                        ( mpsLineText[s].col[0].Contains(ch)) &&
                        (!mpsLineText[s].col[1].Contains(ch)) &&
                        (!mpsLineText[s].col[2].Contains(ch)) &&
                        (!mpsLineText[s].col[3].Contains(ch))
                    )
                    {
                        ret |= FColLoser(objBoard, s, 0, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].col[0].Contains(ch)) &&
                        ( mpsLineText[s].col[1].Contains(ch)) &&
                        (!mpsLineText[s].col[2].Contains(ch)) &&
                        (!mpsLineText[s].col[3].Contains(ch))
                    )
                    {
                        ret |= FColLoser(objBoard, s, 1, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].col[0].Contains(ch)) &&
                        (!mpsLineText[s].col[1].Contains(ch)) &&
                        ( mpsLineText[s].col[2].Contains(ch)) &&
                        (!mpsLineText[s].col[3].Contains(ch))
                    )
                    {
                        ret |= FColLoser(objBoard, s, 2, ch, objLogBox);
                    }
                    if (
                        (!mpsLineText[s].col[0].Contains(ch)) &&
                        (!mpsLineText[s].col[1].Contains(ch)) &&
                        (!mpsLineText[s].col[2].Contains(ch)) &&
                        ( mpsLineText[s].col[3].Contains(ch))
                    )
                    {
                        ret |= FColLoser(objBoard, s, 3, ch, objLogBox);
                    }
                }
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

        // Map Sector + Row-Sector to actual Row.
        private static int[,] mpsrs2r = {
                { 0,1,2 },  // Sector 0 maps to Rows 0,1,2
                { 0,1,2 },  // Sector 1
                { 0,1,2 },  // Sector 2
                { 3,4,5 },  // Sector 3 maps to Rows 3,4,5
                { 3,4,5 },  // Sector 4
                { 3,4,5 },  // Sector 5
                { 6,7,8 },  // Sector 6 maps to Rows 6,7,8
                { 6,7,8 },  // Sector 7
                { 6,7,8 },  // Sector 8
                { 1,2,3 },  // HypSec 9 maps to Rows 1,2,3
                { 1,2,3 },  // HypSec A
                { 5,6,7 },  // HypSec B maps to Rows 5,6,7
                { 5,6,7 }   // HypSec C
            };

        // Map Sector + Col-Sector to actual Column,
        private static int[,] mpscs2c = {
                { 0,1,2 },  // Sector 0 maps to Cols 0,1,2
                { 3,4,5 },  // Sector 1              3,4,5
                { 6,7,8 },  // Sector 2              6,7,8
                { 0,1,2 },  // Sector 3 maps to Cols 0,1,2
                { 3,4,5 },  // Sector 4              3,4,5
                { 6,7,8 },  // Sector 5              6,7,8
                { 0,1,2 },  // Sector 6 maps to Cols 0,1,2
                { 3,4,5 },  // Sector 7              3,4,5
                { 6,7,8 },  // Sector 8              6,7,8
                { 1,2,3 },  // HypSec 9 maps to Cols 1,2,3
                { 5,6,7 },  // HypSec A              5,6,7
                { 1,2,3 },  // HypSec B maps to Cols 1,2,3
                { 5,6,7 }   // HypSec C              5,6,7
            };

        // Map Sector + Row-Sector to actual Row.
        private static int[,] mpsrs2rSuper = {
                {   0,  1,  2,  3 },  // Sector 0 maps to Rows 0,1,2,3
                {   0,  1,  2,  3 },  // Sector 1
                {   0,  1,  2,  3 },  // Sector 2
                {   0,  1,  2,  3 },  // Sector 3
                {   4,  5,  6,  7 },  // Sector 4 maps to Rows 4,5,6,7
                {   4,  5,  6,  7 },  // Sector 5
                {   4,  5,  6,  7 },  // Sector 6
                {   4,  5,  6,  7 },  // Sector 7
                {   8,  9,0xA,0xB },  // Sector 8 maps to Rows 8,9,A,B
                {   8,  9,0xA,0xB },  // Sector 9
                {   8,  9,0xA,0xB },  // Sector A
                {   8,  9,0xA,0xB },  // Sector B
                { 0xC,0xD,0xE,0xF },  // Sector C maps to Rows C,D,E,F
                { 0xC,0xD,0xE,0xF },  // Sector D
                { 0xC,0xD,0xE,0xF },  // Sector E
                { 0xC,0xD,0xE,0xF }   // Sector F
            };

        // Map Sector + Col-Sector to actual Column,
        private static int[,] mpscs2cSuper = {
                {   0,  1,  2,  3 },  // Sector 0 maps to Cols 0,1,2,3
                {   4,  5,  6,  7 },  // Sector 1              4,5,6,7
                {   8,  9,0xA,0xB },  // Sector 2              8,9,A,B
                { 0xC,0xD,0xE,0xF },  // Sector 3              C,D,E,F
                {   0,  1,  2,  3 },  // Sector 4
                {   4,  5,  6,  7 },  // Sector 5
                {   8,  9,0xA,0xB },  // Sector 6
                { 0xC,0xD,0xE,0xF },  // Sector 7
                {   0,  1,  2,  3 },  // Sector 8
                {   4,  5,  6,  7 },  // Sector 9
                {   8,  9,0xA,0xB },  // Sector A
                { 0xC,0xD,0xE,0xF },  // Sector B
                {   0,  1,  2,  3 },  // Sector C
                {   4,  5,  6,  7 },  // Sector D
                {   8,  9,0xA,0xB },  // Sector E
                { 0xC,0xD,0xE,0xF }   // Sector F
            };

        private static bool FRowLoser(Board objBoard, int s, int rs, char ch, LogBox objLogBox)
        {
            // For squares in the same row, but not the same sector, ch is a Loser.
            // The trick is, the row value is 0-1-2, relative to the sector. We have
            // to map it to row [0-8] of the board.

            // If there's actually a square that changes (a real Loser), then:
            //   - Set the bg of all the matching squares IN the sector.
            //   - Call FLoser on all the matching squares NOT IN the sector.
            //   - Reset the entire row.

            bool ret = false;
            Square sq;
            int row;

            string szLog = "LineFind Sec" + s.ToString("X") + " Row" + rs + " '" + ch + "'"; // "Sec3 Row0 '2'"

            if (objBoard.fSuper) { row = mpsrs2rSuper[s, rs]; } else { row = mpsrs2r[s, rs]; }

            // Do we have any actual Losers?
            for (int x = 0; x < objBoard.cDimension; x++)
            {
                sq = objBoard.rgSquare[x, row];
                if ((sq.sector != s) && (sq.hypersector != s))
                {
                    ret |= sq.FLoserTest(ch);
                }
            }
            if (!ret) { return false; }

            // Highlight the squares in the sector, in the row, that have our character.
            // They're the reason we're doing this.
            for (int x = 0; x < objBoard.cDimension; x++)
            {
                sq = objBoard.rgSquare[x, row];
                if (((sq.sector == s) || (sq.hypersector == s)) && (sq.btn.Text.Contains(ch)))
                {
                    sq.SetBackColor(sq.colorNSome);
                }
            }

            // Update the squares in the row, not in the sector, that are Losers.
            for (int x = 0; x < objBoard.cDimension; x++)
            {
                sq = objBoard.rgSquare[x, row];
                if ((sq.sector != s) && (sq.hypersector != s))
                {
                    ret |= sq.FLoser(ch, objBoard);
                }
            }

            // Reset the colors for the row.
            for (int x = 0; x < objBoard.cDimension; x++)
            {
                sq = objBoard.rgSquare[x, row];
                sq.btn.BackColor = sq.MyBackColor();
                sq.btn.Refresh();
            }

            if (ret)
            {
                objLogBox.Log(szLog);
            }

            return ret;
        }

        private static bool FColLoser(Board objBoard, int s, int cs, char ch, LogBox objLogBox)
        {
            bool ret = false;
            Square sq;
            int col;

            string szLog = "LineFind Sec" + s.ToString("X") + " Col" + cs + " '" + ch + "'"; // "SecB Col2 '4'"

            if (objBoard.fSuper) { col = mpscs2cSuper[s, cs]; } else { col = mpscs2c[s, cs]; }

            // Do we have any actual Losers?
            for (int y = 0; y < objBoard.cDimension; y++)
            {
                sq = objBoard.rgSquare[col, y];
                if ((sq.sector != s) && (sq.hypersector != s))
                {
                    ret |= sq.FLoserTest(ch);
                }
            }
            if (!ret) { return false; }

            // Highlight the squares in the sector, in the col, that have our char.
            // They're the reason we're doing this.
            for (int y = 0; y < objBoard.cDimension; y++)
            {
                sq = objBoard.rgSquare[col, y];
                if (((sq.sector == s) || (sq.hypersector == s)) && (sq.btn.Text.Contains(ch)))
                {
                    sq.SetBackColor(sq.colorNSome);
                }
            }

            // Update the squares in the col, not in the sector, that are Losers.
            for (int y = 0; y < objBoard.cDimension; y++)
            {
                sq = objBoard.rgSquare[col, y];
                if ((sq.sector != s) && (sq.hypersector != s))
                {
                    ret |= sq.FLoser(ch, objBoard);
                }
            }

            // Reset the colors for the row.
            for (int y = 0; y < objBoard.cDimension; y++)
            {
                sq = objBoard.rgSquare[col, y];
                sq.btn.BackColor = sq.MyBackColor();
                sq.btn.Refresh();
            }

            if (ret)
            {
                objLogBox.Log(szLog);
            }

            return ret;
        }

        // ==================================================================
        //
        // This is the flipside version of FLineFind, above.
        //
        // For each row and column
        //   For ch 1..9 or 0..F
        //     If ch is found in only one sector of the row|column
        //       Call FLoser(ch) on the squares of the sector, not in our row|column.

        public static bool FSectorsFind(Board objBoard, LogBox objLogBox)
        {
            bool ret = false;

            for (int i = 0; i < objBoard.cDimension; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Col, i);
                ret |= FSectorFind(objBoard, objRange, objLogBox);
            }
            for (int i = 0; i < objBoard.cDimension; i++)
            {
                Range objRange = new Range(objBoard, Range.Type.Row, i);
                ret |= FSectorFind(objBoard, objRange, objLogBox);
            }

            return ret;
        }

        public static bool FSectorFind(Board objBoard, Range objRange, LogBox objLogBox)
        {
            // What sector(s) we've seen for each character, across all the squares of
            // this range.
            //   -1 : no sectors - init value, will get reset (we see all chars).
            //   0..9 or 0..F : the only sector seen by that char
            //   -2 : multiple sectors

            char[] rgch;
            if (objBoard.fSuper) { rgch = rgchSuper; } else { rgch = rgchSudoku; }

            int[] mpchs = new int[objBoard.cDimension];
            for (int ich = 0; ich < objBoard.cDimension; ich++) { mpchs[ich] = -1;  }

            foreach (Square sq in objRange.rgSquare)
            {
                for (int ich = 0; ich < objBoard.cDimension; ich++)
                {
                    if (sq.btn.Text.Contains(rgch[ich]))
                    {
                        switch (mpchs[ich])
                        {
                            case -1:
                                mpchs[ich] = sq.sector;
                                break;
                            // 0..8 or 0..F
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

            String szLog = "FSectorFind ";
            if (objRange.type == Range.Type.Col) {
                szLog += "col" + objRange.i.ToString("X") + ":";
            } else {
                szLog += "row" + objRange.i.ToString("X") + ":";
            }
            for (int ich = 0; ich < objBoard.cDimension; ich++)
            {
                if (mpchs[ich] == -2) {
                    szLog += " X";
                } else {
                    szLog += " " + mpchs[ich].ToString("X");
                }
            }
            //objLogBox.Log(szLog);

            bool ret = false;

            for (int ich = 0; ich < objBoard.cDimension; ich++)
            {
                char ch = rgch[ich];
                int sec = mpchs[ich];

                if (sec >= 0) // ignore when sec = -1 or -2
                {
                    foreach (Square sq in objBoard.rgSquare)
                    {
                        if (sq.sector == sec)
                        {
                            // The value ch was found (in our row|column) _only_ in
                            // this sector. If this square is not in the row|column
                            // of our Range, then ch is a Loser.

                            // BUGBUG: These objLogBox entries aren't very good.  They show
                            // the square getting reset, but not the square(s) causing it.

                            switch (objRange.type)
                            {
                                case Range.Type.Row:
                                    if (sq.row != objRange.i)
                                    {
                                        if (sq.FLoser(ch, objBoard))
                                        {
                                            objLogBox.Log("FSectorFind row s" + sec.ToString("X") + " r" + sq.row.ToString("X") + " c" + sq.col.ToString("X") + " '" + ch + "'");
                                            ret = true;
                                        }
                                    }
                                    break;
                                case Range.Type.Col:
                                    if (sq.col != objRange.i)
                                    {
                                        if (sq.FLoser(ch, objBoard))
                                        {
                                            objLogBox.Log("FSectorFind col s" + sec.ToString("X") + " r" + sq.row.ToString("X") + " c" + sq.col.ToString("X") + " '" + ch + "'");
                                            ret = true;
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
            }

            objRange.ResetRangeColor();

            return ret;
        }

        // ==================================================================
        //
        // https://www.learn-sudoku.com/x-wing.html
        //
        // 'rcs' = 'row or column or sector'
        // (I don't think that X-Wing is ever applied to Sectors.)
        //
        // Count the chars in each rcs
        // These two rows each have two 4s, so they're our X-Wing.
        //                                      1 2 3 4 5 6 7 8 9
        //   [1][49][69][3][469][5][7][2][8] -> 1 1 1 2 1 2 1 1 2
        //   [8][34] [1][9] [34][6][2][5][7] -> 1 1 2 2 1 1 1 1 1
        //                               Our X-Wing : ^
        // For each character count == 2, look for another row in the
        // array where the same offset is also 2.  That's our X-Wing.
        // 

        public static bool XWing(Board objBoard, LogBox objLogBox)
        {
            bool fRet = false;
            int ich;

            // For each row|col|sec, the count of each char across the row|col|sec
            int[,] mpicchRow = new int[objBoard.cDimension, objBoard.cDimension];
            int[,] mpicchCol = new int[objBoard.cDimension, objBoard.cDimension];
            //int[,] mpicchSec = new int[objBoard.cDimension, objBoard.cDimension];

            if (objBoard.fSuper)
            {
                foreach (Square sq in objBoard.rgSquare)
                {
                    foreach (char ch in sq.btn.Text)
                    {
                        if (ch != ' ')
                        {
                            // Super
                            //   '0'..'9' --> 0x30..0x39 --> 0..9 : subtract '0'
                            //   'A'..'F' --> 0x65..0x70 --> A..F : subtract 'A', add 10
                            if (ch <= '9') {
                                ich = ch - '0';
                            } else {
                                ich = ch - 'A' + 10;
                            }
                            mpicchRow[sq.row, ich]++;
                            mpicchCol[sq.col, ich]++;
                            //mpicchSec[sq.sector, ich]++;
                        }
                    }
                }
            }
            else
            {
                foreach (Square sq in objBoard.rgSquare)
                {
                    foreach (char ch in sq.btn.Text)
                    {
                        if (ch != ' ')
                        {
                            // Sudoku and Hyper
                            //   '1'..'9' --> 0x31..0x39 --> 0..8 : subtract '1'
                            ich = ch - '1';
                            mpicchRow[sq.row, ich]++;
                            mpicchCol[sq.col, ich]++;
                            //mpicchSec[sq.sector, ich]++;
                        }
                    }
                }
            }

            for (int i1 = 0; i1 < objBoard.cDimension; i1++)
            {
                for (int j = 0; j < objBoard.cDimension; j++)
                {
                    string szj = (objBoard.fSuper ? (j).ToString("X") : (j + 1).ToString("X"));

                    int i1jj;   // The two locations of j within row|col i1
                    int i2jj;   // The two locations of j within row|col i2

                    // Row:
                    if (mpicchRow[i1,j] == 2)
                    {
                        // We have found that the ith row has two "j" characters.
                        // Is there another row, same value of j, that's also 2?
                        // Note we start looking in the row _after_ where we just matched.
                        for (int i2 = i1 + 1; i2 < objBoard.cDimension; i2++)
                        {
                            if (mpicchRow[i2,j] == 2)
                            {
                                // Rows i1 and i2 have two 'j' characters.  Are they in the same columns?
                                i1jj = FindChsRow(objBoard, i1, j);
                                i2jj = FindChsRow(objBoard, i2, j);
                                //objLogBox.Log("XWing: Row" + i1.ToString("X") + "(" + i1jj.ToString("X").PadLeft(2, '0') + ") " +
                                //                     "and Row" + i2.ToString("X") + "(" + i2jj.ToString("X").PadLeft(2, '0') + ") " +
                                //                     "have two " + szj + "s");
                                if (i1jj == i2jj)
                                {
                                    int col1 = i1jj / 16; // divide, the first (hex) digit
                                    int col2 = i1jj % 16; // modulo or remainder, the second (hex) digit

                                    // Rows i1 and i2 have the 'j' character in columns col1 and col2
                                    objLogBox.Log("XWing: Rows " + i1.ToString("X") + " and " + i2.ToString("X") + 
                                                        " in cols [" + i2jj.ToString("X").PadLeft(2, '0') + "] " +
                                                         "have " + szj + "s");

                                    // Call FLoser('j') in the two columns of jj, except in rows i1 and i2.
                                    for (int cRow = 0; cRow < objBoard.cDimension; cRow++)
                                    {
                                        if ((cRow != i1) && (cRow != i2))
                                        {
                                            fRet |= objBoard.rgSquare[col1, cRow].FLoser(szj[0], objBoard);
                                            fRet |= objBoard.rgSquare[col2, cRow].FLoser(szj[0], objBoard);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Column:
                    if (mpicchCol[i1,j] == 2)
                    {
                        // We have found that the ith col has two "j" characters.
                        // Is there another col, same value of j, that's also 2?
                        // Note we start looking in the col _after_ where we just matched.
                        for (int i2 = i1 + 1; i2 < objBoard.cDimension; i2++)
                        {
                            if (mpicchCol[i2,j] == 2)
                            {
                                // Col i1 and i2 have two 'j' characters.  Are they in the same rows?
                                i1jj = FindChsCol(objBoard, i1, j);
                                i2jj = FindChsCol(objBoard, i2, j);
                                //objLogBox.Log("XWing: Col" + i1.ToString("X") + "(" + i1jj.ToString("X").PadLeft(2, '0') + ") " +
                                //                     "and Col" + i2.ToString("X") + "(" + i2jj.ToString("X").PadLeft(2, '0') + ") " +
                                //                     "have two " + szj + "s");
                                if (i1jj == i2jj)
                                {
                                    int row1 = i1jj / 16; // divide, the first (hex) digit
                                    int row2 = i1jj % 16; // modulo or remainder, the second (hex) digit

                                    // Cols i1 and i2 have the 'j' character in rows row1 and row2
                                    objLogBox.Log("XWing: Cols " + i1.ToString("X") + " and " + i2.ToString("X") +
                                                        " in rows [" + i2jj.ToString("X").PadLeft(2, '0') + "] " +
                                                         "have " + szj + "s");

                                    // Call FLoser('j') in the two rows of jj, except in columns i1 and i2.
                                    for (int cCol = 0; cCol < objBoard.cDimension; cCol++)
                                    {
                                        if ((cCol != i1) && (cCol != i2))
                                        {
                                            fRet |= objBoard.rgSquare[cCol, row1].FLoser(szj[0], objBoard);
                                            fRet |= objBoard.rgSquare[cCol, row1].FLoser(szj[0], objBoard);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (fRet)
            {
                objLogBox.Log("X-Wing");
            }
            return fRet;
        }

        private static int FindChsRow(Board objBoard, int iRow, int j)
        {
            // There are exactly two Squares in the iRow'th Row, where the
            // Text contains the char jCh+'1'. We will return both Columns
            // as a two-digit hex number: col1+col4 -> '1' + '4' -> 0x14.

            // Turn our index back into a character.
            char ch;
            if (objBoard.fSuper) {
                // Super
                //   0..9 --> 0x30..0x38 --> '0'..'9' : add '0'
                //   A..F --> 0x41..0x46 --> 'A'..'F' : add 'A', sub 10
                if (j <= 9) {
                    ch = (char)(j + 0x30);
                } else {
                    ch = (char)(j + 0x41 - 10 );
                }
            }
            else {
                // Sudoku and Hyper
                //   0..8 --> 0x31..0x39 --> '1'..'9' : add '1'
                ch = (char)(j + 0x31);
            }

            int iRet = 0;
            for (int iCol = 0; iCol < objBoard.cDimension; iCol++)
            {
                if (objBoard.rgSquare[iCol,iRow].btn.Text.Contains(ch))
                {
                    iRet = (iRet * 16) + iCol;
                }
            }
            return iRet;
        }

        private static int FindChsCol(Board objBoard, int iCol, int j)
        {
            // There are exactly two Squares in the iCol'th Column, where the
            // Text contains the char jCh+'1'. We will return both Rows
            // as a two-digit hex number: row2+rowA -> '2' + 'A' -> 0x2A.

            // Turn our index back into a character.
            char ch;
            if (objBoard.fSuper)
            {
                // Super
                //   0..9 --> 0x30..0x38 --> '0'..'9' : add '0'
                //   A..F --> 0x41..0x46 --> 'A'..'F' : add 'A', sub 10
                if (j <= 9) {
                    ch = (char)(j + 0x30);
                } else {
                    ch = (char)(j + 0x41 - 10);
                }
            }
            else
            {
                // Sudoku and Hyper
                //   0..8 --> 0x31..0x39 --> '1'..'9' : add '1'
                ch = (char)(j + 0x31);
            }

            int iRet = 0;
            for (int iRow = 0; iRow < objBoard.cDimension; iRow++)
            {
                if (objBoard.rgSquare[iCol,iRow].btn.Text.Contains(ch))
                {
                    iRet = (iRet * 16) + iRow;
                }
            }
            return iRet;
        }

        public static bool TryEmBoth(Board objBoard, LogBox objLogBox)
        {
            int row = -1;
            int col = -1;
            char ch1 = 'X';
            char ch2 = 'X';

            // Walk through objBoard, find a Square with two CouldBes.
            foreach (Square sq in objBoard.rgSquare)
            {
                if (sq.btn.Text.Length == 4)
                {
                    ch1 = sq.btn.Text[0];
                    ch2 = sq.btn.Text[2];
                    row = sq.row;
                    col = sq.col;
                    break;
                }
            }

            if (ch1 == 'X') { return false; }

            objLogBox.Log("Try'EmBoth: (" + col + "," + row + ") = '" + ch1 + ch2 + "'");

            TryEmBoards formB1 = new TryEmBoards(objBoard, objLogBox, col, row, ch1);
            formB1.Show();
            Board b1 = new Board(objBoard, formB1, -300, 0);
            b1.rgSquare[col, row].Winner(ch1, false, b1.rgSquare);

            TryEmBoards formB2 = new TryEmBoards(objBoard, objLogBox, col, row, ch2);
            formB2.Show();
            Board b2 = new Board(objBoard, formB2, -300, 0);
            b2.rgSquare[col, row].Winner(ch2, false, b2.rgSquare);

            Go(b1, objLogBox);
            b1.rgSquare[col, row].SetBackColor(b1.rgSquare[col, row].colorTryEmBoth);
            Go(b2, objLogBox);
            b2.rgSquare[col, row].SetBackColor(b2.rgSquare[col, row].colorTryEmBoth);

            // Possible scenarios:
            // 1. Either b1 or b2 completes. Apply that, go.
            // 2. Both b1 and b2 complete. Should not happen! Error!
            // 3. Either b1 or b2 hit an Error condition.
            //    Apply the other action, continue.
            // 4. Walk a/b1/b2; look for the same change (removal
            //    of a CouldBe) from both b1 and b2. Apply that, go.
            // 5. B1 and b2 stalled out, no conclusion can be drawn.
            //    Continue to the next 'Square with two CouldBes',
            //    start over with new b1 and b2.

            return false;
        }

    }
}
