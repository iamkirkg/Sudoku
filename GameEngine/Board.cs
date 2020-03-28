using System;

namespace GameEngine
{
    public class BoardLayout
    {
        public struct Square
        {
            public Square(int argQ)
            {
                iWinner = 0;
                rgf = new bool[] { true, true, true, true, true, true, true, true, true };
                q = argQ;
            }

            public int iWinner { get; set; } // When there's only one left.
            public int q { get; }            // What quadrant we're in.
            public bool[] rgf { get; set; }  // 'true' means it could be, 'false' means it can't be.
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
