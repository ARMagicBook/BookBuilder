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
    /// The main BookBuilder GUI form. 
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Initializes the MainForm. 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

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
    }
}
