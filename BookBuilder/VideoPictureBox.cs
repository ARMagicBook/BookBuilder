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
        private ImagePictureBox imagePictureBox;

        public VideoPictureBox(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.MouseUp += MouseUpHandler;
            this.MouseDown += MouseDownHandler;
            this.MouseMove += MouseMoveHandler;
            this.isMoving = false;
        }

        public void setImagePictureBox(ImagePictureBox imagePictureBox) {
            this.imagePictureBox = imagePictureBox;
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
                int newTop = this.Top + e.Y - yPos;
                int newLeft = this.Left + e.X - xPos;
                int imageRight = imagePictureBox.ImageRectangle.X + imagePictureBox.ImageRectangle.Width;
                int imageBottom = imagePictureBox.ImageRectangle.Y + imagePictureBox.ImageRectangle.Height;

                //Bound video picture box to the images container
                if (newTop - imagePictureBox.ImageRectangle.Y < 0)
                    newTop = imagePictureBox.ImageRectangle.Y;
                if (newLeft - imagePictureBox.ImageRectangle.X < 0)
                    newLeft = imagePictureBox.ImageRectangle.X;
                if (newTop + this.Size.Height > imageBottom)
                    newTop = imageBottom - this.Size.Height;
                if (newLeft + this.Size.Width > imageRight)
                    newLeft = imageRight - this.Size.Width;

                this.Top = newTop;
                this.Left = newLeft;

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
