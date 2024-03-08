namespace VKPlayAchievementTool
{
    partial class AchForm
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
            this.MainListView = new System.Windows.Forms.ListView();
            this.IconColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DisplayNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProgressColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DescriptionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.APINameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IsHiddenColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UnlockAllButton = new System.Windows.Forms.Button();
            this.ResetAllButton = new System.Windows.Forms.Button();
            this.LoadBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.StoreBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.AchContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SetProgressПрогрессToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DumbWinFormsLabel = new System.Windows.Forms.Label();
            this.AchContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainListView
            // 
            this.MainListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IconColumn,
            this.DisplayNameColumn,
            this.ProgressColumn,
            this.DescriptionColumn,
            this.APINameColumn,
            this.IsHiddenColumn});
            this.MainListView.HideSelection = false;
            this.MainListView.Location = new System.Drawing.Point(12, 41);
            this.MainListView.Name = "MainListView";
            this.MainListView.Size = new System.Drawing.Size(983, 520);
            this.MainListView.TabIndex = 2;
            this.MainListView.UseCompatibleStateImageBehavior = false;
            this.MainListView.View = System.Windows.Forms.View.Details;
            this.MainListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainListView_MouseDown);
            // 
            // IconColumn
            // 
            this.IconColumn.Text = "Иконка";
            this.IconColumn.Width = 77;
            // 
            // DisplayNameColumn
            // 
            this.DisplayNameColumn.Text = "Имя";
            // 
            // ProgressColumn
            // 
            this.ProgressColumn.Text = "Прогресс";
            this.ProgressColumn.Width = 79;
            // 
            // DescriptionColumn
            // 
            this.DescriptionColumn.Text = "Описание";
            this.DescriptionColumn.Width = 337;
            // 
            // APINameColumn
            // 
            this.APINameColumn.Text = "API Имя";
            this.APINameColumn.Width = 233;
            // 
            // IsHiddenColumn
            // 
            this.IsHiddenColumn.Text = "Скрытность";
            this.IsHiddenColumn.Width = 174;
            // 
            // UnlockAllButton
            // 
            this.UnlockAllButton.Location = new System.Drawing.Point(12, 12);
            this.UnlockAllButton.Name = "UnlockAllButton";
            this.UnlockAllButton.Size = new System.Drawing.Size(214, 23);
            this.UnlockAllButton.TabIndex = 0;
            this.UnlockAllButton.Text = "Разблокировать все";
            this.UnlockAllButton.UseVisualStyleBackColor = true;
            this.UnlockAllButton.Click += new System.EventHandler(this.UnlockAllButton_Click);
            // 
            // ResetAllButton
            // 
            this.ResetAllButton.Location = new System.Drawing.Point(232, 12);
            this.ResetAllButton.Name = "ResetAllButton";
            this.ResetAllButton.Size = new System.Drawing.Size(214, 23);
            this.ResetAllButton.TabIndex = 1;
            this.ResetAllButton.Text = "Сбросить все";
            this.ResetAllButton.UseVisualStyleBackColor = true;
            this.ResetAllButton.Click += new System.EventHandler(this.ResetAllButton_Click);
            // 
            // LoadBackgroundWorker
            // 
            this.LoadBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadBackgroundWorker_DoWork);
            this.LoadBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadBackgroundWorker_RunWorkerCompleted);
            // 
            // StoreBackgroundWorker
            // 
            this.StoreBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StoreBackgroundWorker_DoWork);
            this.StoreBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.StoreBackgroundWorker_RunWorkerCompleted);
            // 
            // AchContextMenuStrip
            // 
            this.AchContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetToolStripMenuItem,
            this.ResetToolStripMenuItem,
            this.SetProgressПрогрессToolStripMenuItem});
            this.AchContextMenuStrip.Name = "AchContextMenuStrip";
            this.AchContextMenuStrip.Size = new System.Drawing.Size(200, 70);
            // 
            // SetToolStripMenuItem
            // 
            this.SetToolStripMenuItem.Name = "SetToolStripMenuItem";
            this.SetToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.SetToolStripMenuItem.Text = "Разблокировать";
            this.SetToolStripMenuItem.Click += new System.EventHandler(this.SetToolStripMenuItem_Click);
            // 
            // ResetToolStripMenuItem
            // 
            this.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem";
            this.ResetToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.ResetToolStripMenuItem.Text = "Сбросить";
            this.ResetToolStripMenuItem.Click += new System.EventHandler(this.ResetToolStripMenuItem_Click);
            // 
            // SetProgressПрогрессToolStripMenuItem
            // 
            this.SetProgressПрогрессToolStripMenuItem.Name = "SetProgressПрогрессToolStripMenuItem";
            this.SetProgressПрогрессToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.SetProgressПрогрессToolStripMenuItem.Text = "Установить прогресс...";
            this.SetProgressПрогрессToolStripMenuItem.Click += new System.EventHandler(this.SetProgressToolStripMenuItem_Click);
            // 
            // DumbWinFormsLabel
            // 
            this.DumbWinFormsLabel.AutoSize = true;
            this.DumbWinFormsLabel.Location = new System.Drawing.Point(452, 17);
            this.DumbWinFormsLabel.Name = "DumbWinFormsLabel";
            this.DumbWinFormsLabel.Size = new System.Drawing.Size(473, 13);
            this.DumbWinFormsLabel.TabIndex = 3;
            this.DumbWinFormsLabel.Text = "Правой кнопкой мыши по иконке чтобы разблокировать отдельно (список будет обновлё" +
    "н)";
            // 
            // AchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1007, 573);
            this.Controls.Add(this.DumbWinFormsLabel);
            this.Controls.Add(this.ResetAllButton);
            this.Controls.Add(this.UnlockAllButton);
            this.Controls.Add(this.MainListView);
            this.Name = "AchForm";
            this.Text = "Управление достижениями";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AchForm_FormClosed);
            this.AchContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView MainListView;
        private System.Windows.Forms.ColumnHeader IconColumn;
        private System.Windows.Forms.ColumnHeader DisplayNameColumn;
        private System.Windows.Forms.ColumnHeader ProgressColumn;
        private System.Windows.Forms.ColumnHeader DescriptionColumn;
        private System.Windows.Forms.Button UnlockAllButton;
        private System.Windows.Forms.Button ResetAllButton;
        private System.Windows.Forms.ColumnHeader APINameColumn;
        private System.ComponentModel.BackgroundWorker LoadBackgroundWorker;
        private System.ComponentModel.BackgroundWorker StoreBackgroundWorker;
        private System.Windows.Forms.ColumnHeader IsHiddenColumn;
        private System.Windows.Forms.ContextMenuStrip AchContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem SetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SetProgressПрогрессToolStripMenuItem;
        private System.Windows.Forms.Label DumbWinFormsLabel;
    }
}