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

namespace BookBuilder
{


    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

  
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            XMLGenerator.doMain();
        }
    }
}
