using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

// This is for Flavor flav. I don't understand it.
using static SudokuForms.Game;

/*

Old files have <iWinner>0</iWinner>, instead of -1.
  Can we reliably detect this, adjust at load time?
Old files have <chWinner>0<chWinner>, instead of X.
Old files (even old SuperSudoku) don't have <Flavor>
Old files don't have <CouldBe>

<Squares>
    <Flavor>HyperSudoku</Flavor>
    <CouldBe>true</CouldBe>
	<Square>
		<Row>0</Row>
		<Column>0</Column>
		<Sector>0</Sector>
		<iWinner>-1</iWinner>
		<chWinner>X</chWinner>
		<fOriginal>False</fOriginal>
		<Text>1 2 3 4 5 6 7 8 9 </Text>
		<TabIndex>1</TabIndex>
	</Square>
	<Square>
		<Row>3</Row>
		<Column>0</Column>
		<Sector>3</Sector>
		<iWinner>5</iWinner>
		<chWinner>5</chWinner>
		<fOriginal>True</fOriginal>
		<Text>5 </Text>
		<TabIndex>28</TabIndex>
	</Square>
</Squares>
*/

namespace SudokuForms
{
    class FileIO
    {
        public FileIO()
        {
        }

        private enum Field
        {
            none,
            Flavor,
            Square,
            Row,
            Column,
            Sector,
            iWinner,
            chWinner,
            fOriginal,
            Text,
            TabIndex
        }

        public void SaveFile(Game objGame)
        {
            SaveFileDialog objDlg = new SaveFileDialog
            {
                Filter = "Xml files (*.xml)|*.xml"
            };
            DialogResult result = objDlg.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            using (XmlWriter writer = XmlWriter.Create(objDlg.FileName))
            {
                objGame.Text = "SudoKirk: " + objDlg.FileName;
                writer.WriteStartDocument();

                writer.WriteStartElement("Squares");

                Flavor flav = objGame.objBoard.boardFlav;
                writer.WriteElementString("Flavor", flav.ToString());
                writer.WriteElementString("CouldBe", objGame.fCouldBe.ToString());

                foreach (Square sq in objGame.objBoard.rgSquare)
                {
                    writer.WriteStartElement("Square");

                    writer.WriteElementString("Row", sq.row.ToString());
                    writer.WriteElementString("Column", sq.col.ToString());
                    writer.WriteElementString("Sector", sq.sector.ToString());
                    writer.WriteElementString("iWinner", sq.iWinner.ToString());
                    writer.WriteElementString("chWinner", sq.chWinner.ToString());
                    writer.WriteElementString("fOriginal", sq.fOriginal.ToString());
                    writer.WriteElementString("Text", sq.btn.Text);
                    writer.WriteElementString("TabIndex", sq.btn.TabIndex.ToString());

                    writer.WriteEndElement();   // Square
                }

                writer.WriteEndElement();   // Squares
                writer.WriteEndDocument();
            }
        }

        public void LoadFile(Game objGame)
        {
            string szName;
            string szValue;

            Field state = Field.none;

            Flavor flav = Flavor.Sudoku;
            Square sq;
            int Row = -1;
            int Column = -1;
            int Sector = -1;
            int iWinner = -1;
            char chWinner = 'X';
            bool fOriginal = false;
            int TabIndex;
            string Text = null;

            OpenFileDialog objDlg = new OpenFileDialog
            {
                Filter = "Xml files (*.xml)|*.xml"
            };
            DialogResult result = objDlg.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            objGame.objLogBox.Log("LOAD: " + objDlg.FileName);

            XmlReaderSettings settings = new XmlReaderSettings();
            using (XmlReader reader = XmlReader.Create(objDlg.FileName, settings))
            {
                objGame.Text = "SudoKirk: " + objDlg.FileName;
                Debug.WriteLine("Load: " + objDlg.FileName);
                while (reader.Read())
                {
                    szName = reader.Name;
                    szValue = reader.Value;

                    //Debug.WriteLine("Load: " + reader.NodeType + " : " + szName + " : " + szValue);

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            //Debug.WriteLine("  Element: " + szName);
                            switch (szName)
                            {
                                case "Flavor":
                                    state = Field.Flavor;
                                    break;
                                case "Square":
                                    state = Field.Square;
                                    break;
                                case "Row":
                                    state = Field.Row;
                                    break;
                                case "Column":
                                    state = Field.Column;
                                    break;
                                case "Sector":
                                    state = Field.Sector;
                                    break;
                                case "iWinner":
                                    state = Field.iWinner;
                                    break;
                                case "chWinner":
                                    state = Field.chWinner;
                                    break;
                                case "fOriginal":
                                    state = Field.fOriginal;
                                    break;
                                case "Text":
                                    state = Field.Text;
                                    break;
                                case "TabIndex":
                                    state = Field.TabIndex;
                                    break;
                            }
                            break;

                        case XmlNodeType.Text:
                            //Debug.WriteLine("  Text: " + szValue);
                            switch (state)
                            {
                                case Field.Flavor:
                                    switch (szValue) {
                                        case "Sudoku":
                                            flav = Flavor.Sudoku;
                                            break;
                                        case "SuperSudoku":
                                            flav = Flavor.SuperSudoku;
                                            break;
                                        case "HyperSudoku":
                                            flav = Flavor.HyperSudoku;
                                            break;
                                    }
                                    objGame.BoardReset(flav);
                                    break;
                                case Field.Row:
                                    Row = int.Parse(szValue);
                                    break;
                                case Field.Column:
                                    Column = int.Parse(szValue);
                                    break;
                                case Field.Sector:
                                    Sector = int.Parse(szValue);
                                    break;
                                case Field.iWinner:
                                    int i = int.Parse(szValue);
                                    // Try to handle our old files. 
                                    if ((i == 0) && (flav != Flavor.SuperSudoku)) {
                                        i = -1;
                                    }
                                    iWinner = i;
                                    break;
                                case Field.chWinner:
                                    char ch = szValue[0];
                                    // Try to handle our old files. 
                                    if ((ch == '0') && (flav != Flavor.SuperSudoku))
                                    {
                                        ch = 'X';
                                    }
                                    chWinner = ch;
                                    break;
                                case Field.fOriginal:
                                    // Our old xml files don't have this.
                                    // Hard to interpret, as with iWinner and chWinner.
                                    // These files have all blue characters, no green.
                                    fOriginal = szValue.Equals("True");
                                    break;
                                case Field.Text:
                                    // Try to handle our old files.
                                    //if (szValue.Contains("A")) {
                                    //    objGame.curFlavor = Flavor.SuperSudoku;
                                    //}
                                    Text = szValue;
                                    break;
                                case Field.TabIndex:
                                    TabIndex = int.Parse(szValue);
                                    break;
                            }
                            break;

                        case XmlNodeType.EndElement:
                            //Debug.WriteLine("  EndElement: " + szName);
                            if (szName == "Square")
                            {
                                sq = objGame.objBoard.rgSquare[Column, Row];
                                sq.sector = Sector;
                                sq.iWinner = iWinner;
                                sq.chWinner = chWinner;
                                sq.fOriginal = fOriginal;
                                sq.btn.Text = Text;
                                if (iWinner == -1) {
                                    sq.CouldBes(Text);
                                } else {
                                    sq.Winner(chWinner, sq.fOriginal, objGame.objBoard);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
