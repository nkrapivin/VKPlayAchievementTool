namespace VKPlayAchievementTool
{
    partial class ProgressDialog
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
            this.ProgressNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.DOKButton = new System.Windows.Forms.Button();
            this.DCancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgressNumericUpDown
            // 
            this.ProgressNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressNumericUpDown.Location = new System.Drawing.Point(12, 25);
            this.ProgressNumericUpDown.Name = "ProgressNumericUpDown";
            this.ProgressNumericUpDown.Size = new System.Drawing.Size(237, 20);
            this.ProgressNumericUpDown.TabIndex = 1;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Location = new System.Drawing.Point(9, 9);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(102, 13);
            this.ProgressLabel.TabIndex = 0;
            this.ProgressLabel.Text = "Введите прогресс:";
            // 
            // DOKButton
            // 
            this.DOKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DOKButton.Location = new System.Drawing.Point(12, 51);
            this.DOKButton.Name = "DOKButton";
            this.DOKButton.Size = new System.Drawing.Size(75, 23);
            this.DOKButton.TabIndex = 2;
            this.DOKButton.Text = "OK";
            this.DOKButton.UseVisualStyleBackColor = true;
            this.DOKButton.Click += new System.EventHandler(this.DOKButton_Click);
            // 
            // DCancelButton
            // 
            this.DCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DCancelButton.Location = new System.Drawing.Point(174, 51);
            this.DCancelButton.Name = "DCancelButton";
            this.DCancelButton.Size = new System.Drawing.Size(75, 23);
            this.DCancelButton.TabIndex = 3;
            this.DCancelButton.Text = "Отмена";
            this.DCancelButton.UseVisualStyleBackColor = true;
            this.DCancelButton.Click += new System.EventHandler(this.DCancelButton_Click);
            // 
            // ProgressDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 94);
            this.Controls.Add(this.DCancelButton);
            this.Controls.Add(this.DOKButton);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.ProgressNumericUpDown);
            this.Name = "ProgressDialog";
            this.Text = "Прогресс";
            ((System.ComponentModel.ISupportInitialize)(this.ProgressNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.Button DOKButton;
        private System.Windows.Forms.Button DCancelButton;
        public System.Windows.Forms.NumericUpDown ProgressNumericUpDown;
    }
}