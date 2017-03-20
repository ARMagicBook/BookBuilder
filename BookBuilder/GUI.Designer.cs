namespace BookBuilder
{
    partial class GUI
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.VideoFileLabel = new System.Windows.Forms.Label();
            this.PageImageFileLabel = new System.Windows.Forms.Label();
            this.VideoWidthLabel = new System.Windows.Forms.Label();
            this.VideoHeightLabel = new System.Windows.Forms.Label();
            this.VideoXLabel = new System.Windows.Forms.Label();
            this.VideoYLabel = new System.Windows.Forms.Label();
            this.PageFileBox = new System.Windows.Forms.TextBox();
            this.PageImageBrowseButton = new System.Windows.Forms.Button();
            this.hasVideoCheckbox = new System.Windows.Forms.CheckBox();
            this.VideoFileBox = new System.Windows.Forms.TextBox();
            this.AudioFileBox = new System.Windows.Forms.TextBox();
            this.AudioFileLabel = new System.Windows.Forms.Label();
            this.HasAudioCheckbox = new System.Windows.Forms.CheckBox();
            this.VideoFileBrowseButton = new System.Windows.Forms.Button();
            this.AudioFileBrowseButton = new System.Windows.Forms.Button();
            this.VideoWidthBox = new System.Windows.Forms.TextBox();
            this.VideoHeightBox = new System.Windows.Forms.TextBox();
            this.VideoXBox = new System.Windows.Forms.TextBox();
            this.VideoYBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // VideoFileLabel
            // 
            this.VideoFileLabel.AutoSize = true;
            this.VideoFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.VideoFileLabel.Location = new System.Drawing.Point(60, 101);
            this.VideoFileLabel.Name = "VideoFileLabel";
            this.VideoFileLabel.Size = new System.Drawing.Size(79, 20);
            this.VideoFileLabel.TabIndex = 0;
            this.VideoFileLabel.Text = "Video File";
            this.VideoFileLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // PageImageFileLabel
            // 
            this.PageImageFileLabel.AutoSize = true;
            this.PageImageFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PageImageFileLabel.Location = new System.Drawing.Point(60, 48);
            this.PageImageFileLabel.Name = "PageImageFileLabel";
            this.PageImageFileLabel.Size = new System.Drawing.Size(124, 20);
            this.PageImageFileLabel.TabIndex = 1;
            this.PageImageFileLabel.Text = "Page Image File";
            this.PageImageFileLabel.Click += new System.EventHandler(this.PageImageFileLabel_Click);
            // 
            // VideoWidthLabel
            // 
            this.VideoWidthLabel.AutoSize = true;
            this.VideoWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.VideoWidthLabel.Location = new System.Drawing.Point(60, 133);
            this.VideoWidthLabel.Name = "VideoWidthLabel";
            this.VideoWidthLabel.Size = new System.Drawing.Size(95, 20);
            this.VideoWidthLabel.TabIndex = 2;
            this.VideoWidthLabel.Text = "Video Width";
            // 
            // VideoHeightLabel
            // 
            this.VideoHeightLabel.AutoSize = true;
            this.VideoHeightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.VideoHeightLabel.Location = new System.Drawing.Point(60, 163);
            this.VideoHeightLabel.Name = "VideoHeightLabel";
            this.VideoHeightLabel.Size = new System.Drawing.Size(101, 20);
            this.VideoHeightLabel.TabIndex = 3;
            this.VideoHeightLabel.Text = "Video Height";
            // 
            // VideoXLabel
            // 
            this.VideoXLabel.AutoSize = true;
            this.VideoXLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.VideoXLabel.Location = new System.Drawing.Point(60, 191);
            this.VideoXLabel.Name = "VideoXLabel";
            this.VideoXLabel.Size = new System.Drawing.Size(125, 20);
            this.VideoXLabel.TabIndex = 4;
            this.VideoXLabel.Text = "Video X Position";
            // 
            // VideoYLabel
            // 
            this.VideoYLabel.AutoSize = true;
            this.VideoYLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.VideoYLabel.Location = new System.Drawing.Point(59, 220);
            this.VideoYLabel.Name = "VideoYLabel";
            this.VideoYLabel.Size = new System.Drawing.Size(125, 20);
            this.VideoYLabel.TabIndex = 5;
            this.VideoYLabel.Text = "Video Y Position";
            // 
            // PageFileBox
            // 
            this.PageFileBox.Location = new System.Drawing.Point(226, 47);
            this.PageFileBox.Name = "PageFileBox";
            this.PageFileBox.Size = new System.Drawing.Size(288, 20);
            this.PageFileBox.TabIndex = 9;
            // 
            // PageImageBrowseButton
            // 
            this.PageImageBrowseButton.Location = new System.Drawing.Point(536, 44);
            this.PageImageBrowseButton.Name = "PageImageBrowseButton";
            this.PageImageBrowseButton.Size = new System.Drawing.Size(62, 23);
            this.PageImageBrowseButton.TabIndex = 10;
            this.PageImageBrowseButton.Text = "Open...";
            this.PageImageBrowseButton.UseVisualStyleBackColor = true;
            this.PageImageBrowseButton.Click += new System.EventHandler(this.PageImageBrowseButton_Click);
            // 
            // hasVideoCheckbox
            // 
            this.hasVideoCheckbox.AutoSize = true;
            this.hasVideoCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.hasVideoCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.hasVideoCheckbox.Location = new System.Drawing.Point(63, 71);
            this.hasVideoCheckbox.Name = "hasVideoCheckbox";
            this.hasVideoCheckbox.Size = new System.Drawing.Size(107, 24);
            this.hasVideoCheckbox.TabIndex = 11;
            this.hasVideoCheckbox.Text = "Has video?";
            this.hasVideoCheckbox.UseVisualStyleBackColor = true;
            this.hasVideoCheckbox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // VideoFileBox
            // 
            this.VideoFileBox.Location = new System.Drawing.Point(226, 101);
            this.VideoFileBox.Name = "VideoFileBox";
            this.VideoFileBox.Size = new System.Drawing.Size(288, 20);
            this.VideoFileBox.TabIndex = 12;
            // 
            // AudioFileBox
            // 
            this.AudioFileBox.Location = new System.Drawing.Point(226, 280);
            this.AudioFileBox.Name = "AudioFileBox";
            this.AudioFileBox.Size = new System.Drawing.Size(288, 20);
            this.AudioFileBox.TabIndex = 13;
            // 
            // AudioFileLabel
            // 
            this.AudioFileLabel.AutoSize = true;
            this.AudioFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.AudioFileLabel.Location = new System.Drawing.Point(60, 278);
            this.AudioFileLabel.Name = "AudioFileLabel";
            this.AudioFileLabel.Size = new System.Drawing.Size(79, 20);
            this.AudioFileLabel.TabIndex = 6;
            this.AudioFileLabel.Text = "Audio File";
            // 
            // HasAudioCheckbox
            // 
            this.HasAudioCheckbox.AutoSize = true;
            this.HasAudioCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.HasAudioCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.HasAudioCheckbox.Location = new System.Drawing.Point(63, 251);
            this.HasAudioCheckbox.Name = "HasAudioCheckbox";
            this.HasAudioCheckbox.Size = new System.Drawing.Size(109, 24);
            this.HasAudioCheckbox.TabIndex = 14;
            this.HasAudioCheckbox.Text = "Has audio?";
            this.HasAudioCheckbox.UseVisualStyleBackColor = true;
            // 
            // VideoFileBrowseButton
            // 
            this.VideoFileBrowseButton.Location = new System.Drawing.Point(536, 101);
            this.VideoFileBrowseButton.Name = "VideoFileBrowseButton";
            this.VideoFileBrowseButton.Size = new System.Drawing.Size(62, 23);
            this.VideoFileBrowseButton.TabIndex = 15;
            this.VideoFileBrowseButton.Text = "Open...";
            this.VideoFileBrowseButton.UseVisualStyleBackColor = true;
            // 
            // AudioFileBrowseButton
            // 
            this.AudioFileBrowseButton.Location = new System.Drawing.Point(536, 278);
            this.AudioFileBrowseButton.Name = "AudioFileBrowseButton";
            this.AudioFileBrowseButton.Size = new System.Drawing.Size(62, 23);
            this.AudioFileBrowseButton.TabIndex = 16;
            this.AudioFileBrowseButton.Text = "Open...";
            this.AudioFileBrowseButton.UseVisualStyleBackColor = true;
            // 
            // VideoWidthBox
            // 
            this.VideoWidthBox.Location = new System.Drawing.Point(226, 133);
            this.VideoWidthBox.Name = "VideoWidthBox";
            this.VideoWidthBox.Size = new System.Drawing.Size(122, 20);
            this.VideoWidthBox.TabIndex = 17;
            // 
            // VideoHeightBox
            // 
            this.VideoHeightBox.Location = new System.Drawing.Point(226, 165);
            this.VideoHeightBox.Name = "VideoHeightBox";
            this.VideoHeightBox.Size = new System.Drawing.Size(122, 20);
            this.VideoHeightBox.TabIndex = 18;
            // 
            // VideoXBox
            // 
            this.VideoXBox.Location = new System.Drawing.Point(226, 193);
            this.VideoXBox.Name = "VideoXBox";
            this.VideoXBox.Size = new System.Drawing.Size(122, 20);
            this.VideoXBox.TabIndex = 19;
            // 
            // VideoYBox
            // 
            this.VideoYBox.Location = new System.Drawing.Point(226, 222);
            this.VideoYBox.Name = "VideoYBox";
            this.VideoYBox.Size = new System.Drawing.Size(122, 20);
            this.VideoYBox.TabIndex = 20;
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 496);
            this.Controls.Add(this.VideoYBox);
            this.Controls.Add(this.VideoXBox);
            this.Controls.Add(this.VideoHeightBox);
            this.Controls.Add(this.VideoWidthBox);
            this.Controls.Add(this.AudioFileBrowseButton);
            this.Controls.Add(this.VideoFileBrowseButton);
            this.Controls.Add(this.HasAudioCheckbox);
            this.Controls.Add(this.AudioFileBox);
            this.Controls.Add(this.VideoFileBox);
            this.Controls.Add(this.hasVideoCheckbox);
            this.Controls.Add(this.PageImageBrowseButton);
            this.Controls.Add(this.PageFileBox);
            this.Controls.Add(this.AudioFileLabel);
            this.Controls.Add(this.VideoYLabel);
            this.Controls.Add(this.VideoXLabel);
            this.Controls.Add(this.VideoHeightLabel);
            this.Controls.Add(this.VideoWidthLabel);
            this.Controls.Add(this.PageImageFileLabel);
            this.Controls.Add(this.VideoFileLabel);
            this.Name = "GUI";
            this.Text = "GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label VideoFileLabel;
        private System.Windows.Forms.Label PageImageFileLabel;
        private System.Windows.Forms.Label VideoWidthLabel;
        private System.Windows.Forms.Label VideoHeightLabel;
        private System.Windows.Forms.Label VideoXLabel;
        private System.Windows.Forms.Label VideoYLabel;
        private System.Windows.Forms.TextBox PageFileBox;
        private System.Windows.Forms.Button PageImageBrowseButton;
        private System.Windows.Forms.CheckBox hasVideoCheckbox;
        private System.Windows.Forms.TextBox VideoFileBox;
        private System.Windows.Forms.TextBox AudioFileBox;
        private System.Windows.Forms.Label AudioFileLabel;
        private System.Windows.Forms.CheckBox HasAudioCheckbox;
        private System.Windows.Forms.Button VideoFileBrowseButton;
        private System.Windows.Forms.Button AudioFileBrowseButton;
        private System.Windows.Forms.TextBox VideoWidthBox;
        private System.Windows.Forms.TextBox VideoHeightBox;
        private System.Windows.Forms.TextBox VideoXBox;
        private System.Windows.Forms.TextBox VideoYBox;
    }
}