using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyNotes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePage : ContentPage
    {
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "notes.txt");
        public AddNotePage()
        {
            InitializeComponent();

            if (File.Exists(fileName))
            {
                editor.Text = File.ReadAllText(fileName);
            }
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(fileName, editor.Text);
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            editor.Text = string.Empty;
        }
    }
}