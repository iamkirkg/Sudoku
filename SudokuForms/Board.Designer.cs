using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuForms
{
    partial class Board
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

            // 
            // btnReset
            // 
            this.btnReset = new System.Windows.Forms.Button();
            this.btnReset.BackColor = Color.LightGray;
            this.btnReset.ForeColor = Color.Black;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(740, 10);
            this.btnReset.Size = new System.Drawing.Size(100, 60);
            this.btnReset.TabIndex = 101;
            this.btnReset.Text = "Reset";
            this.Controls.Add(this.btnReset);
            // 
            // btnStep
            // 
            this.btnStep = new System.Windows.Forms.Button();
            this.btnStep.Click += Step_Click;
            this.btnStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new System.Drawing.Point(910, 130);
            this.btnStep.Size = new System.Drawing.Size(90, 60);
            this.btnStep.TabIndex = 102;
            this.btnStep.Text = "Step";
            this.Controls.Add(this.btnStep);
            
            // 
            // Neighbor
            // 
            this.Neighbor = new System.Windows.Forms.RadioButton();
            this.Neighbor.AutoSize = true;
            this.Neighbor.Location = new System.Drawing.Point(12, 12);
            this.Neighbor.Name = "Neighbor";
            this.Neighbor.Size = new System.Drawing.Size(98, 24);
            this.Neighbor.TabIndex = 103;
            this.Neighbor.TabStop = true;
            this.Neighbor.Text = "Neighbor";
            this.Neighbor.UseVisualStyleBackColor = true;
            this.Neighbor.CheckedChanged += new System.EventHandler(this.Neighbor_CheckedChanged);
            // 
            // AllNeighbors
            // 
            this.AllNeighbors = new System.Windows.Forms.RadioButton();
            this.AllNeighbors.AutoSize = true;
            this.AllNeighbors.Location = new System.Drawing.Point(12, 42);
            this.AllNeighbors.Name = "AllNeighbors";
            this.AllNeighbors.Size = new System.Drawing.Size(98, 24);
            this.AllNeighbors.TabIndex = 104;
            this.AllNeighbors.TabStop = true;
            this.AllNeighbors.Text = "AllNeighbors";
            this.AllNeighbors.UseVisualStyleBackColor = true;
            this.AllNeighbors.CheckedChanged += new System.EventHandler(this.AllNeighbors_CheckedChanged);
            // 
            // SectorSweep
            // 
            this.SectorSweep = new System.Windows.Forms.RadioButton();
            this.SectorSweep.AutoSize = true;
            this.SectorSweep.Location = new System.Drawing.Point(12, 72);
            this.SectorSweep.Name = "SectorSweep";
            this.SectorSweep.Size = new System.Drawing.Size(130, 24);
            this.SectorSweep.TabIndex = 105;
            this.SectorSweep.TabStop = true;
            this.SectorSweep.Text = "SectorSweep";
            this.SectorSweep.UseVisualStyleBackColor = true;
            this.SectorSweep.CheckedChanged += new System.EventHandler(this.SectorSweep_CheckedChanged);
            // 
            // ColumnSweep
            // 
            this.ColumnSweep = new System.Windows.Forms.RadioButton();
            this.ColumnSweep.AutoSize = true;
            this.ColumnSweep.Location = new System.Drawing.Point(12, 102);
            this.ColumnSweep.Name = "ColumnSweep";
            this.ColumnSweep.Size = new System.Drawing.Size(136, 24);
            this.ColumnSweep.TabIndex = 106;
            this.ColumnSweep.TabStop = true;
            this.ColumnSweep.Text = "ColumnSweep";
            this.ColumnSweep.UseVisualStyleBackColor = true;
            this.ColumnSweep.CheckedChanged += new System.EventHandler(this.ColumnSweep_CheckedChanged);
            // 
            // RowSweep
            // 
            this.RowSweep = new System.Windows.Forms.RadioButton();
            this.RowSweep.AutoSize = true;
            this.RowSweep.Location = new System.Drawing.Point(12, 132);
            this.RowSweep.Name = "RowSweep";
            this.RowSweep.Size = new System.Drawing.Size(115, 24);
            this.RowSweep.TabIndex = 107;
            this.RowSweep.TabStop = true;
            this.RowSweep.Text = "RowSweep";
            this.RowSweep.UseVisualStyleBackColor = true;
            this.RowSweep.CheckedChanged += new System.EventHandler(this.RowSweep_CheckedChanged);
            // 
            // TwoPair
            // 
            this.TwoPair = new System.Windows.Forms.RadioButton();
            this.TwoPair.AutoSize = true;
            this.TwoPair.Location = new System.Drawing.Point(12, 162);
            this.TwoPair.Name = "TwoPair";
            this.TwoPair.Size = new System.Drawing.Size(115, 24);
            this.TwoPair.TabIndex = 108;
            this.TwoPair.TabStop = true;
            this.TwoPair.Text = "TwoPair";
            this.TwoPair.UseVisualStyleBackColor = true;
            this.TwoPair.CheckedChanged += new System.EventHandler(this.TwoPair_CheckedChanged);
            // 
            // ThreesomeRow
            // 
            this.ThreesomeRow = new System.Windows.Forms.RadioButton();
            this.ThreesomeRow.AutoSize = true;
            this.ThreesomeRow.Location = new System.Drawing.Point(12, 192);
            this.ThreesomeRow.Name = "ThreesomeRow";
            this.ThreesomeRow.Size = new System.Drawing.Size(115, 24);
            this.ThreesomeRow.TabIndex = 109;
            this.ThreesomeRow.TabStop = true;
            this.ThreesomeRow.Text = "ThreesomeRow";
            this.ThreesomeRow.UseVisualStyleBackColor = true;
            this.ThreesomeRow.CheckedChanged += new System.EventHandler(this.ThreesomeRow_CheckedChanged);
            // 
            // ThreesomeCol
            // 
            this.ThreesomeCol = new System.Windows.Forms.RadioButton();
            this.ThreesomeCol.AutoSize = true;
            this.ThreesomeCol.Location = new System.Drawing.Point(12, 222);
            this.ThreesomeCol.Name = "ThreesomeCol";
            this.ThreesomeCol.Size = new System.Drawing.Size(115, 24);
            this.ThreesomeCol.TabIndex = 110;
            this.ThreesomeCol.TabStop = true;
            this.ThreesomeCol.Text = "ThreesomeCol";
            this.ThreesomeCol.UseVisualStyleBackColor = true;
            this.ThreesomeCol.CheckedChanged += new System.EventHandler(this.ThreesomeCol_CheckedChanged);
            // 
            // LineFind
            // 
            this.LineFind = new System.Windows.Forms.RadioButton();
            this.LineFind.AutoSize = true;
            this.LineFind.Location = new System.Drawing.Point(12, 252);
            this.LineFind.Name = "LineFind";
            this.LineFind.Size = new System.Drawing.Size(115, 24);
            this.LineFind.TabIndex = 111;
            this.LineFind.TabStop = true;
            this.LineFind.Text = "LineFind";
            this.LineFind.UseVisualStyleBackColor = true;
            this.LineFind.CheckedChanged += new System.EventHandler(this.LineFind_CheckedChanged);
            // 
            // RadioPanel
            // 
            this.RadioPanel = new System.Windows.Forms.Panel();
            this.RadioPanel.SuspendLayout();
            this.RadioPanel.Controls.Add(this.Neighbor);
            this.RadioPanel.Controls.Add(this.AllNeighbors);
            this.RadioPanel.Controls.Add(this.SectorSweep);
            this.RadioPanel.Controls.Add(this.ColumnSweep);
            this.RadioPanel.Controls.Add(this.RowSweep);
            this.RadioPanel.Controls.Add(this.TwoPair);
            this.RadioPanel.Controls.Add(this.ThreesomeRow);
            this.RadioPanel.Controls.Add(this.ThreesomeCol);
            this.RadioPanel.Controls.Add(this.LineFind);
            this.RadioPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RadioPanel.Location = new System.Drawing.Point(740, 75);
            this.RadioPanel.Name = "RadioPanel";
            this.RadioPanel.Size = new System.Drawing.Size(160, 290);
            this.RadioPanel.TabIndex = 112;

            this.Controls.Add(this.RadioPanel);
            this.RadioPanel.ResumeLayout(false);
            this.RadioPanel.PerformLayout();

            // 
            // btnLoad
            // 
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnLoad.Click += Load_Click;
            this.btnLoad.BackColor = Color.LightGray;
            this.btnLoad.ForeColor = Color.Black;
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoad.Location = new System.Drawing.Point(740, 374);
            this.btnLoad.Size = new System.Drawing.Size(100, 60);
            this.btnLoad.TabIndex = 113;
            this.btnLoad.Text = "Load";
            this.Controls.Add(this.btnLoad);

            // 
            // btnSave
            // 
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSave.Click += Save_Click;
            this.btnSave.BackColor = Color.LightGray;
            this.btnSave.ForeColor = Color.Black;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(740, 436);
            this.btnSave.Size = new System.Drawing.Size(100, 60);
            this.btnSave.TabIndex = 114;
            this.btnSave.Text = "Save";
            this.Controls.Add(this.btnSave);

            // 
            // CouldBe
            // 
            this.CouldBe = new System.Windows.Forms.CheckBox();
            this.CouldBe.AutoSize = true;
            this.CouldBe.Checked = true;
            this.CouldBe.Location = new System.Drawing.Point(740, 500);
            this.CouldBe.Name = "CouldBe";
            this.CouldBe.Size = new System.Drawing.Size(104, 24);
            this.CouldBe.TabIndex = 115;
            this.CouldBe.Text = "CouldBe";
            this.CouldBe.UseVisualStyleBackColor = true;
            this.CouldBe.CheckedChanged += new System.EventHandler(this.CouldBe_CheckedChanged);
            this.Controls.Add(this.CouldBe);

            //
            // LogBox
            //
            this.objLogBox = new LogBox(740, 530, 350, 440, 116);
            this.Controls.Add(this.objLogBox.objBox);

            // 
            // SudokuForms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 980);

            this.Name = "Sudokirk";
            //this.Text = "Sudokirk";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // This is the KeyPress function for all 81 of our Squares.
        private void sq_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            KeyPressSquare(btn.TabIndex, e.KeyChar);
        }

        // This is the ButtonClick function for all 81 of our Squares.
        private void sq_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            ClickSquare(btn.TabIndex);
        }

        // This is the ButtonClick function for the Step button.
        private void Step_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            switch (curTechnique)
            {
                case Technique.none:
                    objLogBox.Log("Step: no selection");
                    break;
                case Technique.Neighbor:
                    objLogBox.Log("Step: Neighbor");
                    if (curTab != -1)
                    {
                        Techniques.Neighbor(myBoard, curCol, curRow, curChar);
                    }
                    break;
                case Technique.AllNeighbors:
                    objLogBox.Log("Step: AllNeighbors");
                    Techniques.AllNeighbors(myBoard);
                    break;
                case Technique.SectorSweep:
                    objLogBox.Log("Step: SectorSweep");
                    Techniques.SectorSweep(myBoard);
                    break;
                case Technique.ColumnSweep:
                    objLogBox.Log("Step: ColumnSweep");
                    Techniques.ColumnSweep(myBoard, curCol);
                    break;
                case Technique.RowSweep:
                    objLogBox.Log("Step: RowSweep");
                    Techniques.RowSweep(myBoard, curRow);
                    break;
                case Technique.TwoPair:
                    objLogBox.Log("Step: TwoPair");
                    Techniques.TwoPair(myBoard, objLogBox);
                    break;
                case Technique.ThreesomeRow:
                    objLogBox.Log("Step: ThreesomeRow");
                    Techniques.ThreesomeRow(myBoard, curRow, objLogBox);
                    break;
                case Technique.ThreesomeCol:
                    objLogBox.Log("Step: ThreesomeCol");
                    Techniques.ThreesomeCol(myBoard, curCol, objLogBox);
                    break;
                case Technique.LineFind:
                    objLogBox.Log("Step: LineFind");
                    Techniques.FLineFind(myBoard, objLogBox);
                    break;
            }
        }

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStep;
        private LogBox objLogBox;
        private System.Windows.Forms.CheckBox CouldBe;
        private System.Windows.Forms.RadioButton Neighbor;
        private System.Windows.Forms.RadioButton AllNeighbors;
        private System.Windows.Forms.RadioButton SectorSweep;
        private System.Windows.Forms.RadioButton ColumnSweep;
        private System.Windows.Forms.RadioButton RowSweep;
        private System.Windows.Forms.RadioButton TwoPair;
        private System.Windows.Forms.RadioButton ThreesomeRow;
        private System.Windows.Forms.RadioButton ThreesomeCol;
        private System.Windows.Forms.RadioButton LineFind;
        private System.Windows.Forms.Panel RadioPanel;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;

    }
}