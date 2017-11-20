namespace Hex_Modern_UI
{
    partial class WelcomeScreenTinyUI
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
            this.TxtSpc_Author = new System.Windows.Forms.Label();
            this.Txt_Welcome = new System.Windows.Forms.Label();
            this.TxtSpc_Welcome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtSpc_Author
            // 
            this.TxtSpc_Author.AutoSize = true;
            this.TxtSpc_Author.BackColor = System.Drawing.Color.Transparent;
            this.TxtSpc_Author.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TxtSpc_Author.Font = new System.Drawing.Font("Roboto Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSpc_Author.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.TxtSpc_Author.Location = new System.Drawing.Point(31, 232);
            this.TxtSpc_Author.Margin = new System.Windows.Forms.Padding(0);
            this.TxtSpc_Author.Name = "TxtSpc_Author";
            this.TxtSpc_Author.Size = new System.Drawing.Size(232, 33);
            this.TxtSpc_Author.TabIndex = 235;
            this.TxtSpc_Author.Text = "~ By Sewer56lol ~";
            this.TxtSpc_Author.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Txt_Welcome
            // 
            this.Txt_Welcome.BackColor = System.Drawing.Color.Transparent;
            this.Txt_Welcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Txt_Welcome.Font = new System.Drawing.Font("Roboto Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Txt_Welcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.Txt_Welcome.Location = new System.Drawing.Point(2, 75);
            this.Txt_Welcome.Margin = new System.Windows.Forms.Padding(0);
            this.Txt_Welcome.Name = "Txt_Welcome";
            this.Txt_Welcome.Size = new System.Drawing.Size(291, 106);
            this.Txt_Welcome.TabIndex = 234;
            this.Txt_Welcome.Text = "Sonic Heroes\r\nLive 1337 Hax0r\r\nToolkit Version 0.1\r\n";
            this.Txt_Welcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtSpc_Welcome
            // 
            this.TxtSpc_Welcome.AutoSize = true;
            this.TxtSpc_Welcome.BackColor = System.Drawing.Color.Transparent;
            this.TxtSpc_Welcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TxtSpc_Welcome.Font = new System.Drawing.Font("Roboto Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSpc_Welcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.TxtSpc_Welcome.Location = new System.Drawing.Point(63, 29);
            this.TxtSpc_Welcome.Margin = new System.Windows.Forms.Padding(0);
            this.TxtSpc_Welcome.Name = "TxtSpc_Welcome";
            this.TxtSpc_Welcome.Size = new System.Drawing.Size(168, 33);
            this.TxtSpc_Welcome.TabIndex = 233;
            this.TxtSpc_Welcome.Text = "< Welcome >";
            this.TxtSpc_Welcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WelcomeScreenTinyUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(294, 294);
            this.Controls.Add(this.TxtSpc_Author);
            this.Controls.Add(this.Txt_Welcome);
            this.Controls.Add(this.TxtSpc_Welcome);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WelcomeScreenTinyUI";
            this.Text = "SwappableScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TxtSpc_Author;
        private System.Windows.Forms.Label Txt_Welcome;
        private System.Windows.Forms.Label TxtSpc_Welcome;
    }
}