using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace BookBuilder
{

    //NOT USED. We're using MainForm.cs and SetupForm.cs instead.
    public partial class GUI : Form
    {
        //The book that we're making.
        public BB_Book book = new global::BookBuilder.BB_Book();

        //Note: .m4a is the file extension for AAC files.
        

        //The page we're making. SUPER TEMPORARY.
        //It's only for the current setup where the book we make only has one page.
        public BB_Page page1 = new global::BookBuilder.BB_Page();

        public GUI()
        {
            InitializeComponent();
        }

        

        //Shows the file dialogue and sets the image filename accordingly
        private void PageImageBrowseButton_Click(object sender, EventArgs e)
        {

            Debug.Write(Directory.GetCurrentDirectory());
            //This makes it impossible to open an image of a invalid format.
            openFileDialog1.Filter = StaticBook.imageFileFilter;
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                PageFileBox.Text = openFileDialog1.FileName;
            } else
            {
                Debug.Write("Couldn't open the file.");
            }
        }


        private void VideoFileBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = StaticBook.videoFileFilter;
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                VideoFileBox.Text = openFileDialog1.FileName;
            }
            else
            {
                Debug.Write("Couldn't open the file.");
            }
        }

        private void AudioFileBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = StaticBook.audioFileFilter;
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                AudioFileBox.Text = openFileDialog1.FileName;
            }
            else
            {
                Debug.Write("Couldn't open the file.");
            }
        }

        //Create a one-page book button with the info we now have!
        private void CreateBookButton_Click(object sender, EventArgs e)
        {
            if (BookDestinationBox.Text == "")
            {
                Debug.Write("No destination selected.");
                return;
            }
            //Create the page from the form info.
            page1.PageNumber = 1;
            page1.SourcePageImageFileName = (PageFileBox.Text == "") ? null : PageFileBox.Text;

            if (PageFileBox.Text != "")
            {
                page1.PageImageFileName = Path.GetFileName(page1.SourcePageImageFileName);
            } 

            if (HasAudioCheckbox.Checked)
            {
                page1.SourceAudioFileName = (AudioFileBox.Text == "") ? null : AudioFileBox.Text;
                if (AudioFileBox.Text != "")
                {
                    page1.AudioFileName = Path.GetFileName(page1.SourceAudioFileName);
                    try
                    {
                        page1.AudioMD5 = BB_Page.GetMD5(page1.SourceAudioFileName);
                    }
                    catch
                    {
                        Debug.Write("Failed to open audio file to create MD5.");
                        page1.AudioMD5 = null;
                    }
                } else
                {
                    Debug.Write("HasAudioCheckbox was checked, but the field was blank!");
                }
            } else
            {
                page1.SourceAudioFileName = null;
                page1.AudioMD5 = null;
            }

            if (HasVideoCheckbox.Checked)
            {
                page1.SourceVideoFileName = (VideoFileBox.Text == "") ? null : VideoFileBox.Text;
                if (VideoFileBox.Text != "")
                {
                    page1.VideoFileName = Path.GetFileName(page1.SourceVideoFileName);
                    try
                    {
                        page1.VideoMD5 = BB_Page.GetMD5(page1.SourceVideoFileName);
                    }
                    catch
                    {
                        Debug.Write("Failed to open video file to create MD5.");
                        page1.VideoMD5 = null;
                    }
                }
                else
                {
                    Debug.Write("HasVideoCheckbox was checked, but the field was blank!");
                }
                page1.VideoWidth = Convert.ToInt32(VideoWidthBox.Text);
                page1.VideoHeight = Convert.ToInt32(VideoHeightBox.Text);
                page1.VideoX = Convert.ToInt32(VideoXBox.Text);
                page1.VideoY = Convert.ToInt32(VideoYBox.Text);
            }
            else
            {
                page1.SourceVideoFileName = null;
                page1.VideoMD5 = null;
            }

            book.AddPage(page1);
            book.AudioFileCheck();

		//////// -----   Dummy values to make sure new XML code works (need to eventually get from GUI) ----- ///////
			book.Title = "Test";
			book.Authors.Add("Eric");
			book.Authors.Add("Ryan");
			book.CreationDate = "2015-01-03";
			book.Description = "Test description";
		/////// ------------------------------------------------------------ ///////

			XMLGenerator.GenerateXML(book);
            book.CreateZipFile(BookDestinationBox.Text);
            Debug.Write("Successfully created book!");
        }

        private void DestinationBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                BookDestinationBox.Text = folderBrowserDialog1.SelectedPath;
            } else
            {
                Debug.Write("Couldn't open directory.");
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
