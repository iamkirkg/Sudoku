using System;
using System.Windows.Forms;

namespace SudokuForms
{
    public class Board
    {
        public Square[,] rgSquare;

        public Game objGame;
        public LogBox objLogBox;

        // map TabIndex to Sector. There's probably an arithmetic way to
        // do this (index, 27, 9, 3, modulo, remainders?) but I can't 
        // see it, and it'd probably be a pain to document or maintain.
        readonly private int[] mpTabSector =
        {
            0,0,0, 1,1,1, 2,2,2,
            0,0,0, 1,1,1, 2,2,2,
            0,0,0, 1,1,1, 2,2,2,

            3,3,3, 4,4,4, 5,5,5,
            3,3,3, 4,4,4, 5,5,5,
            3,3,3, 4,4,4, 5,5,5,

            6,6,6, 7,7,7, 8,8,8,
            6,6,6, 7,7,7, 8,8,8,
            6,6,6, 7,7,7, 8,8,8
        };

        readonly private int[] mpTabSectorSuper =
        {
            00,00,00,00, 01,01,01,01, 02,02,02,02, 03,03,03,03,
            00,00,00,00, 01,01,01,01, 02,02,02,02, 03,03,03,03,
            00,00,00,00, 01,01,01,01, 02,02,02,02, 03,03,03,03,
            00,00,00,00, 01,01,01,01, 02,02,02,02, 03,03,03,03,

            04,04,04,04, 05,05,05,05, 06,06,06,06, 07,07,07,07,
            04,04,04,04, 05,05,05,05, 06,06,06,06, 07,07,07,07,
            04,04,04,04, 05,05,05,05, 06,06,06,06, 07,07,07,07,
            04,04,04,04, 05,05,05,05, 06,06,06,06, 07,07,07,07,

            08,08,08,08, 09,09,09,09, 10,10,10,10, 11,11,11,11,
            08,08,08,08, 09,09,09,09, 10,10,10,10, 11,11,11,11,
            08,08,08,08, 09,09,09,09, 10,10,10,10, 11,11,11,11,
            08,08,08,08, 09,09,09,09, 10,10,10,10, 11,11,11,11,

            12,12,12,12, 13,13,13,13, 14,14,14,14, 15,15,15,15,
            12,12,12,12, 13,13,13,13, 14,14,14,14, 15,15,15,15,
            12,12,12,12, 13,13,13,13, 14,14,14,14, 15,15,15,15,
            12,12,12,12, 13,13,13,13, 14,14,14,14, 15,15,15,15
        };

        public Board(Game argGame,
                     bool fSuper,
                     int xOrigin, int yOrigin, int xSize, int ySize, float font,
                     KeyPressEventHandler fnKeyPress,
                     KeyEventHandler fnKeyDown,
                     EventHandler fnClick,
                     LogBox argLogBox
                    )
        {
            objGame = argGame;
            objLogBox = argLogBox;

            int xDelta = xSize + 2;
            int yDelta = ySize + 2;
            int xPoint;
            int yPoint = yOrigin;
            int iSector;

            rgSquare = new Square[argGame.cDimension, argGame.cDimension];

            int iTab = 0; // TabIndex [1..81] or [1..256]
            for (int y = 0; y < argGame.cDimension; y++)
            {
                xPoint = xOrigin;
                for (int x = 0; x < argGame.cDimension; x++)
                {
                    iTab++;
                    iSector = (fSuper ? mpTabSectorSuper[iTab - 1] : mpTabSector[iTab - 1]);
                    rgSquare[x, y] = new Square(argGame, iTab, iSector, xPoint, yPoint, xSize, ySize, font,
                                                fnKeyPress, fnKeyDown, fnClick);
                    xPoint += xDelta;
                }
                yPoint += yDelta;
            }
        }

        public void Delete()
        {
            for (int y = 0; y < objGame.cDimension; y++)
            {
                for (int x = 0; x < objGame.cDimension; x++)
                {
                    rgSquare[x, y].Delete();
                }
            }
        }
    }
}
