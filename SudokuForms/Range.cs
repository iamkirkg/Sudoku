
namespace SudokuForms
{
    // A Range is simply an array of nine-or-sixteen Squares.
    // It is the Squares of a row, or a column, or a sector.
    class Range
    {
        public Square[] rgSquare;
        public Type type;
        public int i;   // Which row|column|sector we are

        public enum Type
        {
            Row,
            Col,
            Sec
        }

    public Range(Board objBoard, Type argType, int argI)
        {
            rgSquare = new Square[objBoard.objGame.cDimension];
            type = argType;
            i = argI;

            int r = 0;
            switch (type)
            {
                case Type.Row:
                    for (r = 0; r < objBoard.objGame.cDimension; r++)
                    {
                        rgSquare[r] = objBoard.rgSquare[r, i];
                    }
                    break;
                case Type.Col:
                    for (r = 0; r < objBoard.objGame.cDimension; r++)
                    {
                        rgSquare[r] = objBoard.rgSquare[i, r];
                    }
                    break;
                case Type.Sec:
                    foreach (Square sq in objBoard.rgSquare)
                    {
                        if (sq.sector == i)
                        {
                            rgSquare[r++] = sq;
                        }
                        if (sq.hypersector == i)
                        {
                            rgSquare[r++] = sq;
                        }
                    }
                    break;
            }
        }

    public void SetRangeColor(bool fRange)
        {
            foreach (Square sq in rgSquare) {
                if (fRange) {
                    sq.btn.BackColor = sq.colorRange;
                } else {
                    sq.btn.BackColor = sq.MyBackColor();
                }
                sq.btn.Refresh();
            }
        }

        /*

        // Output of GenerateMapping below (and then cut'n'pasted to here):
        // This is the count of bits in the values [0..511], or 2^9.
        // This represents every permutation of nine Squares within a Range.
        // And, alas, it turns out we don't even need it.

        public static int[] rgBitCount = 
        {
            0,1,1,2,1,2,2,3,1,2,2,3,2,3,3,4,
            1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,
            1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            4,5,5,6,5,6,6,7,5,6,6,7,6,7,7,8,
            1,2,2,3,2,3,3,4,2,3,3,4,3,4,4,5,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            4,5,5,6,5,6,6,7,5,6,6,7,6,7,7,8,
            2,3,3,4,3,4,4,5,3,4,4,5,4,5,5,6,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            4,5,5,6,5,6,6,7,5,6,6,7,6,7,7,8,
            3,4,4,5,4,5,5,6,4,5,5,6,5,6,6,7,
            4,5,5,6,5,6,6,7,5,6,6,7,6,7,7,8,
            4,5,5,6,5,6,6,7,5,6,6,7,6,7,7,8,
            5,6,6,7,6,7,7,8,6,7,7,8,7,8,8,9
        };

        public int CountBits(int i)
        {
            int cBit = 0;
            while (i != 0)
            {
                // If it's odd, then the low bit is a 1.
                if ((i % 2) == 1)
                {
                    cBit++;
                }
                // Divide by two
                i = i / 2;
            }
            return cBit;
        }

        public void GenerateMapping(LogBox logBox)
        {
            string sz = null;
            logBox.Log("public static int[] rgBitCount =");
            logBox.Log("{");
            for (int i = 0; i < Math.Pow(2, 9); i++)
            {
                if (i != (Math.Pow(2, 9) - 1))
                {
                    sz = sz + CountBits(i).ToString() + ",";
                }
                else
                {
                    sz = sz + CountBits(i).ToString();
                }
                if ((i % 16) == 15)
                {
                    logBox.Log("\t" + sz);
                    sz = null;
                }
            }
            logBox.Log("};");
        }

        */

    }
}
