using System;
using System.Drawing;
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

            int iTabIndex = 100;

            // 
            // btnReset
            // 
            this.btnReset = new Button();
            this.btnReset.Click += Reset_Click;
            this.btnReset.BackColor = Color.LightGray;
            this.btnReset.ForeColor = Color.Black;
            this.btnReset.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new Point(740, 10);
            this.btnReset.Size = new Size(100, 60);
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
            this.btnClear.Location = new Point(846, 10);
            this.btnClear.Size = new Size(100, 60);
            this.btnClear.TabIndex = iTabIndex++;
            this.btnClear.Text = "Clear";
            this.Controls.Add(this.btnClear);
            // 
            // btnStep
            // 
            this.btnStep = new Button();
            this.btnStep.Click += Step_Click;
            this.btnStep.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new Point(910, 100);
            this.btnStep.Size = new Size(90, 60);
            this.btnStep.TabIndex = iTabIndex++;
            this.btnStep.Text = "Step";
            this.Controls.Add(this.btnStep);

            // ------------------------------------------
            // All our radio buttons.

            int yPoint = 12;
            int yPointDelta = 30;

            /*
            // 
            // Neighbor
            // 
            this.Neighbor = new RadioButton();
            this.Neighbor.AutoSize = true;
            this.Neighbor.Location = new Point(12, yPoint);
            this.Neighbor.Name = "Neighbor";
            this.Neighbor.Size = new Size(98, 24);
            // This is the only button with TabStop set. Don't know why.
            this.Neighbor.TabStop = true;
            this.Neighbor.Text = "Neighbor";
            this.Neighbor.UseVisualStyleBackColor = true;
            this.Neighbor.CheckedChanged += new EventHandler(this.Neighbor_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // AllNeighbors
            // 
            this.AllNeighbors = new RadioButton();
            this.AllNeighbors.AutoSize = true;
            this.AllNeighbors.Location = new Point(12, yPoint);
            this.AllNeighbors.Name = "AllNeighbors";
            this.AllNeighbors.Size = new Size(98, 24);
            this.AllNeighbors.Text = "AllNeighbors";
            this.AllNeighbors.UseVisualStyleBackColor = true;
            this.AllNeighbors.CheckedChanged += new EventHandler(this.AllNeighbors_CheckedChanged);
            yPoint += yPointDelta;
            */

            // 
            // RangeCheck
            // 
            this.RangeCheck = new RadioButton();
            this.RangeCheck.AutoSize = true;
            this.RangeCheck.Location = new Point(12, yPoint);
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
            this.LineFind.Location = new Point(12, yPoint);
            this.LineFind.Name = "LineFind";
            this.LineFind.Size = new Size(115, 24);
            this.LineFind.Text = "LineFind";
            this.LineFind.UseVisualStyleBackColor = true;
            this.LineFind.CheckedChanged += new EventHandler(this.LineFind_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // SectorFind
            // 
            this.SectorFind = new RadioButton();
            this.SectorFind.AutoSize = true;
            this.SectorFind.Location = new Point(12, yPoint);
            this.SectorFind.Name = "SectorFind";
            this.SectorFind.Size = new Size(115, 24);
            this.SectorFind.Text = "SectorFind";
            this.SectorFind.UseVisualStyleBackColor = true;
            this.SectorFind.CheckedChanged += new EventHandler(this.SectorFind_CheckedChanged);
            yPoint += yPointDelta;

            /* ------------------------------------------------------------
            // 
            // SectorSweep
            // 
            this.SectorSweep = new RadioButton();
            this.SectorSweep.AutoSize = true;
            this.SectorSweep.Location = new Point(12, yPoint);
            this.SectorSweep.Name = "SectorSweep";
            this.SectorSweep.Size = new Size(115, 24);
            this.SectorSweep.Text = "SectorSweep";
            this.SectorSweep.UseVisualStyleBackColor = true;
            this.SectorSweep.CheckedChanged += new EventHandler(this.SectorSweep_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // ColumnSweeps
            // 
            this.ColumnSweeps = new RadioButton();
            this.ColumnSweeps.AutoSize = true;
            this.ColumnSweeps.Location = new Point(12, yPoint);
            this.ColumnSweeps.Name = "ColumnSweeps";
            this.ColumnSweeps.Size = new Size(115, 24);
            this.ColumnSweeps.Text = "ColumnSweeps";
            this.ColumnSweeps.UseVisualStyleBackColor = true;
            this.ColumnSweeps.CheckedChanged += new EventHandler(this.ColumnSweeps_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // RowSweeps
            // 
            this.RowSweeps = new RadioButton();
            this.RowSweeps.AutoSize = true;
            this.RowSweeps.Location = new Point(12, yPoint);
            this.RowSweeps.Name = "RowSweeps";
            this.RowSweeps.Size = new Size(115, 24);
            this.RowSweeps.Text = "RowSweeps";
            this.RowSweeps.UseVisualStyleBackColor = true;
            this.RowSweeps.CheckedChanged += new EventHandler(this.RowSweeps_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // TwoPair
            // 
            this.TwoPair = new RadioButton();
            this.TwoPair.AutoSize = true;
            this.TwoPair.Location = new Point(12, yPoint);
            this.TwoPair.Name = "TwoPair";
            this.TwoPair.Size = new Size(115, 24);
            //this.TwoPair.TabIndex = iTabIndex++;
            //this.TwoPair.TabStop = true;
            this.TwoPair.Text = "TwoPair";
            this.TwoPair.UseVisualStyleBackColor = true;
            this.TwoPair.CheckedChanged += new EventHandler(this.TwoPair_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // ThreesomeRows
            // 
            this.ThreesomeRows = new RadioButton();
            this.ThreesomeRows.AutoSize = true;
            this.ThreesomeRows.Location = new Point(12, yPoint);
            this.ThreesomeRows.Name = "ThreesomeRows";
            this.ThreesomeRows.Size = new Size(115, 24);
            //this.ThreesomeRows.TabIndex = iTabIndex++;
            //this.ThreesomeRows.TabStop = true;
            this.ThreesomeRows.Text = "ThreesomeRows";
            this.ThreesomeRows.UseVisualStyleBackColor = true;
            this.ThreesomeRows.CheckedChanged += new EventHandler(this.ThreesomeRows_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // ThreesomeCols
            // 
            this.ThreesomeCols = new RadioButton();
            this.ThreesomeCols.AutoSize = true;
            this.ThreesomeCols.Location = new Point(12, yPoint);
            this.ThreesomeCols.Name = "ThreesomeCols";
            this.ThreesomeCols.Size = new Size(115, 24);
            //this.ThreesomeCols.TabIndex = iTabIndex++;
            //this.ThreesomeCols.TabStop = true;
            this.ThreesomeCols.Text = "ThreesomeCols";
            this.ThreesomeCols.UseVisualStyleBackColor = true;
            this.ThreesomeCols.CheckedChanged += new EventHandler(this.ThreesomeCols_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // FoursomeRows
            // 
            this.FoursomeRows = new RadioButton();
            this.FoursomeRows.AutoSize = true;
            this.FoursomeRows.Location = new Point(12, yPoint);
            this.FoursomeRows.Name = "FoursomeRows";
            this.FoursomeRows.Size = new Size(115, 24);
            //this.FoursomeRows.TabIndex = iTabIndex++;
            //this.FoursomeRows.TabStop = true;
            this.FoursomeRows.Text = "FoursomeRows";
            this.FoursomeRows.UseVisualStyleBackColor = true;
            this.FoursomeRows.CheckedChanged += new EventHandler(this.FoursomeRows_CheckedChanged);
            yPoint += yPointDelta;
            // 
            // FoursomeCols
            // 
            this.FoursomeCols = new RadioButton();
            this.FoursomeCols.AutoSize = true;
            this.FoursomeCols.Location = new Point(12, yPoint);
            this.FoursomeCols.Name = "FoursomeCols";
            this.FoursomeCols.Size = new Size(115, 24);
            //this.FoursomeCols.TabIndex = iTabIndex++;
            //this.FoursomeCols.TabStop = true;
            this.FoursomeCols.Text = "FoursomeCols";
            this.FoursomeCols.UseVisualStyleBackColor = true;
            this.FoursomeCols.CheckedChanged += new EventHandler(this.FoursomeCols_CheckedChanged);
            yPoint += yPointDelta;

            ------------------------------------------------------------ */

            // 
            // RadioPanel
            // 
            this.RadioPanel = new Panel();
            this.RadioPanel.SuspendLayout();
            //this.RadioPanel.Controls.Add(this.Neighbor);
            //this.RadioPanel.Controls.Add(this.AllNeighbors);
            this.RadioPanel.Controls.Add(this.RangeCheck);
            this.RadioPanel.Controls.Add(this.LineFind);
            this.RadioPanel.Controls.Add(this.SectorFind);
            //this.RadioPanel.Controls.Add(this.SectorSweep);
            //this.RadioPanel.Controls.Add(this.ColumnSweeps);
            //this.RadioPanel.Controls.Add(this.RowSweeps);
            //this.RadioPanel.Controls.Add(this.TwoPair);
            //this.RadioPanel.Controls.Add(this.ThreesomeRows);
            //this.RadioPanel.Controls.Add(this.ThreesomeCols);
            //this.RadioPanel.Controls.Add(this.FoursomeRows);
            //this.RadioPanel.Controls.Add(this.FoursomeCols);
            this.RadioPanel.BorderStyle = BorderStyle.FixedSingle;
            this.RadioPanel.Location = new Point(740, 75);
            this.RadioPanel.Name = "RadioPanel";
            this.RadioPanel.Size = new Size(160, 110);
            this.RadioPanel.TabIndex = iTabIndex++;

            this.Controls.Add(this.RadioPanel);
            this.RadioPanel.ResumeLayout(false);
            this.RadioPanel.PerformLayout();

            // 
            // btnLoad
            // 
            this.btnLoad = new Button();
            this.btnLoad.Click += Load_Click;
            this.btnLoad.BackColor = Color.LightGray;
            this.btnLoad.ForeColor = Color.Black;
            this.btnLoad.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new Point(740, 194);
            this.btnLoad.Size = new Size(100, 60);
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
            this.btnSave.Location = new Point(740, 260);
            this.btnSave.Size = new Size(100, 60);
            this.btnSave.TabIndex = iTabIndex++;
            this.btnSave.Text = "Save";
            this.Controls.Add(this.btnSave);

            // 
            // CouldBe
            // 
            this.CouldBe = new CheckBox();
            this.CouldBe.AutoSize = true;
            this.CouldBe.Checked = true;
            this.CouldBe.Location = new Point(740, 326);
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
            this.objLogBox = new LogBox(740, 360, 350, 600, iTabIndex);
            this.Controls.Add(this.objLogBox.objBox);

            // 
            // SudokuForms
            // 
            this.AutoScaleDimensions = new SizeF(9F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1100, 980);

            this.Name = "Sudokirk";
            this.Text = "Sudokirk";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // This is the KeyPress function for all 81 of our Squares.
        void sq_KeyPress(object sender, KeyPressEventArgs e)
        {
            Button btn = sender as Button;
            int iTab = btn.TabIndex;
            char keyChar = e.KeyChar;

            // Calculate objBoard.rgSquare[col,row] location from the tabindex.
            // TabIndex is [1..81]; the array is [0..8][0..8].
            curTab = iTab;
            curCol = ((iTab - 1) % 9);  // Modulo (remainder)
            curRow = ((iTab - 1) / 9);  // Divide
            curChar = keyChar;

            //objLogBox.Log("KeyPress " + keyChar);

            if (keyChar >= '1' && keyChar <= '9')
            {
                //objLogBox.Log("Set: tab " + iTab + ": [" + curCol + "," + curRow + "] key = " + keyChar);
                objBoard.rgSquare[curCol, curRow].Winner(keyChar, true, Color.DarkGreen, objBoard);
                //Techniques.Neighbor(objBoard.rgSquare, curCol, curRow, keyChar);
            }
        }

        // KeyDown for all 81 squares
        void sq_KeyDown(object sender, KeyEventArgs e)
        {
            Button btn = sender as Button;
            int iTab = btn.TabIndex;
            Keys keyCode = e.KeyCode;

            // Calculate objBoard.rgSquare[col,row] location from the tabindex.
            // TabIndex is [1..81]; the array is [0..8][0..8].
            curTab = iTab;
            curCol = ((iTab - 1) % 9);  // Modulo (remainder)
            curRow = ((iTab - 1) / 9);  // Divide

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

        // This is the ButtonClick function for all 81 of our Squares.
        void sq_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ClickSquare(btn.TabIndex);
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

        /*
        void Neighbor_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.Neighbor;
            }
        }

        void AllNeighbors_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.AllNeighbors;
            }
        }
        */

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

        /* -----------------------------------------------------------------

        void SectorSweep_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.SectorSweep;
            }
        }

        void ColumnSweeps_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.ColumnSweeps;
            }
        }

        void RowSweeps_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.RowSweeps;
            }
        }

        void TwoPair_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.TwoPair;
            }
        }

        void ThreesomeRows_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.ThreesomeRows;
            }
        }

        void ThreesomeCols_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.ThreesomeCols;
            }
        }

        void FoursomeRows_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.FoursomeRows;
            }
        }

        void FoursomeCols_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio.Checked)
            {
                curTechnique = Technique.FoursomeCols;
            }
        }

        ----------------------------------------------------------------- */

        void CouldBe_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = sender as CheckBox;
            foreach (Square objSq in objBoard.rgSquare)
            {
                if (objSq.iWinner == 0)
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
                //case Technique.Neighbor:
                //    if (curTab != -1)
                //    {
                //        Techniques.Neighbor(objBoard, curCol, curRow, curChar);
                //    }
                //    break;
                //case Technique.AllNeighbors:
                //    Techniques.AllNeighbors(objBoard, objLogBox);
                //    break;
                case Technique.RangeCheck:
                    Techniques.AllRanges(objBoard, objLogBox);
                    break;
                case Technique.LineFind:
                    Techniques.FLineFind(objBoard, objLogBox);
                    break;
                case Technique.SectorFind:
                    Techniques.FSectorsFind(objBoard, objLogBox);
                    break;
                //case Technique.SectorSweep:
                //    Techniques.SectorSweep(objBoard, objLogBox);
                //    break;
                //case Technique.ColumnSweeps:
                //    Techniques.ColumnSweeps(objBoard, objLogBox);
                //    break;
                //case Technique.RowSweeps:
                //    Techniques.RowSweeps(objBoard, objLogBox);
                //    break;
                //case Technique.TwoPair:
                //    Techniques.TwoPair(objBoard, objLogBox);
                //    break;
                //case Technique.ThreesomeRows:
                //    Techniques.ThreesomeRows(objBoard, objLogBox);
                //    break;
                //case Technique.ThreesomeCols:
                //    Techniques.ThreesomeCols(objBoard, objLogBox);
                //    break;
                //case Technique.FoursomeRows:
                //    Techniques.FoursomeRows(objBoard, objLogBox);
                //    break;
                //case Technique.FoursomeCols:
                //    Techniques.FoursomeCols(objBoard, objLogBox);
                //    break;
            }
        }

        void ClickSquare(int iTab)
        {
            {
                // Calculate objBoard.rgSquare[col,row] location from the tabindex.
                // TabIndex is [1..81]; the array is [0..8][0..8].
                curTab = iTab;
                curCol = ((iTab - 1) % 9);  // Modulo (remainder)
                curRow = ((iTab - 1) / 9);  // Divide
                Square mySquare = objBoard.rgSquare[curCol, curRow];
                if (mySquare.iWinner != 0)
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
            f.SaveFile(objBoard);
        }

        // This is the ButtonClick function for the Load button.
        void Load_Click(object sender, EventArgs e)
        {
            FileIO f = new FileIO();
            f.LoadFile(this, objBoard);
        }

        Button btnReset;
        Button btnClear;
        Button btnStep;
        LogBox objLogBox;
        CheckBox CouldBe;
        //RadioButton Neighbor;
        //RadioButton AllNeighbors;
        RadioButton RangeCheck;
        RadioButton LineFind;
        RadioButton SectorFind;
        //RadioButton SectorSweep;
        //RadioButton ColumnSweeps;
        //RadioButton RowSweeps;
        //RadioButton TwoPair;
        //RadioButton ThreesomeRows;
        //RadioButton ThreesomeCols;
        //RadioButton FoursomeRows;
        //RadioButton FoursomeCols;
        Panel RadioPanel;
        Button btnLoad;
        Button btnSave;

    }
}
 
 