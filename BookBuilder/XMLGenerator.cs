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
        /// <summary>
        /// Generates an XML file from a BB_Book that was passed in.
        /// </summary>
        /// <param name="book"></param>
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

    }
}

