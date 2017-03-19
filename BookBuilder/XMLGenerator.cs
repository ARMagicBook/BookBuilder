using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;

namespace BookBuilder
{
    /// <summary>Used to parse input from a text file into BB_Book and BB_Pages and generate a config.xml file from that.</summary>
    public class XMLGenerator
    {
        private BB_Book book;
        private BB_Page page;

        /// <summary>Parses input from a file named testInput.txt to get data about a book.</summary>
        /// <remarks>This is only really for testing the XML generator; it will be replaced once we have a GUI to input book data.</remarks>
        public void ParseInput()
        {
            System.Console.WriteLine("Hello!");
            book = new BB_Book();

            string path = System.IO.Directory.GetCurrentDirectory();

            //GetCurrentDirectory includes /bin/Debug - we need to back up 2 directories and then give the input name
            path += "/../../testInput.txt";

            //Store input file in array, one line per index
            string[] lines = System.IO.File.ReadAllLines(path);

            int pageNum = 0;
            foreach (string line in lines)
            {
                Console.WriteLine("On line " + line);
                string[] splitLine = line.Split('=');

                Console.WriteLine("The left of the = is " + splitLine[0]);

                //Each time we see a page tag add old page to book and create a new page object
                if (splitLine[0].Equals("page"))
                {
                    if (page != null)
                        book.Pages.Add(page);

                    page = new BB_Page();
                    Console.WriteLine("Making a new page");
                }

                switch (splitLine[0])
                {
                    case "title":
                        book.Title = splitLine[1];
                        break;
                    case "author":
                        //Authors are separated by commas
                        string[] authors = splitLine[1].Split(',');
                        foreach (string author in authors)
                        {
                            book.Authors.Add(author);
                        }
                        break;
                    case "creation_date":
                        book.CreationDate = splitLine[1];
                        break;
                    case "description":
                        book.Description = splitLine[1];
                        break;
                    case "file_version":
                        book.FileVersion = splitLine[1];
                        break;
                    case "button_image":
                        book.ButtonImageName = splitLine[1];
                        break;
                    case "page":
                        page.PageNumber = pageNum;
                        pageNum++;
                        page.SourcePageImageFileName = splitLine[1];

                        if (splitLine[1].Contains("/"))
                        {
                            string[] pagePath = splitLine[1].Split('/');
                            page.PageImageFileName = pagePath[pagePath.Length - 1];
                        }
                        else  //User just entered the file name, no path
                        {
                            page.PageImageFileName = splitLine[1];
                        }
                        break;
                    case "audio_file":
                        page.SourceAudioFileName = splitLine[1];

                        if (splitLine[1].Contains("/"))
                        {
                            string[] pagePath = splitLine[1].Split('/');
                            page.AudioFileName = pagePath[pagePath.Length - 1];
                        }
                        else  //User just entered the file name, no path
                        {
                            page.AudioFileName = splitLine[1];
                        }

                        try
                        {
                            string filePath = splitLine[1].Insert(0, "../../");
                            page.AudioMD5 = BB_Page.GetMD5(filePath);
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                            page.AudioMD5 = "";
                        }
                        break;
                    case "video_file":
                        page.SourceVideoFileName = splitLine[1];

                        if (splitLine[1].Contains("/"))
                        {
                            string[] pagePath = splitLine[1].Split('/');
                            page.VideoFileName = pagePath[pagePath.Length - 1];
                        }
                        else  //User just entered the file name, no path
                        {
                            page.VideoFileName = splitLine[1];
                        }

                        try
                        {
                            string filePath = splitLine[1].Insert(0, "../../");
                            page.VideoMD5 = BB_Page.GetMD5(filePath);
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                            page.VideoMD5 = "";
                        }
                        break;
                    case "video_width":
                        page.VideoWidth = Convert.ToInt32(splitLine[1]);
                        break;
                    case "video_height":
                        page.VideoHeight = Convert.ToInt32(splitLine[1]);
                        break;
                    case "video_coordx":
                        page.VideoX = Convert.ToInt32(splitLine[1]);
                        break;
                    case "video_coordy":
                        page.VideoY = Convert.ToInt32(splitLine[1]);
                        break;
                    default:
                        Console.WriteLine("Bad Input");
                        System.Environment.Exit(1);
                        break;
                }
            }
            // Add last page to book (since input does not include closing tags we have to manually do this)
            book.Pages.Add(page);
        }


        /// <summary>Generates config.xml file from the stored metadata</summary>
        /// <remarks>This doesn't use an XML library; all file modifications are hardcoded. This will probably be changed at some point.
        /// Also, any existing config.xml file will be overwritten.</remarks>
        public void GenerateXML()
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            path += "/../../config.xml";
            Console.WriteLine("Writing to " + path);
            System.IO.StreamWriter outFile = new System.IO.StreamWriter(path); //Will overwrite file if it already exists

            //Leaving version and encoding hardcoded for now
            outFile.WriteLine("<? version=\"1.0\" encoding=\"UTF-8\"?>");
            outFile.WriteLine("<book file_version = \"" + book.FileVersion + "\">");
            outFile.WriteLine(Tabs(1) + "<title> " + book.Title + "</title>");

            foreach (string author in book.Authors)
            {
                outFile.WriteLine(Tabs(1) + "<author>" + author + "</author>");
            }

            outFile.WriteLine(Tabs(1) + "<creation_date>" + book.CreationDate + "</creation_date>");
            outFile.WriteLine(Tabs(1) + "<description>" + book.Description + "</description>");
            outFile.WriteLine(Tabs(1) + "<button_image>" + book.ButtonImageName + "</button_image>");
            outFile.WriteLine(Tabs(1) + "<pages>");

            foreach (BB_Page currentPage in book.Pages)
            {
                outFile.WriteLine(Tabs(2) + "<page num=\"" + currentPage.PageNumber + "\">");
                outFile.WriteLine(Tabs(3) + "<page_image>" + "images/" + currentPage.PageImageFileName + "</page_image>");
                if (currentPage.AudioFileName != null)
                {
                    outFile.WriteLine(Tabs(3) + "<audio>");
                    outFile.WriteLine(Tabs(4) + "<audio_file>" + "audio/" + currentPage.AudioFileName + "</audio_file>");
                    outFile.WriteLine(Tabs(4) + "<md5_checksum>" + currentPage.AudioMD5 + "</md5_checksum>");
                    outFile.WriteLine(Tabs(3) + "</audio>");
                }
                if (currentPage.VideoFileName != null)
                {
                    outFile.WriteLine(Tabs(3) + "<video>");
                    outFile.WriteLine(Tabs(4) + "<video_file>" + "video/" + currentPage.VideoFileName + "</video_file>");
                    outFile.WriteLine(Tabs(4) + "<md5_checksum>" + currentPage.VideoMD5 + "</md5_checksum>");

                    outFile.WriteLine(Tabs(4) + "<size>");
                    outFile.WriteLine(Tabs(5) + "<width>" + currentPage.VideoWidth + "</width>");
                    outFile.WriteLine(Tabs(5) + "<height>" + currentPage.VideoHeight + "</height>");
                    outFile.WriteLine(Tabs(4) + "</size>");

                    outFile.WriteLine(Tabs(4) + "<coord>");
                    outFile.WriteLine(Tabs(5) + "<x>" + currentPage.VideoX + "</x>");
                    outFile.WriteLine(Tabs(5) + "<y>" + currentPage.VideoY + "</y>");
                    outFile.WriteLine(Tabs(4) + "</coord>");

                    outFile.WriteLine(Tabs(3) + "</video>");
                }
                outFile.WriteLine(Tabs(2) + "</page>");
            }

            //Done writing individual pages - close page tag
            outFile.WriteLine(Tabs(1) + "</pages>");
            outFile.WriteLine("</book>");

            outFile.Close();

        }

        /// <summary>Returns a string with the specified number of tabs.</summary>
        /// <remarks>
        /// This method supports an arbitrary number of tabs. Note that if the specified number of tabs is
        /// negative, then the returned value will be the empty string. (This really should require a <c>uint</c>,
        /// but the string constructor used won't accept them.)
        /// </remarks>
        /// <param name="numTabs">the number of tabs desired in the return string (must be positive)</param>
        /// <returns>string containing the specified number of tabs</returns>
		public static string Tabs(int numTabs)
        {
            if (numTabs < 0)
                return "";
            else
                return new string('\t', numTabs);
        }

        static void Main(string[] args)
        {
            XMLGenerator xmlGenerator = new XMLGenerator();
            xmlGenerator.ParseInput();
            xmlGenerator.GenerateXML();
            xmlGenerator.book.CreateZipFile();
            //Console.WriteLine(xmlGenerator.book);
        }
    }


    /// <summary>Stores info for one page of the BB_Book.</summary>
    public class BB_Page
    {
		/// <summary>
		/// Gets or sets the page number.
		/// </summary>
		/// <value>The page number.</value>
        public int PageNumber { get; set; }

		/// <summary>
		/// Gets or sets the name of the page image file for the .armb file.
		/// </summary>
		/// <value>The name of the page image file.</value>
        public string PageImageFileName { get; set; }

		/// <summary>
		/// Gets or sets the name of the video file for the .armb file.
		/// </summary>
		/// <value>The name of the video file.</value>
        public string VideoFileName { get; set; }

		/// <summary>
		/// Gets or sets the name of the audio file for the .armb file.
		/// </summary>
		/// <value>The name of the audio file.</value>
        public string AudioFileName { get; set; }

		/// <summary>
		/// Gets or sets the name of the source page image file.
		/// </summary>
		/// <value>The name of the source page image file.</value>
        public string SourcePageImageFileName { get; set; }

		/// <summary>
		/// Gets or sets the name of the source video file.
		/// </summary>
		/// <value>The name of the source video file.</value>
        public string SourceVideoFileName { get; set; }

		/// <summary>
		/// Gets or sets the name of the source audio file.
		/// </summary>
		/// <value>The name of the source audio file.</value>
        public string SourceAudioFileName { get; set; }

		/// <summary>
		/// Gets or sets the width of the video.
		/// </summary>
		/// <value>The width of the video.</value>
        public int VideoWidth { get; set; }

		/// <summary>
		/// Gets or sets the height of the video.
		/// </summary>
		/// <value>The height of the video.</value>
        public int VideoHeight { get; set; }

        /// <summary>
        /// Gets or sets the x coordinate of the video.
        /// </summary>
        /// <value>The x coordinate of the video.</value>
        public int VideoX { get; set; }

        /// <summary>
        /// Gets or sets the y coordinate of the video.
        /// </summary>
        /// <value>The y coordinate of the video.</value>
        public int VideoY { get; set; }

		/// <summary>
		/// Gets or sets the video MD5 hash value.
		/// </summary>
		/// <value>The video MD5 hash value.</value>
        public string VideoMD5 { get; set; }

		/// <summary>
		/// Gets or sets the audio MD5 hash value.
		/// </summary>
		/// <value>The audio MD5 hash value.</value>
        public string AudioMD5 { get; set; }

        /// <summary>Tries to open a file and returns its MD5 hash value as a string.</summary>
        /// <param name="filename">The name of the file to be opened.</param>
        /// <returns>the MD5 hash as a string.</returns>
        public static string GetMD5(String filename)
        {
            MD5 md5 = MD5.Create();
            String hash = "";
            //Open the file, compute the hash
            using (FileStream fs = File.Open(filename, FileMode.Open))
            {
                foreach (byte b in md5.ComputeHash(fs))
                {
                    hash += b.ToString("x2").ToLower();
                }
            }
            return hash;
        }
    }

    /// <summary>Stores information for a BB_Book including its BB_Pages, authors, etc.</summary>
    public class BB_Book
    {
		/// <summary>
		/// Gets the pages of the book.
		/// </summary>
		/// <value>The pages of the book.</value>
        public List<BB_Page> Pages { get; } = new List<BB_Page>();

		/// <summary>
		/// Gets the author(s) of the book.
		/// </summary>
		/// <value>The author(s) of the book.</value>
        public List<string> Authors { get; } = new List<string>();

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
        public string Title { get; set; }

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		/// <value>The creation date.</value>
        public string CreationDate { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of the button image.
        /// </summary>
        /// <value>The name of the button image.</value>
        public string ButtonImageName { get; set; }

		/// <summary>
		/// Gets or sets the file version.
		/// </summary>
		/// <value>The file version.</value>
        public string FileVersion { get; set; }

        /// <summary>Creates a zip file of the books data (pages, videos, etc.) and config.xml.</summary>
		public void CreateZipFile()
        {
            //Need to go back 2 directories for each path because the current working directory includes /bin/Debug
            string rootFolderPath = "../../ARMB";
            string imagesFolderPath = "../../ARMB/images";
            string audioFolderPath = "../../ARMB/audio";
            string videoFolderPath = "../../ARMB/video";
            string configPath = "../../config.xml";
            string configZipPath = "../../ARMB/config.xml";
            string zipPath = "../../archive.armb";

            //If there is already a zip file present delete it so a new one can be created.
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            Directory.CreateDirectory(rootFolderPath);
            Directory.CreateDirectory(imagesFolderPath);
            Directory.CreateDirectory(audioFolderPath);
            Directory.CreateDirectory(videoFolderPath);

            File.Copy(configPath, configZipPath);

            foreach (BB_Page page in Pages)
            {
                if (page.SourcePageImageFileName != null)
                {
                    try
                    {
                        string imageSourcePath = page.SourcePageImageFileName.Insert(0, "../../");
                        string imageDestinationPath = page.PageImageFileName.Insert(0, "../../ARMB/images/");

                        File.Copy(imageSourcePath, imageDestinationPath);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                if (page.SourceAudioFileName != null)
                {
                    try
                    {
                        string audioSourcePath = page.SourceAudioFileName.Insert(0, "../../");
                        string audioDestinationPath = page.AudioFileName.Insert(0, "../../ARMB/audio/");

                        File.Copy(audioSourcePath, audioDestinationPath);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                if (page.SourceVideoFileName != null)
                {
                    try
                    {
                        string videoSourcePath = page.SourceVideoFileName.Insert(0, "../../");
                        string videoDestinationPath = page.VideoFileName.Insert(0, "../../ARMB/video/");

                        File.Copy(videoSourcePath, videoDestinationPath);
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

			//Only have one copy of config.xml - the one in the zip file
			File.Delete(configPath);

            try
            {
				// Put ARMB folder in zip file, false parameter means do not include the root directory when unzipping
				ZipFile.CreateFromDirectory(rootFolderPath, zipPath, CompressionLevel.Fastest, false);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }

            //Recursively (boolean parameter) delete ARMB folder to just leave the zip file
            Directory.Delete(rootFolderPath, true);
        }

        /// <summary>Converts information about the book to a string.</summary>
        /// <return>String of info about the book.</return>
		public override string ToString()
        {
            string bookString = "";
            bookString += "Title: " + Title + "\n";

            foreach (string author in Authors)
            {
                bookString += "Author: " + author + "\n";
            }

            foreach (BB_Page page in Pages)
            {
                bookString += "Page Num: " + page.PageNumber + "\n";
                bookString += "Page Image: " + page.PageImageFileName + "\n";
                if (page.AudioFileName != null)
                {
                    bookString += "Page Audio File " + page.AudioFileName + "\n";
                }
                if (page.VideoFileName != null)
                {
                    bookString += "Page Video File " + page.VideoFileName + "\n";
                    bookString += "Page Video width " + page.VideoWidth + "\n";
                    bookString += "Page Video height " + page.VideoHeight + "\n";
                    bookString += "Page Video xcoord " + page.VideoX + "\n";
                    bookString += "Page Video ycoord " + page.VideoY + "\n";
                }
            }

            return bookString;
        }
    }



}
