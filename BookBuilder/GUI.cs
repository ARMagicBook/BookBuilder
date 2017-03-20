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
            xmlGen = new BookBuilder.XMLGenerator();
        }

        //This is where we're storing the book.
        public XMLGenerator xmlGen;


  
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            XMLGenerator.doMain();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_MouseUp(object sender, MouseEventArgs e)
        {

            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void PageImageFileLabel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Debug.Write("sent click event");
        }


        //Shows the file dialogue and sets the image filename accordingly
        private void PageImageBrowseButton_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {

            }
        }
    }
}
