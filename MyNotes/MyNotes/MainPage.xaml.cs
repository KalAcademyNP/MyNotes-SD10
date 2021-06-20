using MyNotes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyNotes
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var notes = new List<Note>();
            var files = Directory.EnumerateFiles(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), "*.notes.txt");
            foreach (var filename in files)
            {
                var note = new Note
                {
                    Text = File.ReadAllText(filename),
                    Date = File.GetCreationTime(filename),
                    FileName = filename
                };
                notes.Add(note);
            }
            NotesListView.ItemsSource = notes.OrderBy(n => n.Date).ToList();
        }

        private async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddNotePage
            {
                BindingContext = new Note()
            });
        }

        private async void OnNoteItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushModalAsync(new AddNotePage
            {
                BindingContext = (Note)e.SelectedItem
            });
        }
    }
}
