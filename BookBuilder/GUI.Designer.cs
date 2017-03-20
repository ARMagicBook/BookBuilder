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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PageFileBox = new System.Windows.Forms.TextBox();
            this.PageImageBrowseButton = new System.Windows.Forms.Button();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(60, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Video Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(60, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Video Height";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(60, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Video X Position";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(59, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Video Y Position";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(60, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Audio File";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(60, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Has video?";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.Location = new System.Drawing.Point(60, 249);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Has Audio?";
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
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 496);
            this.Controls.Add(this.PageImageBrowseButton);
            this.Controls.Add(this.PageFileBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox PageFileBox;
        private System.Windows.Forms.Button PageImageBrowseButton;
    }
}