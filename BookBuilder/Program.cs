using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuilder
{

    class BB_Page
    {
        public int pageNumber;

        public string pageImageName;
        public string videoFileName;
        public int videoWidth;
        public int videoHeight;
        public int videoX;//x coord of video
        public int videoY;//y coord of video
        //public ??? videoCRC; //the CRC hash for the video will go here
        //public ??? pageCRC //the CRC hash for the page image will go here

    }

    //Book for the bookbuilder
    //Prefix so there's no confusion with the ARMB Book class.
    class BB_Book
    {
        public List<BB_Page> pages;
        public String title;
        public List<String> authors;
        public String creationDate;
        public String description;

        //filename of the button_image
        public String buttonImageName;
        public String fileVersion;


    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
