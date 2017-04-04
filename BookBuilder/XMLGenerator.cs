using System;
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
        /*
        [STAThread]
        static void Main(string[] args)
        {
            //This starts the MainForm.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        */
    }
   



}
