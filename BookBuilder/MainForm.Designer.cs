namespace BookBuilder
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
            this.MainLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.BottomlayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.VideoInfoPanel = new System.Windows.Forms.Panel();
            this.HeightBox = new System.Windows.Forms.TextBox();
            this.HeightLabel = new System.Windows.Forms.Label();
            this.WidthBox = new System.Windows.Forms.TextBox();
            this.WidthLabel = new System.Windows.Forms.Label();
            this.YPosBox = new System.Windows.Forms.TextBox();
            this.YPosLabel = new System.Windows.Forms.Label();
            this.XPosBox = new System.Windows.Forms.TextBox();
            this.XPosLabel = new System.Windows.Forms.Label();
            this.OpenVideoButton = new System.Windows.Forms.Button();
            this.VideoFileLabel = new System.Windows.Forms.Label();
            this.VideoLabel = new System.Windows.Forms.Label();
            this.AudioInfoPanel = new System.Windows.Forms.Panel();
            this.OpenAudioButton = new System.Windows.Forms.Button();
            this.AudioFileLabel = new System.Windows.Forms.Label();
            this.AudioLabel = new System.Windows.Forms.Label();
            this.ImageInfoPanel = new System.Windows.Forms.Panel();
            this.OpenImageButton = new System.Windows.Forms.Button();
            this.ImageFileLabel = new System.Windows.Forms.Label();
            this.ImageLabel = new System.Windows.Forms.Label();
            this.PagePicture = new System.Windows.Forms.PictureBox();
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.SaveButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.SaveAsButton = new System.Windows.Forms.Button();
            this.Nextbutton = new System.Windows.Forms.Button();
            this.PageNumBox = new System.Windows.Forms.TextBox();
            this.PrevButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.RemoveAudioButton = new System.Windows.Forms.Button();
            this.RemoveVideoButton = new System.Windows.Forms.Button();
            this.MainLayoutPanel.SuspendLayout();
            this.BottomlayoutPanel.SuspendLayout();
            this.VideoInfoPanel.SuspendLayout();
            this.AudioInfoPanel.SuspendLayout();
            this.ImageInfoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PagePicture)).BeginInit();
            this.ControlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainLayoutPanel
            // 
            this.MainLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainLayoutPanel.AutoSize = true;
            this.MainLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
            this.MainLayoutPanel.ColumnCount = 1;
            this.MainLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainLayoutPanel.Controls.Add(this.BottomlayoutPanel, 0, 2);
            this.MainLayoutPanel.Controls.Add(this.ControlsPanel, 0, 0);
            this.MainLayoutPanel.Controls.Add(this.PagePicture, 0, 1);
            this.MainLayoutPanel.Location = new System.Drawing.Point(8, 7);
            this.MainLayoutPanel.Name = "MainLayoutPanel";
            this.MainLayoutPanel.RowCount = 3;
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.54839F));
            this.MainLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MainLayoutPanel.Size = new System.Drawing.Size(1114, 609);
            this.MainLayoutPanel.TabIndex = 0;
            // 
            // BottomlayoutPanel
            // 
            this.BottomlayoutPanel.ColumnCount = 3;
            this.BottomlayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 290F));
            this.BottomlayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 275F));
            this.BottomlayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.43119F));
            this.BottomlayoutPanel.Controls.Add(this.VideoInfoPanel, 0, 0);
            this.BottomlayoutPanel.Controls.Add(this.AudioInfoPanel, 0, 0);
            this.BottomlayoutPanel.Controls.Add(this.ImageInfoPanel, 0, 0);
            this.BottomlayoutPanel.Location = new System.Drawing.Point(5, 570);
            this.BottomlayoutPanel.Name = "BottomlayoutPanel";
            this.BottomlayoutPanel.RowCount = 1;
            this.BottomlayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BottomlayoutPanel.Size = new System.Drawing.Size(1104, 34);
            this.BottomlayoutPanel.TabIndex = 1;
            // 
            // VideoInfoPanel
            // 
            this.VideoInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VideoInfoPanel.Controls.Add(this.HeightBox);
            this.VideoInfoPanel.Controls.Add(this.HeightLabel);
            this.VideoInfoPanel.Controls.Add(this.WidthBox);
            this.VideoInfoPanel.Controls.Add(this.WidthLabel);
            this.VideoInfoPanel.Controls.Add(this.YPosBox);
            this.VideoInfoPanel.Controls.Add(this.YPosLabel);
            this.VideoInfoPanel.Controls.Add(this.XPosBox);
            this.VideoInfoPanel.Controls.Add(this.XPosLabel);
            this.VideoInfoPanel.Controls.Add(this.OpenVideoButton);
            this.VideoInfoPanel.Controls.Add(this.VideoFileLabel);
            this.VideoInfoPanel.Controls.Add(this.VideoLabel);
            this.VideoInfoPanel.Location = new System.Drawing.Point(568, 3);
            this.VideoInfoPanel.Name = "VideoInfoPanel";
            this.VideoInfoPanel.Size = new System.Drawing.Size(533, 28);
            this.VideoInfoPanel.TabIndex = 2;
            // 
            // HeightBox
            // 
            this.HeightBox.Location = new System.Drawing.Point(488, 3);
            this.HeightBox.Name = "HeightBox";
            this.HeightBox.Size = new System.Drawing.Size(35, 20);
            this.HeightBox.TabIndex = 11;
            this.HeightBox.Text = "0";
            this.HeightBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HeightBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BlockNonDigits);
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(464, 6);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(18, 13);
            this.HeightLabel.TabIndex = 10;
            this.HeightLabel.Text = "H:";
            // 
            // WidthBox
            // 
            this.WidthBox.Location = new System.Drawing.Point(423, 3);
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(35, 20);
            this.WidthBox.TabIndex = 9;
            this.WidthBox.Text = "0";
            this.WidthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.WidthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BlockNonDigits);
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Location = new System.Drawing.Point(399, 6);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(21, 13);
            this.WidthLabel.TabIndex = 8;
            this.WidthLabel.Text = "W:";
            // 
            // YPosBox
            // 
            this.YPosBox.Location = new System.Drawing.Point(358, 3);
            this.YPosBox.Name = "YPosBox";
            this.YPosBox.Size = new System.Drawing.Size(35, 20);
            this.YPosBox.TabIndex = 7;
            this.YPosBox.Text = "0";
            this.YPosBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YPosBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BlockNonDigits);
            // 
            // YPosLabel
            // 
            this.YPosLabel.AutoSize = true;
            this.YPosLabel.Location = new System.Drawing.Point(335, 7);
            this.YPosLabel.Name = "YPosLabel";
            this.YPosLabel.Size = new System.Drawing.Size(17, 13);
            this.YPosLabel.TabIndex = 6;
            this.YPosLabel.Text = "Y:";
            // 
            // XPosBox
            // 
            this.XPosBox.Location = new System.Drawing.Point(293, 4);
            this.XPosBox.Name = "XPosBox";
            this.XPosBox.Size = new System.Drawing.Size(35, 20);
            this.XPosBox.TabIndex = 5;
            this.XPosBox.Text = "0";
            this.XPosBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.XPosBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BlockNonDigits);
            // 
            // XPosLabel
            // 
            this.XPosLabel.AutoSize = true;
            this.XPosLabel.Location = new System.Drawing.Point(269, 7);
            this.XPosLabel.Name = "XPosLabel";
            this.XPosLabel.Size = new System.Drawing.Size(17, 13);
            this.XPosLabel.TabIndex = 4;
            this.XPosLabel.Text = "X:";
            // 
            // OpenVideoButton
            // 
            this.OpenVideoButton.BackColor = System.Drawing.SystemColors.Control;
            this.OpenVideoButton.Location = new System.Drawing.Point(188, 2);
            this.OpenVideoButton.Name = "OpenVideoButton";
            this.OpenVideoButton.Size = new System.Drawing.Size(75, 23);
            this.OpenVideoButton.TabIndex = 3;
            this.OpenVideoButton.Text = "Open...";
            this.OpenVideoButton.UseVisualStyleBackColor = false;
            this.OpenVideoButton.Click += new System.EventHandler(this.OpenVideo);
            // 
            // VideoFileLabel
            // 
            this.VideoFileLabel.AutoSize = true;
            this.VideoFileLabel.Location = new System.Drawing.Point(52, 7);
            this.VideoFileLabel.Name = "VideoFileLabel";
            this.VideoFileLabel.Size = new System.Drawing.Size(130, 13);
            this.VideoFileLabel.TabIndex = 2;
            this.VideoFileLabel.Text = "placeholder_filename.mp4";
            // 
            // VideoLabel
            // 
            this.VideoLabel.AutoSize = true;
            this.VideoLabel.Location = new System.Drawing.Point(3, 7);
            this.VideoLabel.Name = "VideoLabel";
            this.VideoLabel.Size = new System.Drawing.Size(53, 13);
            this.VideoLabel.TabIndex = 1;
            this.VideoLabel.Text = "Video file:";
            // 
            // AudioInfoPanel
            // 
            this.AudioInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AudioInfoPanel.Controls.Add(this.OpenAudioButton);
            this.AudioInfoPanel.Controls.Add(this.AudioFileLabel);
            this.AudioInfoPanel.Controls.Add(this.AudioLabel);
            this.AudioInfoPanel.Location = new System.Drawing.Point(293, 3);
            this.AudioInfoPanel.Name = "AudioInfoPanel";
            this.AudioInfoPanel.Size = new System.Drawing.Size(269, 28);
            this.AudioInfoPanel.TabIndex = 1;
            // 
            // OpenAudioButton
            // 
            this.OpenAudioButton.BackColor = System.Drawing.SystemColors.Control;
            this.OpenAudioButton.Location = new System.Drawing.Point(188, 2);
            this.OpenAudioButton.Name = "OpenAudioButton";
            this.OpenAudioButton.Size = new System.Drawing.Size(75, 23);
            this.OpenAudioButton.TabIndex = 3;
            this.OpenAudioButton.Text = "Open...";
            this.OpenAudioButton.UseVisualStyleBackColor = false;
            this.OpenAudioButton.Click += new System.EventHandler(this.OpenAudio);
            // 
            // AudioFileLabel
            // 
            this.AudioFileLabel.AutoSize = true;
            this.AudioFileLabel.Location = new System.Drawing.Point(52, 7);
            this.AudioFileLabel.Name = "AudioFileLabel";
            this.AudioFileLabel.Size = new System.Drawing.Size(130, 13);
            this.AudioFileLabel.TabIndex = 2;
            this.AudioFileLabel.Text = "placeholder_filename.mp3";
            // 
            // AudioLabel
            // 
            this.AudioLabel.AutoSize = true;
            this.AudioLabel.Location = new System.Drawing.Point(3, 7);
            this.AudioLabel.Name = "AudioLabel";
            this.AudioLabel.Size = new System.Drawing.Size(53, 13);
            this.AudioLabel.TabIndex = 1;
            this.AudioLabel.Text = "Audio file:";
            // 
            // ImageInfoPanel
            // 
            this.ImageInfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImageInfoPanel.Controls.Add(this.OpenImageButton);
            this.ImageInfoPanel.Controls.Add(this.ImageFileLabel);
            this.ImageInfoPanel.Controls.Add(this.ImageLabel);
            this.ImageInfoPanel.Location = new System.Drawing.Point(3, 3);
            this.ImageInfoPanel.Name = "ImageInfoPanel";
            this.ImageInfoPanel.Size = new System.Drawing.Size(281, 28);
            this.ImageInfoPanel.TabIndex = 0;
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.BackColor = System.Drawing.SystemColors.Control;
            this.OpenImageButton.Location = new System.Drawing.Point(197, 2);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(75, 23);
            this.OpenImageButton.TabIndex = 3;
            this.OpenImageButton.Text = "Open...";
            this.OpenImageButton.UseVisualStyleBackColor = false;
            this.OpenImageButton.Click += new System.EventHandler(this.OpenPageImage);
            // 
            // ImageFileLabel
            // 
            this.ImageFileLabel.AutoSize = true;
            this.ImageFileLabel.Location = new System.Drawing.Point(67, 7);
            this.ImageFileLabel.Name = "ImageFileLabel";
            this.ImageFileLabel.Size = new System.Drawing.Size(124, 13);
            this.ImageFileLabel.TabIndex = 2;
            this.ImageFileLabel.Text = "placeholder_filename.jpg";
            // 
            // ImageLabel
            // 
            this.ImageLabel.AutoSize = true;
            this.ImageLabel.Location = new System.Drawing.Point(3, 7);
            this.ImageLabel.Name = "ImageLabel";
            this.ImageLabel.Size = new System.Drawing.Size(67, 13);
            this.ImageLabel.TabIndex = 1;
            this.ImageLabel.Text = "Page Image:";
            // 
            // PagePicture
            // 
            this.PagePicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PagePicture.Location = new System.Drawing.Point(5, 42);
            this.PagePicture.Name = "PagePicture";
            this.PagePicture.Size = new System.Drawing.Size(1104, 520);
            this.PagePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PagePicture.TabIndex = 2;
            this.PagePicture.TabStop = false;
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ControlsPanel.Controls.Add(this.RemoveVideoButton);
            this.ControlsPanel.Controls.Add(this.RemoveAudioButton);
            this.ControlsPanel.Controls.Add(this.SaveButton);
            this.ControlsPanel.Controls.Add(this.OpenButton);
            this.ControlsPanel.Controls.Add(this.SaveAsButton);
            this.ControlsPanel.Controls.Add(this.Nextbutton);
            this.ControlsPanel.Controls.Add(this.PageNumBox);
            this.ControlsPanel.Controls.Add(this.PrevButton);
            this.ControlsPanel.Location = new System.Drawing.Point(5, 5);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(591, 29);
            this.ControlsPanel.TabIndex = 3;
            this.ControlsPanel.Click += new System.EventHandler(this.PrevPage);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(310, 3);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(77, 23);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.Save);
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(144, 3);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(77, 23);
            this.OpenButton.TabIndex = 4;
            this.OpenButton.Text = "Open New";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenBook);
            // 
            // SaveAsButton
            // 
            this.SaveAsButton.Location = new System.Drawing.Point(227, 3);
            this.SaveAsButton.Name = "SaveAsButton";
            this.SaveAsButton.Size = new System.Drawing.Size(77, 23);
            this.SaveAsButton.TabIndex = 3;
            this.SaveAsButton.Text = "Save As...";
            this.SaveAsButton.UseVisualStyleBackColor = true;
            this.SaveAsButton.Click += new System.EventHandler(this.SaveAs);
            // 
            // Nextbutton
            // 
            this.Nextbutton.Location = new System.Drawing.Point(92, 3);
            this.Nextbutton.Name = "Nextbutton";
            this.Nextbutton.Size = new System.Drawing.Size(46, 23);
            this.Nextbutton.TabIndex = 2;
            this.Nextbutton.Text = "Next";
            this.Nextbutton.UseVisualStyleBackColor = true;
            this.Nextbutton.Click += new System.EventHandler(this.NextPage);
            // 
            // PageNumBox
            // 
            this.PageNumBox.Location = new System.Drawing.Point(55, 5);
            this.PageNumBox.Name = "PageNumBox";
            this.PageNumBox.Size = new System.Drawing.Size(31, 20);
            this.PageNumBox.TabIndex = 1;
            this.PageNumBox.Text = "1";
            this.PageNumBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PageNumBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PageNumBoxPress);
            // 
            // PrevButton
            // 
            this.PrevButton.Location = new System.Drawing.Point(3, 3);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(46, 23);
            this.PrevButton.TabIndex = 0;
            this.PrevButton.Text = "Prev";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevPage);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // RemoveAudioButton
            // 
            this.RemoveAudioButton.Location = new System.Drawing.Point(393, 3);
            this.RemoveAudioButton.Name = "RemoveAudioButton";
            this.RemoveAudioButton.Size = new System.Drawing.Size(93, 23);
            this.RemoveAudioButton.TabIndex = 6;
            this.RemoveAudioButton.Text = "Remove Audio";
            this.RemoveAudioButton.UseVisualStyleBackColor = true;
            this.RemoveAudioButton.Click += new System.EventHandler(this.RemoveAudio);
            // 
            // RemoveVideoButton
            // 
            this.RemoveVideoButton.Location = new System.Drawing.Point(492, 3);
            this.RemoveVideoButton.Name = "RemoveVideoButton";
            this.RemoveVideoButton.Size = new System.Drawing.Size(93, 23);
            this.RemoveVideoButton.TabIndex = 7;
            this.RemoveVideoButton.Text = "Remove Video";
            this.RemoveVideoButton.UseVisualStyleBackColor = true;
            this.RemoveVideoButton.Click += new System.EventHandler(this.RemoveVideo);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 615);
            this.Controls.Add(this.MainLayoutPanel);
            this.Name = "MainForm";
            this.Text = "BookBuilder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormClosed);
            this.MainLayoutPanel.ResumeLayout(false);
            this.BottomlayoutPanel.ResumeLayout(false);
            this.VideoInfoPanel.ResumeLayout(false);
            this.VideoInfoPanel.PerformLayout();
            this.AudioInfoPanel.ResumeLayout(false);
            this.AudioInfoPanel.PerformLayout();
            this.ImageInfoPanel.ResumeLayout(false);
            this.ImageInfoPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PagePicture)).EndInit();
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MainLayoutPanel;
        private System.Windows.Forms.Panel ImageInfoPanel;
        private System.Windows.Forms.Label ImageLabel;
        private System.Windows.Forms.Label ImageFileLabel;
        private System.Windows.Forms.Button OpenImageButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TableLayoutPanel BottomlayoutPanel;
        private System.Windows.Forms.Panel AudioInfoPanel;
        private System.Windows.Forms.Button OpenAudioButton;
        private System.Windows.Forms.Label AudioFileLabel;
        private System.Windows.Forms.Label AudioLabel;
        private System.Windows.Forms.Panel VideoInfoPanel;
        private System.Windows.Forms.Button OpenVideoButton;
        private System.Windows.Forms.Label VideoFileLabel;
        private System.Windows.Forms.Label VideoLabel;
        private System.Windows.Forms.TextBox YPosBox;
        private System.Windows.Forms.Label YPosLabel;
        private System.Windows.Forms.TextBox XPosBox;
        private System.Windows.Forms.Label XPosLabel;
        private System.Windows.Forms.PictureBox PagePicture;
        private System.Windows.Forms.TextBox WidthBox;
        private System.Windows.Forms.Label WidthLabel;
        private System.Windows.Forms.TextBox HeightBox;
        private System.Windows.Forms.Label HeightLabel;
        private System.Windows.Forms.Panel ControlsPanel;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Button SaveAsButton;
        private System.Windows.Forms.Button Nextbutton;
        private System.Windows.Forms.TextBox PageNumBox;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button RemoveVideoButton;
        private System.Windows.Forms.Button RemoveAudioButton;
    }
}