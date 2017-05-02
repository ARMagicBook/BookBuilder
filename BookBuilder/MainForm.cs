using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO.Compression;

namespace BookBuilder
{
    /// <summary>
    /// The main BookBuilder GUI form. 
    /// Top third of tableLayoutPanel: next page/previous page/etc controlls
    /// Middle third: the page image
    /// Bottom third: buttons to open image, audio, and video files
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes the MainForm. 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            ImageFileLabel.Text = "";
            AudioFileLabel.Text = "";
            VideoFileLabel.Text = "";
            saveFileDialog.Filter = StaticBook.armbFilter;
        }

        /// <summary>
        /// The page currently being viewed in MainForm.
        /// </summary>
        public BB_Page currentPage;


        private PictureBox videoPlaceholder;

        //The current page number, zero-indexed as it is in the BB_Book
        private int currentPageNum = 0;


        

        /// <summary>
        /// Runs when the main form is closed.
        /// Should prompt the user to save their work before exiting. For now, it just exits the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormClosed(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void OpenPageImage(object sender, EventArgs e)
        {
            openFileDialog.Filter = StaticBook.imageFileFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentPage.SourcePageImageFileName = openFileDialog.FileName;
                currentPage.PageImageFileName = Path.GetFileName(openFileDialog.FileName);
                ImageFileLabel.Text = currentPage.PageImageFileName;
                PagePicture.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void OpenAudio(object sender, EventArgs e)
        {
            openFileDialog.Filter = StaticBook.audioFileFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!StaticBook.Book.AudioFileCheck(currentPageNum))
                {
                    String errorMessage = "Warning: Page ";
                    if (currentPageNum % 2 == 0)
                    {
                        errorMessage += (currentPageNum + 2);
                    } else
                    {
                        errorMessage += (currentPageNum);
                    }
                    errorMessage += " also has an audio file. It will be open and play at the same time as this one. Add audio anyway?";
                    DialogResult dialogResult = MessageBox.Show(errorMessage, "Audio File Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                currentPage.SourceAudioFileName = openFileDialog.FileName;
                currentPage.AudioFileName = Path.GetFileName(openFileDialog.FileName);
                AudioFileLabel.Text = currentPage.AudioFileName;
            }
        }

        private void OpenVideo(object sender, EventArgs e)
        {
            openFileDialog.Filter = StaticBook.videoFileFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentPage.SourceVideoFileName = openFileDialog.FileName;
                currentPage.VideoFileName = Path.GetFileName(openFileDialog.FileName);
                VideoFileLabel.Text = currentPage.VideoFileName;

                videoPlaceholder = new PictureBox();
                videoPlaceholder.Size = new Size(150, 150);
                videoPlaceholder.Image = Image.FromFile("../../video_source/video_placeholder.png");
                Point centerOfPageImage = new Point(PagePicture.Location.X + PagePicture.Size.Width / 2 - videoPlaceholder.Size.Width / 2,
                    PagePicture.Location.Y + PagePicture.Size.Height / 2 - videoPlaceholder.Size.Height / 2);
                videoPlaceholder.Location = centerOfPageImage;
                videoPlaceholder.SizeMode = PictureBoxSizeMode.StretchImage;

                Controls.Add(videoPlaceholder);
                videoPlaceholder.BringToFront();

                DisplayVideoSizeAndLocation();
            }
        }


        /// <summary>
        /// Updates the X, Y, W, and H text fields in the lower righthand corner to 
        /// display the current information of the video placeholder
        /// </summary>
        private void DisplayVideoSizeAndLocation()
        {

            Debug.WriteLine("videoPl x is " + videoPlaceholder.Location.X.ToString());
            Debug.WriteLine("pagepic x is " + PagePicture.Location.X.ToString());
            Debug.WriteLine("videoPl y is " + videoPlaceholder.Location.Y.ToString());
            Debug.WriteLine("pagepic y is " + PagePicture.Location.Y.ToString());

            //Subract the page pictures location because the location of the video placeholder relative to the
            //page picture is what matters.
            //THIS IS A PROBLEM RIGHT NOW. Currently the PagePicure container stretches accross the entire screen.
            //We need to find a way to get the coordinates of where the page image appears on the screen
            XPosBox.Text = (videoPlaceholder.Location.X - PagePicture.Location.X).ToString();
            YPosBox.Text = (videoPlaceholder.Location.Y - PagePicture.Location.Y).ToString();
            WidthBox.Text = videoPlaceholder.Size.Width.ToString();
            HeightBox.Text = videoPlaceholder.Size.Height.ToString();
        }

        private void BlockNonDigits(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PrevPage(object sender, EventArgs e)
        {
            GoToPage(ClampPageNum(currentPageNum - 1), true);
        }

        private void NextPage(object sender, EventArgs e)
        {

            GoToPage(ClampPageNum(currentPageNum + 1), true);
        }

        //Save the entire book
        private void SaveAs(object sender, EventArgs e)
        {
            if (!imageCheck()) return;            
            GoToPage(currentPageNum, true);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveBook(saveFileDialog.FileName);
            }           
        }

        private bool imageCheck()
        {
            if (!StaticBook.Book.hasAllImages())
            {
                MessageBox.Show("Error: Every page must have an image.", "Missing Image Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void SaveBook(string filePath)
        {
            XMLGenerator.GenerateXML(StaticBook.Book);
            //Make sure the filename ends in .armb
            if (Path.GetExtension(filePath) != ".armb")
            {
                filePath += ".armb";
            }
            StaticBook.Book.CreateZipFile(filePath);
            StaticBook.hasBeenSaved = true;
            StaticBook.savePath = filePath;
        }

        private void PageNumBoxPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                GoToPage(ClampPageNum(Int32.Parse(PageNumBox.Text) - 1), true);
            }
        }

        //Clamps the input to a valid page number (input and output is zero-indexed)
        private int ClampPageNum(int num)
        {
            if (num < 0) return 0;
            if (num > StaticBook.Book.Pages.Count - 1) return StaticBook.Book.Pages.Count - 1;
            return num;
        }

        /// <summary>
        /// Go to another page in the BB_Book.
        /// </summary>
        /// <param name="pageNum">Page number to go to, zero-indexed.</param>
        /// <param name="saveCurrent">Whether the current page should be saved to the BB_Book.</param>
        public void GoToPage(int pageNum, bool saveCurrent)
        {

            if (saveCurrent)
            {
                currentPage.VideoX = Int32.Parse(XPosBox.Text);
                currentPage.VideoY = Int32.Parse(YPosBox.Text);
                currentPage.VideoHeight = Int32.Parse(HeightBox.Text);
                currentPage.VideoWidth = Int32.Parse(WidthBox.Text);
                currentPage.PageNumber = currentPageNum;
            }
            currentPageNum = pageNum;
            currentPage = StaticBook.Book.Pages[currentPageNum];

            PageNumBox.Text = (currentPageNum + 1).ToString();
            //If it's a new book, use SourcePage... filenames.
            //Otherwise, use Page... filenames because SourcePage... filenames might be meaningless.
            // if (isNewBook)
            //{
            if (currentPage.SourcePageImageFileName != null)
            {
                ImageFileLabel.Text = Path.GetFileName(currentPage.SourcePageImageFileName);
                if (PagePicture.Image != null)
                    PagePicture.Image.Dispose();
                PagePicture.Image = Image.FromFile(currentPage.SourcePageImageFileName);
            }
            else
            {
                ImageFileLabel.Text = "";
                PagePicture.Image = null;
            }
            if (currentPage.SourceAudioFileName != null)
            {
                AudioFileLabel.Text = Path.GetFileName(currentPage.SourceAudioFileName);
            }
            else
            {
                AudioFileLabel.Text = "";
            }
            if (currentPage.SourceVideoFileName != null)
            {
                VideoFileLabel.Text = Path.GetFileName(currentPage.SourceVideoFileName);
            }
            else
            {
                VideoFileLabel.Text = "";
            }
            //}
            XPosBox.Text = currentPage.VideoX.ToString();
            YPosBox.Text = currentPage.VideoY.ToString();
            HeightBox.Text = currentPage.VideoHeight.ToString();
            WidthBox.Text = currentPage.VideoWidth.ToString();
        }

        //Make sure to add prompt to save current book before opening a new one
        /// <summary>
        /// Give a prompt to open an existing book in the builder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenBook(object sender, EventArgs e)
        {
            openFileDialog.Filter = StaticBook.armbFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                StaticBook.OpenBook(openFileDialog.FileName);
                GoToPage(0, false);
                StaticBook.hasBeenSaved = true;
                StaticBook.savePath = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// Disposes of the image in the picturebox if it's not null.
        /// </summary>
        /// <returns></returns>
        public void DisposeImage()
        {
            if (PagePicture.Image != null)
            {
                PagePicture.Image.Dispose();
            }
        }

        private void Save(object sender, EventArgs e)
        {
            if (!imageCheck()) return;
            GoToPage(currentPageNum, true);
            if (StaticBook.hasBeenSaved == false)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveBook(saveFileDialog.FileName);
                }
            } else {
                SaveBook(StaticBook.savePath);
            }
        }
    }
}
