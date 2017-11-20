namespace Hex_Modern_UI
{
    partial class WarpOverlay
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
            this.TxtBoxSmall_WarpName = new Hex_Modern_UI.CustomTextBoxV2();
            this.SuspendLayout();
            // 
            // TxtBoxSmall_WarpName
            // 
            this.TxtBoxSmall_WarpName.AutoSizeX = true;
            this.TxtBoxSmall_WarpName.BackColor = System.Drawing.Color.Transparent;
            this.TxtBoxSmall_WarpName.BackColorX = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TxtBoxSmall_WarpName.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.TxtBoxSmall_WarpName.BorderSidesX = Hex_Modern_UI.CustomTextBoxV2.BorderSides.TopBottom;
            this.TxtBoxSmall_WarpName.BorderSizeX = 4;
            this.TxtBoxSmall_WarpName.FontX = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBoxSmall_WarpName.ForeColorX = System.Drawing.SystemColors.ControlText;
            this.TxtBoxSmall_WarpName.Location = new System.Drawing.Point(46, 12);
            this.TxtBoxSmall_WarpName.Name = "TxtBoxSmall_WarpName";
            this.TxtBoxSmall_WarpName.PasswordCharX = '\0';
            this.TxtBoxSmall_WarpName.ReadOnlyX = false;
            this.TxtBoxSmall_WarpName.Size = new System.Drawing.Size(201, 31);
            this.TxtBoxSmall_WarpName.TabIndex = 0;
            this.TxtBoxSmall_WarpName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtBoxSmall_WarpName.TextX = "";
            // 
            // WarpOverlay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(293, 53);
            this.Controls.Add(this.TxtBoxSmall_WarpName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WarpOverlay";
            this.Text = "SwappableScreen";
            this.Deactivate += new System.EventHandler(this.WarpOverlay_Deactivate);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WarpOverlay_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion

        public CustomTextBoxV2 TxtBoxSmall_WarpName;
    }
}