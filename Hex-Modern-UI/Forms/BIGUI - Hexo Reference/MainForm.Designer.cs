namespace Hex_Modern_UI
{
    partial class MainForm
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
            this.Panel_TitleBar = new System.Windows.Forms.Panel();
            this.TopBtn_Minimize = new Hex_Modern_UI.CustomButton();
            this.TopBtn_Close = new Hex_Modern_UI.CustomButton();
            this.TopLabel_PageTitle = new System.Windows.Forms.Label();
            this.Panel_SideBar = new System.Windows.Forms.Panel();
            this.SideBtn_ThemeMenu = new System.Windows.Forms.Button();
            this.SideBtn_Register = new System.Windows.Forms.Button();
            this.SideBtn_Login = new System.Windows.Forms.Button();
            this.SideBtn_Welcome = new System.Windows.Forms.Button();
            this.SideBtn_SeparatorBtn = new System.Windows.Forms.Button();
            this.Toolstrip_Bottom = new System.Windows.Forms.ToolStrip();
            this.lbl_ActionBar_BottomRight = new System.Windows.Forms.ToolStripLabel();
            this.Panel_TitleBar.SuspendLayout();
            this.Panel_SideBar.SuspendLayout();
            this.Toolstrip_Bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_TitleBar
            // 
            this.Panel_TitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(53)))), ((int)(((byte)(64)))));
            this.Panel_TitleBar.Controls.Add(this.TopBtn_Minimize);
            this.Panel_TitleBar.Controls.Add(this.TopBtn_Close);
            this.Panel_TitleBar.Controls.Add(this.TopLabel_PageTitle);
            this.Panel_TitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_TitleBar.Location = new System.Drawing.Point(0, 0);
            this.Panel_TitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_TitleBar.Name = "Panel_TitleBar";
            this.Panel_TitleBar.Size = new System.Drawing.Size(968, 87);
            this.Panel_TitleBar.TabIndex = 138;
            this.Panel_TitleBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_TitleBar_MouseMove);
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
            this.TopBtn_Minimize.Click += new System.EventHandler(this.TopBtn_Minimize_Click);
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
            this.TopBtn_Close.Click += new System.EventHandler(this.TopBtn_Close_Click);
            // 
            // TopLabel_PageTitle
            // 
            this.TopLabel_PageTitle.BackColor = System.Drawing.Color.Transparent;
            this.TopLabel_PageTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.TopLabel_PageTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TopLabel_PageTitle.Font = new System.Drawing.Font("Roboto Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopLabel_PageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.TopLabel_PageTitle.Location = new System.Drawing.Point(0, 0);
            this.TopLabel_PageTitle.Margin = new System.Windows.Forms.Padding(0);
            this.TopLabel_PageTitle.Name = "TopLabel_PageTitle";
            this.TopLabel_PageTitle.Size = new System.Drawing.Size(968, 87);
            this.TopLabel_PageTitle.TabIndex = 148;
            this.TopLabel_PageTitle.Text = "Hexo UI Demonstration";
            this.TopLabel_PageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TopLabel_PageTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TopLabel_PageTitle_MouseDown);
            // 
            // Panel_SideBar
            // 
            this.Panel_SideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.Panel_SideBar.Controls.Add(this.SideBtn_ThemeMenu);
            this.Panel_SideBar.Controls.Add(this.SideBtn_Register);
            this.Panel_SideBar.Controls.Add(this.SideBtn_Login);
            this.Panel_SideBar.Controls.Add(this.SideBtn_Welcome);
            this.Panel_SideBar.Controls.Add(this.SideBtn_SeparatorBtn);
            this.Panel_SideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel_SideBar.ForeColor = System.Drawing.Color.White;
            this.Panel_SideBar.Location = new System.Drawing.Point(0, 87);
            this.Panel_SideBar.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_SideBar.Name = "Panel_SideBar";
            this.Panel_SideBar.Size = new System.Drawing.Size(235, 469);
            this.Panel_SideBar.TabIndex = 137;
            // 
            // SideBtn_ThemeMenu
            // 
            this.SideBtn_ThemeMenu.BackColor = this.Panel_SideBar.BackColor;
            this.SideBtn_ThemeMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideBtn_ThemeMenu.FlatAppearance.BorderSize = 0;
            this.SideBtn_ThemeMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SideBtn_ThemeMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SideBtn_ThemeMenu.ForeColor = System.Drawing.Color.White;
            this.SideBtn_ThemeMenu.Image = global::Hex_Modern_UI.Properties.Resources.Button_ThemeMenu_48x;
            this.SideBtn_ThemeMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_ThemeMenu.Location = new System.Drawing.Point(0, 257);
            this.SideBtn_ThemeMenu.Name = "SideBtn_ThemeMenu";
            this.SideBtn_ThemeMenu.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.SideBtn_ThemeMenu.Size = new System.Drawing.Size(235, 72);
            this.SideBtn_ThemeMenu.TabIndex = 147;
            this.SideBtn_ThemeMenu.Text = "  Theming";
            this.SideBtn_ThemeMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_ThemeMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SideBtn_ThemeMenu.UseVisualStyleBackColor = false;
            this.SideBtn_ThemeMenu.Click += new System.EventHandler(this.SideBtn_ThemeMenu_Click);
            // 
            // SideBtn_Register
            // 
            this.SideBtn_Register.BackColor = this.Panel_SideBar.BackColor;
            this.SideBtn_Register.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideBtn_Register.FlatAppearance.BorderSize = 0;
            this.SideBtn_Register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SideBtn_Register.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SideBtn_Register.ForeColor = System.Drawing.Color.White;
            this.SideBtn_Register.Image = global::Hex_Modern_UI.Properties.Resources.Button_Register_48x;
            this.SideBtn_Register.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_Register.Location = new System.Drawing.Point(0, 185);
            this.SideBtn_Register.Name = "SideBtn_Register";
            this.SideBtn_Register.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.SideBtn_Register.Size = new System.Drawing.Size(235, 72);
            this.SideBtn_Register.TabIndex = 148;
            this.SideBtn_Register.Text = "  Sample";
            this.SideBtn_Register.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SideBtn_Register.UseVisualStyleBackColor = false;
            // 
            // SideBtn_Login
            // 
            this.SideBtn_Login.BackColor = this.Panel_SideBar.BackColor;
            this.SideBtn_Login.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideBtn_Login.FlatAppearance.BorderSize = 0;
            this.SideBtn_Login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SideBtn_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SideBtn_Login.ForeColor = System.Drawing.Color.White;
            this.SideBtn_Login.Image = global::Hex_Modern_UI.Properties.Resources.Button_Login_48x;
            this.SideBtn_Login.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_Login.Location = new System.Drawing.Point(0, 113);
            this.SideBtn_Login.Name = "SideBtn_Login";
            this.SideBtn_Login.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.SideBtn_Login.Size = new System.Drawing.Size(235, 72);
            this.SideBtn_Login.TabIndex = 146;
            this.SideBtn_Login.Text = "  Sample";
            this.SideBtn_Login.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_Login.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SideBtn_Login.UseVisualStyleBackColor = false;
            // 
            // SideBtn_Welcome
            // 
            this.SideBtn_Welcome.BackColor = this.Panel_SideBar.BackColor;
            this.SideBtn_Welcome.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideBtn_Welcome.FlatAppearance.BorderSize = 0;
            this.SideBtn_Welcome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SideBtn_Welcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SideBtn_Welcome.ForeColor = System.Drawing.Color.White;
            this.SideBtn_Welcome.Image = global::Hex_Modern_UI.Properties.Resources.Button_Welcome_48x;
            this.SideBtn_Welcome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_Welcome.Location = new System.Drawing.Point(0, 41);
            this.SideBtn_Welcome.Name = "SideBtn_Welcome";
            this.SideBtn_Welcome.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.SideBtn_Welcome.Size = new System.Drawing.Size(235, 72);
            this.SideBtn_Welcome.TabIndex = 145;
            this.SideBtn_Welcome.Text = "  Controls";
            this.SideBtn_Welcome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_Welcome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SideBtn_Welcome.UseVisualStyleBackColor = false;
            this.SideBtn_Welcome.Click += new System.EventHandler(this.SideBtn_Welcome_Click);
            // 
            // SideBtn_SeparatorBtn
            // 
            this.SideBtn_SeparatorBtn.AutoSize = true;
            this.SideBtn_SeparatorBtn.BackColor = this.Panel_SideBar.BackColor;
            this.SideBtn_SeparatorBtn.Dock = System.Windows.Forms.DockStyle.Top;
            this.SideBtn_SeparatorBtn.FlatAppearance.BorderSize = 0;
            this.SideBtn_SeparatorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SideBtn_SeparatorBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SideBtn_SeparatorBtn.ForeColor = System.Drawing.Color.White;
            this.SideBtn_SeparatorBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_SeparatorBtn.Location = new System.Drawing.Point(0, 0);
            this.SideBtn_SeparatorBtn.Name = "SideBtn_SeparatorBtn";
            this.SideBtn_SeparatorBtn.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.SideBtn_SeparatorBtn.Size = new System.Drawing.Size(235, 41);
            this.SideBtn_SeparatorBtn.TabIndex = 144;
            this.SideBtn_SeparatorBtn.Text = "----------------------";
            this.SideBtn_SeparatorBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SideBtn_SeparatorBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SideBtn_SeparatorBtn.UseVisualStyleBackColor = false;
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
            this.Toolstrip_Bottom.Location = new System.Drawing.Point(0, 556);
            this.Toolstrip_Bottom.Name = "Toolstrip_Bottom";
            this.Toolstrip_Bottom.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Toolstrip_Bottom.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Toolstrip_Bottom.Size = new System.Drawing.Size(968, 25);
            this.Toolstrip_Bottom.Stretch = true;
            this.Toolstrip_Bottom.TabIndex = 1;
            this.Toolstrip_Bottom.Text = "toolStrip2";
            // 
            // lbl_ActionBar_BottomRight
            // 
            this.lbl_ActionBar_BottomRight.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lbl_ActionBar_BottomRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lbl_ActionBar_BottomRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lbl_ActionBar_BottomRight.Name = "lbl_ActionBar_BottomRight";
            this.lbl_ActionBar_BottomRight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_ActionBar_BottomRight.Size = new System.Drawing.Size(216, 22);
            this.lbl_ActionBar_BottomRight.Text = "Hexo UI Demo by Sewer56lol";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(968, 581);
            this.Controls.Add(this.Panel_SideBar);
            this.Controls.Add(this.Panel_TitleBar);
            this.Controls.Add(this.Toolstrip_Bottom);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "ExoSkeletal UI Base by Sewer56lol";
            this.Panel_TitleBar.ResumeLayout(false);
            this.Panel_SideBar.ResumeLayout(false);
            this.Panel_SideBar.PerformLayout();
            this.Toolstrip_Bottom.ResumeLayout(false);
            this.Toolstrip_Bottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_TitleBar;
        private System.Windows.Forms.Panel Panel_SideBar;
        private System.Windows.Forms.ToolStrip Toolstrip_Bottom;
        public System.Windows.Forms.ToolStripLabel lbl_ActionBar_BottomRight;
        private CustomButton TopBtn_Close;
        private CustomButton TopBtn_Minimize;
        private System.Windows.Forms.Label TopLabel_PageTitle;
        private System.Windows.Forms.Button SideBtn_SeparatorBtn;
        private System.Windows.Forms.Button SideBtn_Welcome;
        private System.Windows.Forms.Button SideBtn_ThemeMenu;
        private System.Windows.Forms.Button SideBtn_Login;
        private System.Windows.Forms.Button SideBtn_Register;
    }
}

