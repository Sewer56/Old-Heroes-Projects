namespace Hex_Modern_UI
{
    partial class MainFormSmall
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
            this.Toolstrip_Bottom = new System.Windows.Forms.ToolStrip();
            this.lbl_ActionBar_BottomRight = new System.Windows.Forms.ToolStripLabel();
            this.Panel_SideBar = new System.Windows.Forms.Panel();
            this.SideBtn_Themes = new System.Windows.Forms.Button();
            this.SideBtn_Options = new System.Windows.Forms.Button();
            this.Panel_TitleBar = new System.Windows.Forms.Panel();
            this.TinyUI_TopLabel_PageTitle = new System.Windows.Forms.Label();
            this.TopBtn_Minimize = new Hex_Modern_UI.CustomButton();
            this.TopBtn_Close = new Hex_Modern_UI.CustomButton();
            this.Toolstrip_Bottom.SuspendLayout();
            this.Panel_SideBar.SuspendLayout();
            this.Panel_TitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Toolstrip_Bottom
            // 
            this.Toolstrip_Bottom.AutoSize = false;
            this.Toolstrip_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(81)))), ((int)(((byte)(181)))));
            this.Toolstrip_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Toolstrip_Bottom.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Toolstrip_Bottom.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Toolstrip_Bottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_ActionBar_BottomRight});
            this.Toolstrip_Bottom.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.Toolstrip_Bottom.Location = new System.Drawing.Point(0, 377);
            this.Toolstrip_Bottom.Name = "Toolstrip_Bottom";
            this.Toolstrip_Bottom.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Toolstrip_Bottom.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Toolstrip_Bottom.Size = new System.Drawing.Size(291, 25);
            this.Toolstrip_Bottom.Stretch = true;
            this.Toolstrip_Bottom.TabIndex = 2;
            this.Toolstrip_Bottom.Text = "toolStrip2";
            this.Toolstrip_Bottom.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Toolstrip_Bottom_MouseMove);
            // 
            // lbl_ActionBar_BottomRight
            // 
            this.lbl_ActionBar_BottomRight.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lbl_ActionBar_BottomRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_ActionBar_BottomRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lbl_ActionBar_BottomRight.Name = "lbl_ActionBar_BottomRight";
            this.lbl_ActionBar_BottomRight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_ActionBar_BottomRight.Size = new System.Drawing.Size(145, 22);
            this.lbl_ActionBar_BottomRight.Text = "Waiting for Game...";
            // 
            // Panel_SideBar
            // 
            this.Panel_SideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.Panel_SideBar.Controls.Add(this.SideBtn_Themes);
            this.Panel_SideBar.Controls.Add(this.SideBtn_Options);
            this.Panel_SideBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_SideBar.ForeColor = System.Drawing.Color.White;
            this.Panel_SideBar.Location = new System.Drawing.Point(0, 44);
            this.Panel_SideBar.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_SideBar.Name = "Panel_SideBar";
            this.Panel_SideBar.Size = new System.Drawing.Size(291, 42);
            this.Panel_SideBar.TabIndex = 138;
            this.Panel_SideBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_SideBar_MouseMove);
            // 
            // SideBtn_Themes
            // 
            this.SideBtn_Themes.BackColor = this.Panel_SideBar.BackColor;
            this.SideBtn_Themes.BackgroundImage = global::Hex_Modern_UI.Properties.Resources.PaintBrushIcon_58x;
            this.SideBtn_Themes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SideBtn_Themes.Dock = System.Windows.Forms.DockStyle.Right;
            this.SideBtn_Themes.FlatAppearance.BorderSize = 0;
            this.SideBtn_Themes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SideBtn_Themes.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SideBtn_Themes.ForeColor = System.Drawing.Color.White;
            this.SideBtn_Themes.Location = new System.Drawing.Point(207, 0);
            this.SideBtn_Themes.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.SideBtn_Themes.Name = "SideBtn_Themes";
            this.SideBtn_Themes.Size = new System.Drawing.Size(42, 42);
            this.SideBtn_Themes.TabIndex = 146;
            this.SideBtn_Themes.UseVisualStyleBackColor = false;
            this.SideBtn_Themes.Click += new System.EventHandler(this.SideBtn_Themes_Click);
            // 
            // SideBtn_Options
            // 
            this.SideBtn_Options.BackColor = this.Panel_SideBar.BackColor;
            this.SideBtn_Options.BackgroundImage = global::Hex_Modern_UI.Properties.Resources.SettingsIcon_HalogenOS_Sewer56lol1;
            this.SideBtn_Options.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SideBtn_Options.Dock = System.Windows.Forms.DockStyle.Right;
            this.SideBtn_Options.FlatAppearance.BorderSize = 0;
            this.SideBtn_Options.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SideBtn_Options.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SideBtn_Options.ForeColor = System.Drawing.Color.White;
            this.SideBtn_Options.Location = new System.Drawing.Point(249, 0);
            this.SideBtn_Options.Margin = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.SideBtn_Options.Name = "SideBtn_Options";
            this.SideBtn_Options.Size = new System.Drawing.Size(42, 42);
            this.SideBtn_Options.TabIndex = 147;
            this.SideBtn_Options.UseVisualStyleBackColor = false;
            this.SideBtn_Options.Click += new System.EventHandler(this.SideBtn_Options_Click);
            // 
            // Panel_TitleBar
            // 
            this.Panel_TitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.Panel_TitleBar.Controls.Add(this.TinyUI_TopLabel_PageTitle);
            this.Panel_TitleBar.Controls.Add(this.TopBtn_Minimize);
            this.Panel_TitleBar.Controls.Add(this.TopBtn_Close);
            this.Panel_TitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_TitleBar.Location = new System.Drawing.Point(0, 0);
            this.Panel_TitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_TitleBar.Name = "Panel_TitleBar";
            this.Panel_TitleBar.Size = new System.Drawing.Size(291, 44);
            this.Panel_TitleBar.TabIndex = 139;
            this.Panel_TitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_TitleBar_MouseMove);
            // 
            // TinyUI_TopLabel_PageTitle
            // 
            this.TinyUI_TopLabel_PageTitle.BackColor = System.Drawing.Color.Transparent;
            this.TinyUI_TopLabel_PageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TinyUI_TopLabel_PageTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TinyUI_TopLabel_PageTitle.Font = new System.Drawing.Font("Roboto Light", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TinyUI_TopLabel_PageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.TinyUI_TopLabel_PageTitle.Location = new System.Drawing.Point(0, 0);
            this.TinyUI_TopLabel_PageTitle.Margin = new System.Windows.Forms.Padding(0);
            this.TinyUI_TopLabel_PageTitle.Name = "TinyUI_TopLabel_PageTitle";
            this.TinyUI_TopLabel_PageTitle.Size = new System.Drawing.Size(291, 44);
            this.TinyUI_TopLabel_PageTitle.TabIndex = 149;
            this.TinyUI_TopLabel_PageTitle.Text = "Welcome Screen";
            this.TinyUI_TopLabel_PageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TinyUI_TopLabel_PageTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TinyUI_TopLabel_PageTitle_MouseMove);
            // 
            // TopBtn_Minimize
            // 
            this.TopBtn_Minimize.BackColor = System.Drawing.Color.Transparent;
            this.TopBtn_Minimize.FlatAppearance.BorderSize = 0;
            this.TopBtn_Minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TopBtn_Minimize.Image = global::Hex_Modern_UI.Properties.Resources.MinimizeBtn_12x;
            this.TopBtn_Minimize.Location = new System.Drawing.Point(888, 0);
            this.TopBtn_Minimize.Margin = new System.Windows.Forms.Padding(0);
            this.TopBtn_Minimize.Name = "TopBtn_Minimize";
            this.TopBtn_Minimize.OwnerDrawText = null;
            this.TopBtn_Minimize.Size = new System.Drawing.Size(40, 24);
            this.TopBtn_Minimize.TabIndex = 3;
            this.TopBtn_Minimize.UseVisualStyleBackColor = false;
            // 
            // TopBtn_Close
            // 
            this.TopBtn_Close.BackColor = System.Drawing.Color.Transparent;
            this.TopBtn_Close.FlatAppearance.BorderSize = 0;
            this.TopBtn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TopBtn_Close.Image = global::Hex_Modern_UI.Properties.Resources.CloseBtn_12x;
            this.TopBtn_Close.Location = new System.Drawing.Point(928, 0);
            this.TopBtn_Close.Margin = new System.Windows.Forms.Padding(0);
            this.TopBtn_Close.Name = "TopBtn_Close";
            this.TopBtn_Close.OwnerDrawText = null;
            this.TopBtn_Close.Size = new System.Drawing.Size(40, 24);
            this.TopBtn_Close.TabIndex = 2;
            this.TopBtn_Close.UseVisualStyleBackColor = false;
            // 
            // MainFormSmall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(291, 402);
            this.Controls.Add(this.Panel_SideBar);
            this.Controls.Add(this.Toolstrip_Bottom);
            this.Controls.Add(this.Panel_TitleBar);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFormSmall";
            this.Text = "ExoSkeletal UI Base by Sewer56lol";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainFormSmall_MouseMove);
            this.Toolstrip_Bottom.ResumeLayout(false);
            this.Toolstrip_Bottom.PerformLayout();
            this.Panel_SideBar.ResumeLayout(false);
            this.Panel_TitleBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip Toolstrip_Bottom;
        public System.Windows.Forms.ToolStripLabel lbl_ActionBar_BottomRight;
        private System.Windows.Forms.Panel Panel_SideBar;
        private System.Windows.Forms.Panel Panel_TitleBar;
        private CustomButton TopBtn_Minimize;
        private CustomButton TopBtn_Close;
        private System.Windows.Forms.Label TinyUI_TopLabel_PageTitle;
        private System.Windows.Forms.Button SideBtn_Themes;
        private System.Windows.Forms.Button SideBtn_Options;
    }
}