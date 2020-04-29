using MediaPlayer.Core;
using MediaPlayer.Core.Files;
using MediaPlayer.Core.FSM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Karaoke
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Row
        {
            public Row(MediaPlayer.Core.Indexing.Index index)
            {
                Index = index.IndexID;
                Name = index.File.Filename;
                Directory = index.File.Directory;
            }

            public int Index { get; set; }
            public string Name { get; set; }
            public string Directory { get; set; }
            public string RepresentationName => string.Format("{0} - {1}", Index, Name);
        }

        private IMediaPlayer _player;
        private IMediaConfigurator _configurator;
        private IMediaFileManager _fileManager;

        private Regex _numberCheckRegex = new Regex(@"^[0-9]*$");

        public MainWindow()
        {

        // Window initialization
        InitializeComponent();
            // ffmpeg path configuration
            Unosquare.FFME.Library.FFmpegDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"ffmpeg");

            // Instantiating karaoke controller
            MediaPlayer.Core.Player controller = new MediaPlayer.Core.Player(MediaControl);
            _player = controller;
            _configurator = controller;
            _fileManager = controller;


            Play(@"D:\Documentos\Proyectos\test.mp4");
            var indexes = MediaPlayer.Core.Indexing.IndexManager.Instance.GetIndexes();
            var files = MediaPlayer.Core.Files.FileManager.Instance.GetFiles();
            files.ForEach(file =>
            {
                DtMediaNavigator.Items.Add(new Row(_fileManager.GetIndex(file)));
            });

        }

        public async void Play(string directory)
        {
            await _player.Play(new File(directory));
        }

 
        private void ShowedMenu_Navigator_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ShowedMenu_Hide_Click(object sender, RoutedEventArgs e)
        {
            ShowedMenu_Panel.Visibility = Visibility.Collapsed;
            ShowMenuButton.Visibility = Visibility.Visible;
        }

        private void ShowedMenu_Sort_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ShowedMenu_Configurations_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowedMenu_Code_Input_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void DockPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != null && e.Key == Key.Enter)
            {
                
            }
        }

        private void ShowMenuButton_Click(object sender, RoutedEventArgs e)
        {
            ShowedMenu_Panel.Visibility = Visibility.Visible;
            ShowMenuButton.Visibility = Visibility.Collapsed;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                if (DtMediaNavigator.SelectedItems != null)
                {
                    DataGridRow dgr = sender as DataGridRow;
                    Row row = dgr.DataContext as Row;
                    Play(row.Directory);

                }
            }
        }

        private void TbxSerach_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtMediaNavigator.Items.Clear();
            if (string.IsNullOrEmpty(TbxSearch.Text))
            {
                var files = MediaPlayer.Core.Files.FileManager.Instance.GetFiles();
                files.ForEach(file =>
                {
                    DtMediaNavigator.Items.Add(new Row(_fileManager.GetIndex(file)));
                });
            }
            else if (_numberCheckRegex.IsMatch(TbxSearch.Text))
            {
                var file = _fileManager.GetFile(Int32.Parse(TbxSearch.Text));
                DtMediaNavigator.Items.Add(new Row(_fileManager.GetIndex(file)));
            }
            else
            {
                var files = _fileManager.GetFiles(TbxSearch.Text);
                files.ForEach(file =>
                {
                    DtMediaNavigator.Items.Add(new Row(_fileManager.GetIndex(file)));
                });
            }
        }


    }
}
