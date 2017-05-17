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
    /// Contains all the ratios needed to figure out where and how large to display the video placeholder 
    /// when the window is resized.
    /// </summary>
    public class Ratios
    {
        public double widthRatio;
        public double heightRatio;
        public double xCoordRatio;
        public double yCoordRatio;
        public int pageID;
    }

    /// <summary>
    /// The main BookBuilder GUI form. 
    /// Top third of tableLayoutPanel: next page/previous page/etc controlls
    /// Middle third: the page image
    /// Bottom third: buttons to open image, audio, and video files
    /// </summary>
    public partial class MainForm : Form
    {
        //represents where the video will appear on the page.
        private VideoPictureBox videoPlaceholder;

        private List<Ratios> ratioList;

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
            PagePicture.MouseUp += MouseUpHandler;
            videoPlaceholder = new VideoPictureBox(this);
            videoPlaceholder.setImagePictureBox(PagePicture);
            videoPlaceholder.setTableLayoutPanel(MainLayoutPanel);
            videoPlaceholder.Size = new Size(150, 150);
            videoPlaceholder.Image = Image.FromFile("../../video_source/video_placeholder.png");
            Point centerOfPageImage = new Point(PagePicture.Location.X + PagePicture.Size.Width / 2 - videoPlaceholder.Size.Width / 2,
                PagePicture.Location.Y + PagePicture.Size.Height / 2 - videoPlaceholder.Size.Height / 2);
            videoPlaceholder.Location = centerOfPageImage;
            videoPlaceholder.SizeMode = PictureBoxSizeMode.StretchImage;

            Controls.Add(videoPlaceholder);
            videoPlaceholder.BringToFront();
            videoPlaceholder.Visible = false;
            ratioList = new List<Ratios>();
        }
        /// <summary>
        /// The page currently being viewed in MainForm.
        /// </summary>
        public BB_Page currentPage;

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            DisplayVideoSizeAndLocation();
        }

        //The current page number, zero-indexed as it is in the BB_Book
        private int currentPageNum = 0;

        /// <summary>
        /// Whether any change has been made since the book was last saved.
        /// </summary>
        public bool changeMade = false;

        /// <summary>
        /// Runs when the main form is closed.
        /// Should prompt the user to save their work before exiting. For now, it just exits the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormClosed(object sender, FormClosingEventArgs e)
        {
            if (changeMade)
            {
                //TODO: Implement this maybe.
            }
            Application.Exit();
        }

        public const int xVideoOffset = 12;
        public const int yVideoOffset = 5;
        public int videoXPos;
        public int videoYPos;
        public int videoWidth;
        public int videoHeight;


        private void OpenPageImage(object sender, EventArgs e)
        {
            openFileDialog.Filter = StaticBook.imageFileFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var newImg = System.Drawing.Image.FromFile(openFileDialog.FileName);
                currentPage.ImageWidth = newImg.Width;
                currentPage.ImageHeight = newImg.Height;

                /*
                                //if first page, make sure other images have same size
                                if (currentPageNum == 0 && StaticBook.Book.Pages.Count > 1)
                                {
                                    var nextPageImg = System.Drawing.Image.FromFile(StaticBook.Book.Pages[1].SourcePageImageFileName);
                                    int nextPageX = nextPageImg.Width;
                                    int nextPageY = nextPageImg.Height;
                                    if (newImgX != nextPageX || newImgY != nextPageY)
                                    {
                                        MessageBox.Show("All page images must be the same size.", "Image Size Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                } else if (currentPageNum > 0)
                                {
                                    var firstPageImg = System.Drawing.Image.FromFile(StaticBook.Book.Pages[0].SourcePageImageFileName);
                                    int firstPageX = firstPageImg.Width;
                                    int firstPageY = firstPageImg.Height;
                                    if (newImgX != firstPageX || newImgY != firstPageY)
                                    {
                                        MessageBox.Show("All page images must be the same size as the first page.", "Image Size Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                */

                //if not first page, make sure it's the same size as the first page


                currentPage.SourcePageImageFileName = openFileDialog.FileName;
                currentPage.PageImageFileName = Path.GetFileName(openFileDialog.FileName);
                ImageFileLabel.Text = currentPage.PageImageFileName;
                PagePicture.Image = Image.FromFile(openFileDialog.FileName);
            }
            changeMade = true;
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
                    }
                    else
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
            changeMade = true;
        }

        private void OpenVideo(object sender, EventArgs e)
        {
            openFileDialog.Filter = StaticBook.videoFileFilter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentPage.SourceVideoFileName = openFileDialog.FileName;
                currentPage.VideoFileName = Path.GetFileName(openFileDialog.FileName);
                VideoFileLabel.Text = currentPage.VideoFileName;

                videoPlaceholder.Location = new Point(PagePicture.Location.X + PagePicture.Size.Width / 2 - videoPlaceholder.Size.Width / 2,
                PagePicture.Location.Y + PagePicture.Size.Height / 2 - videoPlaceholder.Size.Height / 2);
                videoPlaceholder.Visible = true;
                DisplayVideoSizeAndLocation();


                //Check if this page already has a Ratio struct, if not create a new one
                bool ratioExists = false;
                foreach (Ratios ratio in ratioList)
                {
                    if (ratio.pageID == currentPageNum)
                    {
                        ratioExists = true;
                        break;
                    }
                }

                if (!ratioExists)
                {
                    Ratios ratio = new Ratios();
                    ratio.pageID = currentPageNum;
                    ratioList.Add(ratio);
                    UpdateCurrentRatio();
                
                    /*
                    Console.WriteLine("\n\nWidth ratio is {0}", ratio.widthRatio);
                    Console.WriteLine("Height ratio is {0}", ratio.heightRatio);
                    Console.WriteLine("x coord ratio is {0}", ratio.xCoordRatio);
                    Console.WriteLine("y coord ratio is {0}", ratio.yCoordRatio);
                    */

                    Debug.WriteLine("Added a new ratio with page id {0}", ratio.pageID);
                }
                else
                {
                    Debug.WriteLine("Ratio for this page already exists");
                }

            }
            changeMade = true;
        }

        public void UpdateCurrentRatio()
        {
            Ratios ratio = new Ratios();
            ratio.pageID = -1;  //dummy page ID
            foreach (Ratios currentRatio in ratioList)
            {
                if (currentRatio.pageID == currentPageNum)
                {
                    ratio = currentRatio;
                    break;
                }
            }

            if (ratio.pageID == -1)
                return;

            double imageTop = (MainLayoutPanel.Size.Height - PagePicture.ImageRectangle.Size.Height) / 2.0;
            double widthRatio = (double)videoPlaceholder.Size.Width / PagePicture.ImageRectangle.Size.Width;
            double heightRatio = (double)videoPlaceholder.Size.Height / PagePicture.ImageRectangle.Size.Height;

            double xCoordRatio = (double)(videoPlaceholder.Location.X - PagePicture.ImageRectangle.Location.X - xVideoOffset) / PagePicture.ImageRectangle.Size.Width;
            //Trying to subtract yVideoOffset from this results in a negative number when video is all the way at the top...
            double yCoordRatio = (double)(videoPlaceholder.Location.Y - imageTop /* - yVideoOffset */) / PagePicture.ImageRectangle.Size.Height;
            
            /*
            Console.WriteLine("Width ratio is {0}", widthRatio);
            Console.WriteLine("Height ratio is {0}", heightRatio);
            Console.WriteLine("x coord ratio is {0}", xCoordRatio);
            Console.WriteLine("y coord ratio is {0}", yCoordRatio);
            */
            
            ratio.widthRatio = widthRatio;
            ratio.heightRatio = heightRatio;
            ratio.xCoordRatio = xCoordRatio;
            ratio.yCoordRatio = yCoordRatio;
        }

        /// <summary>
        /// Updates the X, Y, W, and H text fields in the lower righthand corner to 
        /// display the current information of the video placeholder
        /// </summary>
        public void DisplayVideoSizeAndLocation()
        {

            if (videoPlaceholder != null)
            {
                //Subract the page pictures location because the location of the video placeholder relative to the
                //page picture is what matters.
                double imageTop = (MainLayoutPanel.Size.Height - PagePicture.ImageRectangle.Size.Height) / 2.0;
                double scale = ((double)PagePicture.ImageRectangle.Width / currentPage.ImageWidth);
                videoXPos = (int)((videoPlaceholder.Location.X - PagePicture.ImageRectangle.X - xVideoOffset) / scale);
                videoYPos = (int)((videoPlaceholder.Location.Y - imageTop - yVideoOffset) / scale);
                videoWidth = (int)(videoPlaceholder.Size.Width / scale);
                videoHeight = (int)(videoPlaceholder.Size.Height / scale);

                XPosBox.Text = videoXPos.ToString();
                YPosBox.Text = videoYPos.ToString();
                WidthBox.Text = videoWidth.ToString();
                HeightBox.Text = videoHeight.ToString();
            }
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


        private bool hasImagesCheck()
        {
            if (!StaticBook.Book.hasAllImages())
            {
                MessageBox.Show("Every page must have an image.", "Missing Image Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool imageSizeCheck()
        {
            int height = StaticBook.Book.Pages[0].ImageHeight;
            int width = StaticBook.Book.Pages[0].ImageWidth;
            for (int i=1; i < StaticBook.Book.Pages.Count; i++)
            {
                int curHeight = StaticBook.Book.Pages[i].ImageHeight;
                int curWidth = StaticBook.Book.Pages[i].ImageWidth;
                if (curHeight != height || curWidth != width)
                {
                    string ErrString = String.Format("All pages must have the same dimensions.\nPage 1: Height={0} Width={1}\nPage {2}: Height={3} Width={4}", height, width, i + 1, curHeight, curWidth);
                    MessageBox.Show(ErrString, "Image Size Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
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

            if (currentPage.SourcePageImageFileName != null)
            {
                ImageFileLabel.Text = Path.GetFileName(currentPage.SourcePageImageFileName);
                if (PagePicture.Image != null)
                    PagePicture.Image.Dispose();
                PagePicture.Image = Image.FromFile(currentPage.SourcePageImageFileName);
                currentPage.ImageHeight = PagePicture.Image.Height;
                currentPage.ImageWidth = PagePicture.Image.Width;
            }
            else
            {
                ImageFileLabel.Text = "";
                currentPage.ImageHeight = 0;
                currentPage.ImageWidth = 0;
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
                //need to set videoplaceholder object to the position it was in this page
                videoPlaceholder.Visible = true;
                double scale = PagePicture.ImageRectangle.Width / currentPage.ImageWidth;
                videoPlaceholder.Location = new Point(PagePicture.Location.X + (int)(currentPage.VideoX * scale) + PagePicture.ImageRectangle.X 
                    - videoPlaceholder.Size.Width / 2, PagePicture.Location.Y + (int)(currentPage.VideoY * scale) + PagePicture.ImageRectangle.Y 
                    - videoPlaceholder.Size.Height / 2);
            }
            else
            {
                //hide video placeholder object
                videoPlaceholder.Visible = false;
                VideoFileLabel.Text = "";
            }

            XPosBox.Text = currentPage.VideoX.ToString();
            YPosBox.Text = currentPage.VideoY.ToString();
            HeightBox.Text = currentPage.VideoHeight.ToString();
            WidthBox.Text = currentPage.VideoWidth.ToString();
        }

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
                changeMade = true;
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
            if (!hasImagesCheck()) return;
            if (!imageSizeCheck()) return;
            GoToPage(currentPageNum, true);
            if (StaticBook.hasBeenSaved == false)
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveBook(saveFileDialog.FileName);
                }
            }
            else
            {
                SaveBook(StaticBook.savePath);
            }
        }

        private void SaveAs(object sender, EventArgs e)
        {
            if (!hasImagesCheck()) return;
            if (!imageSizeCheck()) return;
            GoToPage(currentPageNum, true);
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveBook(saveFileDialog.FileName);
            }
        }

        //General purpose save function; used by SaveAs and Save
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
            changeMade = false;
        }

        private void RemoveAudio(object sender, EventArgs e)
        {
            currentPage.SourceAudioFileName = null;
            currentPage.AudioFileName = null;
            AudioFileLabel.Text = "";
            changeMade = true;
        }

        private void RemoveVideo(object sender, EventArgs e)
        {
            //Handle this after merge
            changeMade = true;
        }

        private void formResize(object sender, EventArgs e)
        {
            Ratios ratio = new Ratios();
            ratio.pageID = -1;  //dummy page ID
            foreach (Ratios currentRatio in ratioList)
            {
                if (currentRatio.pageID == currentPageNum)
                {
                    ratio = currentRatio;
                    break;
                }
            }

            //This page has no video to be resized, return
            if (ratio.pageID == -1)
                return;

            double imageTop = (MainLayoutPanel.Size.Height - PagePicture.ImageRectangle.Size.Height) / 2.0;
            int newWidth = (int)(PagePicture.ImageRectangle.Size.Width * ratio.widthRatio);
            int newHeight = (int)(PagePicture.ImageRectangle.Size.Height * ratio.heightRatio);
            int newXPos = (int)(PagePicture.ImageRectangle.Location.X + PagePicture.ImageRectangle.Size.Width * ratio.xCoordRatio);
            int newYPos = (int)(imageTop + PagePicture.ImageRectangle.Size.Height * ratio.yCoordRatio);
            videoPlaceholder.Location = new Point(newXPos + xVideoOffset, newYPos);
            videoPlaceholder.Size = new Size(newWidth, newHeight);

            /*
            double scale = ((double)PagePicture.ImageRectangle.Width / currentPage.ImageWidth);
            int imageLeft = PagePicture.ImageRectangle.X;
            double imageTop = (MainLayoutPanel.Size.Height - PagePicture.ImageRectangle.Size.Height) / 2.0;
            int newWidth = (int)(videoWidth * scale);
            int newHeight = (int)(videoHeight * scale);
            int newXpos = (int)(videoXPos * scale + imageLeft);
            Console.WriteLine("new x is {0}", newXpos);
            Console.WriteLine("video width is {0}", videoWidth);
            int newYPos = (int)(videoYPos * scale + imageTop);
            videoPlaceholder.Location = new Point(newXpos+xVideoOffset, newYPos+yVideoOffset);
            videoPlaceholder.Size = new Size(newWidth, newHeight);
            */
        }
    }
}
