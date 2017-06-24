namespace HeroesGHConfigTool
{
    partial class LoadingForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadingForm));
            this.Timer_FadingTimer = new System.Windows.Forms.Timer(this.components);
            this.BGWorker_Loading = new System.ComponentModel.BackgroundWorker();
            this.Circle_Loading = new CircularProgressBar.CircularProgressBar();
            this.lbl_Loading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Timer_FadingTimer
            // 
            this.Timer_FadingTimer.Interval = 10;
            this.Timer_FadingTimer.Tick += new System.EventHandler(this.Timer_FadingTimer_Tick);
            // 
            // BGWorker_Loading
            // 
            this.BGWorker_Loading.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGWorker_Loading_DoWork);
            this.BGWorker_Loading.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BGWorker_Loading_ProgressChanged);
            // 
            // Circle_Loading
            // 
            this.Circle_Loading.AnimationFunction = ((WinFormAnimation.AnimationFunctions.Function)(resources.GetObject("Circle_Loading.AnimationFunction")));
            this.Circle_Loading.AnimationSpeed = 500;
            this.Circle_Loading.BackColor = System.Drawing.Color.Transparent;
            this.Circle_Loading.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Circle_Loading.ForeColor = System.Drawing.Color.White;
            this.Circle_Loading.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Circle_Loading.InnerMargin = 0;
            this.Circle_Loading.InnerWidth = 0;
            this.Circle_Loading.Location = new System.Drawing.Point(0, 0);
            this.Circle_Loading.Margin = new System.Windows.Forms.Padding(0);
            this.Circle_Loading.MarqueeAnimationSpeed = 500;
            this.Circle_Loading.Name = "Circle_Loading";
            this.Circle_Loading.OuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Circle_Loading.OuterMargin = -25;
            this.Circle_Loading.OuterWidth = 26;
            this.Circle_Loading.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(137)))), ((int)(((byte)(161)))));
            this.Circle_Loading.ProgressWidth = 25;
            this.Circle_Loading.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.Circle_Loading.Size = new System.Drawing.Size(320, 320);
            this.Circle_Loading.StartAngle = 270;
            this.Circle_Loading.Step = 1;
            this.Circle_Loading.SubscriptColor = System.Drawing.Color.Transparent;
            this.Circle_Loading.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.Circle_Loading.SubscriptText = "";
            this.Circle_Loading.SuperscriptColor = System.Drawing.Color.Transparent;
            this.Circle_Loading.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.Circle_Loading.SuperscriptText = "";
            this.Circle_Loading.TabIndex = 0;
            this.Circle_Loading.Text = "100%";
            this.Circle_Loading.TextMargin = new System.Windows.Forms.Padding(0);
            this.Circle_Loading.Value = 100;
            // 
            // lbl_Loading
            // 
            this.lbl_Loading.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Loading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_Loading.Font = new System.Drawing.Font("Roboto Light", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Loading.ForeColor = System.Drawing.Color.White;
            this.lbl_Loading.Location = new System.Drawing.Point(0, 327);
            this.lbl_Loading.Name = "lbl_Loading";
            this.lbl_Loading.Size = new System.Drawing.Size(320, 63);
            this.lbl_Loading.TabIndex = 1;
            this.lbl_Loading.Text = "Loading...";
            this.lbl_Loading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(320, 390);
            this.Controls.Add(this.lbl_Loading);
            this.Controls.Add(this.Circle_Loading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingForm";
            this.Opacity = 0D;
            this.Text = "LoadingForm";
            this.Load += new System.EventHandler(this.LoadingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer Timer_FadingTimer;
        private System.ComponentModel.BackgroundWorker BGWorker_Loading;
        private CircularProgressBar.CircularProgressBar Circle_Loading;
        public System.Windows.Forms.Label lbl_Loading;
    }
}