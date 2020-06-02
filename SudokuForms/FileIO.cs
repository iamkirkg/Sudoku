using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

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

        public void SaveFile(Board objBoard)
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
                writer.WriteStartDocument();
                writer.WriteStartElement("Squares");

                foreach (Square sq in objBoard.rgSquare)
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

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void LoadFile(Game objGame, Board objBoard)
        {
            string szName;
            string szValue;

            Field state = Field.none;

            Square sq;
            int Row = -1;
            int Column = -1;
            int Sector = -1;
            int iWinner = 0;
            char chWinner = '0';
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

            objBoard.objLogBox.Log("LOAD: " + objDlg.FileName);

            XmlReaderSettings settings = new XmlReaderSettings();
            using (XmlReader reader = XmlReader.Create(objDlg.FileName, settings))
            {
                objGame.Text = "SudoKirk: " + objDlg.FileName;
                while (reader.Read())
                {
                    szName = reader.Name;
                    szValue = reader.Value;

                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (szName)
                            {
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
                            switch (state)
                            {
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
                                    iWinner = int.Parse(szValue);
                                    break;
                                case Field.chWinner:
                                    chWinner = szValue[0];
                                    break;
                                case Field.fOriginal:
                                    fOriginal = szValue.Equals("True");
                                    break;
                                case Field.Text:
                                    Text = szValue;
                                    break;
                                case Field.TabIndex:
                                    TabIndex = int.Parse(szValue);
                                    break;
                            }
                            break;

                        case XmlNodeType.EndElement:
                            if (szName == "Square")
                            {
                                sq = objBoard.rgSquare[Column, Row];
                                sq.sector = Sector;
                                sq.iWinner = iWinner;
                                sq.chWinner = chWinner;
                                sq.fOriginal = fOriginal;
                                sq.btn.Text = Text;
                                if (iWinner == 0)
                                {
                                    sq.CouldBes(Text);
                                }
                                else
                                {
                                    if (sq.fOriginal)
                                    {
                                        sq.Winner(chWinner, sq.fOriginal, Color.DarkGreen, objBoard);
                                    }
                                    else
                                    {
                                        sq.Winner(chWinner, sq.fOriginal, Color.DarkBlue, objBoard);
                                    }
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
