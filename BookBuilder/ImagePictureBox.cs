using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace BookBuilder
{
    class ImagePictureBox : PictureBox
    {
        //Allow coordinates of the actual image to be found
        PropertyInfo pInfo = typeof(PictureBox).GetProperty("ImageRectangle",
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance
        );
    }
}
