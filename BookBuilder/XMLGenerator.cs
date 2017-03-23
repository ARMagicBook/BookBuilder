﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace BookBuilder
{
    /// <summary>Used to parse input from a text file into BB_Book and BB_Pages and generate a config.xml file from that.</summary>
    public class XMLGenerator
    {
        //private BB_Book book = new BB_Book();


        /// <summary>Parses input from a file named testInput.txt to get data about a book.</summary>
        /// <remarks>This is only really for testing the XML generator; it will be replaced once we have a GUI to input book data.</remarks>
        //Changed! Now this reads data into the book it's given as an argument.
        public static void ParseInput(BB_Book book)
        {

            string path = System.IO.Directory.GetCurrentDirectory();

            //GetCurrentDirectory includes /bin/Debug - we need to back up 2 directories and then give the input name
            path += "/../../testInput.txt";

            //Store input file in array, one line per index
            string[] lines = System.IO.File.ReadAllLines(path);

            int pageNum = 0;

            //(It makes much more sense to have this here than as a class variable.)
            BB_Page page = null;

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
        /// <remarks>
        /// Any existing config.xml file will be overwritten.
		/// </remarks>
        // Changed completely! Now this generates an XML file from the book that gets passed in as an argument. 
        public static void GenerateXML(BB_Book book)
        {
            string path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "config.xml");

			XmlDocument xmlDoc = new XmlDocument();

			XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
			XmlElement root = xmlDoc.DocumentElement;
			xmlDoc.InsertBefore(xmlDeclaration, root);

			XmlElement bookTag = xmlDoc.CreateElement("book");
			XmlAttribute attr = xmlDoc.CreateAttribute("file_version");
			attr.Value = book.FileVersion;
			bookTag.Attributes.SetNamedItem(attr);
			xmlDoc.AppendChild(bookTag);

			XmlElement title = xmlDoc.CreateElement("title");
			XmlText bookTitle = xmlDoc.CreateTextNode(book.Title);
			title.AppendChild(bookTitle);
			bookTag.AppendChild(title);

			foreach (string author in book.Authors) {
				XmlElement newAuthor = xmlDoc.CreateElement("author");
				XmlText name = xmlDoc.CreateTextNode(author);
				newAuthor.AppendChild(name);
				bookTag.AppendChild(newAuthor);
			}

			XmlElement creationDate = xmlDoc.CreateElement("creation_date");
			XmlText date = xmlDoc.CreateTextNode(book.CreationDate);
			creationDate.AppendChild(date);
			bookTag.AppendChild(creationDate);

			XmlElement description = xmlDoc.CreateElement("description");
			XmlText descriptionText = xmlDoc.CreateTextNode(book.Description);
			description.AppendChild(descriptionText);
			bookTag.AppendChild(description);

			XmlElement buttonImage = xmlDoc.CreateElement("button_image");
			XmlText buttonText = xmlDoc.CreateTextNode(book.ButtonImageName);
			buttonImage.AppendChild(buttonText);
			bookTag.AppendChild(buttonImage);

			XmlElement pages = xmlDoc.CreateElement("pages");

			foreach (BB_Page currentPage in book.Pages) {
				XmlElement page = xmlDoc.CreateElement("page"); 
				XmlAttribute pageNum = xmlDoc.CreateAttribute("num");
				pageNum.Value = currentPage.PageNumber.ToString();
				page.Attributes.SetNamedItem(pageNum);

				XmlElement pageImage = xmlDoc.CreateElement("image");
				XmlText imageLoc = xmlDoc.CreateTextNode("images/" + currentPage.PageImageFileName);
				pageImage.AppendChild(imageLoc);
				page.AppendChild(pageImage);

				if (currentPage.AudioFileName != null) 
				{
					XmlElement audio = xmlDoc.CreateElement("audio");

					XmlElement audioFile = xmlDoc.CreateElement("audio_file");
					XmlText audioName = xmlDoc.CreateTextNode("audio/" + currentPage.AudioFileName);
					audioFile.AppendChild(audioName);

					XmlElement md5 = xmlDoc.CreateElement("md5_checksum");
					XmlText checksum = xmlDoc.CreateTextNode(currentPage.AudioMD5);
					md5.AppendChild(checksum);

					audio.AppendChild(audioFile);
					audio.AppendChild(md5);

					page.AppendChild(audio);
				}

				if (currentPage.VideoFileName != null)
				{
					XmlElement video = xmlDoc.CreateElement("video");

					XmlElement videoFile = xmlDoc.CreateElement("video_file");
					XmlText videoName = xmlDoc.CreateTextNode("video/" + currentPage.VideoFileName);
					videoFile.AppendChild(videoName);
					video.AppendChild(videoFile);

					XmlElement md5 = xmlDoc.CreateElement("md5_checksum");
					XmlText checksum = xmlDoc.CreateTextNode(currentPage.VideoMD5);
					md5.AppendChild(checksum);
					video.AppendChild(md5);

					XmlElement size = xmlDoc.CreateElement("size");

					XmlElement width = xmlDoc.CreateElement("width");
					XmlText widthNum = xmlDoc.CreateTextNode(currentPage.VideoWidth.ToString());
					width.AppendChild(widthNum);
					size.AppendChild(width);

					XmlElement height = xmlDoc.CreateElement("height");
					XmlText heightNum = xmlDoc.CreateTextNode(currentPage.VideoHeight.ToString());
					height.AppendChild(heightNum);
					size.AppendChild(height);

					video.AppendChild(size);

					XmlElement coord = xmlDoc.CreateElement("coord");

					XmlElement xCoord = xmlDoc.CreateElement("x");
					XmlText xNum = xmlDoc.CreateTextNode(currentPage.VideoX.ToString());
					xCoord.AppendChild(xNum);
					coord.AppendChild(xCoord);

					XmlElement yCoord = xmlDoc.CreateElement("y");
					XmlText yNum = xmlDoc.CreateTextNode(currentPage.VideoY.ToString());
					yCoord.AppendChild(yNum);
					coord.AppendChild(yCoord);

					video.AppendChild(coord);

					page.AppendChild(video);
				
				}

				pages.AppendChild(page);
			}

			bookTag.AppendChild(pages);
			xmlDoc.Save(path);
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

        //Temporary function: do stuff that was in main before
        //Necessary because we don't want to make book a public variable (yet?)
        //But we do want to parse input, generate XML, and create a zip file from the GUI
		//THIS NEVER GETS CALLED!  title, author, creation date, description, file version, and button image will always be blank until we collec them from the GUI.
        public static void doMain()
        {
            //XMLGenerator xmlGenerator = new XMLGenerator();
            BB_Book book = new BB_Book();
            XMLGenerator.ParseInput(book);
			book.AudioFileCheck();
            //xmlGenerator.GenerateXML();
            //xmlGenerator.book.CreateZipFile();
            //Console.WriteLine(xmlGenerator.book);
        }

        [STAThread]
        static void Main(string[] args)
        {
            //This starts the GUI.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
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
		/// Gets or sets absolute file path + filename of the source page image file.
		/// </summary>
		/// <value>The absolute file path + filename of the source page image file.</value>
        public string SourcePageImageFileName { get; set; }

		/// <summary>
		/// Gets or sets the absolute file path + filename of the source video file.
		/// </summary>
		/// <value>The absolute file path + filename of the source video file.</value>
        public string SourceVideoFileName { get; set; }

		/// <summary>
		/// Gets or sets the absolute file path + filename of the source audio file.
		/// </summary>
		/// <value>The absolute file path + filename of the source audio file.</value>
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

		/// <summary>
		/// Checks if two pages that will be open at the same time both have an audio file. 
		/// If they do a warning is displayed to the user (for now just write to console).
		/// </summary>
		public void AudioFileCheck(){
            if (Pages.Count < 2)
            {
                return;
            }
			for (int i = 0; i < Pages.Count; i += 2) 
			{
				BB_Page leftPage = Pages[i];
				BB_Page rightPage = Pages[i + 1];
				if (leftPage.AudioFileName != null && rightPage.AudioFileName != null)
				{
					Console.WriteLine("Warning: Page {0} and Page {1} both have audio files and will be open at the same time",
									  i, i + 1);
                    //TODO: Make this a dialog box popup instead. Maybe by having AudioFileCheck return a bool, which the GUI would check
                    //when creating the book.
				}
			}
		}

        /// <summary>Creates a zip file of the books data (pages, videos, etc.) and config.xml.</summary>
		public void CreateZipFile(string destDirectory)
        {

            string rootFolderPath = Path.Combine(destDirectory,"ARMB");
            string imagesFolderPath = Path.Combine(rootFolderPath,"images");
            string audioFolderPath = Path.Combine(rootFolderPath, "audio");
            string videoFolderPath = Path.Combine(rootFolderPath, "video");
            string configPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "config.xml");
            string configZipPath = Path.Combine(rootFolderPath,"config.xml");
            string zipPath = Path.Combine(destDirectory,"archive.armb");

            //If there is already a zip file present delete it so a new one can be created.
            if (File.Exists(zipPath))
            {
                File.Delete(zipPath);
            }

            //If the ARMB folder we're about to make into a .zip already exists, delete it. 
            //(It shouldn't, but it does if this function crashes between creating and deleting it.)
            if (Directory.Exists(rootFolderPath))
            {
                Directory.Delete(rootFolderPath,true);
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
                        string imageSourcePath = page.SourcePageImageFileName;
                        string imageDestinationPath = Path.Combine(imagesFolderPath, page.PageImageFileName);

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
                        string audioSourcePath = page.SourceAudioFileName;
                        string audioDestinationPath = Path.Combine(audioFolderPath,page.AudioFileName);

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
                        string videoSourcePath = page.SourceVideoFileName;
                        string videoDestinationPath = Path.Combine(videoFolderPath, page.VideoFileName);

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
				ZipFile.CreateFromDirectory(rootFolderPath, zipPath, CompressionLevel.NoCompression, false);
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

        public void AddPage(BB_Page p)
        {
            Pages.Add(p);
        }
    }



}
