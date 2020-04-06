
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
            // btnStep
            // 
            this.btnStep = new System.Windows.Forms.Button();
            this.btnStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new System.Drawing.Point(740, 38);
            this.btnStep.Size = new System.Drawing.Size(112, 72);
            this.btnStep.TabIndex = 98;
            this.btnStep.Text = "Step";
            // 
            // btnGo
            // 
            this.btnGo = new System.Windows.Forms.Button();
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(740, 126);
            this.btnGo.Size = new System.Drawing.Size(112, 72);
            this.btnGo.TabIndex = 99;
            this.btnGo.Text = "Go";
            //
            // LogBox
            //
            this.objLogBox = new LogBox(740, 450, 350, 518, 100);
            this.Controls.Add(this.objLogBox.objBox);

            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 980);

            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnStep);

            this.Name = "Form3";
            this.Text = "Form3";

            this.ResumeLayout(false);
        }

        public void sq_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            System.Windows.Forms.Button btn = sender as System.Windows.Forms.Button;
            SetSquare(btn.TabIndex, e.KeyChar);
        }

        #endregion

        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnGo;
        private LogBox objLogBox;
    }
}