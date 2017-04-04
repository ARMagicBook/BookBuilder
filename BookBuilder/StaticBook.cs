using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookBuilder
{
    /// <summary>
    /// Stores the book we're creating and associated data, like the SetupForm and MainForm.
    /// This makes it easy to share data between forms.
    /// </summary>
    public static class StaticBook
    {
        /// <summary>
        /// The BB_Book we're creating.
        /// </summary>
        public static BB_Book book = new BB_Book();

        /// <summary>
        /// The main form. This is where the book building takes place.
        /// Should be hidden until the user finishes the SetupForm.
        /// Should be initialized by main function.
        /// </summary>
        public static MainForm mainForm;

        /// <summary>
        /// The setup form where the user chooses the number of pages and enters book information like the authors.
        /// Should be initialized and shown immediately when the user creates the MainForm.
        /// </summary>
        public static SetupForm setupForm;

        [STAThread]
        static void Main(string[] args)
        {
            //This starts the MainForm.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //The MainForm starts as invisible and SetupForm starts as visible.
            mainForm = new MainForm();
            mainForm.Visible = false;
            setupForm = new SetupForm();
            setupForm.Visible = true;
            Application.Run();
            
        }


    }
}
