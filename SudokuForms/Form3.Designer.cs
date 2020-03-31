using GameEngine;

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
            this.sq00 = new System.Windows.Forms.Button();
            this.sq01 = new System.Windows.Forms.Button();
            this.sq02 = new System.Windows.Forms.Button();
            this.sq10 = new System.Windows.Forms.Button();
            this.sq11 = new System.Windows.Forms.Button();
            this.sq12 = new System.Windows.Forms.Button();
            this.sq20 = new System.Windows.Forms.Button();
            this.sq21 = new System.Windows.Forms.Button();
            this.sq22 = new System.Windows.Forms.Button();
            this.btnStep = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sq00
            // 
            this.sq00.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq00.Location = new System.Drawing.Point(2, 2);
            this.sq00.Name = "sq00";
            this.sq00.Size = new System.Drawing.Size(110, 144);
            this.sq00.TabIndex = 0;
            this.sq00.Text = "1 2 3 4 5 6 7 8 9";
            this.sq00.UseVisualStyleBackColor = true;
            this.sq00.KeyPress += sq00_KeyPress;
            // 
            // sq01
            // 
            this.sq01.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq01.Location = new System.Drawing.Point(118, 2);
            this.sq01.Name = "sq01";
            this.sq01.Size = new System.Drawing.Size(110, 144);
            this.sq01.TabIndex = 1;
            this.sq01.Text = "1 2 3 4 5 6 7 8 9";
            this.sq01.UseVisualStyleBackColor = true;
            this.sq01.KeyPress += sq01_KeyPress;
            // 
            // sq02
            // 
            this.sq02.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq02.Location = new System.Drawing.Point(233, 2);
            this.sq02.Name = "sq02";
            this.sq02.Size = new System.Drawing.Size(110, 144);
            this.sq02.TabIndex = 2;
            this.sq02.Text = "1 2 3 4 5 6 7 8 9";
            this.sq02.UseVisualStyleBackColor = true;
            this.sq02.KeyPress += sq02_KeyPress;
            // 
            // sq10
            // 
            this.sq10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq10.Location = new System.Drawing.Point(2, 148);
            this.sq10.Name = "sq10";
            this.sq10.Size = new System.Drawing.Size(110, 144);
            this.sq10.TabIndex = 3;
            this.sq10.Text = "1 2 3 4 5 6 7 8 9";
            this.sq10.UseVisualStyleBackColor = true;
            this.sq10.KeyPress += sq10_KeyPress;
            // 
            // sq11
            // 
            this.sq11.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq11.Location = new System.Drawing.Point(118, 148);
            this.sq11.Name = "sq11";
            this.sq11.Size = new System.Drawing.Size(110, 144);
            this.sq11.TabIndex = 4;
            this.sq11.Text = "1 2 3 4 5 6 7 8 9";
            this.sq11.UseVisualStyleBackColor = true;
            this.sq11.KeyPress += sq11_KeyPress;
            // 
            // sq12
            // 
            this.sq12.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq12.Location = new System.Drawing.Point(233, 148);
            this.sq12.Name = "sq12";
            this.sq12.Size = new System.Drawing.Size(110, 144);
            this.sq12.TabIndex = 5;
            this.sq12.Text = "1 2 3 4 5 6 7 8 9";
            this.sq12.UseVisualStyleBackColor = true;
            this.sq12.KeyPress += sq12_KeyPress;
            // 
            // sq20
            // 
            this.sq20.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq20.Location = new System.Drawing.Point(2, 298);
            this.sq20.Name = "sq20";
            this.sq20.Size = new System.Drawing.Size(110, 144);
            this.sq20.TabIndex = 6;
            this.sq20.Text = "1 2 3 4 5 6 7 8 9";
            this.sq20.UseVisualStyleBackColor = true;
            this.sq20.KeyPress += sq20_KeyPress;
            // 
            // sq21
            // 
            this.sq21.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq21.Location = new System.Drawing.Point(118, 298);
            this.sq21.Name = "sq21";
            this.sq21.Size = new System.Drawing.Size(110, 144);
            this.sq21.TabIndex = 7;
            this.sq21.Text = "1 2 3 4 5 6 7 8 9";
            this.sq21.UseVisualStyleBackColor = true;
            this.sq21.KeyPress += sq21_KeyPress;
            // 
            // sq22
            // 
            this.sq22.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sq22.Location = new System.Drawing.Point(233, 298);
            this.sq22.Name = "sq22";
            this.sq22.Size = new System.Drawing.Size(110, 144);
            this.sq22.TabIndex = 8;
            this.sq22.Text = "1 2 3 4 5 6 7 8 9";
            this.sq22.UseVisualStyleBackColor = true;
            this.sq22.KeyPress += sq22_KeyPress;
            // 
            // btnStep
            // 
            this.btnStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStep.Location = new System.Drawing.Point(374, 38);
            this.btnStep.Name = "Step";
            this.btnStep.Size = new System.Drawing.Size(112, 72);
            this.btnStep.TabIndex = 9;
            this.btnStep.Text = "Step";
            this.btnStep.UseVisualStyleBackColor = true;
            this.btnStep.Click += new System.EventHandler(this.btnStep_Click);
            // 
            // btnGo
            // 
            this.btnGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGo.Location = new System.Drawing.Point(374, 126);
            this.btnGo.Name = "Go";
            this.btnGo.Size = new System.Drawing.Size(112, 72);
            this.btnGo.TabIndex = 10;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 516);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnStep);
            this.Controls.Add(this.sq22);
            this.Controls.Add(this.sq21);
            this.Controls.Add(this.sq20);
            this.Controls.Add(this.sq12);
            this.Controls.Add(this.sq11);
            this.Controls.Add(this.sq10);
            this.Controls.Add(this.sq02);
            this.Controls.Add(this.sq01);
            this.Controls.Add(this.sq00);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        private void sq00_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int iChar = e.KeyChar - '0';
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq00.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq00.Text = e.KeyChar.ToString();
                WWinner(myGameBoard[4,5], iChar);
            }
        }

        private void sq01_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq01.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq01.Text = e.KeyChar.ToString();
            }
        }

        private void sq02_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq02.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq02.Text = e.KeyChar.ToString();
            }
        }

        private void sq10_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq10.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq10.Text = e.KeyChar.ToString();
            }
        }

        private void sq11_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq11.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq11.Text = e.KeyChar.ToString();
            }
        }

        private void sq12_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq12.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq12.Text = e.KeyChar.ToString();
            }
        }

        private void sq20_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq20.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq20.Text = e.KeyChar.ToString();
            }
        }

        private void sq21_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq21.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq21.Text = e.KeyChar.ToString();
            }
        }

        private void sq22_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar >= '1' && e.KeyChar <= '9')
            {
                sq22.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                sq22.Text = e.KeyChar.ToString();
            }
        }

        #endregion

        private System.Windows.Forms.Button sq00;
        private System.Windows.Forms.Button sq01;
        private System.Windows.Forms.Button sq02;
        private System.Windows.Forms.Button sq10;
        private System.Windows.Forms.Button sq11;
        private System.Windows.Forms.Button sq12;
        private System.Windows.Forms.Button sq20;
        private System.Windows.Forms.Button sq21;
        private System.Windows.Forms.Button sq22;
        private System.Windows.Forms.Button btnStep;
        private System.Windows.Forms.Button btnGo;
    }
}