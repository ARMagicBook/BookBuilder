using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BookBuilder
{
    /// <summary>Stores information for a BB_Book including its BB_Pages, authors, etc.</summary>
    public class BB_Book
    {
        /// <summary>
        /// Gets the pages of the book.
        /// </summary>
        /// <value>The pages of the book.</value>
        public List<BB_Page> Pages { get; private set; } = new List<BB_Page>();

        /// <summary>
        /// Gets the author(s) of the book.
        /// </summary>
        /// <value>The author(s) of the book.</value>
        public List<string> Authors { get; private set; } = new List<string>();

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
        /// Checks if two adding an audio file at fromPage will make two pages' audio play at once.
        /// </summary>
        /// <param name="fromPage">The page to check from.</param>
        /// <returns>True if it won't cause a problem, false if it will</returns>
        public bool AudioFileCheck(int fromPage)
        {
            if (Pages.Count < 2)
            {
                return true; 
            }

            //If the page number is even, we check from the next page
            if (fromPage % 2 == 0)
            {
                return (fromPage == Pages.Count - 1) || Pages[fromPage + 1].AudioFileName == null;
            } else //otherwise you check the previous page
            {
                return Pages[fromPage - 1].AudioFileName == null;
            }
        }

        /// <summary>
        /// THIS HAS NOT BEEN TESTED YET. NEED TO GET THE GUI TO THE POINT OF CREATING A BOOK FIRST.
        /// Checks to see if all images in the book are the same size.
        /// Uses the size of the image on the first page as the correct size.
        /// </summary>
        /// <returns>true if all the images in the book are the same size, false otherwise</returns>
        public bool ImageFileCheck()
        {
            if (Pages == null)
                return false;

            System.Drawing.Image img = System.Drawing.Image.FromFile(Pages[0].SourcePageImageFileName);
            int correctHeight = img.Width;
            int correctWidth = img.Height;

            for (int i = 1; i < Pages.Count; i++)
            {
                try
                {
                    img = System.Drawing.Image.FromFile(Pages[i].SourcePageImageFileName);
                    if (img.Height != correctHeight || img.Width != correctWidth)
                    {
                        Console.WriteLine("Warning: Page image {0} has the incorrect size. All pages must have height {1} and width {0}",
                            i, correctHeight, correctWidth);
                        return false;
                    }
                }
                catch
                {
                    Console.WriteLine("Failed to open image file for page {0}", i);
                }
            }
            return true;
        }


        /// <summary>
        /// Serializes the BB_Book.
        /// </summary>
        /// <param name="path">The directory to save the serialized BB_Book in.</param>
        public void SerializeBook(String path)
        {
            // Insert code to set properties and fields of the object.  
            XmlSerializer serializer = new XmlSerializer(typeof(BB_Book));
            // To write to a file, create a StreamWriter object.  
            StreamWriter writer = new StreamWriter(Path.Combine(path,"serialized"));
            serializer.Serialize(writer, this);
            writer.Close();

        }

        /// <summary>
        /// Deserializes the BB_Book and replaces the current values with its values.
        /// </summary>
        /// <param name="path">The directory of the serialized BB_Book.</param>
        public void DeserializeBook(String path)
        {
            BB_Book tempBook;
            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer serializer = new XmlSerializer(typeof(BB_Book));
            // To read the file, create a FileStream.  
            using (FileStream fs = new FileStream(Path.Combine(path, "serialized"), FileMode.Open))
            {
                // Call the Deserialize method and cast to the object type.  
                tempBook = (BB_Book)serializer.Deserialize(fs);

                //Copy tempBook to this book
                Pages = tempBook.Pages;
                Authors = tempBook.Authors;
                Title = tempBook.Title;
                CreationDate = tempBook.CreationDate;
                Description = tempBook.Description;
                ButtonImageName = tempBook.ButtonImageName;
                FileVersion = tempBook.FileVersion;
            }

        }

        /// <summary>
        /// Creates a zip file of the books data (pages, videos, etc.) and config.xml.
        /// </summary>
        /// <param name="filePath">The full path of the intended filename for the archive.</param>
		public void CreateZipFile(string filePath)
        {

            string destDirectory = Path.GetDirectoryName(filePath);
            string rootFolderPath = Path.Combine(destDirectory, "ARMB");
            string imagesFolderPath = Path.Combine(rootFolderPath, "images");
            string audioFolderPath = Path.Combine(rootFolderPath, "audio");
            string videoFolderPath = Path.Combine(rootFolderPath, "video");
            string configPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "config.xml");
            string configZipPath = Path.Combine(rootFolderPath, "config.xml");

            //If there is already a zip file present delete it so a new one can be created.
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            //If the ARMB folder we're about to make into a .zip already exists, delete it. 
            //(It shouldn't, but it does if this function crashes between creating and deleting it.)
            if (Directory.Exists(rootFolderPath))
            {
                Directory.Delete(rootFolderPath, true);
            }

            Directory.CreateDirectory(rootFolderPath);
            Directory.CreateDirectory(imagesFolderPath);
            Directory.CreateDirectory(audioFolderPath);
            Directory.CreateDirectory(videoFolderPath);

            File.Copy(configPath, configZipPath);

            //Serialize book
            SerializeBook(rootFolderPath);

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
                        string audioDestinationPath = Path.Combine(audioFolderPath, page.AudioFileName);

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
                ZipFile.CreateFromDirectory(rootFolderPath, filePath, CompressionLevel.NoCompression, false);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }

            //Recursively (boolean parameter) delete ARMB folder to just leave the zip file
            Directory.Delete(rootFolderPath, true);
        }

        /// <summary>
        /// Checks if every page has an image.
        /// </summary>
        /// <returns>True if every page has an image</returns>
        public bool hasAllImages()
        {
            foreach (BB_Page p in Pages)
            {
                if (p.PageImageFileName == "" || p.PageImageFileName == null)
                {
                    return false;
                }
            }
            return true;
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
