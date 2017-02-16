using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBuilder
{

    class BB_Page
    {
		private int pageNumber;

		private string pageImageName;
		private string videoFileName;
		private string audioFileName;
		private int videoWidth;
		private int videoHeight;
		private int videoX;//x coord of video
		private int videoY;//y coord of video

//For now we will pass this in with input.  When Ryan finished the CRC hash we will use that to generate these values.
		private string videoCRC;
		private string audioCRC; 

		public int PageNumber
		{
			get
			{
				return pageNumber;
			}

			set
			{
				pageNumber = value;
			}
		}

		public string PageImageName
		{
			get
			{
				return pageImageName;
			}

			set
			{
				pageImageName = value;
			}
		}

		public string VideoFileName
		{
			get
			{
				return videoFileName;
			}

			set
			{
				videoFileName = value;
			}
		}

		public string AudioFileName
		{
			get
			{
				return audioFileName;
			}

			set
			{
				audioFileName = value;
			}
		}

		public int VideoWidth
		{
			get
			{
				return videoWidth;
			}

			set
			{
				videoWidth = value;
			}
		}

		public int VideoHeight
		{
			get
			{
				return videoHeight;
			}

			set
			{
				videoHeight = value;
			}
		}

		public int VideoX
		{
			get
			{
				return videoX;
			}

			set
			{
				videoX = value;
			}
		}

		public int VideoY
		{
			get
			{
				return videoY;
			}

			set
			{
				videoY = value;
			}
		}
		public string VideoCRC
		{
			get
			{
				return videoCRC;
			}

			set
			{
				videoCRC = value;
			}
		}

		public string AudioCRC
		{
			get
			{
				return audioCRC;
			}

			set
			{
				audioCRC = value;
			}
		}

    }

    //Book for the bookbuilder
    //Prefix so there's no confusion with the ARMB Book class.
    class BB_Book
    {
		private List<BB_Page> pages;
		private String title;
		private List<String> authors;
		private String creationDate;
		private String description;

        //filename of the button_image
		private String buttonImageName;
		private String fileVersion;

		public BB_Book(){
			pages = new List<BB_Page>();
			authors = new List<String>();
		}

		public List<BB_Page> Pages
		{
			get
			{
				return pages;
			}

			set
			{
				pages = value;
			}
		}

		public string Title
		{
			get
			{
				return title;
			}

			set
			{
				title = value;
			}
		}

		public List<string> Authors
		{
			get
			{
				return authors;
			}

			set
			{
				authors = value;
			}
		}

		public string CreationDate
		{
			get
			{
				return creationDate;
			}

			set
			{
				creationDate = value;
			}
		}

		public string Description
		{
			get
			{
				return description;
			}

			set
			{
				description = value;
			}
		}

		public string ButtonImageName
		{
			get
			{
				return buttonImageName;
			}

			set
			{
				buttonImageName = value;
			}
		}

		public string FileVersion
		{
			get
			{
				return fileVersion;
			}

			set
			{
				fileVersion = value;
			}
		}

		public override string ToString() {
			string bookString = "";
			bookString += "Title: " + Title + "\n";

			foreach (string author in authors) {
				bookString += "Author: " + author + "\n";
			}

			foreach (BB_Page page in pages) {
				bookString += "Page Num: " + page.PageNumber + "\n";
				bookString += "Page Image: " + page.PageImageName + "\n";
				if (page.AudioFileName != null){
					bookString += "Page Audio File " + page.AudioFileName + "\n";
				}
				if (page.VideoFileName != null){
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
						//Authors are seperated by commas
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
						page.PageImageName = splitLine[1];
						break;
					case "audio_file":
						page.AudioFileName = splitLine[1];
						break;
					case "crc-32_checksum_audio":
						page.AudioCRC = splitLine[1];
						break;
					case "video_file":
						page.VideoFileName = splitLine[1];
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
				outFile.WriteLine(tabs(3) + "<page_image>" + currentPage.PageImageName + "</page_image>");
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
