
using System;

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
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(800, 38);
            this.btnReset.Size = new System.Drawing.Size(120, 72);
            this.btnReset.TabIndex = 98;
            this.btnReset.Text = "Reset";
            this.Controls.Add(this.btnReset);
            // 
            // btnStep
            // 
            this.btnStep = new System.Windows.Forms.Button();
            this.btnStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new System.Drawing.Point(900, 160);
            this.btnStep.Size = new System.Drawing.Size(112, 72);
            this.btnStep.TabIndex = 99;
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
            this.Neighbor.TabIndex = 91;
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
            this.SectorSweep.TabIndex = 92;
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
            this.ColumnSweep.TabIndex = 93;
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
            this.RowSweep.TabIndex = 94;
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
            this.RadioPanel.Location = new System.Drawing.Point(740, 126);
            this.RadioPanel.Name = "RadioPanel";
            this.RadioPanel.Size = new System.Drawing.Size(150, 140);
            this.RadioPanel.TabIndex = 95;
            this.Controls.Add(this.RadioPanel);
            this.RadioPanel.ResumeLayout(false);
            this.RadioPanel.PerformLayout();

            // 
            // CouldBes
            // 
            this.CouldBes = new System.Windows.Forms.CheckBox();
            this.CouldBes.AutoSize = true;
            this.CouldBes.Location = new System.Drawing.Point(750, 300);
            this.CouldBes.Name = "CouldBes";
            this.CouldBes.Size = new System.Drawing.Size(104, 24);
            this.CouldBes.TabIndex = 90;
            this.CouldBes.Text = "CouldBes";
            this.CouldBes.UseVisualStyleBackColor = true;
            this.CouldBes.CheckedChanged += new System.EventHandler(this.CouldBes_CheckedChanged);
            this.Controls.Add(this.CouldBes);


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

        public void sq_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            SetSquare(btn.TabIndex, e.KeyChar);
        }

        #endregion

        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStep;
        private LogBox objLogBox;
        private System.Windows.Forms.CheckBox CouldBes;
        private System.Windows.Forms.RadioButton Neighbor;
        private System.Windows.Forms.RadioButton SectorSweep;
        private System.Windows.Forms.RadioButton ColumnSweep;
        private System.Windows.Forms.RadioButton RowSweep;
        private System.Windows.Forms.Panel RadioPanel;

    }
}