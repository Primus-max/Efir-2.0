﻿using System;
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
using Efir.ViewModels;
using System.Threading;
using Efir.Data;
using System.Data.Entity;

namespace Efir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IAsyncDisposable
    {
        ApplicationContext db = new ApplicationContext();

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
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            db.Serieses.Load();
            db.Films.Load();
            db.Documentarieses.Load();
            db.Educationals.Load();
            db.Entertainments.Load();
            db.Preventions.Load();
            db.SeriesCollections.Load();
            // и устанавливаем данные в качестве контекста
            /* var asdfdfg = db.Films.Local.ToObservableCollection();
             foreach (var item in asdfdfg)
             {
                 var eoriowiepriw = item;
             }*/
            // загружаем данные из БД
            // db.Serieses.Load();
            // и устанавливаем данные в качестве контекста
            //DataContext = db.Serieses.Local.ToObservableCollection();
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
        // добавление документалок
        public async void AddDocumentariestDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Documentaries documentaries = new Documentaries();
            List<Documentaries> Documentarieses = new List<Documentaries>();

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                try
                {
                    DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
                    if (listDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с лекциями, " +
                    "Скорее всего надо выбрать папку - Лекции, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

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


                        StringNumberComparer comparer = new StringNumberComparer();
                        MainWindowViewModel viewModel = new MainWindowViewModel();

                        foreach (FileInfo item in filteredFileList)
                        {
                            if (filteredFileList != null)
                            {
                                documentaries.Name = listDirectories[i].Name;
                                documentaries.Path = item.FullName;
                                documentaries.Duration = DurationContent(pathToContent, item.ToString());
                                documentaries.NumOfSeries = filteredFileList.Count();
                                documentaries.Series += 1;

                                //добавдяю сериал в базу
                                db.Documentarieses.Add(documentaries);
                                db.SaveChanges();
                                documentaries = new Documentaries();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContent.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        CountOfSeriesTextBlock.Text = Convert.ToString(listDirectories.Length);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            await System.Threading.Tasks.Task.Yield();
        }

        // добавление образовательных
        public async void AddEducationaltDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Educational educational = new Educational();
            //List<Educational> Ed = new List<Documentaries>();

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                try
                {
                    DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
                    if (listDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с образовательным, " +
                    "Скорее всего надо выбрать папку - Лекции, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

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


                        StringNumberComparer comparer = new StringNumberComparer();
                        MainWindowViewModel viewModel = new MainWindowViewModel();

                        foreach (FileInfo item in filteredFileList)
                        {
                            if (filteredFileList != null)
                            {
                                educational.Name = listDirectories[i].Name;
                                educational.Path = item.FullName;
                                educational.Duration = DurationContent(pathToContent, item.ToString());
                                educational.NumOfSeries = filteredFileList.Count();
                                educational.Series += 1;

                                //добавдяю сериал в базу
                                db.Educationals.Add(educational);
                                db.SaveChanges();
                                educational = new Educational();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContent.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        CountOfSeriesTextBlock.Text = Convert.ToString(listDirectories.Length);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            await System.Threading.Tasks.Task.Yield();
        }

        // добавление развлекательных
        public async void AddEntertainmenttDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Entertainment entertainment = new Entertainment();
            //List<Educational> Ed = new List<Documentaries>();

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                try
                {
                    DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
                    if (listDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с образовательным, " +
                    "Скорее всего надо выбрать папку - Лекции, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

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


                        StringNumberComparer comparer = new StringNumberComparer();
                        MainWindowViewModel viewModel = new MainWindowViewModel();

                        foreach (FileInfo item in filteredFileList)
                        {
                            if (filteredFileList != null)
                            {
                                entertainment.Name = listDirectories[i].Name;
                                entertainment.Path = item.FullName;
                                entertainment.Duration = DurationContent(pathToContent, item.ToString());
                                entertainment.NumOfSeries = filteredFileList.Count();
                                entertainment.Series += 1;

                                //добавдяю сериал в базу
                                db.Entertainments.Add(entertainment);
                                db.SaveChanges();
                                entertainment = new Entertainment();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContent.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        CountOfSeriesTextBlock.Text = Convert.ToString(listDirectories.Length);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            await System.Threading.Tasks.Task.Yield();
        }

        // добавление фильмов
        public void AddFilmAtDB(string pathToContent)
        {
            DirectoryInfo directory = new DirectoryInfo(pathToContent);
            Film film = new Film();
            List<Film> Films = new List<Film>();

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (directory.Exists)
            {
                try
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
                            film.Duration = DurationContent(pathToContent, item.FullName);
                            film.NumOfSeries = 1; //TODO посчитать сколько серий в сезоне или сколько частей в фильме, по дефолту - 1

                            db.Films.Add(film);
                            db.SaveChanges();
                            film = new Film();
                        }
                    }
                    //TODO сделать правильное отображение колличества фильмов если он есть в базе
                    CountOfFilmTextBlock.Text = Convert.ToString(Films.Count);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        // добавление лекций
        public async void AddLectiontDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Lection lection = new Lection();
            List<Lection> Lections = new List<Lection>();

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                try
                {
                    DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
                    if (listDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с лекциями, " +
                    "Скорее всего надо выбрать папку - Лекции, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

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


                        StringNumberComparer comparer = new StringNumberComparer();
                        MainWindowViewModel viewModel = new MainWindowViewModel();

                        foreach (FileInfo item in filteredFileList)
                        {
                            if (filteredFileList != null)
                            {
                                lection.Name = listDirectories[i].Name;
                                lection.Path = item.FullName;
                                lection.Duration = DurationContent(pathToContent, item.ToString());
                                lection.NumOfSeries = filteredFileList.Count();
                                lection.Series += 1;

                                //добавдяю сериал в базу
                                db.Lections.Add(lection);
                                db.SaveChanges();
                                lection = new Lection();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContent.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        CountOfSeriesTextBlock.Text = Convert.ToString(listDirectories.Length);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            await System.Threading.Tasks.Task.Yield();
        }

        // добавление профилактических
        public async void AddPreventiontDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Prevention prevention = new Prevention();
            //List<Educational> Ed = new List<Documentaries>();

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                try
                {
                    DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
                    if (listDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с образовательным, " +
                    "Скорее всего надо выбрать папку - Лекции, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

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


                        StringNumberComparer comparer = new StringNumberComparer();
                        MainWindowViewModel viewModel = new MainWindowViewModel();

                        foreach (FileInfo item in filteredFileList)
                        {
                            if (filteredFileList != null)
                            {
                                prevention.Name = listDirectories[i].Name;
                                prevention.Path = item.FullName;
                                prevention.Duration = DurationContent(pathToContent, item.ToString());
                                prevention.NumOfSeries = filteredFileList.Count();
                                prevention.Series += 1;

                                //добавдяю сериал в базу
                                db.Preventions.Add(prevention);
                                db.SaveChanges();
                                prevention = new Prevention();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContent.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        CountOfSeriesTextBlock.Text = Convert.ToString(listDirectories.Length);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            await System.Threading.Tasks.Task.Yield();
        }

        // добавление сериалов
        public async void AddSreiestDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Series series = new Series();
            List<Series> Series = new List<Series>();

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                try
                {
                    DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
                    if (listDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с сериалами, " +
                    "Скорее всего надо выбрать папку - Сериалы, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

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


                        StringNumberComparer comparer = new StringNumberComparer();
                        MainWindowViewModel viewModel = new MainWindowViewModel();
                        foreach (FileInfo item in filteredFileList.OrderBy(f => f.Name, comparer))
                        {
                            if (filteredFileList != null)
                            {
                                series.Name = listDirectories[i].Name;
                                series.Path = item.FullName;
                                series.Duration = DurationContent(pathToContent, item.ToString());
                                series.NumOfSeries = filteredFileList.Count();
                                series.IsSeries += 1;

                                //добавдяю сериал в базу
                                db.Serieses.Add(series);
                                db.SaveChanges();
                                series = new Series();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContent.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        CountOfSeriesTextBlock.Text = Convert.ToString(listDirectories.Length);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            await System.Threading.Tasks.Task.Yield();
        }



        // реализация интерфейса для сортировки строк с нумерическим значением(ч частном случае: сортировка по именам для сериалов у которых имена - это цифры)
        //TODO  вынести данный класс в отдельный файл
        /// <summary>
        /// Реализация сортировки цифр в строковом типе
        /// </summary>
        class StringNumberComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                int compareResult;
                int xIndex = 0, yIndex = 0;
                int xIndexLast = 0, yIndexLast = 0;
                int xNumber, yNumber;
                int xLength = x.Length;
                int yLength = y.Length;

                do
                {
                    bool xHasNextNumber = TryGetNextNumber(x, ref xIndex, out xNumber);
                    bool yHasNextNumber = TryGetNextNumber(y, ref yIndex, out yNumber);

                    if (!(xHasNextNumber && yHasNextNumber))
                    {
                        // At least one the strings has either no more number or contains non-numeric chars
                        // In this case do a string comparison of that last part
                        return x.Substring(xIndexLast).CompareTo(y.Substring(yIndexLast));
                    }

                    xIndexLast = xIndex;
                    yIndexLast = yIndex;

                    compareResult = xNumber.CompareTo(yNumber);
                }
                while (compareResult == 0
                && xIndex < xLength
                && yIndex < yLength);

                return compareResult;
            }

            private bool TryGetNextNumber(string text, ref int startIndex, out int number)
            {
                number = 0;

                int pos = text.IndexOf('.', startIndex);
                if (pos < 0) pos = text.Length;

                if (!int.TryParse(text.Substring(startIndex, pos - startIndex), out number))
                    return false;

                startIndex = pos + 1;

                return true;
            }
        }


        // получаю длительность файла
        //TODO отрефакторить: сократить время работы
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
            // await System.Threading.Tasks.Task.Yield();
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

        /// <summary>
        /// Реализаация одноименного интерфейса
        /// </summary>        
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }


    }
}
