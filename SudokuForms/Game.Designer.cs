using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

namespace SudokuForms
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // REVIEW KirkG: This suspend/resume should go around our double-for-loops too.
            this.SuspendLayout();

            // The Squares occupy TabIndex=[1..81] or [1..256].
            int iTabIndex = 300;

            // ------------------------------------------
            // All our radio buttons.

            int yPoint = 8;
            int yPointDelta = 20;
            //
            // FlavorSudoku
            //
            this.FlavorSudoku = new RadioButton();
            this.FlavorSudoku.AutoSize = true;
            this.FlavorSudoku.Location = new Point(8, yPoint);
            this.FlavorSudoku.Name = "FlavorSudoku";
            this.FlavorSudoku.Size = new Size(98, 24);
            this.FlavorSudoku.Text = "Sudoku";
            this.FlavorSudoku.UseVisualStyleBackColor = true;
            this.FlavorSudoku.CheckedChanged += new EventHandler(this.FlavorSudoku_CheckedChanged);
            yPoint += yPointDelta;
            //
            // FlavorSuperSudoku
            //
            this.FlavorSuperSudoku = new RadioButton();
            this.FlavorSuperSudoku.AutoSize = true;
            this.FlavorSuperSudoku.Location = new Point(8, yPoint);
            this.FlavorSuperSudoku.Name = "FlavorSuperSudoku";
            this.FlavorSuperSudoku.Size = new Size(98, 24);
            this.FlavorSuperSudoku.Text = "SuperSudoku";
            this.FlavorSuperSudoku.UseVisualStyleBackColor = true;
            this.FlavorSuperSudoku.CheckedChanged += new EventHandler(this.FlavorSuperSudoku_CheckedChanged);
            yPoint += yPointDelta;
            //
            // FlavorHyperSudoku
            //
            this.FlavorHyperSudoku = new RadioButton();
            this.FlavorHyperSudoku.AutoSize = true;
            this.FlavorHyperSudoku.Location = new Point(8, yPoint);
            this.FlavorHyperSudoku.Name = "FlavorHyperSudoku";
            this.FlavorHyperSudoku.Size = new Size(98, 24);
            this.FlavorHyperSudoku.Text = "HyperSudoku";
            this.FlavorHyperSudoku.UseVisualStyleBackColor = true;
            this.FlavorHyperSudoku.CheckedChanged += new EventHandler(this.FlavorHyperSudoku_CheckedChanged);
            yPoint += yPointDelta;

            //
            // FlavorPanel
            //
            this.FlavorPanel = new Panel();
            this.FlavorPanel.SuspendLayout();
            this.FlavorPanel.Controls.Add(this.FlavorSudoku);
            this.FlavorPanel.Controls.Add(this.FlavorSuperSudoku);
            this.FlavorPanel.Controls.Add(this.FlavorHyperSudoku);
            this.FlavorPanel.BorderStyle = BorderStyle.FixedSingle;
            this.FlavorPanel.Location = new Point(490 + xDelta - 490, 10);
            this.FlavorPanel.Name = "FlavorPanel";
            this.FlavorPanel.Size = new Size(110, 74);
            this.FlavorPanel.TabIndex = iTabIndex++;

            switch (curFlavor)
            {
                case Flavor.Sudoku:
                    this.FlavorSudoku.Checked = true;
                    break;
                case Flavor.SuperSudoku:
                    this.FlavorSuperSudoku.Checked = true;
                    break;
                case Flavor.HyperSudoku:
                    this.FlavorHyperSudoku.Checked = true;
                    break;
            }

            this.Controls.Add(this.FlavorPanel);
            this.FlavorPanel.ResumeLayout(false);
            this.FlavorPanel.PerformLayout();

            // ------------------------------------------
            // 
            // btnReset
            // 
            this.btnReset = new Button();
            this.btnReset.Click += Reset_Click;
            this.btnReset.BackColor = Color.LightGray;
            this.btnReset.ForeColor = Color.Black;
            this.btnReset.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new Point(490+xDelta - 490, 88);
            this.btnReset.Size = new Size(80, 36);
            this.btnReset.TabIndex = iTabIndex++;
            this.btnReset.Text = "Reset";
            this.Controls.Add(this.btnReset);

            // 
            // btnClear
            // 
            this.btnClear = new Button();
            this.btnClear.Click += Clear_Click;
            this.btnClear.BackColor = Color.LightGray;
            this.btnClear.ForeColor = Color.Black;
            this.btnClear.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new Point(574+xDelta - 490, 88);
            this.btnClear.Size = new Size(80, 36);
            this.btnClear.TabIndex = iTabIndex++;
            this.btnClear.Text = "Clear";
            this.Controls.Add(this.btnClear);
            // 
            // btnStep
            // 
            this.btnStep = new Button();
            this.btnStep.Click += Step_Click;
            this.btnStep.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new Point(596+xDelta - 490, 148);
            this.btnStep.Size = new Size(80, 36);
            this.btnStep.TabIndex = iTabIndex++;
            this.btnStep.Text = "Step";
            this.Controls.Add(this.btnStep);

            // ------------------------------------------

            yPoint = 8;

            // 
            // RangeCheck
            // 
            this.RangeCheck = new RadioButton();
            this.RangeCheck.AutoSize = true;
            this.RangeCheck.Location = new Point(8, yPoint);
            this.RangeCheck.Name = "RangeCheck";
            this.RangeCheck.Size = new Size(98, 24);
            this.RangeCheck.Text = "RangeCheck";
            this.RangeCheck.UseVisualStyleBackColor = true;
            this.RangeCheck.CheckedChanged += new EventHandler(this.RangeCheck_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // LineFind
            // 
            this.LineFind = new RadioButton();
            this.LineFind.AutoSize = true;
            this.LineFind.Location = new Point(8, yPoint);
            this.LineFind.Name = "LineFind";
            this.LineFind.Size = new Size(98, 24);
            this.LineFind.Text = "LineFind";
            this.LineFind.UseVisualStyleBackColor = true;
            this.LineFind.CheckedChanged += new EventHandler(this.LineFind_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // SectorFind
            // 
            this.SectorFind = new RadioButton();
            this.SectorFind.AutoSize = true;
            this.SectorFind.Location = new Point(8, yPoint);
            this.SectorFind.Name = "SectorFind";
            this.SectorFind.Size = new Size(98, 24);
            this.SectorFind.Text = "SectorFind";
            this.SectorFind.UseVisualStyleBackColor = true;
            this.SectorFind.CheckedChanged += new EventHandler(this.SectorFind_CheckedChanged);
            yPoint += yPointDelta;

            // 
            // TechniquePanel
            // 
            this.TechniquePanel = new Panel();
            this.TechniquePanel.SuspendLayout();
            this.TechniquePanel.Controls.Add(this.RangeCheck);
            this.TechniquePanel.Controls.Add(this.LineFind);
            this.TechniquePanel.Controls.Add(this.SectorFind);
            this.TechniquePanel.BorderStyle = BorderStyle.FixedSingle;
            this.TechniquePanel.Location = new Point(490+xDelta - 490, 130);
            this.TechniquePanel.Name = "TechniquePanel";
            this.TechniquePanel.Size = new Size(100, 74);
            this.TechniquePanel.TabIndex = iTabIndex++;

            // Our default button.
            this.RangeCheck.Checked = true;

            this.Controls.Add(this.TechniquePanel);
            this.TechniquePanel.ResumeLayout(false);
            this.TechniquePanel.PerformLayout();

            // 
            // btnLoad
            // 
            this.btnLoad = new Button();
            this.btnLoad.Click += Load_Click;
            this.btnLoad.BackColor = Color.LightGray;
            this.btnLoad.ForeColor = Color.Black;
            this.btnLoad.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new Point(490+xDelta - 490, 208);
            this.btnLoad.Size = new Size(80, 36);
            this.btnLoad.TabIndex = iTabIndex++;
            this.btnLoad.Text = "Load";
            this.Controls.Add(this.btnLoad);

            // 
            // btnSave
            // 
            this.btnSave = new Button();
            this.btnSave.Click += Save_Click;
            this.btnSave.BackColor = Color.LightGray;
            this.btnSave.ForeColor = Color.Black;
            this.btnSave.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new Point(490+xDelta - 490, 248);
            this.btnSave.Size = new Size(80, 36);
            this.btnSave.TabIndex = iTabIndex++;
            this.btnSave.Text = "Save";
            this.Controls.Add(this.btnSave);

            //
            // btnPrint
            //
            this.btnPrint = new Button();
            this.btnPrint.Click += Print_Click;
            this.btnPrint.BackColor = Color.LightGray;
            this.btnPrint.ForeColor = Color.Black;
            this.btnPrint.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new Point(596 + xDelta - 490, 228);
            this.btnPrint.Size = new Size(80, 36);
            this.btnPrint.TabIndex = iTabIndex++;
            this.btnPrint.Text = "Print";
            this.Controls.Add(this.btnPrint);

            // 
            // CouldBe
            // 
            this.CouldBe = new CheckBox();
            this.CouldBe.AutoSize = true;
            this.CouldBe.Checked = true;
            this.CouldBe.Location = new Point(490+xDelta - 490, 288);
            this.CouldBe.Name = "CouldBe";
            this.CouldBe.Size = new Size(104, 24);
            this.CouldBe.TabIndex = iTabIndex++;
            this.CouldBe.Text = "CouldBe";
            this.CouldBe.UseVisualStyleBackColor = true;
            this.CouldBe.CheckedChanged += new EventHandler(this.CouldBe_CheckedChanged);
            this.Controls.Add(this.CouldBe);

            //
            // LogBox
            //
            // BUGBUG: Add an fSuper test, grow this box downward.
            objLogBox = new LogBox(490+xDelta - 490, 308, 294, 330, iTabIndex);
            this.Controls.Add(objLogBox.objBox);

            // 
            // SudokuForms
            // 
            // I still don't get what these do.
            // https://docs.microsoft.com/en-us/dotnet/desktop/winforms/automatic-scaling-in-windows-forms?view=netframeworkdesktop-4.8
            //this.AutoScaleDimensions = new SizeF(9F, 20F);
            //this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(iBoardWidth, iBoardHeight);

            this.Name = szTitle;
            this.Text = szTitle;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // This is the KeyPress function for all 81-or-256 of our Squares.
        void sq_KeyPress(object sender, KeyPressEventArgs e)
        {
            Button btn = sender as Button;
            int iTab = btn.TabIndex;
            char keyChar = Char.ToUpper(e.KeyChar, CultureInfo.CreateSpecificCulture("en-US"));

            // Calculate objBoard.rgSquare[col,row] location from the tabindex.
            // TabIndex is [1..81] or [1..256]; the array is [0..8][0..8] or [0..15][0..15].
            curTab = iTab;
            curCol = ((iTab - 1) % cDimension);  // Modulo (remainder)
            curRow = ((iTab - 1) / cDimension);  // Divide
            curChar = keyChar;

            //objLogBox.Log("KeyPress " + keyChar);

            if ((keyChar >= '0' && keyChar <= '9') || (fSuper && keyChar >= 'A' && keyChar <= 'F'))
            {
                //objLogBox.Log("Set: tab " + iTab + ": [" + curCol + "," + curRow + "] key = " + keyChar);
                objBoard.rgSquare[curCol, curRow].Winner(keyChar, true, objBoard);
                //Techniques.Neighbor(objBoard.rgSquare, curCol, curRow, keyChar);
            }
        }

        // KeyDown for all 81-or-256 squares
        void sq_KeyDown(object sender, KeyEventArgs e)
        {
            Button btn = sender as Button;
            int iTab = btn.TabIndex;
            Keys keyCode = e.KeyCode;

            // Calculate objBoard.rgSquare[col,row] location from the tabindex.
            // TabIndex is [1..81] or [1..256]; the array is [0..8][0..8] or [0..15][0..15].
            curTab = iTab;
            curCol = ((iTab - 1) % cDimension);  // Modulo (remainder)
            curRow = ((iTab - 1) / cDimension);  // Divide

            //objLogBox.Log("KeyDown " + keyCode.ToString());

            switch (keyCode)
            {
                case Keys.Left:
                    break;
                case Keys.Right:
                    break;
                case Keys.Up:
                    break;
                case Keys.Down:
                    break;

                case Keys.Delete:
                    objBoard.rgSquare[curCol, curRow].Reset();
                    break;
            }
        }

        // This is the ButtonClick function for all 81-or-256 of our Squares.
        void sq_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ClickSquare(btn.TabIndex);
        }

        void FlavorSudoku_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            Debug.WriteLine("FlavorSudoku_CheckedChanged(" + radio.Checked.ToString() + ")");
            if (radio.Checked)
            {
                BoardReset(Flavor.Sudoku);
            }
        }

        void FlavorSuperSudoku_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            Debug.WriteLine("FlavorSuperSudoku_CheckedChanged(" + radio.Checked.ToString() + ")");
            if (radio.Checked)
            {
                BoardReset(Flavor.SuperSudoku);
            }
        }

        void FlavorHyperSudoku_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            Debug.WriteLine("FlavorHyperSudoku_CheckedChanged(" + radio.Checked.ToString() + ")");
            if (radio.Checked)
            {
                BoardReset(Flavor.HyperSudoku);
            }
        }

        void Reset_Click(object sender, EventArgs e)
        {
            objLogBox.Log("------- RESET ------------------");
            foreach (Square sq in objBoard.rgSquare)
            {
                if (!sq.fOriginal)
                {
                    sq.Reset();
                }
            }
        }

        void Clear_Click(object sender, EventArgs e)
        {
            this.Text = "SudoKirk";
            objLogBox.Log("------- CLEAR ------------------");
            foreach (Square sq in objBoard.rgSquare)
            {
                sq.Reset();
            }
        }

        void RangeCheck_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.RangeCheck;
            }
        }

        void LineFind_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.LineFind;
            }
        }

        void SectorFind_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.SectorFind;
            }
        }

        void CouldBe_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            fCouldBe = box.Checked;

            foreach (Square objSq in objBoard.rgSquare)
            {
                if (objSq.iWinner == -1)
                {
                    if (box.Checked)
                    {
                        objSq.btn.ForeColor = Color.Black;
                    }
                    else
                    {
                        objSq.btn.ForeColor = objSq.MyBackColor();
                    }
                }
            }
        }

        // This is the ButtonClick function for the Step button.
        void Step_Click(object sender, EventArgs e)
        {
            //Button btn = sender as Button;
            switch (curTechnique)
            {
                case Technique.none:
                    break;
                case Technique.RangeCheck:
                    Techniques.AllRanges(objBoard, objLogBox);
                    break;
                case Technique.LineFind:
                    Techniques.FLineFind(objBoard, objLogBox);
                    break;
                case Technique.SectorFind:
                    Techniques.FSectorsFind(objBoard, objLogBox);
                    break;
            }
        }

        void ClickSquare(int iTab)
        {
            {
                // Calculate objBoard.rgSquare[col,row] location from the tabindex.
                // TabIndex is [1..81] or [1..256]; the array is [0..8][0..8] or [0..15][0..15].
                curTab = iTab;
                curCol = ((iTab - 1) % cDimension);  // Modulo (remainder)
                curRow = ((iTab - 1) / cDimension);  // Divide
                Square mySquare = objBoard.rgSquare[curCol, curRow];
                if (mySquare.iWinner != -1)
                {
                    curChar = mySquare.chWinner;
                    //objLogBox.Log("Select: tab " + iTab + ": [" + curCol + "," + curRow + "], Winner=" + curChar);
                }
                else
                {
                    // I don't like this, but you can't assign null to a char.
                    curChar = ' ';
                    //objLogBox.Log("Select: tab " + iTab + ": [" + curCol + "," + curRow + "]");
                }
            }
        }

        // This is the ButtonClick function for the Save button.
        void Save_Click(object sender, EventArgs e)
        {
            FileIO f = new FileIO();
            f.SaveFile(this);
        }

        // This is the ButtonClick function for the Load button.
        void Load_Click(object sender, EventArgs e)
        {
            FileIO f = new FileIO();
            f.LoadFile(this);
        }

        // This is the ButtonClick function for the Print button.
        void Print_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDialog.Document = printDocument;
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            DialogResult result = printDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                CaptureScreen();
                printDocument.Print();
            }
        }

        Bitmap memoryImage;
        private void CaptureScreen()
        {
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }

        RadioButton FlavorSudoku;
        RadioButton FlavorSuperSudoku;
        RadioButton FlavorHyperSudoku;
        Panel FlavorPanel;
        Button btnReset;
        Button btnClear;
        Button btnStep;
        public LogBox objLogBox;
        CheckBox CouldBe;
        RadioButton RangeCheck;
        RadioButton LineFind;
        RadioButton SectorFind;
        Panel TechniquePanel;
        Button btnLoad;
        Button btnPrint;
        Button btnSave;

    }
}
 
 