using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        /// <summary>
        /// Initializes the SetupForm.
        /// </summary>
        public SetupForm()
        {
            InitializeComponent();
            AuthorBoxes.Add(AuthorBox1);
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
            StaticBook.book.Title = TitleBox.Text;
            StaticBook.book.Description = DescriptionBox.Text;

            for (int i=0; i < PagesBox.Value; i++)
            {
                StaticBook.book.Pages.Add(new BookBuilder.BB_Page());
            }

            foreach (TextBox Box in AuthorBoxes){
                if (Box.Text != "")
                {
                    StaticBook.book.Authors.Add(Box.Text);
                }
            }

            //Set CreationDate to today's date.
            StaticBook.book.CreationDate = DateTime.Today.ToLongDateString();

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
        }
    }
}
