namespace HeroesWarp
{
    partial class MainWindow
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
            this.Btn_TeleportNext = new System.Windows.Forms.Button();
            this.lbl_TeleportNext = new System.Windows.Forms.Label();
            this.lbl_Teleport = new System.Windows.Forms.Label();
            this.Btn_Teleport = new System.Windows.Forms.Button();
            this.lbl_TeleportLast = new System.Windows.Forms.Label();
            this.Btn_TeleportLast = new System.Windows.Forms.Button();
            this.lbl_GameStatus = new System.Windows.Forms.Label();
            this.Btn_SaveStatus = new System.Windows.Forms.Button();
            this.lbl_SaveStatus = new System.Windows.Forms.Label();
            this.lbl_YPosition = new System.Windows.Forms.Label();
            this.Btn_LockYPosition = new System.Windows.Forms.Button();
            this.ChkBox_Lock = new System.Windows.Forms.CheckBox();
            this.lbl_ToggleValues = new System.Windows.Forms.Label();
            this.Btn_ToggleMode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_TeleportNext
            // 
            this.Btn_TeleportNext.Location = new System.Drawing.Point(126, 42);
            this.Btn_TeleportNext.Name = "Btn_TeleportNext";
            this.Btn_TeleportNext.Size = new System.Drawing.Size(146, 23);
            this.Btn_TeleportNext.TabIndex = 0;
            this.Btn_TeleportNext.Text = "Next Teleport Key";
            this.Btn_TeleportNext.UseVisualStyleBackColor = true;
            this.Btn_TeleportNext.Click += new System.EventHandler(this.Btn_Forward_Click);
            // 
            // lbl_TeleportNext
            // 
            this.lbl_TeleportNext.AutoSize = true;
            this.lbl_TeleportNext.Location = new System.Drawing.Point(12, 47);
            this.lbl_TeleportNext.Name = "lbl_TeleportNext";
            this.lbl_TeleportNext.Size = new System.Drawing.Size(108, 13);
            this.lbl_TeleportNext.TabIndex = 1;
            this.lbl_TeleportNext.Text = "Next Teleport Button:";
            // 
            // lbl_Teleport
            // 
            this.lbl_Teleport.AutoSize = true;
            this.lbl_Teleport.Location = new System.Drawing.Point(13, 72);
            this.lbl_Teleport.Name = "lbl_Teleport";
            this.lbl_Teleport.Size = new System.Drawing.Size(83, 13);
            this.lbl_Teleport.TabIndex = 2;
            this.lbl_Teleport.Text = "Teleport Button:";
            // 
            // Btn_Teleport
            // 
            this.Btn_Teleport.Location = new System.Drawing.Point(126, 67);
            this.Btn_Teleport.Name = "Btn_Teleport";
            this.Btn_Teleport.Size = new System.Drawing.Size(146, 23);
            this.Btn_Teleport.TabIndex = 3;
            this.Btn_Teleport.Text = "Teleport Key";
            this.Btn_Teleport.UseVisualStyleBackColor = true;
            this.Btn_Teleport.Click += new System.EventHandler(this.Btn_Teleport_Click);
            // 
            // lbl_TeleportLast
            // 
            this.lbl_TeleportLast.AutoSize = true;
            this.lbl_TeleportLast.Location = new System.Drawing.Point(12, 22);
            this.lbl_TeleportLast.Name = "lbl_TeleportLast";
            this.lbl_TeleportLast.Size = new System.Drawing.Size(106, 13);
            this.lbl_TeleportLast.TabIndex = 4;
            this.lbl_TeleportLast.Text = "Last Teleport Button:";
            // 
            // Btn_TeleportLast
            // 
            this.Btn_TeleportLast.Location = new System.Drawing.Point(126, 17);
            this.Btn_TeleportLast.Name = "Btn_TeleportLast";
            this.Btn_TeleportLast.Size = new System.Drawing.Size(146, 23);
            this.Btn_TeleportLast.TabIndex = 5;
            this.Btn_TeleportLast.Text = "Last Teleport Key";
            this.Btn_TeleportLast.UseVisualStyleBackColor = true;
            this.Btn_TeleportLast.Click += new System.EventHandler(this.Btn_TeleportLast_Click);
            // 
            // lbl_GameStatus
            // 
            this.lbl_GameStatus.AutoSize = true;
            this.lbl_GameStatus.Location = new System.Drawing.Point(13, 236);
            this.lbl_GameStatus.Name = "lbl_GameStatus";
            this.lbl_GameStatus.Size = new System.Drawing.Size(152, 13);
            this.lbl_GameStatus.TabIndex = 6;
            this.lbl_GameStatus.Text = "Waiting for the game to launch";
            // 
            // Btn_SaveStatus
            // 
            this.Btn_SaveStatus.Location = new System.Drawing.Point(126, 92);
            this.Btn_SaveStatus.Name = "Btn_SaveStatus";
            this.Btn_SaveStatus.Size = new System.Drawing.Size(146, 23);
            this.Btn_SaveStatus.TabIndex = 7;
            this.Btn_SaveStatus.Text = "Save Status";
            this.Btn_SaveStatus.UseVisualStyleBackColor = true;
            this.Btn_SaveStatus.Click += new System.EventHandler(this.Btn_SaveStatus_Click);
            // 
            // lbl_SaveStatus
            // 
            this.lbl_SaveStatus.AutoSize = true;
            this.lbl_SaveStatus.Location = new System.Drawing.Point(13, 97);
            this.lbl_SaveStatus.Name = "lbl_SaveStatus";
            this.lbl_SaveStatus.Size = new System.Drawing.Size(68, 13);
            this.lbl_SaveStatus.TabIndex = 8;
            this.lbl_SaveStatus.Text = "Save Status:";
            // 
            // lbl_YPosition
            // 
            this.lbl_YPosition.AutoSize = true;
            this.lbl_YPosition.Location = new System.Drawing.Point(13, 215);
            this.lbl_YPosition.Name = "lbl_YPosition";
            this.lbl_YPosition.Size = new System.Drawing.Size(91, 13);
            this.lbl_YPosition.TabIndex = 9;
            this.lbl_YPosition.Text = "Modify Y Position:";
            // 
            // Btn_LockYPosition
            // 
            this.Btn_LockYPosition.Location = new System.Drawing.Point(126, 210);
            this.Btn_LockYPosition.Name = "Btn_LockYPosition";
            this.Btn_LockYPosition.Size = new System.Drawing.Size(146, 23);
            this.Btn_LockYPosition.TabIndex = 10;
            this.Btn_LockYPosition.Text = "Lock Y Position Key";
            this.Btn_LockYPosition.UseVisualStyleBackColor = true;
            this.Btn_LockYPosition.Click += new System.EventHandler(this.Btn_LockYPosition_Click);
            // 
            // ChkBox_Lock
            // 
            this.ChkBox_Lock.AutoSize = true;
            this.ChkBox_Lock.Location = new System.Drawing.Point(178, 235);
            this.ChkBox_Lock.Name = "ChkBox_Lock";
            this.ChkBox_Lock.Size = new System.Drawing.Size(94, 17);
            this.ChkBox_Lock.TabIndex = 11;
            this.ChkBox_Lock.Text = "Toggle Values";
            this.ChkBox_Lock.UseVisualStyleBackColor = true;
            this.ChkBox_Lock.CheckedChanged += new System.EventHandler(this.ChkBox_Lock_CheckedChanged);
            // 
            // lbl_ToggleValues
            // 
            this.lbl_ToggleValues.AutoSize = true;
            this.lbl_ToggleValues.Location = new System.Drawing.Point(12, 190);
            this.lbl_ToggleValues.Name = "lbl_ToggleValues";
            this.lbl_ToggleValues.Size = new System.Drawing.Size(73, 13);
            this.lbl_ToggleValues.TabIndex = 12;
            this.lbl_ToggleValues.Text = "Toggle Mode:";
            // 
            // Btn_ToggleMode
            // 
            this.Btn_ToggleMode.Location = new System.Drawing.Point(126, 185);
            this.Btn_ToggleMode.Name = "Btn_ToggleMode";
            this.Btn_ToggleMode.Size = new System.Drawing.Size(146, 23);
            this.Btn_ToggleMode.TabIndex = 13;
            this.Btn_ToggleMode.Text = "Toggle Mode Key";
            this.Btn_ToggleMode.UseVisualStyleBackColor = true;
            this.Btn_ToggleMode.Click += new System.EventHandler(this.Btn_ToggleMode_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Btn_ToggleMode);
            this.Controls.Add(this.lbl_ToggleValues);
            this.Controls.Add(this.ChkBox_Lock);
            this.Controls.Add(this.Btn_LockYPosition);
            this.Controls.Add(this.lbl_YPosition);
            this.Controls.Add(this.lbl_SaveStatus);
            this.Controls.Add(this.Btn_SaveStatus);
            this.Controls.Add(this.lbl_GameStatus);
            this.Controls.Add(this.Btn_TeleportLast);
            this.Controls.Add(this.lbl_TeleportLast);
            this.Controls.Add(this.Btn_Teleport);
            this.Controls.Add(this.lbl_Teleport);
            this.Controls.Add(this.lbl_TeleportNext);
            this.Controls.Add(this.Btn_TeleportNext);
            this.Name = "MainWindow";
            this.Text = "HeroesQuickWarper";
            this.Shown += new System.EventHandler(this.MainWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_TeleportNext;
        private System.Windows.Forms.Label lbl_TeleportNext;
        private System.Windows.Forms.Label lbl_Teleport;
        private System.Windows.Forms.Button Btn_Teleport;
        private System.Windows.Forms.Label lbl_TeleportLast;
        private System.Windows.Forms.Button Btn_TeleportLast;
        private System.Windows.Forms.Label lbl_GameStatus;
        private System.Windows.Forms.Button Btn_SaveStatus;
        private System.Windows.Forms.Label lbl_SaveStatus;
        private System.Windows.Forms.Label lbl_YPosition;
        private System.Windows.Forms.Button Btn_LockYPosition;
        private System.Windows.Forms.CheckBox ChkBox_Lock;
        private System.Windows.Forms.Label lbl_ToggleValues;
        private System.Windows.Forms.Button Btn_ToggleMode;
    }
}

