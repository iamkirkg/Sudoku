using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace SudokuForms
{
    // A Range is simply an array of nine Squares.
    // It is the Squares of a row, or a column, or a sector.
    class Range
    {
        public Square[] rgSquare;
        public Type type;
        public int i;   // Which row|column|sector we are

        public enum Type
        {
            Row,
            Column,
            Sector
        }

        public Range(Board objBoard, Type argType, int argI)
        {
            rgSquare = new Square[9];
            type = argType;
            i = argI;

            int r = 0;
            switch (type)
            {
                case Type.Row:
                    for (r = 0; r <= 8; r++)
                    {
                        rgSquare[r] = objBoard.rgSquare[r, i];
                    }
                    break;
                case Type.Column:
                    for (r = 0; r <= 8; r++)
                    {
                        rgSquare[r] = objBoard.rgSquare[i, r];
                    }
                    break;
                case Type.Sector:
                    foreach (Square sq in objBoard.rgSquare)
                    {
                        if (sq.sector == i)
                        {
                            rgSquare[r++] = sq;
                        }
                    }
                    break;
            }
        }

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
            for (int i = 0; i < Math.Pow(2, 9); i++)
            {
                logBox.Log(i.ToString() + ": " + CountBits(i).ToString());
            }
        }
    }
}
