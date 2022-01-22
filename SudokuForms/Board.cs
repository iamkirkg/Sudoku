using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

// This is for Flavor flav. I don't understand it.
using static SudokuForms.Game;

namespace SudokuForms
{
    public class Board
    {
        private Flavor boardFlav;
        public Square[,] rgSquare;

        // If this board is broken, in an error state,
        // this will be nonNull, the Red square.
        public Square sqError = null;

        public bool fSuper
        { // Are we 3x3 or 4x4?
            get { return boardFlav == Flavor.SuperSudoku; }
        }

        // Board origin
        private int xOrigin = 302;
        private int yOrigin = 4;

        // Board size
        private int iBoardWidth
        {
            get { return fSuper ? 1168 : 790; }
        }
        private int iBoardHeight
        {
            get { return fSuper ? 996 : 640; }
        }

        // Square size
        private int xSize
        {
            get { return fSuper ? 52 : 52; }
        }
        private int ySize
        {
            get { return fSuper ? 60 : 68; }
        }

        public int cDimension
        { // Are we 3x3 or 4x4?
            get { return fSuper ? 16 : 9; }
        }
        public int bitCount
        {
            get { return fSuper ? 16 : 9; }
        }

        public Single emSizeWinner
        {
            get { return fSuper ? 36F : 40F; }
        }
        public float font
        {
            get { return fSuper ? 7F : 12F; }
        }

        public int cSector
        {
            get
            {
                switch (boardFlav)
                {
                    case Flavor.Sudoku:
                        return 9;
                    case Flavor.SuperSudoku:
                        return 16;
                    case Flavor.HyperSudoku: // 9 + 4
                        return 13;
                    default:
                        return -1;
                }
            }
        }

        // map TabIndex to Sector.
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

        // HyperSudoku has four more sectors overlaid in the middle.
        readonly private int[] mpTabHyperSector =
        {
            -1, -1,-1,-1, -1, -1,-1,-1, -1,
            -1, 09,09,09, -1, 10,10,10, -1,
            -1, 09,09,09, -1, 10,10,10, -1,
            -1, 09,09,09, -1, 10,10,10, -1,
            -1, -1,-1,-1, -1, -1,-1,-1, -1,
            -1, 11,11,11, -1, 12,12,12, -1,
            -1, 11,11,11, -1, 12,12,12, -1,
            -1, 11,11,11, -1, 12,12,12, -1,
            -1, -1,-1,-1, -1, -1,-1,-1, -1
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

        public Board(Game argGame, Flavor flav,
                     KeyPressEventHandler fnKeyPress,
                     KeyEventHandler fnKeyDown,
                     EventHandler fnClick
                    )
        {
            boardFlav = flav;

            int xDelta = xSize + 2;
            int yDelta = ySize + 2;
            int xPoint;
            int yPoint = yOrigin;
            int iSector;
            int iHyperSector;

            Debug.WriteLine("Board(" + cDimension + ") new");

            argGame.ClientSize = new System.Drawing.Size(iBoardWidth, iBoardHeight);

            rgSquare = new Square[cDimension, cDimension];

            int iTab = 0; // TabIndex [1..81] or [1..256]
            for (int y = 0; y < cDimension; y++)
            {
                xPoint = xOrigin;
                for (int x = 0; x < cDimension; x++)
                {
                    iTab++;
                    switch (boardFlav)
                    {
                        case Flavor.Sudoku:
                            iSector = mpTabSector[iTab - 1];
                            iHyperSector = -1;
                            break;
                        case Flavor.SuperSudoku:
                            iSector = mpTabSectorSuper[iTab - 1];
                            iHyperSector = -1;
                            break;
                        default: // case Flavor.HyperSudoku:
                            iSector = mpTabSector[iTab - 1];
                            iHyperSector = mpTabHyperSector[iTab - 1];
                            break;
                    }
                    rgSquare[x, y] = new Square(argGame, iTab, iSector, iHyperSector, 
                                                new Point(xPoint, yPoint), new Size(xSize, ySize), font,
                                                fnKeyPress, fnKeyDown, fnClick);
                    xPoint += xDelta;
                }
                yPoint += yDelta;
            }
        }

        public Board(Board boardPrev, Form homeForm, int xDelta, int yDelta)
        {
            boardFlav = boardPrev.boardFlav;
            xOrigin = boardPrev.xOrigin;
            yOrigin = boardPrev.yOrigin;

            rgSquare = new Square[cDimension, cDimension];
            for (int y = 0; y < cDimension; y++)
            {
                for (int x = 0; x < cDimension; x++)
                {
                    rgSquare[x, y] = new Square(boardPrev.rgSquare[x, y], homeForm, xDelta, yDelta);
                }
            }
        }

        public void Delete()
        {
            Debug.WriteLine("Board(" + cDimension + ") delete");

            for (int y = 0; y < cDimension; y++)
            {
                for (int x = 0; x < cDimension; x++)
                {
                    rgSquare[x, y].Delete();
                }
            }
        }
    }
}
