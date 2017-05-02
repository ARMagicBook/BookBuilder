using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BookBuilder
{
    class VideoPictureBox : PictureBox
    {
        private MainForm mainForm;
        private bool isMoving;
        private int xPos;
        private int yPos;

        public VideoPictureBox(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.MouseUp += MouseUpHandler;
            this.MouseDown += MouseDownHandler;
            this.MouseMove += MouseMoveHandler;
            this.isMoving = false;
        }


        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            this.isMoving = false;
            mainForm.DisplayVideoSizeAndLocation();  //Re-update size and location of video when user is done resizing.
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            this.isMoving = true;
            xPos = e.X;
            yPos = e.Y;
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        { 
            if (this.isMoving)
            {
                this.Top = this.Top + e.Y - yPos;
                this.Left = this.Left + e.X - xPos;

                // USE IMAGE COORDS TO BOUND THE VIDEO TO THE IMAGE HERE
                Console.WriteLine("left is " + this.Left);
                Console.WriteLine("Top is " + this.Top);
            }
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
