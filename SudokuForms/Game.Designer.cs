using System;
using System.Drawing;
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

            int iTabIndex = 100;

            // 
            // btnReset
            // 
            this.btnReset = new Button();
            this.btnReset.Click += Reset_Click;
            this.btnReset.BackColor = Color.LightGray;
            this.btnReset.ForeColor = Color.Black;
            this.btnReset.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new Point(740+xDelta, 10);
            this.btnReset.Size = new Size(80, 36);
            this.btnReset.TabIndex = iTabIndex++;
            this.btnReset.Text = "Reset";
            this.Controls.Add(this.btnReset);
            //this.objLogBox.Log("Init; btnReset.Left  is set to " + this.btnReset.Left);

            // 
            // btnClear
            // 
            this.btnClear = new Button();
            this.btnClear.Click += Clear_Click;
            this.btnClear.BackColor = Color.LightGray;
            this.btnClear.ForeColor = Color.Black;
            this.btnClear.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new Point(824+xDelta, 10);
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
            this.btnStep.Location = new Point(846+xDelta, 70);
            this.btnStep.Size = new Size(80, 36);
            this.btnStep.TabIndex = iTabIndex++;
            this.btnStep.Text = "Step";
            this.Controls.Add(this.btnStep);

            // ------------------------------------------
            // All our radio buttons.

            int yPoint = 8;
            int yPointDelta = 20;

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
            // RadioPanel
            // 
            this.RadioPanel = new Panel();
            this.RadioPanel.SuspendLayout();
            this.RadioPanel.Controls.Add(this.RangeCheck);
            this.RadioPanel.Controls.Add(this.LineFind);
            this.RadioPanel.Controls.Add(this.SectorFind);
            this.RadioPanel.BorderStyle = BorderStyle.FixedSingle;
            this.RadioPanel.Location = new Point(740+xDelta, 52);
            this.RadioPanel.Name = "RadioPanel";
            this.RadioPanel.Size = new Size(100, 74);
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
            this.btnLoad.Location = new Point(740+xDelta, 130);
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
            this.btnSave.Location = new Point(740+xDelta, 170);
            this.btnSave.Size = new Size(80, 36);
            this.btnSave.TabIndex = iTabIndex++;
            this.btnSave.Text = "Save";
            this.Controls.Add(this.btnSave);

            // 
            // CouldBe
            // 
            this.CouldBe = new CheckBox();
            this.CouldBe.AutoSize = true;
            this.CouldBe.Checked = true;
            this.CouldBe.Location = new Point(740+xDelta, 210);
            this.CouldBe.Name = "CouldBe";
            this.CouldBe.Size = new Size(104, 24);
            this.CouldBe.TabIndex = iTabIndex++;
            this.CouldBe.Text = "CouldBe";
            this.CouldBe.UseVisualStyleBackColor = true;
            this.CouldBe.CheckedChanged += new EventHandler(this.CouldBe_CheckedChanged);
            this.Controls.Add(this.CouldBe);

            // 
            // Super
            // 
            this.Super = new CheckBox();
            this.Super.AutoSize = true;
            this.Super.Checked = fSuper;
            this.Super.Location = new Point(826+xDelta, 210);
            this.Super.Name = "Super";
            this.Super.Size = new Size(104, 24);
            this.Super.TabIndex = iTabIndex++;
            this.Super.Text = "SuperSudoku";
            this.Super.UseVisualStyleBackColor = true;
            this.Super.CheckedChanged += new EventHandler(this.Super_CheckedChanged);
            this.Controls.Add(this.Super);

            //
            // LogBox
            //
            this.objLogBox = new LogBox(740+xDelta, 230, 350, 600, iTabIndex);
            this.Controls.Add(this.objLogBox.objBox);

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

        private Point Relocate(Point p) {
            p.X += xMove;
            return p;
        }

        private void MoveButton(Button b) {
            b.Location = Relocate(b.Location);
        }

        private void MoveButtons() {
            MoveButton(this.btnReset);
            MoveButton(this.btnClear);
            MoveButton(this.btnStep);
            MoveButton(this.btnLoad);
            MoveButton(this.btnSave);
            this.RadioPanel.Location = Relocate(this.RadioPanel.Location);
            this.CouldBe.Location = Relocate(this.CouldBe.Location);
            this.Super.Location = Relocate(this.Super.Location);
            this.objLogBox.objBox.Location = Relocate(this.objLogBox.objBox.Location);
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
                objBoard.rgSquare[curCol, curRow].Winner(keyChar, true, Color.DarkGreen, objBoard);
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

        void Super_CheckedChanged(object sender, EventArgs e)
        {
            SuperToggle();
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
        CheckBox Super;
        RadioButton RangeCheck;
        RadioButton LineFind;
        RadioButton SectorFind;
        Panel RadioPanel;
        Button btnLoad;
        Button btnSave;

    }
}
 
 