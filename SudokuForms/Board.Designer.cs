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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
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
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(800, 38);
            this.btnReset.Size = new System.Drawing.Size(120, 72);
            this.btnReset.TabIndex = 90;
            this.btnReset.Text = "Reset";
            this.Controls.Add(this.btnReset);
            // 
            // btnStep
            // 
            this.btnStep = new System.Windows.Forms.Button();
            this.btnStep.Click += Step_Click;
            this.btnStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new System.Drawing.Point(900, 160);
            this.btnStep.Size = new System.Drawing.Size(112, 72);
            this.btnStep.TabIndex = 93;
            this.btnStep.Text = "Step";
            this.Controls.Add(this.btnStep);
            //
            // LogBox
            //
            this.objLogBox = new LogBox(740, 450, 350, 518, 100);
            this.Controls.Add(this.objLogBox.objBox);
            
            // 
            // Neighbor
            // 
            this.Neighbor = new System.Windows.Forms.RadioButton();
            this.Neighbor.AutoSize = true;
            this.Neighbor.Location = new System.Drawing.Point(12, 12);
            this.Neighbor.Name = "Neighbor";
            this.Neighbor.Size = new System.Drawing.Size(98, 24);
            this.Neighbor.TabIndex = 94;
            this.Neighbor.TabStop = true;
            this.Neighbor.Text = "Neighbor";
            this.Neighbor.UseVisualStyleBackColor = true;
            this.Neighbor.CheckedChanged += new System.EventHandler(this.Neighbor_CheckedChanged);
            // 
            // SectorSweep
            // 
            this.SectorSweep = new System.Windows.Forms.RadioButton();
            this.SectorSweep.AutoSize = true;
            this.SectorSweep.Location = new System.Drawing.Point(12, 42);
            this.SectorSweep.Name = "SectorSweep";
            this.SectorSweep.Size = new System.Drawing.Size(130, 24);
            this.SectorSweep.TabIndex = 95;
            this.SectorSweep.TabStop = true;
            this.SectorSweep.Text = "SectorSweep";
            this.SectorSweep.UseVisualStyleBackColor = true;
            this.SectorSweep.CheckedChanged += new System.EventHandler(this.SectorSweep_CheckedChanged);
            // 
            // ColumnSweep
            // 
            this.ColumnSweep = new System.Windows.Forms.RadioButton();
            this.ColumnSweep.AutoSize = true;
            this.ColumnSweep.Location = new System.Drawing.Point(12, 72);
            this.ColumnSweep.Name = "ColumnSweep";
            this.ColumnSweep.Size = new System.Drawing.Size(137, 24);
            this.ColumnSweep.TabIndex = 96;
            this.ColumnSweep.TabStop = true;
            this.ColumnSweep.Text = "ColumnSweep";
            this.ColumnSweep.UseVisualStyleBackColor = true;
            this.ColumnSweep.CheckedChanged += new System.EventHandler(this.ColumnSweep_CheckedChanged);
            // 
            // RowSweep
            // 
            this.RowSweep = new System.Windows.Forms.RadioButton();
            this.RowSweep.AutoSize = true;
            this.RowSweep.Location = new System.Drawing.Point(12, 102);
            this.RowSweep.Name = "RowSweep";
            this.RowSweep.Size = new System.Drawing.Size(115, 24);
            this.RowSweep.TabIndex = 97;
            this.RowSweep.TabStop = true;
            this.RowSweep.Text = "RowSweep";
            this.RowSweep.UseVisualStyleBackColor = true;
            this.RowSweep.CheckedChanged += new System.EventHandler(this.RowSweep_CheckedChanged);
            // 
            // RadioPanel
            // 
            this.RadioPanel = new System.Windows.Forms.Panel();
            this.RadioPanel.SuspendLayout();
            this.RadioPanel.Controls.Add(this.Neighbor);
            this.RadioPanel.Controls.Add(this.RowSweep);
            this.RadioPanel.Controls.Add(this.SectorSweep);
            this.RadioPanel.Controls.Add(this.ColumnSweep);
            this.RadioPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RadioPanel.Location = new System.Drawing.Point(740, 126);
            this.RadioPanel.Name = "RadioPanel";
            this.RadioPanel.Size = new System.Drawing.Size(150, 140);
            this.RadioPanel.TabIndex = 92;

            this.Controls.Add(this.RadioPanel);
            this.RadioPanel.ResumeLayout(false);
            this.RadioPanel.PerformLayout();

            // 
            // CouldBe
            // 
            this.CouldBe = new System.Windows.Forms.CheckBox();
            this.CouldBe.AutoSize = true;
            this.CouldBe.Checked = true;
            this.CouldBe.Location = new System.Drawing.Point(750, 300);
            this.CouldBe.Name = "CouldBe";
            this.CouldBe.Size = new System.Drawing.Size(104, 24);
            this.CouldBe.TabIndex = 98;
            this.CouldBe.Text = "CouldBe";
            this.CouldBe.UseVisualStyleBackColor = true;
            this.CouldBe.CheckedChanged += new System.EventHandler(this.CouldBe_CheckedChanged);
            this.Controls.Add(this.CouldBe);


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
            SetSquare(btn.TabIndex, e.KeyChar);
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
            }
        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStep;
        private LogBox objLogBox;
        private System.Windows.Forms.CheckBox CouldBe;
        private System.Windows.Forms.RadioButton Neighbor;
        private System.Windows.Forms.RadioButton SectorSweep;
        private System.Windows.Forms.RadioButton ColumnSweep;
        private System.Windows.Forms.RadioButton RowSweep;
        private System.Windows.Forms.Panel RadioPanel;

    }
}