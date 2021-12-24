using System;
using System.Windows.Forms;

namespace SudokuForms
{
    public class LogBox
    {
        public TextBox objBox { get; set; }

        public LogBox(int xPoint, int yPoint, int xSize, int ySize, int iTab)
        {
            objBox = new System.Windows.Forms.TextBox();
            objBox.Location = new System.Drawing.Point(xPoint, yPoint);
            objBox.Multiline = true;
            objBox.Name = "LogBox";
            objBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            objBox.Size = new System.Drawing.Size(xSize, ySize);
            objBox.TabIndex = iTab;
            objBox.Text = "Sudokirk";
        }

        public void Log (String sz)
        {
            objBox.AppendText(Environment.NewLine + sz);
        }
    }
}
