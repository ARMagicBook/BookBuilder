using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookBuilder
{
    /// <summary>
    /// This form runs on startup and is used to set book-wide properties.
    /// Sets authors, description, number of pages, title, and creation date.
    /// </summary>
    public partial class SetupForm : Form
    {
        private int insertRowNum;

        /// <summary>
        /// Initializes the SetupForm.
        /// </summary>
        public SetupForm()
        {
            InitializeComponent();
            AuthorBoxes.Add(AuthorBox1);
            insertRowNum = tableLayoutPanel.GetRow(AuthorBox1) + 1;  //Want to insert rows belows the bottomost author row
        }

        /// <summary>
        /// List of author text boxes the user has created.
        /// </summary>
        private List<TextBox> AuthorBoxes = new List<TextBox>();

        /// <summary>
        /// Store all the current information in the BB_Book and load the main form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToBuilder(object sender, EventArgs e)
        {
            if (TitleBox.Text == "")
            {
                MessageBox.Show("Your book must have a title.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StaticBook.Book.Title = TitleBox.Text;
            StaticBook.Book.Description = DescriptionBox.Text;

            for (int i=0; i < PagesBox.Value; i++)
            {
                StaticBook.Book.Pages.Add(new global::BookBuilder.BB_Page());
            }

            foreach (TextBox Box in AuthorBoxes){
                if (Box.Text != "")
                {
                    StaticBook.Book.Authors.Add(Box.Text);
                }
            }

            //Set CreationDate to today's date.
            StaticBook.Book.CreationDate = DateTime.Today.ToLongDateString();

            StaticBook.mainForm.currentPage = StaticBook.Book.Pages[0];

            //Hide the setup form and show the main bookbuilder form.
            this.Visible = false;
            StaticBook.mainForm.Visible = true;

        }
        /// <summary>
        /// Runs when the setup form is closed.
        /// Exits the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetupFormClosed(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Adds another author textbox between the last one added and AddAuthorButton.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAuthorBox(object sender, EventArgs e)
        {
            //Create new row in TableLayoutPanel. (Resize TableLayoutPanel first?)
            //Put a new textbox in it and add it to AuthorBoxes
            //Resize the window to accomodate the new textbox.

            tableLayoutPanel.Visible = false;

            RowStyle newRowStyle = new RowStyle();
            newRowStyle.SizeType = SizeType.AutoSize;
            tableLayoutPanel.RowStyles.Insert(insertRowNum, newRowStyle);

            tableLayoutPanel.RowCount += 1;

            //Move existing rows below the inserted row down 1
            foreach (Control existingControl in tableLayoutPanel.Controls)
            {
                if (tableLayoutPanel.GetRow(existingControl) >= insertRowNum)
                {
                    tableLayoutPanel.SetRow(existingControl, tableLayoutPanel.GetRow(existingControl) + 1);
                }
            }

            //Add new author text box
            TextBox nextAuthorBox = new TextBox();
            nextAuthorBox.Size = new Size(AuthorBox1.Size.Width, nextAuthorBox.Size.Height);
            tableLayoutPanel.Controls.Add(nextAuthorBox, 1, insertRowNum);
            AuthorBoxes.Add(nextAuthorBox);

            //Adjust size of layout panel and setup form to account for the new author row
            tableLayoutPanel.Size = new Size(tableLayoutPanel.Size.Width, tableLayoutPanel.Size.Height + nextAuthorBox.Size.Height + 6); //6 is padding for layout cell size
            this.Size = new Size(this.Size.Width, this.Size.Height + nextAuthorBox.Size.Height + 6);
            
            insertRowNum++; //Next time add is pressed we insert underneath this one
            tableLayoutPanel.Visible = true;
        }

        private void OpenExistingBook(object sender, EventArgs e)
        {
            openFileDialog.Filter = "ARMB files (*.armb)| *.armb";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Extract zip into temp folder
                String tempFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "../Local/ARMB/temp/bookbuilder/building");

                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }

                Directory.CreateDirectory(tempFolder);
                ZipFile.ExtractToDirectory(openFileDialog.FileName, tempFolder);
                //Parse the serialized BB_Book and copy it into our book.
                StaticBook.Book.DeserializeBook(tempFolder);
                foreach (BB_Page p in StaticBook.Book.Pages)
                {
                    if (p.PageImageFileName != null && p.PageImageFileName != "")
                    {
                        p.SourcePageImageFileName = Path.Combine(tempFolder, "images", p.PageImageFileName);
                    }
                    if (p.AudioFileName != null && p.AudioFileName != "")
                    {
                        p.SourceAudioFileName = Path.Combine(tempFolder, "audio", p.AudioFileName);
                    }
                    if (p.VideoFileName != null && p.VideoFileName != "")
                    {
                        p.SourceVideoFileName = Path.Combine(tempFolder, "video", p.VideoFileName);
                    }
                }
                StaticBook.mainForm.GoToPage(0, false);
                this.Visible = false;
                StaticBook.mainForm.Visible = true;
            }

        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}