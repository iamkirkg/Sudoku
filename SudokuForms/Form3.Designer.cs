﻿
using System;

namespace SudokuForms
{
    partial class Form3
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
            this.btn00 = new System.Windows.Forms.Button();
            this.btn01 = new System.Windows.Forms.Button();
            this.btn02 = new System.Windows.Forms.Button();
            this.btn10 = new System.Windows.Forms.Button();
            this.btn11 = new System.Windows.Forms.Button();
            this.btn12 = new System.Windows.Forms.Button();
            this.btn20 = new System.Windows.Forms.Button();
            this.btn21 = new System.Windows.Forms.Button();
            this.btn22 = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            //this.LogOriginal = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btn00
            // 
            this.btn00.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn00.Location = new System.Drawing.Point(2, 2);
            this.btn00.Size = new System.Drawing.Size(110, 144);
            this.btn00.TabIndex = 0;
            this.btn00.Text = "1 2 3 4 5 6 7 8 9";
            this.btn00.KeyPress += sq00_KeyPress;
            // 
            // btn01
            // 
            this.btn01.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn01.Location = new System.Drawing.Point(118, 2);
            this.btn01.Size = new System.Drawing.Size(110, 144);
            this.btn01.TabIndex = 1;
            this.btn01.Text = "1 2 3 4 5 6 7 8 9";
            this.btn01.KeyPress += sq01_KeyPress;
            // 
            // btn02
            // 
            this.btn02.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn02.Location = new System.Drawing.Point(234, 2);
            this.btn02.Size = new System.Drawing.Size(110, 144);
            this.btn02.TabIndex = 2;
            this.btn02.Text = "1 2 3 4 5 6 7 8 9";
            this.btn02.KeyPress += sq02_KeyPress;
            // 
            // btn10
            // 
            this.btn10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn10.Location = new System.Drawing.Point(2, 148);
            this.btn10.Size = new System.Drawing.Size(110, 144);
            this.btn10.TabIndex = 3;
            this.btn10.Text = "1 2 3 4 5 6 7 8 9";
            this.btn10.KeyPress += sq10_KeyPress;
            // 
            // btn11
            // 
            this.btn11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn11.Location = new System.Drawing.Point(118, 148);
            this.btn11.Size = new System.Drawing.Size(110, 144);
            this.btn11.TabIndex = 4;
            this.btn11.Text = "1 2 3 4 5 6 7 8 9";
            this.btn11.KeyPress += sq11_KeyPress;
            // 
            // btn12
            // 
            this.btn12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn12.Location = new System.Drawing.Point(234, 148);
            this.btn12.Size = new System.Drawing.Size(110, 144);
            this.btn12.TabIndex = 5;
            this.btn12.Text = "1 2 3 4 5 6 7 8 9";
            this.btn12.KeyPress += sq12_KeyPress;
            // 
            // btn20
            // 
            this.btn20.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn20.Location = new System.Drawing.Point(2, 298);
            this.btn20.Size = new System.Drawing.Size(110, 144);
            this.btn20.TabIndex = 6;
            this.btn20.Text = "1 2 3 4 5 6 7 8 9";
            this.btn20.KeyPress += sq20_KeyPress;
            // 
            // btn21
            // 
            this.btn21.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn21.Location = new System.Drawing.Point(118, 298);
            this.btn21.Size = new System.Drawing.Size(110, 144);
            this.btn21.TabIndex = 7;
            this.btn21.Text = "1 2 3 4 5 6 7 8 9";
            this.btn21.KeyPress += sq21_KeyPress;
            // 
            // btn22
            // 
            this.btn22.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn22.Location = new System.Drawing.Point(234, 298);
            this.btn22.Size = new System.Drawing.Size(110, 144);
            this.btn22.TabIndex = 8;
            this.btn22.Text = "1 2 3 4 5 6 7 8 9";
            this.btn22.KeyPress += sq22_KeyPress;
            // 
            // btnStep
            // 
            this.btnStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new System.Drawing.Point(374, 38);
            this.btnStep.Size = new System.Drawing.Size(112, 72);
            this.btnStep.TabIndex = 9;
            this.btnStep.Text = "Step";
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(374, 126);
            this.btnGo.Size = new System.Drawing.Size(112, 72);
            this.btnGo.TabIndex = 10;
            this.btnGo.Text = "Go";
            //
            // LogBox
            //
            this.objLogBox = new LogBox(2, 450, 350, 200, 11);
            this.Controls.Add(this.objLogBox.objBox);
            //this.LogOriginal.Location = new System.Drawing.Point(2, 450);
            //this.LogOriginal.Multiline = true;
            //this.LogOriginal.Name = "LogBox";
            //this.LogOriginal.Size = new System.Drawing.Size(350, 200);
            //this.LogOriginal.TabIndex = 11;
            //this.LogOriginal.Text = "Text Box 1";

            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 516);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.btn22);
            this.Controls.Add(this.btn21);
            this.Controls.Add(this.btn20);
            this.Controls.Add(this.btn12);
            this.Controls.Add(this.btn11);
            this.Controls.Add(this.btn10);
            this.Controls.Add(this.btn02);
            this.Controls.Add(this.btn01);
            this.Controls.Add(this.btn00);
            //this.Controls.Add(this.LogOriginal);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        private void sq00_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(0, 0, btn00, e.KeyChar);
            //LogOriginal.AppendText("Button sq00 KeyPress of " + e.KeyChar + Environment.NewLine);
            objLogBox.Log("Button sq00 KeyPress of " + e.KeyChar);
        }

        private void sq01_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(0, 1, btn01, e.KeyChar);
            objLogBox.Log("Button sq01 KeyPress of " + e.KeyChar);
        }

        private void sq02_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(0, 2, btn02, e.KeyChar);
            objLogBox.Log("Button sq02 KeyPress of " + e.KeyChar);
        }

        private void sq10_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(1, 0, btn10, e.KeyChar);
            objLogBox.Log("Button sq10 KeyPress of " + e.KeyChar);
        }

        private void sq11_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(1, 1, btn11, e.KeyChar);
        }

        private void sq12_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(1, 2, btn12, e.KeyChar);
        }

        private void sq20_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(2, 0, btn20, e.KeyChar);
        }

        private void sq21_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(2, 1, btn21, e.KeyChar);
        }

        private void sq22_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Winner(2, 2, btn22, e.KeyChar);
        }

        #endregion

        private System.Windows.Forms.Button btn00;
        private System.Windows.Forms.Button btn01;
        private System.Windows.Forms.Button btn02;
        private System.Windows.Forms.Button btn10;
        private System.Windows.Forms.Button btn11;
        private System.Windows.Forms.Button btn12;
        private System.Windows.Forms.Button btn20;
        private System.Windows.Forms.Button btn21;
        private System.Windows.Forms.Button btn22;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnGo;
        //private System.Windows.Forms.TextBox LogOriginal;
        private LogBox objLogBox;
    }
}