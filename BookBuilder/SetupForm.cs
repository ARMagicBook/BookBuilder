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
    /// Sets authors, description, creation date, number of pages, and title.
    /// </summary>
    public partial class SetupForm : Form
    {
        /// <summary>
        /// Initializes the SetupForm.
        /// </summary>
        public SetupForm()
        {
            InitializeComponent();
        }



    }
}
