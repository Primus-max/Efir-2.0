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
using Efir.ViewModels;
using System.Threading;
using Efir.Data;
using System.Data.Entity;
using Efir.ViewModels.Base;

namespace Efir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IAsyncDisposable
    {
        //TODO подумать над тем что решением проблемы с определением что будет именем файла в базе, имя папки или имя самого файла, может быть писать одно в Name другое в Description, а пользователь потом это сможет поменять поменяв местами поля в списках        
        //TODO сделать в настройках программы возможность добавления флага для определения жанра, этот флаг будет отображаться в имении папки
        //TODO запуск программы по середине окна
        //TODO сделать чтобы коллчиство добавляемых элементов показывалось в рантайме а не по факту добавленного
        //TODO поработать надо высвобождением ресурсов, слишком много по памяти жрет 
        ApplicationContext db = new ApplicationContext();
        MainWindowViewModel mainModel = new MainWindowViewModel();

        #region ПЕРМЕННЫЕ: блок медиа
        private string pathToFilms = "";
        private string pathToSeries = "";
        private string pathToLection = "";
        private string pathToDocumentaries = "";
        private string pathToEntertainment = "";
        private string pathToPrevention = "";

        #endregion

        string CountFilm = "";


        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO отрефаткориить загрузку начальных данных. Изменить место хранения, и способ отбражения, но пока пойдет


            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            db.Serieses.Load();
            db.Films.Load();
            //db.Documentarieses.Load();
            db.Educationals.Load();
            //db.Entertainments.Load();
            db.Preventions.Load();
            db.TvShows.Load();


            CountOfFilmTextBlock.Text = Convert.ToString(db?.Films.Count());
            //FilePathToFilmTextBox.Text = db?.Films.ToList()?[0].Path == null ? "" : db?.Films.ToList()[0].Path;


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

        #region открытие диалогов для выбора файлов
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
                    AddSreiesAtDB(pathToSeries);
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
                    AddLectiontAtDB(pathToLection);
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }

            }
        }

        /* private void OpenDocumentariesDialog_Click(object sender, RoutedEventArgs e)
         {
             CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
             commonOpenFileDialog.IsFolderPicker = true;
             commonOpenFileDialog.AddToMostRecentlyUsedList = true;

             if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
             {
                 try
                 {
                     FilePathToDocumentariesTextBox.Text = commonOpenFileDialog.FileName;
                     pathToDocumentaries = FilePathToDocumentariesTextBox.Text;
                     AddDocumentariesAtDB(pathToDocumentaries);
                     //TODO профиксить почему не обновляется информация в текстовом поле если использую переменную из MAinViewModel
                     //mainModel.FilePathToDocumentariesextBox = commonOpenFileDialog.FileName;
                     //pathToDocumental = mainModel.FilePathToDocumentariesextBox;
                     // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                 }
                 catch (Exception ex)
                 {
                     // TODO обработать правильно ошибки, найти значения и передать по русски
                     MessageBox.Show($"Произошла ошибка: {ex.Message}");
                 }
             }
         }*/

        /*private void OpenEntertainmentDialog_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;
            commonOpenFileDialog.ShowPlacesList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    FilePathToEntertainmentTextBox.Text = commonOpenFileDialog.FileName;
                    pathToEntertainment = FilePathToEntertainmentTextBox.Text;
                    AddEntertainmentAtDB(pathToEntertainment);
                    //TODO профиксить почему не обновляется информация в текстовом поле если использую переменную из MAinViewModel
                    //mainModel.FilePathToDocumentariesextBox = commonOpenFileDialog.FileName;
                    //pathToDocumental = mainModel.FilePathToDocumentariesextBox;
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    // TODO обработать правильно ошибки, найти значения и передать по русски
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }*/

        private void OpenPreventionDialog_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;
            commonOpenFileDialog.ShowPlacesList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    FilePathToPreventionTextBox.Text = commonOpenFileDialog.FileName;
                    pathToPrevention = FilePathToPreventionTextBox.Text;
                    AddPreventionAtDB(pathToPrevention);
                    //TODO профиксить почему не обновляется информация в текстовом поле если использую переменную из MAinViewModel
                    //mainModel.FilePathToDocumentariesextBox = commonOpenFileDialog.FileName;
                    //pathToDocumental = mainModel.FilePathToDocumentariesextBox;
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    // TODO обработать правильно ошибки, найти значения и передать по русски
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }

        private void OpenTvShowDialog_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;
            commonOpenFileDialog.ShowPlacesList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    FilePathToTvShowTextBox.Text = commonOpenFileDialog.FileName;
                    pathToPrevention = FilePathToTvShowTextBox.Text;

                    //TODO профиксить почему не обновляется информация в текстовом поле если использую переменную из MAinViewModel
                    //mainModel.FilePathToDocumentariesextBox = commonOpenFileDialog.FileName;
                    //pathToDocumental = mainModel.FilePathToDocumentariesextBox;
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    // TODO обработать правильно ошибки, найти значения и передать по русски
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }
            }
        }
        #endregion

        #endregion


        #region МЕТОДЫ

        #region добавление и сохранение контента в базу

        // TODO для документалок сделать показ всех документалок а не колличество папок в отличии от сериалов
        // добавление документалок
        /*public async void AddDocumentariesAtDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Documentaries documentaries = new Documentaries();


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
                        int countDoc = 0;

                        string directroryName = listDirectories[i].FullName;
                        DirectoryInfo secondDirectory = new DirectoryInfo(directroryName);
                        List<Documentaries> documentaries1 = new List<Documentaries>();

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
                            countDoc += 1;

                            documentaries.Name = listDirectories[i].Name;
                            documentaries.Path = item.FullName;
                            documentaries.Duration = DurationContent(pathToContent, item.ToString());
                            documentaries.NumOfSeries = filteredFileList.Count();
                            documentaries.Series = countDoc;


                            // db.Documentarieses.Add(documentaries);
                            // db.SaveChanges();
                            documentaries1.Add(documentaries);
                            documentaries = new Documentaries();



                            viewModel.ValueProgressDownlaodingSeries += 1;

                            ProgressDownLoadingContentDocumentaries.Value += viewModel.ValueProgressDownlaodingSeries;

                        }
                        CountOfDocumentalTextBlock.Text = Convert.ToString(listDirectories.Length);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            await System.Threading.Tasks.Task.Yield();
        }*/

        // добавление образовательных
        public async void AddEducationalAtDB(string pathToContent)
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

                                // ProgressDownLoadingContent.Value += viewModel.ValueProgressDownlaodingSeries;
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
        /* public async void AddEntertainmentAtDB(string pathToContent)
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

                                 ProgressDownLoadingContentEntertainment.Value += viewModel.ValueProgressDownlaodingSeries;
                             }
                         }
                         CountOfEntertainmentTextBlock.Text = Convert.ToString(listDirectories.Length);

                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
             }
             await System.Threading.Tasks.Task.Yield();
         }*/

        // добавление фильмов
        public void AddFilmAtDB(string pathToContent)
        {

            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Film film = new Film();
            List<Film> Films = new List<Film>();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            IEnumerable<FileInfo> contentListMedia;

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                int countFilm = 0;
                try
                {
                    bool searchOpt = true;
                    contentListMedia = (IEnumerable<FileInfo>)GetedFileFromDirectory(firstDirectory, searchOpt);

                    StringNumberComparer comparer = new StringNumberComparer();
                    //MainWindowViewModel viewModel = new MainWindowViewModel();
                    foreach (FileInfo item in contentListMedia.OrderBy(f => f.Name, comparer))
                    {
                        countFilm += 1;

                        if (contentListMedia != null)
                        {
                            film.Name = item.Name;
                            film.Path = item.FullName;
                            film.Duration = DurationContent(pathToContent, item.ToString());
                            film.NumOfSeries = contentListMedia.Count();
                            film.Series += countFilm;

                            db.Films.Add(film);
                            db.SaveChanges();
                            film = new Film();
                            searchOpt = false;

                            viewModel.ValueProgressDownlaodingSeries += 1;

                            // ProgressDownLoadingContentFilm.Value += viewModel.ValueProgressDownlaodingSeries;
                        }
                    }

                    DirectoryInfo[] listInnerDirectories = firstDirectory.GetDirectories();
                    if (listInnerDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с сериалами, " +
                    "Скорее всего надо выбрать папку - Сериалы, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

                    for (int i = 0; i < listInnerDirectories.Length; i++)
                    {
                        countFilm = 0;
                        string directroryName = listInnerDirectories[i].FullName;
                        DirectoryInfo secondDirectory = new DirectoryInfo(directroryName);
                        searchOpt = true;
                        contentListMedia = (IEnumerable<FileInfo>)GetedFileFromDirectory(secondDirectory, searchOpt);

                        foreach (FileInfo item in contentListMedia.OrderBy(f => f.Name, comparer))
                        {
                            countFilm += 1;

                            if (contentListMedia != null)
                            {
                                film.Name = listInnerDirectories[i].Name;
                                film.Path = item.FullName;
                                film.Duration = DurationContent(pathToContent, item.ToString());
                                film.NumOfSeries = contentListMedia.Count();
                                film.Series += countFilm;

                                db.Films.Add(film);
                                db.SaveChanges();
                                film = new Film();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentFilm.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }

                        CountOfFilmTextBlock.Text = Convert.ToString(db.Films.Count());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }




        // добавление лекций
        public async void AddLectiontAtDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Lection lection = new Lection();
            List<Lection> Lections = new List<Lection>();
            MainWindowViewModel viewModel = new MainWindowViewModel();
            IEnumerable<FileInfo> contentListMedia;

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            //if (firstDirectory.Exists)
            //{
            //    try
            //    {
            //        DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
            //        if (listDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с лекциями, " +
            //        "Скорее всего надо выбрать папку - Лекции, а не папку с одним сериалом " +
            //        "ознакомьтесь пожалуйста с правилами добавления контента. ");

            //        for (int i = 0; i < listDirectories.Length; i++)
            //        {
            //            string directroryName = listDirectories[i].FullName;
            //            DirectoryInfo secondDirectory = new DirectoryInfo(directroryName);

            //            IEnumerable<FileInfo> allFileList = secondDirectory.GetFiles("*.*", SearchOption.AllDirectories);
            //            IEnumerable<FileSystemInfo> filteredFileList =
            //                from file in allFileList
            //                where file.Extension == ".avi" || file.Extension == ".mp4" || file.Extension == ".mp4" ||
            //                file.Extension == ".mkv" || file.Extension == ".m4v" || file.Extension == ".mov"
            //                select file;


            //            StringNumberComparer comparer = new StringNumberComparer();
            //            MainWindowViewModel viewModel = new MainWindowViewModel();

            //            foreach (FileInfo item in filteredFileList)
            //            {
            //                if (filteredFileList != null)
            //                {
            //                    lection.Name = listDirectories[i].Name;
            //                    lection.Path = item.FullName;
            //                    lection.Duration = DurationContent(pathToContent, item.ToString());
            //                    lection.NumOfSeries = filteredFileList.Count();
            //                    lection.Series += 1;

            //                    //добавдяю сериал в базу
            //                    db.Lections.Add(lection);
            //                    db.SaveChanges();
            //                    lection = new Lection();

            //                    viewModel.ValueProgressDownlaodingSeries += 1;
            //                    ProgressDownLoadingContentLection.Value += viewModel.ValueProgressDownlaodingSeries;
            //                }
            //            }
            //            CountOfLectionTextBlock.Text = Convert.ToString(listDirectories.Length);

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //await System.Threading.Tasks.Task.Yield();






            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                int countLection = 0;
                try
                {
                    bool searchOpt = true;
                    contentListMedia = (IEnumerable<FileInfo>)GetedFileFromDirectory(firstDirectory, searchOpt);

                    StringNumberComparer comparer = new StringNumberComparer();
                    //MainWindowViewModel viewModel = new MainWindowViewModel();
                    foreach (FileInfo item in contentListMedia.OrderBy(f => f.Name, comparer))
                    {
                        countLection += 1;

                        if (contentListMedia != null)
                        {
                            lection.Name = item.Name;
                            lection.Path = item.FullName;
                            lection.Duration = DurationContent(pathToContent, item.ToString());
                            lection.NumOfSeries = contentListMedia.Count();
                            lection.Series += countLection;

                            db.Lections.Add(lection);
                            db.SaveChanges();
                            lection = new Lection();
                            searchOpt = false;

                            viewModel.ValueProgressDownlaodingSeries += 1;
                            //ProgressDownLoadingContentFilm.Value += viewModel.ValueProgressDownlaodingSeries;
                        }
                    }

                    //TODO пересмотреть данный диалог
                    DirectoryInfo[] listInnerDirectories = firstDirectory.GetDirectories();
                    if (listInnerDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с сериалами, " +
                    "Скорее всего надо выбрать папку - Сериалы, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

                    for (int i = 0; i < listInnerDirectories.Length; i++)
                    {
                        countLection = 0;
                        string directroryName = listInnerDirectories[i].FullName;
                        DirectoryInfo secondDirectory = new DirectoryInfo(directroryName);
                        searchOpt = true;
                        contentListMedia = (IEnumerable<FileInfo>)GetedFileFromDirectory(secondDirectory, searchOpt);

                        foreach (FileInfo item in contentListMedia.OrderBy(f => f.Name, comparer))
                        {
                            countLection += 1;

                            if (contentListMedia != null)
                            {

                                lection.Name = item.Name;
                                lection.Description = listInnerDirectories[i].Name;
                                lection.Path = item.FullName;
                                lection.Duration = DurationContent(pathToContent, item.ToString());
                                lection.NumOfSeries = contentListMedia.Count();
                                lection.Series += countLection;

                                db.Lections.Add(lection);
                                db.SaveChanges();
                                lection = new Lection();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentLection.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }

                        CountOfLectionTextBlock.Text = Convert.ToString(db.Lections.Count());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                await System.Threading.Tasks.Task.Yield();
            }
        }

        // TODO для профилактических отображать колличество контента а не папок
        // добавление профилактических
        public async void AddPreventionAtDB(string pathToContent)
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

                                ProgressDownLoadingContentPrevent.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        CountOfPreventionlTextBlock.Text = Convert.ToString(listDirectories.Length);

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
        public async void AddSreiesAtDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Series series = new Series();

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

                                //seriesCollection.Id = series.Id;
                                //seriesCollection.Name = series.Name;
                                //seriesCollection.Path = listDirectories[i].FullName;
                                //seriesCollection.NumOfSeries = series.NumOfSeries;

                                db.Serieses.Add(series);
                                db.SaveChanges();
                                series = new Series();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentSeries.Value += viewModel.ValueProgressDownlaodingSeries;
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

        // добавление телепередач
        public async void AddTvShowAtDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            TvShow tvShow = new TvShow();

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
                                tvShow.Name = listDirectories[i].Name;
                                tvShow.Path = item.FullName;
                                tvShow.Duration = DurationContent(pathToContent, item.ToString());
                                tvShow.NumOfSeries = filteredFileList.Count();
                                tvShow.Series += 1;

                                db.TvShows.Add(tvShow);
                                db.SaveChanges();
                                tvShow = new TvShow();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentTvShow.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }

                        CountOfTvShowTextBlock.Text = Convert.ToString(listDirectories.Length);
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

        // получаю файлы из директорий
        private IEnumerable<FileSystemInfo> GetedFileFromDirectory(DirectoryInfo dir, bool searchOpt)
        {
            IEnumerable<FileInfo> allFileList = dir.GetFiles("*.*", searchOpt ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);
            IEnumerable<FileSystemInfo> filteredFileList =
                from file in allFileList
                where file.Extension == ".avi" || file.Extension == ".mp4" || file.Extension == ".mp4" ||
                file.Extension == ".mkv" || file.Extension == ".m4v" || file.Extension == ".mov"
                select file;

            return filteredFileList;
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
