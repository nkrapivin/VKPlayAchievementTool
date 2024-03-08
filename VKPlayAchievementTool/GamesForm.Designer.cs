namespace VKPlayAchievementTool
{
    partial class GamesForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.GamesListView = new System.Windows.Forms.ListView();
            this.AvatarPictureBox = new System.Windows.Forms.PictureBox();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.UserIdLabel = new System.Windows.Forms.Label();
            this.GamesBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.WarningLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AvatarPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GamesListView
            // 
            this.GamesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GamesListView.HideSelection = false;
            this.GamesListView.Location = new System.Drawing.Point(12, 82);
            this.GamesListView.Name = "GamesListView";
            this.GamesListView.Size = new System.Drawing.Size(776, 356);
            this.GamesListView.TabIndex = 3;
            this.GamesListView.UseCompatibleStateImageBehavior = false;
            this.GamesListView.ItemActivate += new System.EventHandler(this.GamesListView_ItemActivate);
            // 
            // AvatarPictureBox
            // 
            this.AvatarPictureBox.Location = new System.Drawing.Point(12, 12);
            this.AvatarPictureBox.Name = "AvatarPictureBox";
            this.AvatarPictureBox.Size = new System.Drawing.Size(64, 64);
            this.AvatarPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AvatarPictureBox.TabIndex = 1;
            this.AvatarPictureBox.TabStop = false;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Location = new System.Drawing.Point(82, 12);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(106, 13);
            this.UserNameLabel.TabIndex = 0;
            this.UserNameLabel.Text = "user name goes here";
            // 
            // UserIdLabel
            // 
            this.UserIdLabel.AutoSize = true;
            this.UserIdLabel.Location = new System.Drawing.Point(82, 25);
            this.UserIdLabel.Name = "UserIdLabel";
            this.UserIdLabel.Size = new System.Drawing.Size(72, 13);
            this.UserIdLabel.TabIndex = 1;
            this.UserIdLabel.Text = "ID: 11223344";
            // 
            // GamesBackgroundWorker
            // 
            this.GamesBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GamesBackgroundWorker_DoWork);
            this.GamesBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GamesBackgroundWorker_RunWorkerCompleted);
            // 
            // WarningLabel
            // 
            this.WarningLabel.AutoSize = true;
            this.WarningLabel.Location = new System.Drawing.Point(82, 63);
            this.WarningLabel.Name = "WarningLabel";
            this.WarningLabel.Size = new System.Drawing.Size(509, 13);
            this.WarningLabel.TabIndex = 2;
            this.WarningLabel.Text = "Приблизительный список ваших игр: (Взято из GamePersIds, иконки подтянуты через a" +
    "pi.vkplay.ru)";
            // 
            // GamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.WarningLabel);
            this.Controls.Add(this.UserIdLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.AvatarPictureBox);
            this.Controls.Add(this.GamesListView);
            this.Name = "GamesForm";
            this.Text = "VK Play Achievement Manager";
            ((System.ComponentModel.ISupportInitialize)(this.AvatarPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView GamesListView;
        private System.Windows.Forms.PictureBox AvatarPictureBox;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label UserIdLabel;
        private System.ComponentModel.BackgroundWorker GamesBackgroundWorker;
        private System.Windows.Forms.Label WarningLabel;
    }
}

