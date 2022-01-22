
namespace SudokuForms
{
    partial class TryEmBoards
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // BUGBUG: Needs to be fixed for Super.  See iBoardWidth, iBoardHeight
            this.ClientSize = new System.Drawing.Size(490, 640);
            this.Text = "TryEmBoards";
        }

    }
}