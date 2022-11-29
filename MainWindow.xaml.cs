using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using MaterialDesignThemes.Wpf;
using Efir.Model;
using Efir.ViewModels;
using Efir.Data;
using System.Data.Entity;
using System.Collections.ObjectModel;
using DayOfWeek = Efir.Model.DayOfWeek;
using System.Text.Json;

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
        DayOfWeek dayOfWeek = new DayOfWeek();

        #region ПЕРЕМЕННЫЕ: блок эфир

        #region Модели дней недели для хранения и добавления в базу
        public ObservableCollection<DayOfWeek> EfirOfDayList = new ObservableCollection<DayOfWeek>();
        public ObservableCollection<EfirOnMonday> EfirOfMonday = new ObservableCollection<EfirOnMonday>();
        public ObservableCollection<EfirOnTuesday> EfirOfTuesday = new ObservableCollection<EfirOnTuesday>();
        public ObservableCollection<EfirOnWednesday> EfirOfWednesday = new ObservableCollection<EfirOnWednesday>();
        public ObservableCollection<EfirOnThursday> EfirOfThursday = new ObservableCollection<EfirOnThursday>();
        public ObservableCollection<EfirOnFriday> EfirOfFriday = new ObservableCollection<EfirOnFriday>();
        public ObservableCollection<EfirOnSaturday> EfirOfSaturday = new ObservableCollection<EfirOnSaturday>();
        public ObservableCollection<EfirOnSunday> EfirOfSunday = new ObservableCollection<EfirOnSunday>();
        #endregion

        #endregion


        #region ПЕРМЕННЫЕ: блок медиа
        private string pathToFilms = "";
        private string pathToSeries = "";
        private string pathToLection = "";
        private string pathToDocumentaries = "";
        private string pathToEntertainment = "";
        private string pathToPrevention = "";

        string CountFilm = "";
        #endregion



        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            /* db.Serieses.Load();
             db.Films.Load();
             //db.Documentarieses.Load();
             db.Educationals.Load();
             //db.Entertainments.Load();
             db.Preventions.Load();
             db.TvShows.Load();

             db.OnMonday.Load();*/

            //TODO отрефаткориить загрузку начальных данных. Изменить место хранения, и способ отбражения, но пока пойдет
            CountOfFilmTextBlock.Text = Convert.ToString(db?.Films.Count());
            //FilePathToFilmTextBox.Text = db?.Films.ToList()?[0].Path == null ? "" : db?.Films.ToList()[0].Path;


            // загружаем данные из БД
            // db.Serieses.Load();
            // и устанавливаем данные в качестве контекста
            //DataContext = db.Serieses.Local.ToObservableCollection();

            #region Установка источников данных для листов
            MainWindowViewModel model = new MainWindowViewModel();

            //Понедельник
            foreach (var item in db.OnMonday.ToList())
            {
                model.EventListSourceMonday.Add(item);
            }
            EfirListOnMonday.ItemsSource = model.EventListSourceMonday;

            //Вторник
            foreach (var item in db.OnTuesday.ToList())
            {
                model.EventListSourceTuesday.Add(item);
            }
            EfirListOnTuesday.ItemsSource = model.EventListSourceTuesday;

            //Среда
            foreach (var item in db.OnWednesday.ToList())
            {
                model.EventListSourceWednesday.Add(item);
            }
            EfirListOnWednesday.ItemsSource = model.EventListSourceWednesday;

            //четврег
            foreach (var item in db.OnThursday.ToList())
            {
                model.EventListSourceThursday.Add(item);
            }
            EfirListOnThursday.ItemsSource = model.EventListSourceThursday;
            #endregion
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


        // поиск элемента в дереве(например textblock в listview)
        /*public static T? FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
            {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
              {
              var child = VisualTreeHelper.GetChild(parent, i);
              string? controlName = child.GetValue(Control.NameProperty) as string;
              if (controlName == name)
              {
              return child as T;
              }
              else
              {
            T? result = FindVisualChildByName<T>(child, name);
                if (result != null)
                return result;
                }
                }
                return null;
                }*/

        // так же, получение ребенка из родительского дерева
        /*private static T? GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement

            {

            FrameworkElement? child = null;

            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++)

              {

              child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement;

              System.Diagnostics.Debug.WriteLine(child);

              if (child != null && child.GetType() == typeof(T))

              { break; }

              else if (child != null)

              {

            child = GetFrameworkElementByName<T>(child);

                if (child != null && child.GetType() == typeof(T))

                {

                break;

                }

                }

                }

                return child as T;

                }*/

        /* private void FindElement(object sender, RoutedEventArgs e)

        {

        // get the current selected item

        ListViewItem? item = Testing.ItemContainerGenerator.ContainerFromIndex(Testing.SelectedIndex) as ListViewItem;

        TextBlock textYear = null;

        if (item != null)

        {

        //get the item's template parent

        ContentPresenter templateParent = GetFrameworkElementByName<ContentPresenter>(item);

            //get the DataTemplate that TextBlock in.

            DataTemplate dataTemplate = listview.ItemTemplate;

            if (dataTemplate != null && templateParent != null)

            {

            textYear = dataTemplate.FindName("textYear", templateParent) as TextBlock;

            }

            if (textYear != null)

            {

            MessageBox.Show(String.Format("Current item's Year is:{0}", textYear.Text));

            }

            }



            }*/
        #endregion

        #region БЛОК ЭФИР

        #region Добавление события с учетом дня недели

        #region Профилактика
        private void AddPreventionAtList_Click(object sender, RoutedEventArgs e)
        {
            // TabItem? SelectedTab = TabOfDayWeek.SelectedItem as TabItem;

            var menuItem = sender as MenuItem;
            string eventName = (string)menuItem.Header;

            AddEventByEventName(eventName);
        }
        #endregion

        #region Телепередачи
        private void AddTvShowAtList_Click(object sender, RoutedEventArgs e)
        {
            //TabItem? SelectedTab = TabOfDayWeek.SelectedItem as TabItem;

            var menuItem = sender as MenuItem;
            string eventName = (string)menuItem.Header;
            /*Guid guid = Guid.NewGuid();
            string RandomId = guid.ToString();*/

            AddEventByEventName(eventName);

            /*if (SelectedTab?.Header?.ToString()?.ToLower() == "Понедельник".ToLower())
            {
            EfirOfMonday.Add(new EfirOnMonday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
            EfirListOnMonday.ItemsSource = EfirOfMonday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Вторник".ToLower())
            {
            EfirOfTuesday.Add(new EfirOnTuesday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
            EventListOnTuesday.ItemsSource = EfirOfTuesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Среда".ToLower())
            {
            EfirOfWednesday.Add(new EfirOnWednesday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
            EventListOnWednesday.ItemsSource = EfirOfWednesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Четверг".ToLower())
            {
            EfirOfThursday.Add(new EfirOnThursday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
            EventListOnThursday.ItemsSource = EfirOfThursday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Пятница".ToLower())
            {
            EfirOfFriday.Add(new EfirOnFriday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
            EventListOnFriday.ItemsSource = EfirOfFriday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Суббота".ToLower())
            {
            EfirOfSaturday.Add(new EfitOnSaturday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
            EventListOnSaturday.ItemsSource = EfirOfSaturday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Воскресение".ToLower())
            {
            EfirOfSunday.Add(new EfirOnSunday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
            EventListOnSunday.ItemsSource = EfirOfSunday;
            }*/
        }



        #endregion

        #region Сериалы
        private void AddSeriesAtList_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            string eventName = (string)menuItem.Header;

            AddEventByEventName(eventName);
        }
        #endregion

        #region Новости
        private void AddNewsAtList_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            string eventName = (string)menuItem.Header;

            AddEventByEventName(eventName);
        }
        #endregion

        #region Лекции
        private void AddLectionAtList_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            string eventName = (string)menuItem.Header;

            AddEventByEventName(eventName);
        }
        #endregion

        #region Перерыв
        private void AddBreakAtList_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            string eventName = (string)menuItem.Header;

            AddEventByEventName(eventName);
        }
        #endregion

        #region Фильмы
        private void AddFilmsAtList_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            string eventName = (string)menuItem.Header;

            AddEventByEventName(eventName);
        }
        #endregion
        /// <summary>
        /// Метод добавления события по дням недели
        /// </summary>
        private void AddEventByEventName(string eventName)
        {
            TabItem? SelectedTab = TabOfDayWeek.SelectedItem as TabItem;
            MainWindowViewModel model = new MainWindowViewModel();
            /*var menuItem = sender as MenuItem;
            string eventName = (string)menuItem.Header;*/
            /* Guid guid = Guid.NewGuid();
            string RandomId = guid.ToString();*/

            if (SelectedTab?.Header?.ToString()?.ToLower() == "Понедельник".ToLower())
            {

                EfirOnMonday efir = new EfirOnMonday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                db.OnMonday.Add(efir);
                db.SaveChanges();
                foreach (var item in db.OnMonday.ToList())
                {
                    model.EventListSourceMonday.Add(item);
                }
                EfirListOnMonday.ItemsSource = model.EventListSourceMonday;

            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Вторник".ToLower())
            {
                //EfirOfTuesday.Add(new EfirOnTuesday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
                //EfirListOnTuesday.ItemsSource = EfirOfTuesday;

                EfirOnTuesday efir = new EfirOnTuesday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                db.OnTuesday.Add(efir);
                db.SaveChanges();
                foreach (var item in db.OnTuesday.ToList())
                {
                    model.EventListSourceTuesday.Add(item);
                }
                EfirListOnTuesday.ItemsSource = model.EventListSourceTuesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Среда".ToLower())
            {
                //EfirOfWednesday.Add(new EfirOnWednesday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
                //EfirListOnWednesday.ItemsSource = EfirOfWednesday;

                EfirOnWednesday efir = new EfirOnWednesday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                db.OnWednesday.Add(efir);
                db.SaveChanges();
                foreach (var item in db.OnWednesday.ToList())
                {
                    model.EventListSourceWednesday.Add(item);
                }
                EfirListOnWednesday.ItemsSource = model.EventListSourceWednesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Четверг".ToLower())
            {
                //EfirOfThursday.Add(new EfirOnThursday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
                //EfirListOnThursday.ItemsSource = EfirOfThursday;

                EfirOnThursday efir = new EfirOnThursday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                db.OnThursday.Add(efir);
                db.SaveChanges();
                foreach (var item in db.OnThursday.ToList())
                {
                    model.EventListSourceThursday.Add(item);
                }
                EfirListOnThursday.ItemsSource = model.EventListSourceThursday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Пятница".ToLower())
            {
                EfirOfFriday.Add(new EfirOnFriday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
                EfirListOnFriday.ItemsSource = EfirOfFriday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Суббота".ToLower())
            {
                EfirOfSaturday.Add(new EfirOnSaturday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
                EfirtListOnSaturday.ItemsSource = EfirOfSaturday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Воскресение".ToLower())
            {
                EfirOfSunday.Add(new EfirOnSunday { EventName = eventName, TimeToEfir = new TimeSpan(0, 0, 0) });
                EfirtListOnSunday.ItemsSource = EfirOfSunday;
            }
        }

        #endregion




        #region Удаление события с учетом дня недели
        private void RemoveEvent_Click(object sender, RoutedEventArgs e)
        {

            TabItem? SelectedTab = TabOfDayWeek.SelectedItem as TabItem;

            if (SelectedTab?.Header?.ToString()?.ToLower() == "Понедельник".ToLower())
            {
                MainWindowViewModel model = new MainWindowViewModel();

                var selectedItem = EfirListOnMonday.SelectedItem as EfirOnMonday;
                EfirOfMonday.Remove(selectedItem);

                var itemInBase = db.OnMonday.ToList().Find(r => r.Id == selectedItem.Id);
                db.OnMonday.Remove(itemInBase);
                db.SaveChanges();

                //TODO рефактор этой функции. сделать из нее полноценный рефреш
                foreach (var item in db.OnMonday.ToList())
                {
                    model.EventListSourceMonday.Add(item);
                }

                EfirListOnMonday.ItemsSource = model.EventListSourceMonday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Вторник".ToLower())
            {
                var removableItem = EfirListOnTuesday.SelectedItem as EfirOnTuesday;
                EfirOfTuesday.Remove(removableItem);
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Среда".ToLower())
            {
                var removableItem = EfirListOnWednesday.SelectedItem as EfirOnWednesday;
                EfirOfWednesday.Remove(removableItem);
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Четверг".ToLower())
            {
                var removableItem = EfirListOnThursday.SelectedItem as EfirOnThursday;
                EfirOfThursday.Remove(removableItem);
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Пятница".ToLower())
            {
                var removableItem = EfirListOnFriday.SelectedItem as EfirOnFriday;
                EfirOfFriday.Remove(removableItem);
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Суббота".ToLower())
            {
                var removableItem = EfirtListOnSaturday.SelectedItem as EfirOnSaturday;
                EfirOfSaturday.Remove(removableItem);
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Воскресение".ToLower())
            {
                var removableItem = EfirtListOnSunday.SelectedItem as EfirOnSunday;
                EfirOfSunday.Remove(removableItem);
            }
        }
        #endregion

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
                    AddTvShowAtDB(pathToPrevention);
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
            IEnumerable<FileInfo> contentListMedia;
            //List<Educational> Ed = new List<Documentaries>();

            //TODO сделать проверку, если в папке не видео файл или еще что - сделать что-то
            if (firstDirectory.Exists)
            {
                int counPrevention = 0;
                try
                {
                    bool searchOpt = true;
                    contentListMedia = (IEnumerable<FileInfo>)GetedFileFromDirectory(firstDirectory, searchOpt);

                    StringNumberComparer comparer = new StringNumberComparer();
                    MainWindowViewModel viewModel = new MainWindowViewModel();

                    foreach (FileInfo item in contentListMedia.OrderBy(f => f.Name, comparer))
                    {
                        counPrevention += 1;

                        if (contentListMedia != null)
                        {
                            prevention.Name = item.Name;
                            prevention.Path = item.FullName;
                            prevention.Duration = DurationContent(pathToContent, item.ToString());
                            prevention.NumOfSeries = contentListMedia.Count();
                            prevention.Series += counPrevention;

                            db.Preventions.Add(prevention);
                            db.SaveChanges();
                            prevention = new Prevention();
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
                        counPrevention = 0;
                        string directroryName = listInnerDirectories[i].FullName;
                        DirectoryInfo secondDirectory = new DirectoryInfo(directroryName);
                        searchOpt = true;
                        contentListMedia = (IEnumerable<FileInfo>)GetedFileFromDirectory(secondDirectory, searchOpt);

                        foreach (FileInfo item in contentListMedia.OrderBy(f => f.Name, comparer))
                        {
                            counPrevention += 1;

                            if (contentListMedia != null)
                            {

                                prevention.Name = item.Name;
                                prevention.Description = listInnerDirectories[i].Name;
                                prevention.Path = item.FullName;
                                prevention.Duration = DurationContent(pathToContent, item.ToString());
                                prevention.NumOfSeries = contentListMedia.Count();
                                prevention.Series += counPrevention;

                                db.Preventions.Add(prevention);
                                db.SaveChanges();
                                prevention = new Prevention();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentLection.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }

                        CountOfPreventionlTextBlock.Text = Convert.ToString(db.Preventions.Count());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                await System.Threading.Tasks.Task.Yield();
            }
            await System.Threading.Tasks.Task.Yield();
        }

        // добавление сериалов
        public async void AddSreiesAtDB(string pathToContent)
        {
            DirectoryInfo firstDirectory = new DirectoryInfo(pathToContent);
            Series series = new Series();
            IEnumerable<FileInfo> contentListMedia;

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

                        bool searchOpt = false;
                        contentListMedia = (IEnumerable<FileInfo>)GetedFileFromDirectory(secondDirectory, searchOpt);
                        /*IEnumerable<FileInfo> allFileList = secondDirectory.GetFiles("*.*", SearchOption.AllDirectories);
                            IEnumerable<FileSystemInfo> filteredFileList =
                                from file in allFileList
                                where file.Extension == ".avi" || file.Extension == ".mp4" || file.Extension == ".mp4" ||
                                file.Extension == ".mkv" || file.Extension == ".m4v" || file.Extension == ".mov"
                                select file;*/


                        StringNumberComparer comparer = new StringNumberComparer();
                        MainWindowViewModel viewModel = new MainWindowViewModel();
                        foreach (FileInfo item in contentListMedia.OrderBy(f => f.Name, comparer))
                        {
                            if (contentListMedia != null)
                            {
                                series.Name = listDirectories[i].Name;
                                series.Path = item.FullName;
                                series.Duration = DurationContent(pathToContent, item.ToString());
                                series.NumOfSeries = contentListMedia.Count();
                                series.IsSeries += 1;

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
                int countTvShow = 0;
                try
                {
                    DirectoryInfo[] listDirectories = firstDirectory.GetDirectories();
                    if (listDirectories.Length == 0) MessageBox.Show("Скорее всего вы выбрали папку в которой нет подпапок с сериалами, " +
                    "Скорее всего надо выбрать папку - Сериалы, а не папку с одним сериалом " +
                    "ознакомьтесь пожалуйста с правилами добавления контента. ");

                    for (int i = 0; i < listDirectories.Length; i++)
                    {
                        countTvShow = 0;
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
                            countTvShow += 1;
                            if (filteredFileList != null)
                            {
                                tvShow.Name = listDirectories[i].Name;
                                tvShow.Path = item.FullName;
                                tvShow.Duration = DurationContent(pathToContent, item.ToString());
                                tvShow.NumOfSeries = filteredFileList.Count();
                                tvShow.Series = countTvShow;

                                db.TvShows.Add(tvShow);
                                db.SaveChanges();
                                tvShow = new TvShow();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentTvShow.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }

                        CountOfTvShowTextBlock.Text = Convert.ToString(db.Preventions.Count());
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
