using System;
using System.Windows.Forms;

namespace SudokuForms
{
    public class Board
    {
        public Square[,] rgSquare;

        public LogBox objLogBox;

        // map TabIndex to Sector. There's probably an arithmetic way to
        // do this (index, 27, 9, 3, modulo, remainders?) but I can't 
        // see it, and it'd probably be a pain to document or maintain.
        readonly private int[] mpTabSector =
        {
            0, 0, 0, 1, 1, 1, 2, 2, 2,
            0, 0, 0, 1, 1, 1, 2, 2, 2,
            0, 0, 0, 1, 1, 1, 2, 2, 2,
            3, 3, 3, 4, 4, 4, 5, 5, 5,
            3, 3, 3, 4, 4, 4, 5, 5, 5,
            3, 3, 3, 4, 4, 4, 5, 5, 5,
            6, 6, 6, 7, 7, 7, 8, 8, 8,
            6, 6, 6, 7, 7, 7, 8, 8, 8,
            6, 6, 6, 7, 7, 7, 8, 8, 8
        };

        public Board(Game objGame,
                     int xOrigin, int yOrigin, int xSize, int ySize, float font,
                     KeyPressEventHandler fnKeyPress,
                     KeyEventHandler fnKeyDown,
                     EventHandler fnClick,
                     LogBox argLogBox
                    )
        {
            objLogBox = argLogBox;

            int xDelta = xSize + 2;
            int yDelta = ySize + 2;
            int xPoint;
            int yPoint = yOrigin;

            rgSquare = new Square[9, 9];

            int iTab = 0; // [1 ... 81]
            for (int y = 0; y <= 8; y++)
            {
                xPoint = xOrigin;
                for (int x = 0; x <= 8; x++)
                {
                    iTab++;
                    int iSector = mpTabSector[iTab - 1];
                    rgSquare[x, y] = new Square(objGame, iTab, iSector, xPoint, yPoint, xSize, ySize, font, 
                                                fnKeyPress, fnKeyDown, fnClick);
                    xPoint += xDelta;
                }
                yPoint += yDelta;
            }
        }
    }
}
