using System;

namespace GameEngine
{
    public class BoardLayout
    {
        public struct Square
        {
            public Square(int argSector)
            {
                iWinner = 0;
                rgf = new bool[] { true, true, true, true, true, true, true, true, true };
                sector = argSector;
            }
            public int iWinner { get; set; } // When there's only one left.
            public int sector { get; }       // What sector we're in.
            public bool[] rgf { get; set; }  // 'true' means it could be, 'false' means it can't be.
        }

        public static void WinnerWinner(Square sq, int iWinner)
        {
            sq.iWinner = iWinner;
            for (int i = 0; i <= 8; i++)
            {
                sq.rgf[i] = false;
            }
            sq.rgf[iWinner] = true;
        }

        public Square[,] GameBoard = new Square[9, 9] 
        {
            { new Square(0), new Square(0), new Square(0), new Square(1), new Square(1), new Square(1), new Square(2), new Square(2), new Square(2) },
            { new Square(0), new Square(0), new Square(0), new Square(1), new Square(1), new Square(1), new Square(2), new Square(2), new Square(2) },
            { new Square(0), new Square(0), new Square(0), new Square(1), new Square(1), new Square(1), new Square(2), new Square(2), new Square(2) },
            { new Square(3), new Square(3), new Square(3), new Square(4), new Square(4), new Square(4), new Square(5), new Square(5), new Square(5) },
            { new Square(3), new Square(3), new Square(3), new Square(4), new Square(4), new Square(4), new Square(5), new Square(5), new Square(5) },
            { new Square(3), new Square(3), new Square(3), new Square(4), new Square(4), new Square(4), new Square(5), new Square(5), new Square(5) },
            { new Square(6), new Square(6), new Square(6), new Square(7), new Square(7), new Square(7), new Square(8), new Square(8), new Square(8) },
            { new Square(6), new Square(6), new Square(6), new Square(7), new Square(7), new Square(7), new Square(8), new Square(8), new Square(8) },
            { new Square(6), new Square(6), new Square(6), new Square(7), new Square(7), new Square(7), new Square(8), new Square(8), new Square(8) }
        };
    }
}
