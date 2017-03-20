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


    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
            book = new BB_Book();
        }

        //This is where we're storing the book.
        public BB_Book book;

        //This is SUPER TEMPORARY.
        //It's only for the current setup where the book we make only has one page.
        public BB_Page page1;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PageImageFileLabel_Click(object sender, EventArgs e)
        {

        }

        //Shows the file dialogue and sets the image filename accordingly
        private void PageImageBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                PageFileBox.Text = openFileDialog1.FileName;
            } else
            {
                Debug.Write("Couldn't open the file.");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
