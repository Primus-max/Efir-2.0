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
using FFmpeg.NET;
using System.Globalization;

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


        public MainWindow()
        {
            InitializeComponent();
        }




        #region ПОЛЕЗНЫЕ МЕТОДЫ И ПРОЧЕЕ
        // очень хороший способ получения длительности прямо из байтов, но надо найти информацию о том в каких байтах хранится эа инфа
        /*public void GetDutayion()
        {
        string path = @"Z:\cd1.avi";
        int frameWidth = 0;
        int frameHeight = 0;
        byte[] fileDataByte = new byte[8];
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
        stream.Seek(64, SeekOrigin.Begin);
        stream.Read(fileDataByte, 4, 12);
        frameWidth = BitConverter.ToInt32(fileDataByte, 4);
        frameHeight = BitConverter.ToInt32(fileDataByte, 8);

        // var media = new MediaInfoWrapper(stream);


        }

        }*/
        #endregion


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
                    AddSreiestDB(pathToSeries);
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

        // добавление фильма
        public void AddFilmAtDB(string pathToContent)
        {
            DirectoryInfo directory = new DirectoryInfo(pathToContent);
            Film film = new Film();
            List<Film> Films = new List<Film>();



            //CountOfFilmTextBlock.Text = Convert.ToString(dic.Length);
            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (directory.Exists)
            {
                IEnumerable<FileInfo> allFileList = directory.GetFiles("*.*", SearchOption.AllDirectories);
                IEnumerable<FileSystemInfo> filteredFileList =
                    from file in allFileList
                    where file.Extension == ".avi" || file.Extension == ".mp4" || file.Extension == ".mp4" ||
                    file.Extension == ".mkv" || file.Extension == ".m4v" || file.Extension == ".mov"
                    select file;

                // собираю класс Film
                foreach (FileInfo item in filteredFileList)
                {
                    if (filteredFileList != null)
                    {
                        film.Name = item.Name;
                        film.Path = item.FullName;
                        film.Duration = DurationContent(pathToContent, film.Name);
                        film.NumOfSeries = 1; //TODO посчитать сколько серий в сезоне или сколько частей в фильме, по дефолту - 1

                        Films.Add(film);
                        // film = new Film();
                    }

                }
                DirectoryInfo[] dirs = directory.GetDirectories();
                foreach (DirectoryInfo dir in dirs)
                {
                    var swertsert = dir.FullName;
                }
            }

            // -------------------------------- ВРЕМЕННО!!!!!!!!!!!!!!!!---------------------------

            CountOfFilmTextBlock.Text = Convert.ToString(Films.Count);
        }

        // добавление сериала
        public void AddSreiestDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Series series = new Series();
            List<Series> Series = new List<Series>();


            //CountOfFilmTextBlock.Text = Convert.ToString(dic.Length);
            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists && firstDirectory.GetDirectories().Length > 0)
            {
                DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
                //DirectoryInfo secondDirectory;


                for (int i = 0; i < listDirectories.Length; i++)
                {
                    string directroryName = listDirectories[i].FullName;
                    DirectoryInfo secondDirectory = new DirectoryInfo(directroryName);

                    IEnumerable<FileInfo> allFileList = secondDirectory.GetFiles("*.*", SearchOption.AllDirectories);
                    IEnumerable<FileSystemInfo> filteredFileList =
                        from file in allFileList
                        where file.Extension == ".avi" || file.Extension == ".mp4" || file.Extension == ".mp4" ||
                        file.Extension == ".mkv" || file.Extension == ".m4v" || file.Extension == ".mov"
                        select file;

                    foreach (FileInfo item in filteredFileList)
                    {
                        if (filteredFileList != null)
                        {
                            series.Name = listDirectories[i].Name;
                            series.Path = item.FullName;
                            series.DurationOfSeries = DurationContent(pathToContent, item.ToString());

                            series.NumOfSeries = 1; //TODO посчитать сколько серий в сезоне или сколько частей в фильме, по дефолту - 1

                            Series.Add(series);
                            // film = new Film();
                        }
                    }
                }
                // directory.FullName;

                /* IEnumerable<FileInfo> allFileList = secondDirectory.GetFiles("*.*", SearchOption.AllDirectories);
                 IEnumerable<FileSystemInfo> filteredFileList =
                     from file in allFileList
                     where file.Extension == ".avi" || file.Extension == ".mp4" || file.Extension == ".mp4" ||
                     file.Extension == ".mkv" || file.Extension == ".m4v" || file.Extension == ".mov"
                     select file;
 */

                // собираю класс Film

            }

            // -------------------------------- ВРЕМЕННО!!!!!!!!!!!!!!!!---------------------------

            CountOfSeriesTextBlock.Text = Convert.ToString(Series.Count);
        }


        // получаю длительность файла
        //TODO отрефакторить: сократить время работ
        //TODO отрефакторить: сделать проверки на нулевые значения ловля ошибок
        public TimeSpan DurationContent(string pathToContent, string contentName)
        {
            MediaInfo.MediaInfo mi = new MediaInfo.MediaInfo();
            // string fullPathToContentItem = pathToContent + "\\" + contentName;
            mi.Open(contentName);

            string mediaDataFromVideo = mi.Inform();

            string durationFromMediaList = mediaDataFromVideo.Split("\r\n").First(s => s.StartsWith("Duration"));
            string durationFromString = "";

            // TODO здесь можно отрефакторить убрав личшнее прохождение по пустому пространству
            for (int i = 0; i < durationFromMediaList.Length; i++)
            {
                if (durationFromMediaList[i].ToString() == ":")
                {
                    durationFromString = durationFromMediaList.Remove(0, i + 1);
                }

            }

            int h = 0;
            int m = 0;
            int s = 0;


            var durationSplit = durationFromString.Split(" ");

            for (int j = 0; j < durationSplit.Length; j++)
            {
                if (durationSplit[j].ToLower().StartsWith("h".ToLower()))
                {
                    h = Convert.ToInt16(durationSplit[j - 1]);
                }
                if (durationSplit[j].ToLower().StartsWith("m".ToLower()))
                {
                    m = Convert.ToInt16(durationSplit[j - 1]);
                }
                if (durationSplit[j].ToLower().StartsWith("s".ToLower()))
                {
                    s = Convert.ToInt16(durationSplit[j - 1]);
                }
            }
            TimeSpan duration = new TimeSpan(h, m, s);

            return duration;
        }


        #endregion

        #endregion

        #endregion

        private void GenerateEfir_Click(object sender, RoutedEventArgs e)
        {
            //TODO Сюда доавить заполнение поля  Film.LastRun  просто вписать в него DateTime.Now();
            //TODO сделать реальную проверку сколько раз запускался фильм (а не когда база создавалась) film.NumOfRun++;
        }
    }
}
