using System;
using System.Collections.Generic;
using DamienG.Security.Cryptography;
using System.IO;

namespace BookBuilder
{

    class BB_Page
    {
		public int PageNumber { get; set; }

		public string PageImageFileName { get; set; }

        public string VideoFileName { get; set; }

        public string AudioFileName { get; set; }

        public int VideoWidth { get; set; }

        public int VideoHeight { get; set; }

        //x coord of video
        public int VideoX { get; set; }

        //y coord of video
        public int VideoY { get; set; }

        // For now we will pass this in with input.  When Ryan finished the CRC hash we will use that
        // to generate these values.
        public string VideoCRC { get; set; }

        public string AudioCRC { get; set; }

        public string ImageCRC { get; set; }
        
        //Tries to open a file and returns its CRC32 hash value.
        //See this: https://damieng.com/blog/2006/08/08/calculating_crc32_in_c_and_net
        public static string getCRC32(String filename)
        {
            Crc32 crc32 = new Crc32();
            String hash = "";
            //Open the file, compute the hash
            using (FileStream fs = File.Open(filename, FileMode.Open))
            {
                foreach (byte b in crc32.ComputeHash(fs))
                {
                    hash += b.ToString("x2").ToLower();
                }
            }
            return hash;
        }
    }

    //Book for the bookbuilder
    //Prefix so there's no confusion with the ARMB Book class.
    class BB_Book
    {
        public List<BB_Page> Pages { get; } = new List<BB_Page>();

        public List<string> Authors { get; } = new List<string>();

        public string Title { get; set; }

        public string CreationDate { get; set; }

        public string Description { get; set; }

        //filename of the button_image
        public string ButtonImageName { get; set; }

        public string FileVersion { get; set; }

        public override string ToString() {
            string bookString = "";
            bookString += "Title: " + Title + "\n";

            foreach (string author in Authors) {
                bookString += "Author: " + author + "\n";
            }

            foreach (BB_Page page in Pages) {
                bookString += "Page Num: " + page.PageNumber + "\n";
                bookString += "Page Image: " + page.PageImageFileName + "\n";
                if (page.AudioFileName != null) {
                    bookString += "Page Audio File " + page.AudioFileName + "\n";
                }
                if (page.VideoFileName != null) {
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

    class XMLGenerator
    {
		private BB_Book book;
		private BB_Page page;

		public void parseInput(){
			System.Console.WriteLine("Hello!");
			book = new BB_Book();

			string path = System.IO.Directory.GetCurrentDirectory();

			//GetCurrentDirectory includes /bin/Debug - we need to back up 2 directories and then give the input name
			path += "/../../testInput.txt";

			//Store input file in array, one line per index
			string[] lines = System.IO.File.ReadAllLines(path);

			int pageNum = 0;
			foreach (string line in lines) {
				Console.WriteLine("On line " + line);
				string[] splitLine = line.Split('=');

				Console.WriteLine("The left of the = is " + splitLine[0]);

				//Each time we see a page tag add old page to book and create a new page object
				if (splitLine[0].Equals("page")) {
					if (page != null)
						book.Pages.Add(page);
					
					page = new BB_Page();
					Console.WriteLine("Making a new page");
				}

				switch (splitLine[0]) {
					case "title":
						book.Title = splitLine[1];
						break;
					case "author":
						//Authors are separated by commas
						string[] authors = splitLine[1].Split(',');
						foreach (string author in authors) {
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
						page.PageImageFileName = splitLine[1];

                        //Open file and set CRC. If file can't be opened, CRC is set to "".
                        try
                        {
                            page.ImageCRC = BB_Page.getCRC32(splitLine[1]);
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                            page.ImageCRC = "";
                        }

                        break;
					case "audio_file":
						page.AudioFileName = splitLine[1];
                        try
                        {
                            page.AudioCRC = BB_Page.getCRC32(splitLine[1]);
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                            page.AudioCRC = "";
                        }
                        break;
					case "crc-32_checksum_audio":
						page.AudioCRC = splitLine[1];
						break;
					case "video_file":
						page.VideoFileName = splitLine[1];
                        try
                        {
                            page.VideoCRC = BB_Page.getCRC32(splitLine[1]);
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                            page.VideoCRC = "";
                        }
                        break;
					case "crc-32_checksum_video":
						page.VideoCRC = splitLine[1];
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

		public void generateXML(){ 
			string path = System.IO.Directory.GetCurrentDirectory();
			path += "/../../config.xml";
			Console.WriteLine("Writing to " + path);
			System.IO.StreamWriter outFile = new System.IO.StreamWriter(path); //Will overwrite file if it already exists

			//Leaving version and encoding hardcoded for now
			outFile.WriteLine("<? version=\"1.0\" encoding=\"UTF-8\"?>");
			outFile.WriteLine("<book file_version = \"" + book.FileVersion + "\">");
			outFile.WriteLine(tabs(1) + "<title> " + book.Title + "</title>");

			foreach (string author in book.Authors){
				outFile.WriteLine(tabs(1) + "<author>" + author + "</author>");
			}

			outFile.WriteLine(tabs(1) + "<creation_date>" + book.CreationDate + "</creation_date>");
			outFile.WriteLine(tabs(1) + "<description>" + book.Description + "</description>");
			outFile.WriteLine(tabs(1) + "<button_image>" + book.ButtonImageName + "</button_image>");
			outFile.WriteLine(tabs(1) + "<pages>");

			foreach (BB_Page currentPage in book.Pages) {
				outFile.WriteLine(tabs(2) + "<page num=\"" + currentPage.PageNumber + "\">");
				outFile.WriteLine(tabs(3) + "<page_image>" + currentPage.PageImageFileName + "</page_image>");
				if (currentPage.AudioFileName != null) {
					outFile.WriteLine(tabs(3) + "<audio>");
					outFile.WriteLine(tabs(4) + "<audio_file>" + currentPage.AudioFileName + "</audio_file>");
					outFile.WriteLine(tabs(4) + "<crc-32_checksum>" + currentPage.AudioCRC + "</crc-32_checksum>");
					outFile.WriteLine(tabs(3) + "</audio>");
				}
				if (currentPage.VideoFileName != null) {
					outFile.WriteLine(tabs(3) + "<video>");
					outFile.WriteLine(tabs(4) + "<video_file>" + currentPage.VideoFileName + "</video_file>");
					outFile.WriteLine(tabs(4) + "<crc-32_checksum>" + currentPage.VideoCRC + "</crc-32_checksum>");

					outFile.WriteLine(tabs(4) + "<size>");
					outFile.WriteLine(tabs(5) + "<width>" + currentPage.VideoWidth + "</width>");
					outFile.WriteLine(tabs(5) + "<height>" + currentPage.VideoHeight + "</height>");
					outFile.WriteLine(tabs(4) + "</size>");

					outFile.WriteLine(tabs(4) + "<coord>");
					outFile.WriteLine(tabs(5) + "<x>" + currentPage.VideoX + "</x>");
					outFile.WriteLine(tabs(5) + "<y>" + currentPage.VideoY + "</y>");
					outFile.WriteLine(tabs(4) + "</coord>");

					outFile.WriteLine(tabs(3) + "</video>");
				}
				outFile.WriteLine(tabs(2) + "</page>");
			}

			//Done writing individual pages - close page tag
			outFile.WriteLine(tabs(1) + "</pages>");
			outFile.WriteLine("</book>");

			outFile.Close();

		}

		public string tabs(int numTabs) {
			switch (numTabs) { 
				case 1:
					return "\t";
				case 2:
					return "\t\t";
				case 3:
					return "\t\t\t";
				case 4:
					return "\t\t\t\t";
				case 5:
					return "\t\t\t\t\t";
				default:
					return "";
			}
		}

        static void Main(string[] args)
        {
            XMLGenerator xmlGenerator = new XMLGenerator();
			xmlGenerator.parseInput();
			xmlGenerator.generateXML();
			Console.WriteLine(xmlGenerator.book);
            
        }
    }
}
