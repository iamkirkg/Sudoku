using System;
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
            Text,
            TabIndex
        }

        public void SaveFile(Square[,] myBoard)
        {
            using (XmlWriter writer = XmlWriter.Create("Squares.xml"))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Squares");

                for (int y = 0; y <= 8; y++)
                {
                    for (int x = 0; x <= 8; x++)
                    {
                        Square sq = myBoard[x, y];
                        writer.WriteStartElement("Square");

                        writer.WriteElementString("Row", y.ToString());
                        writer.WriteElementString("Column", x.ToString());
                        writer.WriteElementString("Sector", sq.sector.ToString());
                        writer.WriteElementString("iWinner", sq.iWinner.ToString());
                        writer.WriteElementString("chWinner", sq.chWinner.ToString());
                        writer.WriteElementString("Text", sq.btn.Text);
                        writer.WriteElementString("TabIndex", sq.btn.TabIndex.ToString());

                        writer.WriteEndElement();
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public void LoadFile(Square[,] myBoard)
        {
            string szName;
            string szValue;

            Field state = Field.none;

            int Row = -1;
            int Column = -1;
            int Sector = -1;
            int iWinner = 0;
            char chWinner = '0';
            int TabIndex = -1;
            string Text = null;

            Square sq;

            XmlReaderSettings settings = new XmlReaderSettings();
            using (XmlReader reader = XmlReader.Create("Squares.xml", settings))
            {
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
                                sq = myBoard[Column, Row];
                                sq.sector = Sector;
                                sq.iWinner = iWinner;
                                sq.chWinner = chWinner;
                                sq.btn.Text = Text;
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
