using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BookBuilder
{
    class CustomPictureBox : PictureBox
    {
        private MainForm mainForm;

        public CustomPictureBox(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.MouseUp += MouseUpHandler;
        }


        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            mainForm.DisplayVideoSizeAndLocation();  //Re-update size and location of video when user is done resizing.
        }

        //http://stackoverflow.com/questions/17264225/how-can-user-resize-control-at-runtime-in-winforms
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style |= 0x840000;  //WS_BORDER + WS_THICKFRAME
                return cp;
            }
        }

    }
}
