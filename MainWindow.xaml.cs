using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using System.Drawing;

using System.IO;
using Microsoft.Win32;
using DevExpress.Utils.CommonDialogs.Internal;
using Microsoft.WindowsAPICodePack.Dialogs;
using MaterialDesignThemes.Wpf;
using Efir.Model;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;
using System.Text.RegularExpressions;
using MediaInfoLib;

namespace Efir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region ПЕРМЕННЫЕ: блок медиа
        private string pathToFilms = "";
        private string pathToSeries = "";
        private string pathToLection = "";
        private string pathToDocumental = "";
        #endregion

        string CountFilm = "";
        private static Duration time;

        public MainWindow()
        {
            InitializeComponent();
            /*string path = ""*/
            /*  using (FileStream fs = File.Open(@"Z:\cd1.avi", FileMode.Open))
            {
            var media = new MediaInfoWrapper(fs);

            var test = media.Duration;
            }*/


        }
        #region БЛОК МЕДИА

        #region СОБЫТИЯ

        #region открытие диалогов для выбора путей
        private void OpenFilmsDialog_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    FilePathToFilmTextBox.Text = commonOpenFileDialog.FileName;
                    pathToFilms = FilePathToFilmTextBox.Text;
                    // Film film = new Film();
                    AddFilmAtDB(pathToFilms);
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }


            }
        }

        private void OpenSeriesDialog_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    FilePathToSeriesTextBox.Text = commonOpenFileDialog.FileName;
                    pathToSeries = FilePathToSeriesTextBox.Text;
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }

            }
        }

        private void OpenLectionDialog_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    FilePathToLectionTextBox.Text = commonOpenFileDialog.FileName;
                    pathToLection = FilePathToLectionTextBox.Text;
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }

            }
        }

        private void OpenDocumentalDialog_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    FilePathToDocumentalTextBox.Text = commonOpenFileDialog.FileName;
                    pathToDocumental = FilePathToDocumentalTextBox.Text;
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");


                }

            }
        }
        #endregion

        #endregion



        #region МЕТОДЫ

        #region добавление и сохранение контента в базу

        public void AddFilmAtDB(string pathToContent)
        {
            DirectoryInfo directory = new DirectoryInfo(pathToContent);
            Film film = new Film();
            List<Film> Films = new List<Film>();

            IEnumerable<FileInfo> allFileList = directory.GetFiles("*.*", SearchOption.AllDirectories);
            IEnumerable<FileSystemInfo> filteredFileList =
                from file in allFileList
                where file.Extension == ".avi" || file.Extension == ".mp4" || file.Extension == ".mp4" ||
                file.Extension == ".mkv" || file.Extension == ".m4v" || file.Extension == ".mov"
                select file;


            foreach (FileInfo item in filteredFileList)
            {
                film.Name = item.Name;
                film.Path = item.FullName;
                film.LastRun = DateTime.Now;
                film.Duration = GetDurationContent(pathToContent, item.Name);
                film.NumOfRun++;
                film.NumOfSeries = 1;



                Films.Add(film);
            }

            var test = Films;
            CountOfFilm.Text = Convert.ToString(Films.Count);
        }

        public static Duration GetDurationContent(string path, string nameContent)
        {
            Shell32.Shell sh = new Shell32.Shell();
            Shell32.Folder rFolder = sh.NameSpace(path);
            Shell32.FolderItem rFiles = rFolder.ParseName(System.IO.Path.GetFileName(nameContent));
            string videosLength = rFolder.GetDetailsOf(rFiles, 27).Trim();

            /*  using (FileStream fs = File.Open(path, FileMode.Open))
            {
            var media = new MediaInfoWrapper(fs);
            var test = media.Duration;
            }
            */

            /* if (!string.IsNullOrEmpty(videosLength))
            {
            DateTime contentDuration = Convert.ToDateTime(videosLength);
            Duration time = contentDuration.TimeOfDay;
            }
            else
            {
            MessageBox.Show(nameContent);
            }*/
            /*   try
            {
            DateTime contentDuration = Convert.ToDateTime(videosLength);
            Duration time = contentDuration.TimeOfDay;
            }
            catch (Exception ex)
            {
            MessageBox.Show(ex.Message.ToString());
            }*/

            if (!DateTime.TryParse(videosLength, out _))
            {

                // var test = nameContent;
                //MessageBox.Show(nameContent);
            }
            else
            {/*
                DateTime contentDuration = Convert.ToDateTime(videosLength);
                Duration time = contentDuration.TimeOfDay;*/
            }

            DateTime contentDuration = Convert.ToDateTime(videosLength);
            Duration time = contentDuration.TimeOfDay;

            return time;
        }






        #endregion


        #endregion

        #endregion





    }
}
