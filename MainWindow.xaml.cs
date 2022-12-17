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
using System.Windows.Documents;
using Word = Microsoft.Office.Interop.Word;
using System.Globalization;
using System.Reflection;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;



namespace Efir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IAsyncDisposable
    {
        //TODO Профиксить добавление контента на стадии сбора данных, есть поврежденные файлы, и программа крашится если не может их открыть. 
        //TODO надо сделать проверку, и пропускать битые файлы, а в конце показывать их пользователю, чтобы разобрался с проблемой или удалил. показывать можно в текстовом файле

        //TODO Профиксить отображение путей в медиа, сейчас отображется полный путь до первого файла. Нужно указывать только директорию
        //TODO Сделать рефреш эфирной сетки по времени по кнопке - схранить эфир или по другому событию
        //xTODO Сделать массовое удаление событий в эфире, типо отчистить или что то еще
        //TODO Сделать проверку на наличие контента в базе, перез созданием эфира, и сделать записб в текстовый файл если по некоторым путям контент отсутствует
        //TODO Сделать удаление контента из базы если нажата кнопка выбора контента(отчистка моделей), чтобы не догружалось, а с нуля грузилось, хотя может есть сммысл оставить, чтобы просто догружалось
        //TODO Профиксить отчистку всех моделей в базе, на занчение null  в одном из полей(бывает в одно из полей записывается NULL и при старте программы выкидывает ошибку, для пользователя это краш программы. удалять приходится в ручную из базы)
        //xTODO Сделать заполнение событий по понедельнику, если другие не трогались(зафиксировать эвент, что менялись, значит кастом)
        //xTODO Доделать поиск и добавление контента по дням неделям, но после того как сделаю пункт выше.
        //xTODO Сделать отчистку эфира по дням недели перед созданием нового эфира(просто обнуление)
        //TODO Сделать сохранение листа по евенту добавления item в list (если есть такой евент) сейчас сохраняется по кнопке - Создать
        //xTODO  Добавить события Начало трансляции и Конец трансляции (обязательные поля)
        //xTODO Добавить модели для создания эфира по остальным дням

        //xTODO подумать над тем что решением проблемы с определением что будет именем файла в базе, имя папки или имя самого файла, может быть писать одно в Name другое в Description, а пользователь потом это сможет поменять поменяв местами поля в списках
        //xTODO запуск программы посередине окна
        //TODO сделать чтобы колличество добавляемых элементов показывалось в рантайме а не по факту добавленного
        //TODO поработать надо высвобождением ресурсов, слишком много по памяти жрет

        //ApplicationContext db = new ApplicationContext();
        //DayOfWeek dayOfWeek = new DayOfWeek();

        #region ПЕРЕМЕННЫЕ: блок эфир
        #endregion


        #region ПЕРМЕННЫЕ: блок медиа
        private string pathToFilms = "";
        private string pathToSeries = "";
        private string pathToLection = "";
        private string pathToDocumentaries = "";
        private string pathToEntertainment = "";
        private string pathToPrevention = "";
        private string pathToEducationals = "";

        string CountFilm = "";
        #endregion



        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            // new GeneratedCode.GeneratedClass().CreatePackage(@"D:\Temp\Output.docx");

            // гарантируем, что база данных создана
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Database.EnsureCreated();
            }

            /* db.Serieses.Load();
            db.Films.Load();
            //db.Documentarieses.Load();
            db.Educationals.Load();
            //db.Entertainments.Load();
            db.Preventions.Load();
            db.TvShows.Load();

            db.OnMonday.Load();*/



            // загружаем данные из БД
            // db.Serieses.Load();
            // и устанавливаем данные в качестве контекста
            //DataContext = db.Serieses.Local.ToObservableCollection();

            #region Установка источников данных для отображения колличества контента в категории медиа
            //TODO отрефаткориить загрузку начальных данных. Изменить место хранения, и способ отбражения, но пока пойдет
            using (ApplicationContext context = new ApplicationContext())
            {
                CountOfFilmTextBlock.Text = Convert.ToString(context?.Films.Count());
            }

            #endregion

            #region Установка данных для отображения путей контента и его колличества
            using (ApplicationContext context = new ApplicationContext())
            {
                //TODO Профиксить отображение путей, убрать лишнее
                if (context.Lections.Count() != 0)
                {
                    FilePathToEducationalsTextBox.Text = context.Educationals.ToList()[0].Path;
                    CountOfEducationalsTextBlock.Text = context.Educationals.Count().ToString();
                }

                if (context.Lections.Count() != 0)
                {
                    FilePathToLectionTextBox.Text = context.Lections.ToList()[0].Path;
                    CountOfLectionTextBlock.Text = context.Lections.Count().ToString();
                }

                if (context.Films.Count() != 0)
                {
                    FilePathToFilmTextBox.Text = context.Films.First().Path;
                    CountOfFilmTextBlock.Text = context.Films.Count().ToString();
                }

                if (context.Serieses.Count() != 0)
                {
                    string seriesName = "";
                    int seriesCount = 0;
                    foreach (var item in context.Serieses.ToList())
                    {

                        if (seriesName != item.Name)
                        {
                            seriesCount += 1;
                        }
                        seriesName = item.Name;
                    }

                    FilePathToSeriesTextBox.Text = context.Serieses.First().Path;
                    CountOfSeriesTextBlock.Text = seriesCount.ToString();
                }

                if (context.Preventions.Count() != 0)
                {
                    FilePathToPreventionTextBox.Text = context.Preventions.First().Path;
                    CountOfPreventionlTextBlock.Text = context.Preventions.Count().ToString();
                }

                if (context.TvShows.Count() != 0)
                {
                    FilePathToTvShowTextBox.Text = context.TvShows.First().Path;
                    CountOfTvShowTextBlock.Text = context.TvShows.Count().ToString();
                }

                if (context.LectionGraphs.Count() != 0)
                {
                    FilePathToLectionDocTextBox.Text = context.LectionGraphs.First().Path;
                }
            }
            #endregion

            #region Установка источников данных для евентов по дням недели
            //TODO Доделать сортировку отображаемых данных для всех дней
            //TODO Убрать вызов объекта из общего в каждый юзинг
            MainWindowViewModel model = new MainWindowViewModel();

            //Понедельник
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsMonday = context?.OnMonday.ToList();
                var sortedListEventsByTimeMonday = listEventsMonday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeMonday == null) return;
                foreach (var item in sortedListEventsByTimeMonday)
                {
                    model.EventListSourceMonday.Add(item);
                }
                EfirListOnMonday.ItemsSource = model.EventListSourceMonday;
            }


            // Вторник
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsTuesday = context?.OnTuesday.ToList();
                var sortedListEventsByTimeTuesday = listEventsTuesday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeTuesday == null) return;
                foreach (var item in sortedListEventsByTimeTuesday)
                {
                    model.EventListSourceTuesday.Add(item);
                }
                EfirListOnTuesday.ItemsSource = model.EventListSourceTuesday;
            }

            //Среда
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsWednesday = context?.OnWednesday.ToList();
                var sortedListEventsByTimeWednesday = listEventsWednesday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeWednesday == null) return;
                foreach (var item in sortedListEventsByTimeWednesday)
                {
                    model.EventListSourceWednesday.Add(item);
                }
                EfirListOnWednesday.ItemsSource = model.EventListSourceWednesday;
            }

            //четврег
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsThursday = context?.OnThursday.ToList();
                var sortedListEventsByTimeThursday = listEventsThursday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeThursday == null) return;
                foreach (var item in sortedListEventsByTimeThursday)
                {
                    model.EventListSourceThursday.Add(item);
                }
                EfirListOnThursday.ItemsSource = model.EventListSourceThursday;
            }

            //Пятница
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsFriday = context?.OnFriday.ToList();
                var sortedListEventsByTimeFriday = listEventsFriday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeFriday == null) return;
                foreach (var item in sortedListEventsByTimeFriday)
                {
                    model.EventListSourceFriday.Add(item);
                }
                EfirListOnFriday.ItemsSource = model.EventListSourceFriday;
            }

            //Суббота
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsSaturday = context?.OnSaturday.ToList();
                var sortedListEventsByTimeSaturday = listEventsSaturday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeSaturday == null) return;
                foreach (var item in sortedListEventsByTimeSaturday)
                {
                    model.EventListSourceSaturday.Add(item);
                }
                EfirtListOnSaturday.ItemsSource = model.EventListSourceSaturday;
            }

            //Воскресение
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsSunday = context?.OnSunday.ToList();
                var sortedListEventsByTimeSunday = listEventsSunday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeSunday == null) return;
                foreach (var item in sortedListEventsByTimeSunday)
                {
                    model.EventListSourceSunday.Add(item);
                }
                EfirtListOnSunday.ItemsSource = model.EventListSourceSunday;
            }
            #endregion
        }

        #region ПАРСИНГ ДОКУМЕНТА С ГРАФИКОМ ЛЕКЦИЙ
        private void ParsingDoc_Click(object sender, RoutedEventArgs e)
        {
            ParseBase();
        }

        public static void ParseBase()
        {
            MainWindowViewModel model = new MainWindowViewModel();
            List<LectionGraph> lectionGraphs = new List<LectionGraph>();
            LectionGraph lection = new LectionGraph();
            var wordApp = new Word.Application();
            wordApp.Visible = false;

            try
            {
                var wordBaza = wordApp.Documents.Open(@"Z:\Programming\ProjectC#\Efir\lection.docx");
                var contentBaza = wordBaza.Content;
                string stringBaza = contentBaza.Text;
                string[] parsBaza = stringBaza.Split('\a');


                using (ApplicationContext context = new ApplicationContext())
                {
                    // отчищаю модель в базу
                    foreach (var item in context.LectionGraphs.ToList())
                    {
                        context.LectionGraphs.Remove(item);
                    }

                    for (int i = 0; i < parsBaza.Length; i++)
                    {
                        if (parsBaza[i].Contains("Лекция на тему"))
                        {
                            Guid guid = Guid.NewGuid();
                            string RandomId = guid.ToString();

                            lection.Id = RandomId;
                            lection.Name = parsBaza[i].Replace("\r", "");
                            lection.Lecturer = parsBaza[i + 2].Replace("\r", "");
                            lection.LectionDate = Convert.ToDateTime(parsBaza[i + 3].Replace("\r", ""));
                            lection.Path = @"Z:\Programming\ProjectC#\Efir\lection.docx";

                            lectionGraphs.Add(lection);
                            context.LectionGraphs.Add(lection);
                            context.SaveChanges();

                        }

                    }
                    wordBaza.Close();
                    wordApp.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка " + ex.Message);
            }
        }
        #endregion

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

        #region Начало трансляции


        #endregion


        // метод добавления события в лист событий
        private void AddEventAtList(object sender)
        {
            MenuItem? menuItem = sender as MenuItem;
            if (menuItem == null) return;
            string eventName = (string)menuItem.Header;

            AddEventByEventName(eventName);
        }

        #region Образование
        private void AddEducationalsAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }
        #endregion

        #region Профилактика
        private void AddPreventionAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }
        #endregion

        #region Телепередачи
        private void AddTvShowAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }
        #endregion

        #region Сериалы
        private void AddSeriesAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }
        #endregion

        #region Новости
        private void AddNewsAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }
        #endregion

        #region Лекции
        private void AddLectionAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }
        #endregion

        #region Перерыв
        private void AddBreakAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }
        #endregion

        #region Фильмы
        private void AddFilmsAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }
        #endregion

        #region Конец трансляции

        private void AddEndEfirAtList_Click(object sender, RoutedEventArgs e)
        {
            AddEventAtList(sender);
        }

        #endregion

        /// <summary>
        /// Метод добавления события по дням недели
        /// </summary>
        private void AddEventByEventName(string eventName)
        {
            TabItem? SelectedTab = TabOfDayWeek.SelectedItem as TabItem;
            MainWindowViewModel model = new MainWindowViewModel();


            if (SelectedTab?.Header?.ToString()?.ToLower() == "Понедельник".ToLower())
            {
                EfirOnMonday efir = new EfirOnMonday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                using (ApplicationContext context = new ApplicationContext())
                {
                    context.OnMonday.Add(efir);
                    context.SaveChanges();
                    foreach (var item in context.OnMonday.ToList())
                    {
                        model.EventListSourceMonday.Add(item);
                    }
                }
                EfirListOnMonday.ItemsSource = model.EventListSourceMonday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Вторник".ToLower())
            {
                EfirOnTuesday efir = new EfirOnTuesday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                using (ApplicationContext context = new ApplicationContext())
                {
                    context.OnTuesday.Add(efir);
                    context.SaveChanges();
                    foreach (var item in context.OnTuesday.ToList())
                    {
                        model.EventListSourceTuesday.Add(item);
                    }
                }

                EfirListOnTuesday.ItemsSource = model.EventListSourceTuesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Среда".ToLower())
            {
                EfirOnWednesday efir = new EfirOnWednesday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                using (ApplicationContext context = new ApplicationContext())
                {
                    context.OnWednesday.Add(efir);
                    context.SaveChanges();
                    foreach (var item in context.OnWednesday.ToList())
                    {
                        model.EventListSourceWednesday.Add(item);
                    }
                }

                EfirListOnWednesday.ItemsSource = model.EventListSourceWednesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Четверг".ToLower())
            {
                EfirOnThursday efir = new EfirOnThursday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                using (ApplicationContext context = new ApplicationContext())
                {
                    context.OnThursday.Add(efir);
                    context.SaveChanges();
                    foreach (var item in context.OnThursday.ToList())
                    {
                        model.EventListSourceThursday.Add(item);
                    }
                }

                EfirListOnThursday.ItemsSource = model.EventListSourceThursday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Пятница".ToLower())
            {
                EfirOnFriday efir = new EfirOnFriday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                using (ApplicationContext context = new ApplicationContext())
                {
                    context.OnFriday.Add(efir);
                    context.SaveChanges();
                    foreach (var item in context.OnFriday.ToList())
                    {
                        model.EventListSourceFriday.Add(item);
                    }
                }

                EfirListOnFriday.ItemsSource = model.EventListSourceFriday;

            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Суббота".ToLower())
            {
                EfirOnSaturday efir = new EfirOnSaturday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                using (ApplicationContext context = new ApplicationContext())
                {
                    context.OnSaturday.Add(efir);
                    context.SaveChanges();
                    foreach (var item in context.OnSaturday.ToList())
                    {
                        model.EventListSourceSaturday.Add(item);
                    }
                }

                EfirtListOnSaturday.ItemsSource = model.EventListSourceSaturday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Воскресение".ToLower())
            {
                EfirOnSunday efir = new EfirOnSunday();
                efir.EventName = eventName;
                efir.TimeToEfir = new TimeSpan(0, 0, 0);

                using (ApplicationContext context = new ApplicationContext())
                {
                    context.OnSunday.Add(efir);
                    context.SaveChanges();
                    foreach (var item in context.OnSunday.ToList())
                    {
                        model.EventListSourceSunday.Add(item);
                    }
                }

                EfirtListOnSunday.ItemsSource = model.EventListSourceSunday;
            }
        }

        #endregion

        #region Удаление события с учетом дня недели
        private void RemoveEvent_Click(object sender, RoutedEventArgs e)
        {
            TabItem? SelectedTab = TabOfDayWeek.SelectedItem as TabItem;
            MainWindowViewModel model = new MainWindowViewModel();

            if (SelectedTab?.Header?.ToString()?.ToLower() == "Понедельник".ToLower())
            {
                // var selectedItem = EfirListOnMonday.SelectedItem as EfirOnMonday;
                var selectedItems = EfirListOnMonday.SelectedItems;

                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var item in selectedItems)
                    {
                        var selectedItem = item as EfirOnMonday;

                        var itemInBase = context.OnMonday.ToList().Find(r => r.Id == selectedItem?.Id);

                        if (itemInBase != null) context.OnMonday.Remove(itemInBase);
                    }

                    context.SaveChanges();

                    //TODO рефактор этой функции. сделать из нее полноценный рефреш
                    foreach (var item in context.OnMonday.ToList())
                    {
                        model.EventListSourceMonday.Add(item);
                    }
                }
                EfirListOnMonday.ItemsSource = model.EventListSourceMonday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Вторник".ToLower())
            {
                var selectedItems = EfirListOnTuesday.SelectedItems;

                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var item in selectedItems)
                    {
                        var selectedItem = item as EfirOnTuesday;

                        var itemInBase = context.OnTuesday.ToList().Find(r => r.Id == selectedItem?.Id);

                        if (itemInBase != null) context.OnTuesday.Remove(itemInBase);
                    }
                    context.SaveChanges();

                    foreach (var item in context.OnTuesday.ToList())
                    {
                        model.EventListSourceTuesday.Add(item);
                    }
                }

                EfirListOnTuesday.ItemsSource = model.EventListSourceTuesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Среда".ToLower())
            {
                var selectedItems = EfirListOnWednesday.SelectedItems;

                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var item in selectedItems)
                    {
                        var selectedItem = item as EfirOnWednesday;

                        var itemInBase = context.OnWednesday.ToList().Find(r => r.Id == selectedItem?.Id);

                        if (itemInBase != null) context.OnWednesday.Remove(itemInBase);
                    }

                    context.SaveChanges();

                    foreach (var item in context.OnWednesday.ToList())
                    {
                        model.EventListSourceWednesday.Add(item);
                    }
                }
                EfirListOnWednesday.ItemsSource = model.EventListSourceWednesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Четверг".ToLower())
            {
                var selectedItems = EfirListOnThursday.SelectedItems;

                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var item in selectedItems)
                    {
                        var selectedItem = item as EfirOnThursday;

                        var itemInBase = context.OnThursday.ToList().Find(r => r.Id == selectedItem?.Id);

                        if (itemInBase != null) context.OnThursday.Remove(itemInBase);
                    }

                    context.SaveChanges();

                    foreach (var item in context.OnThursday.ToList())
                    {
                        model.EventListSourceThursday.Add(item);
                    }
                }
                EfirListOnThursday.ItemsSource = model.EventListSourceThursday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Пятница".ToLower())
            {
                var selectedItems = EfirListOnFriday.SelectedItems;

                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var item in selectedItems)
                    {
                        var selectedItem = item as EfirOnFriday;

                        var itemInBase = context.OnFriday.ToList().Find(r => r.Id == selectedItem?.Id);

                        if (itemInBase != null) context.OnFriday.Remove(itemInBase);
                    }
                    context.SaveChanges();

                    foreach (var item in context.OnFriday.ToList())
                    {
                        model.EventListSourceFriday.Add(item);
                    }
                }
                EfirListOnFriday.ItemsSource = model.EventListSourceFriday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Суббота".ToLower())
            {
                var selectedItems = EfirtListOnSaturday.SelectedItems;

                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var item in selectedItems)
                    {
                        var selectedItem = item as EfirOnSaturday;

                        var itemInBase = context.OnSaturday.ToList().Find(r => r.Id == selectedItem?.Id);

                        if (itemInBase != null) context.OnSaturday.Remove(itemInBase);
                    }

                    context.SaveChanges();

                    foreach (var item in context.OnSaturday.ToList())
                    {
                        model.EventListSourceSaturday.Add(item);
                    }
                }


                EfirtListOnSaturday.ItemsSource = model.EventListSourceSaturday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Воскресение".ToLower())
            {
                var selectedItems = EfirtListOnSunday.SelectedItems;

                using (ApplicationContext context = new ApplicationContext())
                {
                    foreach (var item in selectedItems)
                    {
                        var selectedItem = item as EfirOnSunday;

                        var itemInBase = context.OnSunday.ToList().Find(r => r.Id == selectedItem?.Id);

                        if (itemInBase != null) context.OnSunday.Remove(itemInBase);
                    }
                    context.SaveChanges();

                    foreach (var item in context.OnSunday.ToList())
                    {
                        model.EventListSourceSunday.Add(item);
                    }
                }
                EfirtListOnSunday.ItemsSource = model.EventListSourceSunday;
            }
        }
        #endregion

        #region Изменение времени и запись в базу данных
        private void ChangeTimeEvent(object sender, RoutedEventArgs e)
        {
            TimePicker? userTime = sender as TimePicker;

            SetNewTimeEvent(userTime);
        }


        private void SetNewTimeEvent(TimePicker userTime)
        {
            TabItem? SelectedTab = TabOfDayWeek.SelectedItem as TabItem;
            MainWindowViewModel model = new MainWindowViewModel();

            if (SelectedTab?.Header?.ToString()?.ToLower() == "Понедельник".ToLower())
            {
                EfirOnMonday? selectedItem = EfirListOnMonday.SelectedItem as EfirOnMonday;

                using (ApplicationContext context = new ApplicationContext())
                {
                    var itemInBase = context.OnMonday.ToList().Find(match: r => r.Id == selectedItem?.Id);

                    if (userTime.SelectedTime == null) return;
                    var convertedTime = userTime.SelectedTime.Value.TimeOfDay;

                    if (itemInBase == null) return;
                    itemInBase.TimeToEfir = convertedTime;

                    context.SaveChanges();

                    foreach (var item in context.OnMonday.ToList())
                    {
                        model.EventListSourceMonday.Add(item);
                    }
                }


                EfirListOnMonday.ItemsSource = model.EventListSourceMonday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Вторник".ToLower())
            {
                EfirOnTuesday? selectedItem = EfirListOnTuesday.SelectedItem as EfirOnTuesday;

                using (ApplicationContext context = new ApplicationContext())
                {
                    var itemInBase = context.OnTuesday.ToList().Find(match: r => r.Id == selectedItem?.Id);

                    if (userTime.SelectedTime == null) return;
                    var convertedTime = userTime.SelectedTime.Value.TimeOfDay;

                    if (itemInBase == null) return;
                    itemInBase.TimeToEfir = convertedTime;

                    context.SaveChanges();

                    foreach (var item in context.OnTuesday.ToList())
                    {
                        model.EventListSourceTuesday.Add(item);
                    }
                }


                EfirListOnTuesday.ItemsSource = model.EventListSourceTuesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Среда".ToLower())
            {
                EfirOnWednesday? selectedItem = EfirListOnWednesday.SelectedItem as EfirOnWednesday;

                using (ApplicationContext context = new ApplicationContext())
                {
                    var itemInBase = context.OnWednesday.ToList().Find(match: r => r.Id == selectedItem?.Id);

                    if (userTime.SelectedTime == null) return;
                    var convertedTime = userTime.SelectedTime.Value.TimeOfDay;

                    if (itemInBase == null) return;
                    itemInBase.TimeToEfir = convertedTime;

                    context.SaveChanges();

                    foreach (var item in context.OnWednesday.ToList())
                    {
                        model.EventListSourceWednesday.Add(item);
                    }
                }


                EfirListOnWednesday.ItemsSource = model.EventListSourceWednesday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Четверг".ToLower())
            {
                EfirOnThursday? selectedItem = EfirListOnThursday.SelectedItem as EfirOnThursday;

                using (ApplicationContext context = new ApplicationContext())
                {
                    var itemInBase = context.OnThursday.ToList().Find(match: r => r.Id == selectedItem?.Id);

                    if (userTime.SelectedTime == null) return;
                    var convertedTime = userTime.SelectedTime.Value.TimeOfDay;


                    if (itemInBase == null) return;
                    itemInBase.TimeToEfir = convertedTime;

                    context.SaveChanges();

                    foreach (var item in context.OnThursday.ToList())
                    {
                        model.EventListSourceThursday.Add(item);
                    }

                }

                EfirListOnThursday.ItemsSource = model.EventListSourceThursday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Пятница".ToLower())
            {
                EfirOnFriday? selectedItem = EfirListOnFriday.SelectedItem as EfirOnFriday;

                using (ApplicationContext context = new ApplicationContext())
                {
                    var itemInBase = context.OnFriday.ToList().Find(match: r => r.Id == selectedItem?.Id);

                    if (userTime.SelectedTime == null) return;
                    var convertedTime = userTime.SelectedTime.Value.TimeOfDay;

                    if (itemInBase == null) return;
                    itemInBase.TimeToEfir = convertedTime;

                    context.SaveChanges();

                    foreach (var item in context.OnFriday.ToList())
                    {
                        model.EventListSourceFriday.Add(item);
                    }
                }


                EfirListOnFriday.ItemsSource = model.EventListSourceFriday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Суббота".ToLower())
            {
                EfirOnSaturday? selectedItem = EfirtListOnSaturday.SelectedItem as EfirOnSaturday;

                using (ApplicationContext context = new ApplicationContext())
                {
                    var itemInBase = context.OnSaturday.ToList().Find(match: r => r.Id == selectedItem?.Id);

                    if (userTime.SelectedTime == null) return;
                    var convertedTime = userTime.SelectedTime.Value.TimeOfDay;

                    if (itemInBase == null) return;
                    itemInBase.TimeToEfir = convertedTime;

                    context.SaveChanges();

                    foreach (var item in context.OnSaturday.ToList())
                    {
                        model.EventListSourceSaturday.Add(item);
                    }
                }


                EfirtListOnSaturday.ItemsSource = model.EventListSourceSaturday;
            }
            if (SelectedTab?.Header?.ToString()?.ToLower() == "Воскресение".ToLower())
            {
                EfirOnSunday? selectedItem = EfirtListOnSunday.SelectedItem as EfirOnSunday;

                using (ApplicationContext context = new ApplicationContext())
                {
                    var itemInBase = context.OnSunday.ToList().Find(match: r => r.Id == selectedItem?.Id);

                    if (userTime.SelectedTime == null) return;
                    var convertedTime = userTime.SelectedTime.Value.TimeOfDay;

                    if (itemInBase == null) return;
                    itemInBase.TimeToEfir = convertedTime;

                    context.SaveChanges();

                    foreach (var item in context.OnSunday.ToList())
                    {
                        model.EventListSourceSunday.Add(item);
                    }
                }


                EfirtListOnSunday.ItemsSource = model.EventListSourceSunday;
            }
        }
        #endregion

        #endregion

        #region БЛОК ЛЕКЦИИ

        public void GetLectionFromDoc()
        {

        }

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
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        foreach (var item in context.Films.ToList())
                        {
                            context.Films.Remove(item);
                        }
                        context.SaveChanges();
                    }
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
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        foreach (var item in context.Serieses.ToList())
                        {
                            context.Serieses.Remove(item);
                        }
                        context.SaveChanges();
                    }
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
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        foreach (var item in context.Lections.ToList())
                        {
                            context.Lections.Remove(item);
                        }
                        context.SaveChanges();
                    }
                    AddLectiontAtDB(pathToLection);
                    // ToDo профиксить подсказку, при добавлении строки изменять подсказу в текстовом поле
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }

            }
        }

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
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        foreach (var item in context.Preventions.ToList())
                        {
                            context.Preventions.Remove(item);
                        }
                        context.SaveChanges();
                    }
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
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        foreach (var item in context.TvShows.ToList())
                        {
                            context.TvShows.Remove(item);
                        }
                        context.SaveChanges();
                    }
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

        private void OpenEducationalsDialog_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;
            commonOpenFileDialog.ShowPlacesList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    FilePathToEducationalsTextBox.Text = commonOpenFileDialog.FileName;
                    pathToEducationals = FilePathToEducationalsTextBox.Text;
                    using (ApplicationContext context = new ApplicationContext())
                    {
                        foreach (var item in context.Educationals.ToList())
                        {
                            context.Educationals.Remove(item);
                        }
                        context.SaveChanges();
                    }
                    AddEducationalAtDB(pathToEducationals);
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
                        int countSeries = 0;
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
                                countSeries += 1;
                                string[] splitName = item.Name.Split(".");
                                string formattedName = splitName[0];

                                educational.Name = formattedName;
                                educational.Description = listDirectories[i].Name;
                                educational.Path = item.FullName;
                                educational.Duration = DurationContent(pathToContent, item.ToString());
                                educational.NumOfSeries = filteredFileList.Count();
                                educational.Series = countSeries;

                                //добавдяю сериал в базу
                                using (ApplicationContext context = new ApplicationContext())
                                {
                                    context.Educationals.Add(educational);
                                    context.SaveChanges();
                                }

                                educational = new Educational();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                // ProgressDownLoadingContent.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        CountOfEducationalsTextBlock.Text = Convert.ToString(listDirectories.Length);

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
                            film.Series += countFilm;
                            film.LastRun = Convert.ToDateTime(DateTime.Now.AddDays(-31).ToString("dd.MM.yy"));

                            using (ApplicationContext context = new ApplicationContext())
                            {
                                context.Films.Add(film);
                                context.SaveChanges();
                            }

                            film = new Film();
                            searchOpt = false;

                            viewModel.ValueProgressDownlaodingSeries += 1;
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
                                film.LastRun = Convert.ToDateTime(DateTime.Now.AddDays(-2).ToString("dd.MM.yy"));

                                using (ApplicationContext context = new ApplicationContext())
                                {
                                    context.Films.Add(film);
                                    context.SaveChanges();
                                }

                                film = new Film();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentFilm.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        using (ApplicationContext context = new ApplicationContext())
                            CountOfFilmTextBlock.Text = Convert.ToString(context.Films.Count());
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

                            using (ApplicationContext context = new ApplicationContext())
                            {
                                context.Lections.Add(lection);
                                context.SaveChanges();
                            }

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

                                using (ApplicationContext context = new ApplicationContext())
                                {
                                    context.Lections.Add(lection);
                                    context.SaveChanges();
                                }

                                lection = new Lection();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentLection.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        using (ApplicationContext context = new ApplicationContext())
                            CountOfLectionTextBlock.Text = Convert.ToString(context.Lections.Count());
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

                    foreach (FileInfo item in contentListMedia)
                    {
                        counPrevention += 1;

                        if (contentListMedia != null)
                        {
                            prevention.Name = item.Name;
                            prevention.Path = item.FullName;
                            prevention.Duration = DurationContent(pathToContent, item.ToString());
                            prevention.NumOfSeries = contentListMedia.Count();
                            prevention.Series += counPrevention;

                            using (ApplicationContext context = new ApplicationContext())
                            {
                                context.Preventions.Add(prevention);
                                context.SaveChanges();
                            }
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

                                using (ApplicationContext context = new ApplicationContext())
                                {
                                    context.Preventions.Add(prevention);
                                    context.SaveChanges();
                                }

                                prevention = new Prevention();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentLection.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }
                        using (ApplicationContext context = new ApplicationContext())
                            CountOfPreventionlTextBlock.Text = Convert.ToString(context.Preventions.Count());
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
                            string[] splittedName = item.Name.Split(".");
                            int parsedName = int.Parse(splittedName[0]);

                            //?TODO убрать рандомное подставление даты, это для тестирования!
                            Random random = new Random();


                            if (contentListMedia != null)
                            {
                                series.Name = listDirectories[i].Name;
                                series.Path = item.FullName;
                                series.Duration = DurationContent(pathToContent, item.ToString());
                                series.NumOfSeries = contentListMedia.Count();
                                series.IsSeries = parsedName;
                                series.LastRun = new DateTime();
                                series.NumOfRun = 0;
                                //Convert.ToDateTime(DateTime.Now.AddDays(-random.Next(1, 60)).ToString("dd.MM.yy")) - рандомайзер
                                using (ApplicationContext context = new ApplicationContext())
                                {
                                    context.Serieses.Add(series);
                                    context.SaveChanges();
                                }

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
                                tvShow.Description = item.Name;
                                var asdff = listDirectories[i];
                                tvShow.Path = item.FullName;
                                tvShow.Duration = DurationContent(pathToContent, item.ToString());
                                tvShow.NumOfSeries = filteredFileList.Count();
                                tvShow.Series = countTvShow;

                                using (ApplicationContext context = new ApplicationContext())
                                {
                                    context.TvShows.Add(tvShow);
                                    context.SaveChanges();
                                }
                                tvShow = new TvShow();

                                viewModel.ValueProgressDownlaodingSeries += 1;

                                ProgressDownLoadingContentTvShow.Value += viewModel.ValueProgressDownlaodingSeries;
                            }
                        }

                        using (ApplicationContext context = new ApplicationContext())
                            CountOfTvShowTextBlock.Text = Convert.ToString(context.Preventions.Count());
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



        /// <summary>
        /// Реализаация одноименного интерфейса
        /// </summary>
        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }


        #region ПОДБОР КОНТЕНТА
        static void Shuffle<T>(List<T> a)
        {
            Random rand = new Random();
            for (int i = a.Count - 1; i > 0; i--)
            {
                int j = rand.Next(0, i + 1);
                T tmp = a[i];
                a[i] = a[j];
                a[j] = tmp;
            }
        }

        private void GenerateEfir()
        {
            TabItem? SelectedTab = TabOfDayWeek.SelectedItem as TabItem;
            MainWindowViewModel model = new MainWindowViewModel();
            int TheRestTime = 0;
            TabControl tabControl = TabOfDayWeek;

            foreach (var tab in tabControl.Items)
            {
                TabItem? currentTabItem = tab as TabItem;

                // -------------------------------- WARNING!!! ---------------------------------//
                if (EfirListOnMonday.Items.Count == 0)
                {
                    MessageBox.Show("Надо создать список событий на день. " +
                    "Нажмите правой кнопкой на пустом пространстве программы и выберите " +
                    "из пункта Добавить один из подоходящих пунктов");
                    return;

                }
                // -------------------------------- WARNING!!! ---------------------------------//

                #region ДОБАВЛЕНИЕ КОНТЕНТА ПО ДНЯМ НЕДЕЛИ

                if (currentTabItem?.Header?.ToString()?.ToLower() == "Понедельник".ToLower())
                {

                    using (ApplicationContext context = new ApplicationContext())
                    {
                        var listEvents = context?.OnMonday.ToList();
                        var sortedListEventsByTime = listEvents?.OrderBy(x => x.TimeToEfir);

                        if (sortedListEventsByTime == null) break;
                        foreach (var item in sortedListEventsByTime)
                        {
                            model.EventListSourceMonday.Add(item);
                        }
                        EfirListOnMonday.ItemsSource = model.EventListSourceMonday;

                        for (int i = 0; i < model.EventListSourceMonday.Count; i++)
                        {
                            if (model.EventListSourceMonday.Count == 0) MessageBox.Show("Фильмы в базе не найдены, проверьте загружены ли фильмы в базу");

                            if (i < model.EventListSourceMonday.Count - 1)
                            {

                                var curItemTime = model.EventListSourceMonday[i];
                                var nextItemTime = model.EventListSourceMonday[i + 1];

                                var substractTimeWithinEvents = nextItemTime.TimeToEfir.Subtract(curItemTime.TimeToEfir);

                                int h = substractTimeWithinEvents.Hours * 60;
                                int m = substractTimeWithinEvents.Minutes;
                                int s = substractTimeWithinEvents.Seconds;

                                int totalMinuteEvent = h + m;
                                string eventName = model.EventListSourceMonday[i].EventName;
                                int totalMinute = totalMinuteEvent;
                                //узнаю начала события
                                // EfirOnMonday? startEvent = context.OnMonday.ToList().Find(w => w.EventName == "СЕРИАЛЫ");
                                //------------------------------------------поиск контента------------------------------------------//

                                #region ОБРАЗОВАНИЕ
                                if (model.EventListSourceMonday[i].EventName == "ОБРАЗОВАНИЕ")
                                {
                                    List<Educational> educationals = context.Educationals.ToList();
                                    PrintMonday? print = new PrintMonday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    //var listSortedByDate = context.Educationals.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    //Educational sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    //int indexElement = educationals.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    //int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                    //IfLengthIsOver:
                                    Educational? minEducationalTime = context?.Educationals.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minEducationalTime.Duration.Days;

                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, educationals.Count - 1); j < educationals.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = educationals[j].Duration.Hours * 60;
                                        mm = educationals[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        DateTime lastRunnedDate = educationals[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = educationals[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = educationals[j].NumOfSeries > 0 ? educationals[j].Series : 0;
                                        print.Description = "Образование:";
                                        print.Option = educationals[j].Path;
                                        print.Id = RandomId;
                                        educationals[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintMondays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == educationals.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }

                                }
                                #endregion

                                #region ТЕЛЕПЕРЕДАЧИ
                                if (model.EventListSourceMonday[i].EventName == "ТЕЛЕПЕРЕДАЧИ")
                                {
                                    PrintMonday print = new PrintMonday();
                                    List<TvShow> tvShows = context.TvShows.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    TvShow? minFilmTime = context?.TvShows.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days;
                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, tvShows.Count - 1); j < tvShows.Count; j++)
                                    {

                                        int maybeDays = 10;

                                        #region Определение времени
                                        hh = tvShows[j].Duration.Hours * 60;
                                        mm = tvShows[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = tvShows[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней



                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = tvShows[j].Description.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        bool isNumber = int.TryParse(formattedName, out int fake);

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = isNumber ? "" : formattedName;
                                        print.Series = tvShows[j].NumOfSeries > 0 ? tvShows[j].Series : 0;
                                        print.Description = tvShows[j]?.Name;
                                        print.Option = tvShows[j].Path;
                                        tvShows[j].LastRun = DateTime.Now;
                                        tvShows[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintMondays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (j == tvShows.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }
                                }
                                #endregion

                                #region ЛЕКЦИИ
                                if (model.EventListSourceMonday[i].EventName == "ЛЕКЦИИ")
                                {
                                    PrintMonday? print = new PrintMonday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();
                                    string possibleDate = "";
                                    LectionGraph? properLection = null;


                                    for (int j = 0; j < 7; j++)
                                    {

                                        if (DateTime.Now.AddDays(j).DayOfWeek.ToString().ToLower() != "Monday".ToLower()) continue;

                                        possibleDate = DateTime.Now.AddDays(j).ToShortDateString();

                                        properLection =
                                        context?.LectionGraphs.ToList().Find(d => d.LectionDate.ToShortDateString() == possibleDate);
                                    }
                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    if (properLection != null)
                                    {
                                        print.EventName = properLection.Name;
                                        print.Description = properLection.Lecturer;
                                        print.Id = RandomId;

                                        var lectionSplitName = properLection.Name.Split(":");
                                        var strName = lectionSplitName[1].Trim(new Char[] { '»', '.' }).Replace("«", "");
                                        var lection = context?.Lections.ToList().Find(l => l.Name.ToLower().Contains(strName.TrimStart().ToLower()));

                                        print.Option = lection?.Path;
                                    }
                                    context?.PrintMondays.Add(print);
                                    context?.SaveChanges();
                                }
                                #endregion

                                #region ФИЛЬМЫ
                                if (model.EventListSourceMonday[i].EventName == "ФИЛЬМЫ")
                                {
                                    PrintMonday print = new PrintMonday();
                                    List<Film> films = context.Films.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    Film? minFilmTime = context?.Films.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days == 0 ? minFilmTime.Duration.Minutes : minFilmTime.Duration.Days;


                                    List<Film> filmList = films.ToList();
                                    Shuffle<Film>(filmList);

                                    for (int j = 0; j < filmList.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = films[j].Duration.Hours * 60;
                                        mm = films[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion


                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = filmList[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = filmList[j].Name.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = filmList[j].NumOfSeries > 0 ? filmList[j].Series : 0;
                                        print.Description = "Фильм:";
                                        print.Option = filmList[j].Path;
                                        films[j].LastRun = DateTime.Now;
                                        films[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;


                                        context?.PrintMondays.Add(print);
                                        context?.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (TheRestTime < minFilmDuration) break;

                                    }
                                }
                                #endregion

                                #region СЕРИАЛЫ
                                if (model.EventListSourceMonday[i].EventName == "СЕРИАЛЫ")
                                {
                                    List<Series> series = context.Serieses.ToList();
                                    PrintMonday? print = new PrintMonday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Serieses.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Series sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = series.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement == listSortedByDate.Count() ? 0 : (indexElement);

                                IfLengthIsOver:
                                    for (int j = lastSeries; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = series[j].Duration.Hours * 60;
                                        mm = series[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = series[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = series[j].NumOfSeries > 0 ? series[j].IsSeries : 0;
                                        print.Description = "Сериал:";
                                        print.Option = series[j].Path;
                                        series[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintMondays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == listSortedByDate.Count() - 1)
                                        {
                                            lastSeries = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region ПРОФИЛАКТИКА
                                if (model.EventListSourceMonday[i].EventName == "ПРОФИЛАКТИКА")
                                {
                                    List<Prevention> preventions = context.Preventions.ToList();
                                    PrintMonday? print = new PrintMonday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Preventions.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Prevention sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = preventions.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию

                                IfLengthIsOver:
                                    for (int j = indexElement; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = preventions[j].Duration.Hours * 60;
                                        mm = preventions[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = preventions[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        //print.Series = preventions[j].NumOfSeries > 0 ? preventions[j].IsSeries : 0;
                                        print.Description = preventions[j].Description;
                                        print.Option = preventions[j].Path;
                                        preventions[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintMondays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == preventions.Count - 1)
                                        {
                                            indexElement = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region НОВОСТИ
                                if (model.EventListSourceMonday[i].EventName == "НОВОСТИ")
                                {
                                    PrintMonday? print = new PrintMonday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "НОВОСТИ";
                                    print.Id = RandomId;

                                    context.PrintMondays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ПЕРЕРЫВ
                                if (model.EventListSourceMonday[i].EventName == "ПЕРЕРЫВ")
                                {
                                    PrintMonday? print = new PrintMonday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "ПЕРЕРЫВ";
                                    print.Id = RandomId;

                                    context.PrintMondays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ЗАВРЕШЕНИЕ ТРАНСЛЯЦИИ
                                if (model.EventListSourceMonday[i + 1].EventName == "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ")
                                {
                                    PrintMonday? print = new PrintMonday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = nextItemTime.TimeToEfir;
                                    print.EventName = "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ";
                                    print.Id = RandomId;

                                    context.PrintMondays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion
                            }
                        }
                    }
                }

                if (currentTabItem?.Header?.ToString()?.ToLower() == "Вторник".ToLower())
                {
                    using (ApplicationContext? context = new ApplicationContext())
                    {
                        var listEvents = context?.OnTuesday.ToList();
                        var sortedListEventsByTime = listEvents?.OrderBy(x => x.TimeToEfir);

                        if (sortedListEventsByTime == null) break;
                        foreach (var item in sortedListEventsByTime)
                        {
                            model.EventListSourceTuesday.Add(item);
                        }
                        EfirListOnTuesday.ItemsSource = model.EventListSourceTuesday;

                        //var MinTimeEfir = context?.OnTuesday.ToList().Min(t => t.TimeToEfir);
                        //var MaxTimeEfir = context?.OnTuesday.ToList().Max(t => t.TimeToEfir);


                        for (int i = 0; i < model.EventListSourceTuesday.Count; i++)
                        {
                            if (model.EventListSourceTuesday.Count == 0) MessageBox.Show("Фильмы в базе не найдены, проверьте загружены ли фильмы в базу");

                            if (i < model.EventListSourceTuesday.Count - 1)
                            {
                                // if (model.EventListSourceTuesday[i].EventName == "ПЕРЕРЫВ") continue;

                                // TODO ПРОФИКСИТЬ: если нет последнего события, то не получаю время предыдущего.
                                // TODO Нужны начальные и конечные точки эфира(хотябы конечная)
                                // TODO Варианты: 1. Сделать где-то в верхней части прожграммы два пикера с выбором веремени начала и конца,
                                // TODO 2. сделать два событие и добавить их в список осбытий, они будут константами, но выбор времени будет за пользователем
                                var curItemTime = model.EventListSourceTuesday[i];
                                var nextItemTime = model.EventListSourceTuesday[i + 1];

                                var substractTimeWithinEvents = nextItemTime.TimeToEfir.Subtract(curItemTime.TimeToEfir);

                                int h = substractTimeWithinEvents.Hours * 60;
                                int m = substractTimeWithinEvents.Minutes;
                                int s = substractTimeWithinEvents.Seconds;

                                int totalMinuteEvent = h + m;
                                int totalMinute = totalMinuteEvent;
                                //------------------------------------------поиск контента------------------------------------------//

                                #region ОБРАЗОВАНИЕ
                                if (model.EventListSourceTuesday[i].EventName == "ОБРАЗОВАНИЕ")
                                {
                                    List<Educational> educationals = context.Educationals.ToList();
                                    PrintTuesday? print = new PrintTuesday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    /*var listSortedByDate = context.Educationals.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Educational sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = educationals.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);*/


                                    Educational? minEducationalTime = context?.Educationals.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minEducationalTime.Duration.Days;

                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, educationals.Count - 1); j < educationals.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = educationals[j].Duration.Hours * 60;
                                        mm = educationals[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        DateTime lastRunnedDate = educationals[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = educationals[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = educationals[j].NumOfSeries > 0 ? educationals[j].Series : 0;
                                        print.Description = "Образование:";
                                        print.Option = educationals[j].Path;
                                        print.Id = RandomId;
                                        educationals[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintTuesdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == educationals.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }

                                }
                                #endregion

                                #region ТЕЛЕПЕРЕДАЧИ
                                if (model.EventListSourceTuesday[i].EventName == "ТЕЛЕПЕРЕДАЧИ")
                                {
                                    PrintTuesday print = new PrintTuesday();
                                    List<TvShow> tvShows = context.TvShows.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    TvShow? minFilmTime = context?.TvShows.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days;
                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, tvShows.Count - 1); j < tvShows.Count; j++)
                                    {

                                        int maybeDays = 10;

                                        #region Определение времени
                                        hh = tvShows[j].Duration.Hours * 60;
                                        mm = tvShows[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = tvShows[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней



                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = tvShows[j].Description.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        bool isNumber = int.TryParse(formattedName, out int fake);

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = isNumber ? "" : formattedName;
                                        print.Series = tvShows[j].NumOfSeries > 0 ? tvShows[j].Series : 0;
                                        print.Description = tvShows[j]?.Name;
                                        print.Option = tvShows[j].Path;
                                        tvShows[j].LastRun = DateTime.Now;
                                        tvShows[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintTuesdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (j == tvShows.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }
                                }
                                #endregion

                                #region ЛЕКЦИИ
                                if (model.EventListSourceTuesday[i].EventName == "ЛЕКЦИИ")
                                {
                                    PrintTuesday? print = new PrintTuesday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();
                                    string possibleDate = "";
                                    LectionGraph? properLection = null;

                                    for (int j = 0; j < 7; j++)
                                    {
                                        if (DateTime.Now.AddDays(j).DayOfWeek.ToString().ToLower() != "Tuesday".ToLower()) continue;

                                        possibleDate = DateTime.Now.AddDays(j).ToShortDateString();
                                        properLection =
                                        context?.LectionGraphs.ToList().Find(d => d.LectionDate.ToShortDateString() == possibleDate);
                                    }
                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    if (properLection != null)
                                    {
                                        print.EventName = properLection.Name;
                                        print.Description = properLection.Lecturer;
                                        print.Id = RandomId;

                                        var lectionSplitName = properLection.Name.Split(":");
                                        var strName = lectionSplitName[1].Trim(new Char[] { '»', '.' }).Replace("«", "");
                                        var lection = context?.Lections.ToList().Find(l => l.Name.ToLower().Contains(strName.TrimStart().ToLower()));

                                        print.Option = lection?.Path;
                                    }

                                    context?.PrintTuesdays.Add(print);
                                    context?.SaveChanges();
                                }
                                #endregion

                                #region ФИЛЬМЫ
                                if (model.EventListSourceTuesday[i].EventName == "ФИЛЬМЫ")
                                {
                                    PrintTuesday print = new PrintTuesday();
                                    List<Film> films = context.Films.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    Film? minFilmTime = context?.Films.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days == 0 ? minFilmTime.Duration.Minutes : minFilmTime.Duration.Days;

                                    List<Film> filmList = films.ToList();
                                    Shuffle<Film>(filmList);

                                    for (int j = 0; j < filmList.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = films[j].Duration.Hours * 60;
                                        mm = films[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion


                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = filmList[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = filmList[j].Name.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = filmList[j].NumOfSeries > 0 ? filmList[j].Series : 0;
                                        print.Description = "Фильм:";
                                        print.Option = filmList[j].Path;
                                        films[j].LastRun = DateTime.Now;
                                        films[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;


                                        context?.PrintTuesdays.Add(print);
                                        context?.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (TheRestTime < minFilmDuration) break;
                                    }
                                }
                                #endregion

                                #region СЕРИАЛЫ
                                //int totalMinute = totalMinuteEvent;
                                if (model.EventListSourceTuesday[i].EventName == "СЕРИАЛЫ")
                                {
                                    List<Series> series = context.Serieses.ToList();
                                    PrintTuesday? print = new PrintTuesday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Serieses.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Series sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = series.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                IfLengthIsOver:
                                    for (int j = lastSeries; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = series[j].Duration.Hours * 60;
                                        mm = series[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) break;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = series[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = series[j].NumOfSeries > 0 ? series[j].IsSeries : 0;
                                        print.Description = "Сериал:";
                                        print.Option = series[j].Path;
                                        series[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintTuesdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == listSortedByDate.Count() - 1)
                                        {
                                            lastSeries = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region ПРОФИЛАКТИКА
                                if (model.EventListSourceTuesday[i].EventName == "ПРОФИЛАКТИКА")
                                {
                                    List<Prevention> preventions = context.Preventions.ToList();
                                    PrintTuesday? print = new PrintTuesday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Preventions.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Prevention sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = preventions.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию

                                IfLengthIsOver:
                                    for (int j = indexElement; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = preventions[j].Duration.Hours * 60;
                                        mm = preventions[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = preventions[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Option = preventions[j].Path;
                                        //print.Series = preventions[j].NumOfSeries > 0 ? preventions[j].IsSeries : 0;
                                        print.Description = preventions[j].Description;
                                        preventions[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintTuesdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == preventions.Count - 1)
                                        {
                                            indexElement = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region НОВОСТИ
                                if (model.EventListSourceTuesday[i].EventName == "НОВОСТИ")
                                {
                                    PrintTuesday? print = new PrintTuesday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "НОВОСТИ";
                                    print.Id = RandomId;

                                    context.PrintTuesdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ПЕРЕРЫВ
                                if (model.EventListSourceTuesday[i].EventName == "ПЕРЕРЫВ")
                                {
                                    PrintTuesday? print = new PrintTuesday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "ПЕРЕРЫВ";
                                    print.Id = RandomId;

                                    context.PrintTuesdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ЗАВРЕШЕНИЕ ТРАНСЛЯЦИИ
                                if (model.EventListSourceTuesday[i + 1].EventName == "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ")
                                {
                                    PrintTuesday? print = new PrintTuesday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = nextItemTime.TimeToEfir;
                                    print.EventName = "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ";
                                    print.Id = RandomId;

                                    context.PrintTuesdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion
                            }
                        }
                    }
                }

                if (currentTabItem?.Header?.ToString()?.ToLower() == "Среда".ToLower())
                {
                    using (ApplicationContext? context = new ApplicationContext())
                    {
                        var listEvents = context?.OnWednesday.ToList();
                        var sortedListEventsByTime = listEvents?.OrderBy(x => x.TimeToEfir);

                        if (sortedListEventsByTime == null) break;
                        foreach (var item in sortedListEventsByTime)
                        {
                            model.EventListSourceWednesday.Add(item);
                        }
                        EfirListOnWednesday.ItemsSource = model.EventListSourceWednesday;

                        //var MinTimeEfir = context?.OnTuesday.ToList().Min(t => t.TimeToEfir);
                        //var MaxTimeEfir = context?.OnTuesday.ToList().Max(t => t.TimeToEfir);

                        for (int i = 0; i < model.EventListSourceWednesday.Count; i++)
                        {
                            if (model.EventListSourceWednesday.Count == 0) MessageBox.Show("Фильмы в базе не найдены, проверьте загружены ли фильмы в базу");

                            if (i < model.EventListSourceWednesday.Count - 1)
                            {
                                //if (model.EventListSourceWednesday[i].EventName == "ПЕРЕРЫВ") continue;

                                // TODO ПРОФИКСИТЬ: если нет последнего события, то не получаю время предыдущего.
                                // TODO Нужны начальные и конечные точки эфира(хотябы конечная)
                                // TODO Варианты: 1. Сделать где-то в верхней части прожграммы два пикера с выбором веремени начала и конца,
                                // TODO 2. сделать два событие и добавить их в список осбытий, они будут константами, но выбор времени будет за пользователем
                                var curItemTime = model.EventListSourceWednesday[i];
                                var nextItemTime = model.EventListSourceWednesday[i + 1];

                                var substractTimeWithinEvents = nextItemTime.TimeToEfir.Subtract(curItemTime.TimeToEfir);

                                int h = substractTimeWithinEvents.Hours * 60;
                                int m = substractTimeWithinEvents.Minutes;
                                int s = substractTimeWithinEvents.Seconds;

                                int totalMinuteEvent = h + m;
                                int totalMinute = totalMinuteEvent;

                                //------------------------------------------поиск контента------------------------------------------//

                                #region ОБРАЗОВАНИЕ
                                if (model.EventListSourceWednesday[i].EventName == "ОБРАЗОВАНИЕ")
                                {
                                    List<Educational> educationals = context.Educationals.ToList();
                                    PrintWednesday? print = new PrintWednesday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;

                                    Educational? minEducationalTime = context?.Educationals.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minEducationalTime.Duration.Days;

                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, educationals.Count - 1); j < educationals.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = educationals[j].Duration.Hours * 60;
                                        mm = educationals[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        DateTime lastRunnedDate = educationals[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = educationals[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = educationals[j].NumOfSeries > 0 ? educationals[j].Series : 0;
                                        print.Description = "Образование:";
                                        print.Option = educationals[j].Path;
                                        print.Id = RandomId;
                                        educationals[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintWednesdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == educationals.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }

                                }
                                #endregion

                                #region ТЕЛЕПЕРЕДАЧИ
                                if (model.EventListSourceWednesday[i].EventName == "ТЕЛЕПЕРЕДАЧИ")
                                {
                                    PrintWednesday print = new PrintWednesday();
                                    List<TvShow> tvShows = context.TvShows.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    TvShow? minFilmTime = context?.TvShows.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days;
                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, tvShows.Count - 1); j < tvShows.Count; j++)
                                    {

                                        int maybeDays = 10;

                                        #region Определение времени
                                        hh = tvShows[j].Duration.Hours * 60;
                                        mm = tvShows[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = tvShows[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней



                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = tvShows[j].Description.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        bool isNumber = int.TryParse(formattedName, out int fake);

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = isNumber ? "" : formattedName;
                                        print.Series = tvShows[j].NumOfSeries > 0 ? tvShows[j].Series : 0;
                                        print.Description = tvShows[j]?.Name;
                                        print.Option = tvShows[j].Path;
                                        tvShows[j].LastRun = DateTime.Now;
                                        tvShows[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintWednesdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (j == tvShows.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }
                                }
                                #endregion

                                #region ЛЕКЦИИ
                                if (model.EventListSourceWednesday[i].EventName == "ЛЕКЦИИ")
                                {
                                    PrintWednesday? print = new PrintWednesday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();
                                    string possibleDate = "";
                                    LectionGraph? properLection = null;

                                    for (int j = 0; j < 7; j++)
                                    {
                                        if (DateTime.Now.AddDays(j).DayOfWeek.ToString().ToLower() != "Wednesday".ToLower()) continue;

                                        possibleDate = DateTime.Now.AddDays(j).ToShortDateString();
                                        properLection =
                                        context?.LectionGraphs.ToList().Find(d => d.LectionDate.ToShortDateString() == possibleDate);
                                    }
                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    if (properLection != null)
                                    {
                                        print.EventName = properLection.Name;
                                        print.Description = properLection.Lecturer;
                                        print.Id = RandomId;

                                        var lectionSplitName = properLection.Name.Split(":");
                                        var strName = lectionSplitName[1].Trim(new Char[] { '»', '.' }).Replace("«", "");
                                        var lection = context?.Lections.ToList().Find(l => l.Name.ToLower().Contains(strName.TrimStart().ToLower()));

                                        print.Option = lection?.Path;
                                    }

                                    context?.PrintWednesdays.Add(print);
                                    context?.SaveChanges();
                                }
                                #endregion

                                #region ФИЛЬМЫ
                                if (model.EventListSourceWednesday[i].EventName == "ФИЛЬМЫ")
                                {
                                    PrintWednesday print = new PrintWednesday();
                                    List<Film> films = context.Films.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    Film? minFilmTime = context?.Films.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days == 0 ? minFilmTime.Duration.Minutes : minFilmTime.Duration.Days;

                                    List<Film> filmList = films.ToList();
                                    Shuffle<Film>(filmList);

                                    for (int j = 0; j < filmList.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = films[j].Duration.Hours * 60;
                                        mm = films[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion


                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = filmList[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = filmList[j].Name.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = filmList[j].NumOfSeries > 0 ? filmList[j].Series : 0;
                                        print.Description = "Фильм:";
                                        print.Option = filmList[j].Path;
                                        films[j].LastRun = DateTime.Now;
                                        films[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;


                                        context?.PrintWednesdays.Add(print);
                                        context?.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (TheRestTime < minFilmDuration) break;
                                    }
                                }
                                #endregion

                                #region СЕРИАЛЫ
                                //int totalMinute = totalMinuteEvent;
                                if (model.EventListSourceWednesday[i].EventName == "СЕРИАЛЫ")
                                {
                                    List<Series> series = context.Serieses.ToList();
                                    PrintWednesday? print = new PrintWednesday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Serieses.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Series sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = series.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                IfLengthIsOver:
                                    for (int j = lastSeries; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = series[j].Duration.Hours * 60;
                                        mm = series[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) break;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = series[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = series[j].NumOfSeries > 0 ? series[j].IsSeries : 0;
                                        print.Description = "Сериал:";
                                        print.Option = series[j].Path;
                                        series[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintWednesdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == listSortedByDate.Count() - 1)
                                        {
                                            lastSeries = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region ПРОФИЛАКТИКА
                                if (model.EventListSourceWednesday[i].EventName == "ПРОФИЛАКТИКА")
                                {
                                    List<Prevention> preventions = context.Preventions.ToList();
                                    PrintWednesday? print = new PrintWednesday();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    var listSortedByDate = context.Preventions.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Prevention sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = preventions.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию

                                IfLengthIsOver:
                                    for (int j = indexElement; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = preventions[j].Duration.Hours * 60;
                                        mm = preventions[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = preventions[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Option = preventions[j].Path;
                                        //print.Series = preventions[j].NumOfSeries > 0 ? preventions[j].IsSeries : 0;
                                        print.Description = preventions[j].Description;
                                        preventions[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintWednesdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == preventions.Count - 1)
                                        {
                                            indexElement = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region НОВОСТИ
                                if (model.EventListSourceWednesday[i].EventName == "НОВОСТИ")
                                {
                                    PrintWednesday? print = new PrintWednesday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "НОВОСТИ";
                                    print.Id = RandomId;

                                    context.PrintWednesdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ПЕРЕРЫВ
                                if (model.EventListSourceWednesday[i].EventName == "ПЕРЕРЫВ")
                                {
                                    PrintWednesday? print = new PrintWednesday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "ПЕРЕРЫВ";
                                    print.Id = RandomId;

                                    context.PrintWednesdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ЗАВРЕШЕНИЕ ТРАНСЛЯЦИИ
                                if (model.EventListSourceWednesday[i + 1].EventName == "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ")
                                {
                                    PrintWednesday? print = new PrintWednesday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = nextItemTime.TimeToEfir;
                                    print.EventName = "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ";
                                    print.Id = RandomId;

                                    context.PrintWednesdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion
                            }
                        }
                    }
                }

                if (currentTabItem?.Header?.ToString()?.ToLower() == "Четверг".ToLower())
                {
                    using (ApplicationContext? context = new ApplicationContext())
                    {
                        var listEvents = context?.OnThursday.ToList();
                        var sortedListEventsByTime = listEvents?.OrderBy(x => x.TimeToEfir);

                        if (sortedListEventsByTime == null) break;
                        foreach (var item in sortedListEventsByTime)
                        {
                            model.EventListSourceThursday.Add(item);
                        }
                        EfirListOnThursday.ItemsSource = model.EventListSourceThursday;

                        //var MinTimeEfir = context?.OnTuesday.ToList().Min(t => t.TimeToEfir);
                        //var MaxTimeEfir = context?.OnTuesday.ToList().Max(t => t.TimeToEfir);

                        for (int i = 0; i < model.EventListSourceThursday.Count; i++)
                        {
                            if (model.EventListSourceThursday.Count == 0) MessageBox.Show("Фильмы в базе не найдены, проверьте загружены ли фильмы в базу");

                            if (i < model.EventListSourceThursday.Count - 1)
                            {
                                //if (model.EventListSourceThursday[i].EventName == "ПЕРЕРЫВ") continue;

                                // TODO ПРОФИКСИТЬ: если нет последнего события, то не получаю время предыдущего.
                                // TODO Нужны начальные и конечные точки эфира(хотябы конечная)
                                // TODO Варианты: 1. Сделать где-то в верхней части прожграммы два пикера с выбором веремени начала и конца,
                                // TODO 2. сделать два событие и добавить их в список осбытий, они будут константами, но выбор времени будет за пользователем
                                var curItemTime = model.EventListSourceThursday[i];
                                var nextItemTime = model.EventListSourceThursday[i + 1];

                                var substractTimeWithinEvents = nextItemTime.TimeToEfir.Subtract(curItemTime.TimeToEfir);

                                int h = substractTimeWithinEvents.Hours * 60;
                                int m = substractTimeWithinEvents.Minutes;
                                int s = substractTimeWithinEvents.Seconds;

                                int totalMinuteEvent = h + m;
                                int totalMinute = totalMinuteEvent;

                                //------------------------------------------поиск контента------------------------------------------//

                                #region ОБРАЗОВАНИЕ
                                if (model.EventListSourceThursday[i].EventName == "ОБРАЗОВАНИЕ")
                                {
                                    List<Educational> educationals = context.Educationals.ToList();
                                    PrintThursday? print = new PrintThursday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    /* var listSortedByDate = context.Educationals.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                     Educational sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                     int indexElement = educationals.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                     int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                 IfLengthIsOver:*/

                                    Educational? minEducationalTime = context?.Educationals.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minEducationalTime.Duration.Days;

                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, educationals.Count - 1); j < educationals.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = educationals[j].Duration.Hours * 60;
                                        mm = educationals[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        DateTime lastRunnedDate = educationals[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = educationals[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = educationals[j].NumOfSeries > 0 ? educationals[j].Series : 0;
                                        print.Description = "Образование:";
                                        print.Option = educationals[j].Path;
                                        print.Id = RandomId;
                                        educationals[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintThursdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == educationals.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }

                                }
                                #endregion

                                #region ТЕЛЕПЕРЕДАЧИ
                                if (model.EventListSourceThursday[i].EventName == "ТЕЛЕПЕРЕДАЧИ")
                                {
                                    PrintThursday print = new PrintThursday();
                                    List<TvShow> tvShows = context.TvShows.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    TvShow? minFilmTime = context?.TvShows.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days;
                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, tvShows.Count - 1); j < tvShows.Count; j++)
                                    {

                                        int maybeDays = 10;

                                        #region Определение времени
                                        hh = tvShows[j].Duration.Hours * 60;
                                        mm = tvShows[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = tvShows[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней



                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = tvShows[j].Description.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        bool isNumber = int.TryParse(formattedName, out int fake);

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = isNumber ? "" : formattedName;
                                        print.Series = tvShows[j].NumOfSeries > 0 ? tvShows[j].Series : 0;
                                        print.Description = tvShows[j]?.Name;
                                        print.Option = tvShows[j].Path;
                                        tvShows[j].LastRun = DateTime.Now;
                                        tvShows[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintThursdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (TheRestTime < minFilmDuration) break;

                                    }
                                }
                                #endregion

                                #region ЛЕКЦИИ
                                if (model.EventListSourceThursday[i].EventName == "ЛЕКЦИИ")
                                {
                                    PrintThursday? print = new PrintThursday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();
                                    string possibleDate = "";
                                    LectionGraph? properLection = null;

                                    for (int j = 0; j < 7; j++)
                                    {
                                        if (DateTime.Now.AddDays(j).DayOfWeek.ToString().ToLower() != "Thursday".ToLower()) continue;

                                        possibleDate = DateTime.Now.AddDays(j).ToShortDateString();
                                        properLection =
                                        context?.LectionGraphs.ToList().Find(d => d.LectionDate.ToShortDateString() == possibleDate);
                                    }
                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    if (properLection != null)
                                    {
                                        print.EventName = properLection.Name;
                                        print.Description = properLection.Lecturer;
                                        print.Id = RandomId;

                                        var lectionSplitName = properLection.Name.Split(":");
                                        var strName = lectionSplitName[1].Trim(new Char[] { '»', '.' }).Replace("«", "");
                                        var lection = context?.Lections.ToList().Find(l => l.Name.ToLower().Contains(strName.TrimStart().ToLower()));

                                        print.Option = lection?.Path;
                                    }

                                    context?.PrintThursdays.Add(print);
                                    context?.SaveChanges();
                                }
                                #endregion

                                #region ФИЛЬМЫ
                                if (model.EventListSourceThursday[i].EventName == "ФИЛЬМЫ")
                                {
                                    PrintThursday print = new PrintThursday();
                                    List<Film> films = context.Films.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    Film? minFilmTime = context?.Films.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days == 0 ? minFilmTime.Duration.Minutes : minFilmTime.Duration.Days;

                                    List<Film> filmList = films.ToList();
                                    Shuffle<Film>(filmList);

                                    for (int j = 0; j < filmList.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = films[j].Duration.Hours * 60;
                                        mm = films[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion


                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = filmList[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = filmList[j].Name.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = filmList[j].NumOfSeries > 0 ? filmList[j].Series : 0;
                                        print.Description = "Фильм:";
                                        print.Option = filmList[j].Path;
                                        films[j].LastRun = DateTime.Now;
                                        films[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;


                                        context?.PrintThursdays.Add(print);
                                        context?.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (TheRestTime < minFilmDuration) break;
                                    }
                                }
                                #endregion

                                #region СЕРИАЛЫ
                                //int totalMinute = totalMinuteEvent;
                                if (model.EventListSourceThursday[i].EventName == "СЕРИАЛЫ")
                                {
                                    List<Series> series = context.Serieses.ToList();
                                    PrintThursday? print = new PrintThursday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Serieses.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Series sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = series.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                IfLengthIsOver:
                                    for (int j = lastSeries; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = series[j].Duration.Hours * 60;
                                        mm = series[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = series[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = series[j].NumOfSeries > 0 ? series[j].IsSeries : 0;
                                        print.Description = "Сериал:";
                                        print.Option = series[j].Path;
                                        series[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintThursdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == listSortedByDate.Count() - 1)
                                        {
                                            lastSeries = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region ПРОФИЛАКТИКА
                                if (model.EventListSourceThursday[i].EventName == "ПРОФИЛАКТИКА")
                                {
                                    List<Prevention> preventions = context.Preventions.ToList();
                                    PrintThursday? print = new PrintThursday();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    var listSortedByDate = context.Preventions.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Prevention sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = preventions.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию

                                IfLengthIsOver:
                                    for (int j = indexElement; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = preventions[j].Duration.Hours * 60;
                                        mm = preventions[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = preventions[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Option = preventions[j].Path;
                                        //print.Series = preventions[j].NumOfSeries > 0 ? preventions[j].IsSeries : 0;
                                        print.Description = preventions[j].Description;
                                        preventions[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintThursdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == preventions.Count - 1)
                                        {
                                            indexElement = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region НОВОСТИ
                                if (model.EventListSourceThursday[i].EventName == "НОВОСТИ")
                                {
                                    PrintThursday? print = new PrintThursday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "НОВОСТИ";
                                    print.Id = RandomId;

                                    context.PrintThursdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ПЕРЕРЫВ
                                if (model.EventListSourceThursday[i].EventName == "ПЕРЕРЫВ")
                                {
                                    PrintThursday? print = new PrintThursday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "ПЕРЕРЫВ";
                                    print.Id = RandomId;

                                    context.PrintThursdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ЗАВРЕШЕНИЕ ТРАНСЛЯЦИИ
                                if (model.EventListSourceThursday[i + 1].EventName == "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ")
                                {
                                    PrintThursday? print = new PrintThursday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = nextItemTime.TimeToEfir;
                                    print.EventName = "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ";
                                    print.Id = RandomId;

                                    context.PrintThursdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion
                            }
                        }
                    }
                }

                if (currentTabItem?.Header?.ToString()?.ToLower() == "Пятница".ToLower())
                {
                    using (ApplicationContext? context = new ApplicationContext())
                    {
                        var listEvents = context?.OnFriday.ToList();
                        var sortedListEventsByTime = listEvents?.OrderBy(x => x.TimeToEfir);

                        if (sortedListEventsByTime == null) break;
                        foreach (var item in sortedListEventsByTime)
                        {
                            model.EventListSourceFriday.Add(item);
                        }
                        EfirListOnFriday.ItemsSource = model.EventListSourceFriday;

                        //var MinTimeEfir = context?.OnTuesday.ToList().Min(t => t.TimeToEfir);
                        //var MaxTimeEfir = context?.OnTuesday.ToList().Max(t => t.TimeToEfir);

                        for (int i = 0; i < model.EventListSourceFriday.Count; i++)
                        {
                            if (model.EventListSourceFriday.Count == 0) MessageBox.Show("Фильмы в базе не найдены, проверьте загружены ли фильмы в базу");

                            if (i < model.EventListSourceFriday.Count - 1)
                            {
                                //if (model.EventListSourceFriday[i].EventName == "ПЕРЕРЫВ") continue;

                                // TODO ПРОФИКСИТЬ: если нет последнего события, то не получаю время предыдущего.
                                // TODO Нужны начальные и конечные точки эфира(хотябы конечная)
                                // TODO Варианты: 1. Сделать где-то в верхней части прожграммы два пикера с выбором веремени начала и конца,
                                // TODO 2. сделать два событие и добавить их в список осбытий, они будут константами, но выбор времени будет за пользователем
                                var curItemTime = model.EventListSourceFriday[i];
                                var nextItemTime = model.EventListSourceFriday[i + 1];

                                var substractTimeWithinEvents = nextItemTime.TimeToEfir.Subtract(curItemTime.TimeToEfir);

                                int h = substractTimeWithinEvents.Hours * 60;
                                int m = substractTimeWithinEvents.Minutes;
                                int s = substractTimeWithinEvents.Seconds;

                                int totalMinuteEvent = h + m;
                                int totalMinute = totalMinuteEvent;
                                //------------------------------------------поиск контента------------------------------------------//

                                #region ОБРАЗОВАНИЕ
                                if (model.EventListSourceFriday[i].EventName == "ОБРАЗОВАНИЕ")
                                {
                                    List<Educational> educationals = context.Educationals.ToList();
                                    PrintFriday? print = new PrintFriday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    /* var listSortedByDate = context.Educationals.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                     Educational sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                     int indexElement = educationals.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                     int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                 IfLengthIsOver:*/

                                    Educational? minEducationalTime = context?.Educationals.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minEducationalTime.Duration.Days;

                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, educationals.Count - 1); j < educationals.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = educationals[j].Duration.Hours * 60;
                                        mm = educationals[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        DateTime lastRunnedDate = educationals[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = educationals[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = educationals[j].NumOfSeries > 0 ? educationals[j].Series : 0;
                                        print.Description = "Образование:";
                                        print.Option = educationals[j].Path;
                                        print.Id = RandomId;
                                        educationals[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintFridays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == educationals.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }

                                }
                                #endregion

                                #region ТЕЛЕПЕРЕДАЧИ
                                if (model.EventListSourceFriday[i].EventName == "ТЕЛЕПЕРЕДАЧИ")
                                {
                                    PrintFriday print = new PrintFriday();
                                    List<TvShow> tvShows = context.TvShows.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    TvShow? minFilmTime = context?.TvShows.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days;
                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, tvShows.Count - 1); j < tvShows.Count; j++)
                                    {

                                        int maybeDays = 10;

                                        #region Определение времени
                                        hh = tvShows[j].Duration.Hours * 60;
                                        mm = tvShows[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = tvShows[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = tvShows[j].Description.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        bool isNumber = int.TryParse(formattedName, out int fake);

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = isNumber ? "" : formattedName;
                                        print.Series = tvShows[j].NumOfSeries > 0 ? tvShows[j].Series : 0;
                                        print.Description = tvShows[j]?.Name;
                                        tvShows[j].LastRun = DateTime.Now;
                                        tvShows[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintFridays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (j == tvShows.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }
                                }
                                #endregion

                                #region ЛЕКЦИИ
                                if (model.EventListSourceFriday[i].EventName == "ЛЕКЦИИ")
                                {
                                    PrintFriday? print = new PrintFriday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();
                                    string possibleDate = "";
                                    LectionGraph? properLection = null;

                                    for (int j = 0; j < 7; j++)
                                    {
                                        if (DateTime.Now.AddDays(j).DayOfWeek.ToString().ToLower() != "Friday".ToLower()) continue;

                                        possibleDate = DateTime.Now.AddDays(j).ToShortDateString();
                                        properLection =
                                        context?.LectionGraphs.ToList().Find(d => d.LectionDate.ToShortDateString() == possibleDate);
                                    }
                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    if (properLection != null)
                                    {
                                        print.EventName = properLection.Name;
                                        print.Description = properLection.Lecturer;
                                        print.Id = RandomId;

                                        var lectionSplitName = properLection.Name.Split(":");
                                        var strName = lectionSplitName[1].Trim(new Char[] { '»', '.' }).Replace("«", "");
                                        var lection = context?.Lections.ToList().Find(l => l.Name.ToLower().Contains(strName.TrimStart().ToLower()));

                                        print.Option = lection?.Path;
                                    }

                                    context?.PrintFridays.Add(print);
                                    context?.SaveChanges();
                                }
                                #endregion

                                #region ФИЛЬМЫ
                                if (model.EventListSourceFriday[i].EventName == "ФИЛЬМЫ")
                                {
                                    PrintFriday print = new PrintFriday();
                                    List<Film> films = context.Films.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    Film? minFilmTime = context?.Films.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days == 0 ? minFilmTime.Duration.Minutes : minFilmTime.Duration.Days;

                                    List<Film> filmList = films.ToList();
                                    Shuffle<Film>(filmList);

                                    for (int j = 0; j < filmList.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = films[j].Duration.Hours * 60;
                                        mm = films[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion


                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = filmList[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = filmList[j].Name.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = filmList[j].NumOfSeries > 0 ? filmList[j].Series : 0;
                                        print.Description = "Фильм:";
                                        print.Option = filmList[j].Path;
                                        films[j].LastRun = DateTime.Now;
                                        films[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;


                                        context?.PrintFridays.Add(print);
                                        context?.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (TheRestTime < minFilmDuration) break;
                                    }
                                }
                                #endregion

                                #region СЕРИАЛЫ
                                //int totalMinute = totalMinuteEvent;
                                if (model.EventListSourceFriday[i].EventName == "СЕРИАЛЫ")
                                {
                                    List<Series> series = context.Serieses.ToList();
                                    PrintFriday? print = new PrintFriday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Serieses.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Series sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = series.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                IfLengthIsOver:
                                    for (int j = lastSeries; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = series[j].Duration.Hours * 60;
                                        mm = series[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = series[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = series[j].NumOfSeries > 0 ? series[j].IsSeries : 0;
                                        print.Description = "Сериал:";
                                        print.Option = series[j].Path;
                                        series[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintFridays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == listSortedByDate.Count() - 1)
                                        {
                                            lastSeries = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region ПРОФИЛАКТИКА
                                if (model.EventListSourceFriday[i].EventName == "ПРОФИЛАКТИКА")
                                {
                                    List<Prevention> preventions = context.Preventions.ToList();
                                    PrintFriday? print = new PrintFriday();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    var listSortedByDate = context.Preventions.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Prevention sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = preventions.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию

                                IfLengthIsOver:
                                    for (int j = indexElement; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = preventions[j].Duration.Hours * 60;
                                        mm = preventions[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = preventions[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Option = preventions[j].Path;
                                        //print.Series = preventions[j].NumOfSeries > 0 ? preventions[j].IsSeries : 0;
                                        print.Description = preventions[j].Description;
                                        preventions[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintFridays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == preventions.Count - 1)
                                        {
                                            indexElement = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region НОВОСТИ
                                if (model.EventListSourceFriday[i].EventName == "НОВОСТИ")
                                {
                                    PrintFriday? print = new PrintFriday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "НОВОСТИ";
                                    print.Id = RandomId;

                                    context.PrintFridays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ПЕРЕРЫВ
                                if (model.EventListSourceFriday[i].EventName == "ПЕРЕРЫВ")
                                {
                                    PrintFriday? print = new PrintFriday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "ПЕРЕРЫВ";
                                    print.Id = RandomId;

                                    context.PrintFridays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ЗАВРЕШЕНИЕ ТРАНСЛЯЦИИ
                                if (model.EventListSourceFriday[i + 1].EventName == "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ")
                                {
                                    PrintFriday? print = new PrintFriday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = nextItemTime.TimeToEfir;
                                    print.EventName = "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ";
                                    print.Id = RandomId;

                                    context.PrintFridays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion
                            }
                        }
                    }
                }

                if (currentTabItem?.Header?.ToString()?.ToLower() == "Суббота".ToLower())
                {
                    using (ApplicationContext? context = new ApplicationContext())
                    {
                        var listEvents = context?.OnSaturday.ToList();
                        var sortedListEventsByTime = listEvents?.OrderBy(x => x.TimeToEfir);

                        if (sortedListEventsByTime == null) break;
                        foreach (var item in sortedListEventsByTime)
                        {
                            model.EventListSourceSaturday.Add(item);
                        }
                        EfirtListOnSaturday.ItemsSource = model.EventListSourceSaturday;

                        //var MinTimeEfir = context?.OnTuesday.ToList().Min(t => t.TimeToEfir);
                        //var MaxTimeEfir = context?.OnTuesday.ToList().Max(t => t.TimeToEfir);

                        for (int i = 0; i < model.EventListSourceSaturday.Count; i++)
                        {
                            if (model.EventListSourceSaturday.Count == 0) MessageBox.Show("Фильмы в базе не найдены, проверьте загружены ли фильмы в базу");

                            if (i < model.EventListSourceSaturday.Count - 1)
                            {
                                //if (model.EventListSourceSaturday[i].EventName == "ПЕРЕРЫВ") continue;

                                // TODO ПРОФИКСИТЬ: если нет последнего события, то не получаю время предыдущего.
                                // TODO Нужны начальные и конечные точки эфира(хотябы конечная)
                                // TODO Варианты: 1. Сделать где-то в верхней части прожграммы два пикера с выбором веремени начала и конца,
                                // TODO 2. сделать два событие и добавить их в список осбытий, они будут константами, но выбор времени будет за пользователем
                                var curItemTime = model.EventListSourceSaturday[i];
                                var nextItemTime = model.EventListSourceSaturday[i + 1];

                                var substractTimeWithinEvents = nextItemTime.TimeToEfir.Subtract(curItemTime.TimeToEfir);

                                int h = substractTimeWithinEvents.Hours * 60;
                                int m = substractTimeWithinEvents.Minutes;
                                int s = substractTimeWithinEvents.Seconds;

                                int totalMinuteEvent = h + m;
                                int totalMinute = totalMinuteEvent;
                                //------------------------------------------поиск контента------------------------------------------//

                                #region ОБРАЗОВАНИЕ
                                if (model.EventListSourceSaturday[i].EventName == "ОБРАЗОВАНИЕ")
                                {
                                    List<Educational> educationals = context.Educationals.ToList();
                                    PrintSaturday? print = new PrintSaturday();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    /* var listSortedByDate = context.Educationals.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                     Educational sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                     int indexElement = educationals.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                     int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                 IfLengthIsOver:*/

                                    Educational? minEducationalTime = context?.Educationals.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minEducationalTime.Duration.Days;

                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, educationals.Count - 1); j < educationals.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = educationals[j].Duration.Hours * 60;
                                        mm = educationals[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        DateTime lastRunnedDate = educationals[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = educationals[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = educationals[j].NumOfSeries > 0 ? educationals[j].Series : 0;
                                        print.Description = "Образование:";
                                        print.Option = educationals[j].Path;
                                        print.Id = RandomId;
                                        educationals[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintSaturdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == educationals.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }

                                }
                                #endregion

                                #region ТЕЛЕПЕРЕДАЧИ
                                if (model.EventListSourceSaturday[i].EventName == "ТЕЛЕПЕРЕДАЧИ")
                                {
                                    PrintSaturday print = new PrintSaturday();
                                    List<TvShow> tvShows = context.TvShows.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;


                                    TvShow? minFilmTime = context?.TvShows.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days;
                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, tvShows.Count - 1); j < tvShows.Count; j++)
                                    {

                                        int maybeDays = 10;

                                        #region Определение времени
                                        hh = tvShows[j].Duration.Hours * 60;
                                        mm = tvShows[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = tvShows[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = tvShows[j].Description.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        bool isNumber = int.TryParse(formattedName, out int fake);

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = isNumber ? "" : formattedName;
                                        print.Series = tvShows[j].NumOfSeries > 0 ? tvShows[j].Series : 0;
                                        print.Description = tvShows[j]?.Name;
                                        print.Option = tvShows[j].Path;
                                        tvShows[j].LastRun = DateTime.Now;
                                        tvShows[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintSaturdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (j == tvShows.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }
                                }
                                #endregion

                                #region ЛЕКЦИИ
                                if (model.EventListSourceSaturday[i].EventName == "ЛЕКЦИИ")
                                {
                                    PrintSaturday? print = new PrintSaturday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();
                                    string possibleDate = "";
                                    LectionGraph? properLection = null;

                                    for (int j = 0; j < 7; j++)
                                    {
                                        if (DateTime.Now.AddDays(j).DayOfWeek.ToString().ToLower() != "Saturday".ToLower()) continue;

                                        possibleDate = DateTime.Now.AddDays(j).ToShortDateString();
                                        properLection =
                                        context?.LectionGraphs.ToList().Find(d => d.LectionDate.ToShortDateString() == possibleDate);
                                    }
                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    if (properLection != null)
                                    {
                                        print.EventName = properLection.Name;
                                        print.Description = properLection.Lecturer;
                                        print.Id = RandomId;

                                        var lectionSplitName = properLection.Name.Split(":");
                                        var strName = lectionSplitName[1].Trim(new Char[] { '»', '.' }).Replace("«", "");
                                        var lection = context?.Lections.ToList().Find(l => l.Name.ToLower().Contains(strName.TrimStart().ToLower()));

                                        print.Option = lection?.Path;
                                    }

                                    context?.PrintSaturdays.Add(print);
                                    context?.SaveChanges();
                                }
                                #endregion

                                #region ФИЛЬМЫ
                                if (model.EventListSourceSaturday[i].EventName == "ФИЛЬМЫ")
                                {
                                    PrintSaturday print = new PrintSaturday();
                                    List<Film> films = context.Films.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    Film? minFilmTime = context?.Films.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days == 0 ? minFilmTime.Duration.Minutes : minFilmTime.Duration.Days;

                                    Random randomContent = new Random();

                                    List<Film> filmList = films.ToList();
                                    Shuffle<Film>(filmList);

                                    for (int j = 0; j < filmList.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = films[j].Duration.Hours * 60;
                                        mm = films[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion


                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = filmList[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = filmList[j].Name.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = filmList[j].NumOfSeries > 0 ? filmList[j].Series : 0;
                                        print.Description = "Фильм:";
                                        print.Option = filmList[j].Path;
                                        films[j].LastRun = DateTime.Now;
                                        films[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;


                                        context?.PrintSaturdays.Add(print);
                                        context?.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (TheRestTime < minFilmDuration) break;
                                    }
                                }
                                #endregion

                                #region СЕРИАЛЫ
                                //int totalMinute = totalMinuteEvent;
                                if (model.EventListSourceSaturday[i].EventName == "СЕРИАЛЫ")
                                {
                                    List<Series> series = context.Serieses.ToList();
                                    PrintSaturday? print = new PrintSaturday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Serieses.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Series sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = series.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                IfLengthIsOver:
                                    for (int j = lastSeries; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = series[j].Duration.Hours * 60;
                                        mm = series[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = series[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = series[j].NumOfSeries > 0 ? series[j].IsSeries : 0;
                                        print.Description = "Сериал:";
                                        print.Option = series[j].Path;
                                        series[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintSaturdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == listSortedByDate.Count() - 1)
                                        {
                                            lastSeries = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region ПРОФИЛАКТИКА
                                if (model.EventListSourceSaturday[i].EventName == "ПРОФИЛАКТИКА")
                                {
                                    List<Prevention> preventions = context.Preventions.ToList();
                                    PrintSaturday? print = new PrintSaturday();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    var listSortedByDate = context.Preventions.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Prevention sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = preventions.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию

                                IfLengthIsOver:
                                    for (int j = indexElement; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = preventions[j].Duration.Hours * 60;
                                        mm = preventions[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = preventions[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Option = preventions[j].Path;
                                        //print.Series = preventions[j].NumOfSeries > 0 ? preventions[j].IsSeries : 0;
                                        print.Description = preventions[j].Description;
                                        preventions[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintSaturdays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == preventions.Count - 1)
                                        {
                                            indexElement = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region НОВОСТИ
                                if (model.EventListSourceSaturday[i].EventName == "НОВОСТИ")
                                {
                                    PrintSaturday? print = new PrintSaturday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "НОВОСТИ";
                                    print.Id = RandomId;

                                    context.PrintSaturdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ПЕРЕРЫВ
                                if (model.EventListSourceSaturday[i].EventName == "ПЕРЕРЫВ")
                                {
                                    PrintSaturday? print = new PrintSaturday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "ПЕРЕРЫВ";
                                    print.Id = RandomId;

                                    context.PrintSaturdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ЗАВРЕШЕНИЕ ТРАНСЛЯЦИИ
                                if (model.EventListSourceSaturday[i + 1].EventName == "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ")
                                {
                                    PrintSaturday? print = new PrintSaturday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = nextItemTime.TimeToEfir;
                                    print.EventName = "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ";
                                    print.Id = RandomId;

                                    context.PrintSaturdays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion
                            }
                        }
                    }
                }

                if (currentTabItem?.Header?.ToString()?.ToLower() == "Воскресение".ToLower())
                {
                    using (ApplicationContext? context = new ApplicationContext())
                    {
                        var listEvents = context?.OnSunday.ToList();
                        var sortedListEventsByTime = listEvents?.OrderBy(x => x.TimeToEfir);

                        if (sortedListEventsByTime == null) break;
                        foreach (var item in sortedListEventsByTime)
                        {
                            model.EventListSourceSunday.Add(item);
                        }
                        EfirtListOnSunday.ItemsSource = model.EventListSourceSunday;

                        //var MinTimeEfir = context?.OnTuesday.ToList().Min(t => t.TimeToEfir);
                        //var MaxTimeEfir = context?.OnTuesday.ToList().Max(t => t.TimeToEfir);

                        for (int i = 0; i < model.EventListSourceSunday.Count; i++)
                        {
                            if (model.EventListSourceSunday.Count == 0) MessageBox.Show("Фильмы в базе не найдены, проверьте загружены ли фильмы в базу");

                            if (i < model.EventListSourceSunday.Count - 1)
                            {
                                //if (model.EventListSourceSunday[i].EventName == "ПЕРЕРЫВ") continue;

                                // xTODO ПРОФИКСИТЬ: если нет последнего события, то не получаю время предыдущего.
                                // xTODO Нужны начальные и конечные точки эфира(хотябы конечная)
                                // xTODO Варианты: 1. Сделать где-то в верхней части прожграммы два пикера с выбором веремени начала и конца,
                                // xTODO 2. сделать два событие и добавить их в список осбытий, они будут константами, но выбор времени будет за пользователем
                                var curItemTime = model.EventListSourceSunday[i];
                                var nextItemTime = model.EventListSourceSunday[i + 1];

                                var substractTimeWithinEvents = nextItemTime.TimeToEfir.Subtract(curItemTime.TimeToEfir);

                                int h = substractTimeWithinEvents.Hours * 60;
                                int m = substractTimeWithinEvents.Minutes;
                                int s = substractTimeWithinEvents.Seconds;

                                int totalMinuteEvent = h + m;
                                int totalMinute = totalMinuteEvent;
                                //------------------------------------------поиск контента------------------------------------------//

                                #region ОБРАЗОВАНИЕ
                                if (model.EventListSourceSunday[i].EventName == "ОБРАЗОВАНИЕ")
                                {
                                    List<Educational> educationals = context.Educationals.ToList();
                                    PrintSunday? print = new PrintSunday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    /*var listSortedByDate = context.Educationals.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Educational sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = educationals.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                IfLengthIsOver:*/

                                    Educational? minEducationalTime = context?.Educationals.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minEducationalTime.Duration.Days;

                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, educationals.Count - 1); j < educationals.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = educationals[j].Duration.Hours * 60;
                                        mm = educationals[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        DateTime lastRunnedDate = educationals[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = educationals[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = educationals[j].NumOfSeries > 0 ? educationals[j].Series : 0;
                                        print.Description = "Образование:";
                                        print.Option = educationals[j].Path;
                                        print.Id = RandomId;
                                        educationals[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintSundays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == educationals.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }

                                }
                                #endregion

                                #region ТЕЛЕПЕРЕДАЧИ
                                if (model.EventListSourceSunday[i].EventName == "ТЕЛЕПЕРЕДАЧИ")
                                {
                                    PrintSunday print = new PrintSunday();
                                    List<TvShow> tvShows = context.TvShows.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    TvShow? minFilmTime = context?.TvShows.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days;
                                    Random randomContent = new Random();

                                ElseRotation:
                                    for (int j = randomContent.Next(0, tvShows.Count - 1); j < tvShows.Count; j++)
                                    {

                                        int maybeDays = 10;

                                        #region Определение времени
                                        hh = tvShows[j].Duration.Hours * 60;
                                        mm = tvShows[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = tvShows[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = tvShows[j].Description.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        bool isNumber = int.TryParse(formattedName, out int fake);

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = isNumber ? "" : formattedName;
                                        print.Series = tvShows[j].NumOfSeries > 0 ? tvShows[j].Series : 0;
                                        print.Description = tvShows[j]?.Name;
                                        print.Option = tvShows[j].Path;
                                        tvShows[j].LastRun = DateTime.Now;
                                        tvShows[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        context.PrintSundays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (j == tvShows.Count && curMinuteEvent > totalMinuteEvent && curMinuteEvent > minFilmDuration)
                                        {
                                            j = 0;
                                            goto ElseRotation;
                                        }
                                    }
                                }
                                #endregion

                                #region ЛЕКЦИИ
                                if (model.EventListSourceSunday[i].EventName == "ЛЕКЦИИ")
                                {
                                    PrintSunday? print = new PrintSunday();
                                    Guid guid = Guid.NewGuid();
                                    ;
                                    string possibleDate = "";
                                    LectionGraph? properLection = null;

                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (DateTime.Now.AddDays(j).DayOfWeek.ToString().ToLower() != "Sunday".ToLower()) continue;

                                        possibleDate = DateTime.Now.AddDays(j).ToShortDateString();
                                        properLection =
                                        context?.LectionGraphs.ToList().Find(d => d.LectionDate.ToShortDateString() == possibleDate);
                                    }
                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    if (properLection != null)
                                    {
                                        string RandomId = guid.ToString();

                                        print.EventName = properLection.Name;
                                        print.Description = properLection.Lecturer;
                                        print.Id = RandomId;

                                        var lectionSplitName = properLection.Name.Split(":");
                                        var strName = lectionSplitName[1].Trim(new Char[] { '»', '.' }).Replace("«", "");
                                        var lection = context?.Lections.ToList().Find(l => l.Name.ToLower().Contains(strName.TrimStart().ToLower()));

                                        print.Option = lection?.Path;

                                        context?.PrintSundays.Add(print);
                                        context?.SaveChanges();
                                    }


                                }
                                #endregion

                                #region ФИЛЬМЫ
                                if (model.EventListSourceSunday[i].EventName == "ФИЛЬМЫ")
                                {
                                    PrintSunday print = new PrintSunday();
                                    List<Film>? films = context.Films.OrderBy(f => f.LastRun).ToList();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;


                                    Film? minFilmTime = context?.Films.ToList().MinBy(f => f.Duration);
                                    int minFilmDuration = minFilmTime.Duration.Days == 0 ? minFilmTime.Duration.Minutes : minFilmTime.Duration.Days;

                                    List<Film> filmList = films.ToList();
                                    Shuffle<Film>(filmList);

                                    for (int j = 0; j < filmList.Count; j++)
                                    {
                                        int maybeDays = 10;
                                        #region Определение времени
                                        hh = films[j].Duration.Hours * 60;
                                        mm = films[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion


                                        if (curMinuteEvent > totalMinuteEvent) continue; // если время фильма больше необходимого, дальше

                                        DateTime lastRunnedDate = filmList[j].LastRun;
                                        int substrucktedDate = DateTime.Now.Subtract(lastRunnedDate).Days;

                                        if (substrucktedDate < maybeDays) continue; // если показывался раньше 10 дней

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = filmList[j].Name.Split(".");
                                        string formattedName = splitName[0];
                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = filmList[j].NumOfSeries > 0 ? filmList[j].Series : 0;
                                        print.Description = "Фильм:";
                                        print.Option = filmList[j].Path;
                                        films[j].LastRun = DateTime.Now;
                                        films[j].NumOfRun += 1;
                                        print.Id = RandomId;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;


                                        context?.PrintSundays.Add(print);
                                        context?.SaveChanges();

                                        TheRestTime = totalMinuteEvent - curMinuteEvent;
                                        totalMinuteEvent = TheRestTime;
                                        elseFilm = true;

                                        if (TheRestTime < minFilmDuration) break;
                                    }
                                }
                                #endregion

                                #region СЕРИАЛЫ
                                //int totalMinute = totalMinuteEvent;
                                if (model.EventListSourceSunday[i].EventName == "СЕРИАЛЫ")
                                {
                                    List<Series> series = context.Serieses.ToList();
                                    PrintSunday? print = new PrintSunday();
                                    bool elseFilm = false;


                                    int hh = 0;
                                    int mm = 0;


                                    var listSortedByDate = context.Serieses.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Series sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = series.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию
                                    int lastSeries = indexElement + 1 == listSortedByDate.Count() ? 0 : (indexElement + 1);
                                IfLengthIsOver:
                                    for (int j = lastSeries; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = series[j].Duration.Hours * 60;
                                        mm = series[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = series[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Series = series[j].NumOfSeries > 0 ? series[j].IsSeries : 0;
                                        print.Description = "Сериал:";
                                        print.Option = series[j].Path;
                                        series[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintSundays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == listSortedByDate.Count() - 1)
                                        {
                                            lastSeries = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region ПРОФИЛАКТИКА
                                if (model.EventListSourceSunday[i].EventName == "ПРОФИЛАКТИКА")
                                {
                                    List<Prevention> preventions = context.Preventions.ToList();
                                    PrintSunday? print = new PrintSunday();
                                    bool elseFilm = false;

                                    int hh = 0;
                                    int mm = 0;

                                    var listSortedByDate = context.Preventions.ToList().OrderBy(s => s.LastRun);//сортирую лист по дате
                                    Prevention sortedLastItemByDate = listSortedByDate.Last(); // получаю последнюю просмотренную серию
                                    int indexElement = preventions.IndexOf(sortedLastItemByDate);// узнаю индекс этой серии в листе такого же вида, в котором ищую эту серию

                                IfLengthIsOver:
                                    for (int j = indexElement; j < listSortedByDate.Count(); j++)
                                    {
                                        #region Определение времени
                                        hh = preventions[j].Duration.Hours * 60;
                                        mm = preventions[j].Duration.Minutes;

                                        int curMinuteEvent = hh + mm;
                                        #endregion

                                        if (curMinuteEvent > totalMinute) continue;

                                        TimeSpan addedTime = TimeSpan.FromMinutes(curMinuteEvent);
                                        string[] splitName = preventions[j].Name.Split(".");
                                        string formattedName = splitName[0];

                                        print.TimeToEfir = !elseFilm ? curItemTime.TimeToEfir : print.TimeToEfir + addedTime;
                                        print.EventName = formattedName;
                                        print.Option = preventions[j].Path;
                                        //print.Series = preventions[j].NumOfSeries > 0 ? preventions[j].IsSeries : 0;
                                        print.Description = preventions[j].Description;
                                        preventions[j].LastRun = DateTime.Now;

                                        if (print.TimeToEfir > nextItemTime.TimeToEfir) break;

                                        Guid guid = Guid.NewGuid();
                                        string RandomId = guid.ToString();

                                        print.Id = RandomId;

                                        context.PrintSundays.Add(print);
                                        context.SaveChanges();

                                        TheRestTime = totalMinute - curMinuteEvent;
                                        totalMinute = TheRestTime;
                                        elseFilm = true;

                                        if (j == preventions.Count - 1)
                                        {
                                            indexElement = 0;
                                            goto IfLengthIsOver;
                                        }
                                    }

                                }
                                #endregion

                                #region НОВОСТИ
                                if (model.EventListSourceSunday[i].EventName == "НОВОСТИ")
                                {
                                    PrintSunday? print = new PrintSunday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "НОВОСТИ";
                                    print.Id = RandomId;

                                    context.PrintSundays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ПЕРЕРЫВ
                                if (model.EventListSourceSunday[i].EventName == "ПЕРЕРЫВ")
                                {
                                    PrintSunday? print = new PrintSunday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = curItemTime.TimeToEfir;
                                    print.EventName = "ПЕРЕРЫВ";
                                    print.Id = RandomId;

                                    context.PrintSundays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion

                                #region ЗАВРЕШЕНИЕ ТРАНСЛЯЦИИ
                                if (model.EventListSourceSunday[i + 1].EventName == "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ")
                                {
                                    PrintSunday? print = new PrintSunday();
                                    Guid guid = Guid.NewGuid();
                                    string RandomId = guid.ToString();

                                    print.TimeToEfir = nextItemTime.TimeToEfir;
                                    print.EventName = "ЗАВЕРШЕНИЕ ТРАНСЛЯЦИИ";
                                    print.Id = RandomId;

                                    context.PrintSundays.Add(print);
                                    context.SaveChanges();
                                }
                                #endregion
                            }
                        }
                    }
                }

                #endregion
            }
        }
        #endregion

        #region КОПИРОВАНИЕ КОНТЕНТА В КОНЕЧНЫЕ ПАПКИ И ЗАПИСЬ В ТЕКСТОВЫЙ ФАЙЛ
        //Создание эфира
        private void CreateEfir_Click(object sender, RoutedEventArgs e)
        {
            ClearPrintModels();
            GenerateEfir();
            WriteEfirAtFile();
            //CopyContentInDest();
        }

        // выбрать путь сохранения эфира(текстовый файл, медиа)
        public void SavePathEfir()
        {
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.AddToMostRecentlyUsedList = true;

            if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                try
                {
                    using (MainWindowViewModel model = new MainWindowViewModel())
                    {
                        model.SavePathEfir = commonOpenFileDialog.FileName;
                        FilePathToSaveEfirTextBox.Text = model.SavePathEfir;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}");
                }

            }
        }

        private void OpenSaveEfirDialog_Click(object sender, RoutedEventArgs e)
        {
            SavePathEfir();
        }

        //Записываю в текстовый файл программу телепередач на неделю
        private void WriteEfirAtFile()
        {
            MainWindowViewModel model = new MainWindowViewModel();

            string nameFile = "Efir.txt";
            string savePath = FilePathToSaveEfirTextBox.Text + "\\" + nameFile;

            using (ApplicationContext context = new ApplicationContext())
            {
                using (StreamWriter fstream = new StreamWriter(savePath, false))
                {
                    string builtedStr = "";
                    string h = "";
                    string m = "";
                    string? desc = "";
                    string name = "";
                    string series = "";
                    string seriesOrPart = "";

                    for (int i = 0; i < 7; i++)
                    {
                        if (DateTime.Now.AddDays(i).DayOfWeek.ToString().ToLower() != "Monday".ToLower()) continue;
                        string possibleDate = DateTime.Now.AddDays(i).ToShortDateString();

                        fstream.WriteLine("Понедельник" + " " + possibleDate);

                        foreach (var item in context.PrintMondays)
                        {
                            if (item != null)
                            {
                                h = item.TimeToEfir.Hours.ToString().Length == 1 ? "0" + item.TimeToEfir.Hours.ToString() : item.TimeToEfir.Hours.ToString();
                                m = item.TimeToEfir.Minutes.ToString().Length == 1 ? "0" + item.TimeToEfir.Minutes.ToString() : item.TimeToEfir.Minutes.ToString();
                                desc = item.Description == null ? item.EventName : item.Description;
                                name = item.EventName;
                                series = item.Series == 0 ? "" : item.Series.ToString();
                                seriesOrPart = desc == "Фильм:" ? series + " часть" : series + " серия";
                            }
                            builtedStr = h + ":" + m + " " + desc + " " + (desc == name ? "" : name) + " " + (item?.Series == 0 ? series : seriesOrPart);


                            fstream.WriteLine(builtedStr);
                        }
                    }

                    fstream.WriteLine("");
                    fstream.WriteLine("-----------------------------------------------------");

                    for (int i = 0; i < 7; i++)
                    {
                        if (DateTime.Now.AddDays(i).DayOfWeek.ToString().ToLower() != "Tuesday".ToLower()) continue;
                        string possibleDate = DateTime.Now.AddDays(i).ToShortDateString();

                        fstream.WriteLine("Вторник" + " " + possibleDate);

                        foreach (var item in context.PrintTuesdays)
                        {
                            if (item != null)
                            {
                                h = item.TimeToEfir.Hours.ToString().Length == 1 ? "0" + item.TimeToEfir.Hours.ToString() : item.TimeToEfir.Hours.ToString();
                                m = item.TimeToEfir.Minutes.ToString().Length == 1 ? "0" + item.TimeToEfir.Minutes.ToString() : item.TimeToEfir.Minutes.ToString();
                                desc = item.Description == null ? item.EventName : item.Description;
                                name = item.EventName;
                                series = item.Series == 0 ? "" : item.Series.ToString();
                                seriesOrPart = desc == "Фильм:" ? series + " часть" : series + " серия";
                            }
                            builtedStr = h + ":" + m + " " + desc + " " + (desc == name ? "" : name) + " " + (item?.Series == 0 ? series : seriesOrPart);


                            fstream.WriteLine(builtedStr);
                        }
                    }

                    fstream.WriteLine("-----------------------------------------------------");

                    for (int i = 0; i < 7; i++)
                    {
                        if (DateTime.Now.AddDays(i).DayOfWeek.ToString().ToLower() != "Wednesday".ToLower()) continue;
                        string possibleDate = DateTime.Now.AddDays(i).ToShortDateString();

                        fstream.WriteLine("Среда" + " " + possibleDate);

                        foreach (var item in context.PrintWednesdays)
                        {
                            if (item != null)
                            {
                                h = item.TimeToEfir.Hours.ToString().Length == 1 ? "0" + item.TimeToEfir.Hours.ToString() : item.TimeToEfir.Hours.ToString();
                                m = item.TimeToEfir.Minutes.ToString().Length == 1 ? "0" + item.TimeToEfir.Minutes.ToString() : item.TimeToEfir.Minutes.ToString();
                                desc = item.Description == null ? item.EventName : item.Description;
                                name = item.EventName;
                                series = item.Series == 0 ? "" : item.Series.ToString();
                                seriesOrPart = desc == "Фильм:" ? series + " часть" : series + " серия";
                            }
                            builtedStr = h + ":" + m + " " + desc + " " + (desc == name ? "" : name) + " " + (item?.Series == 0 ? series : seriesOrPart);


                            fstream.WriteLine(builtedStr);
                        }
                    }

                    fstream.WriteLine("-----------------------------------------------------");

                    for (int i = 0; i < 7; i++)
                    {
                        if (DateTime.Now.AddDays(i).DayOfWeek.ToString().ToLower() != "Thursday".ToLower()) continue;
                        string possibleDate = DateTime.Now.AddDays(i).ToShortDateString();
                        fstream.WriteLine("Четверг" + " " + possibleDate);

                        foreach (var item in context.PrintThursdays)
                        {
                            if (item != null)
                            {
                                h = item.TimeToEfir.Hours.ToString().Length == 1 ? "0" + item.TimeToEfir.Hours.ToString() : item.TimeToEfir.Hours.ToString();
                                m = item.TimeToEfir.Minutes.ToString().Length == 1 ? "0" + item.TimeToEfir.Minutes.ToString() : item.TimeToEfir.Minutes.ToString();
                                desc = item.Description == null ? item.EventName : item.Description;
                                name = item.EventName;
                                series = item.Series == 0 ? "" : item.Series.ToString();
                                seriesOrPart = desc == "Фильм:" ? series + " часть" : series + " серия";
                            }
                            builtedStr = h + ":" + m + " " + desc + " " + (desc == name ? "" : name) + " " + (item?.Series == 0 ? series : seriesOrPart);


                            fstream.WriteLine(builtedStr);
                        }
                    }

                    fstream.WriteLine("-----------------------------------------------------");

                    for (int i = 0; i < 7; i++)
                    {
                        if (DateTime.Now.AddDays(i).DayOfWeek.ToString().ToLower() != "Friday".ToLower()) continue;
                        string possibleDate = DateTime.Now.AddDays(i).ToShortDateString();
                        fstream.WriteLine("Пятница" + " " + possibleDate);

                        foreach (var item in context.PrintFridays)
                        {
                            if (item != null)
                            {
                                h = item.TimeToEfir.Hours.ToString().Length == 1 ? "0" + item.TimeToEfir.Hours.ToString() : item.TimeToEfir.Hours.ToString();
                                m = item.TimeToEfir.Minutes.ToString().Length == 1 ? "0" + item.TimeToEfir.Minutes.ToString() : item.TimeToEfir.Minutes.ToString();
                                desc = item.Description == null ? item.EventName : item.Description;
                                name = item.EventName;
                                series = item.Series == 0 ? "" : item.Series.ToString();
                                seriesOrPart = desc == "Фильм:" ? series + " часть" : series + " серия";
                            }
                            builtedStr = h + ":" + m + " " + desc + " " + (desc == name ? "" : name) + " " + (item?.Series == 0 ? series : seriesOrPart);


                            fstream.WriteLine(builtedStr);
                        }
                    }

                    fstream.WriteLine("-----------------------------------------------------");

                    for (int i = 0; i < 7; i++)
                    {
                        if (DateTime.Now.AddDays(i).DayOfWeek.ToString().ToLower() != "Saturday".ToLower()) continue;
                        string possibleDate = DateTime.Now.AddDays(i).ToShortDateString();
                        fstream.WriteLine("Суббота" + " " + possibleDate);

                        foreach (var item in context.PrintSaturdays)
                        {
                            if (item != null)
                            {
                                h = item.TimeToEfir.Hours.ToString().Length == 1 ? "0" + item.TimeToEfir.Hours.ToString() : item.TimeToEfir.Hours.ToString();
                                m = item.TimeToEfir.Minutes.ToString().Length == 1 ? "0" + item.TimeToEfir.Minutes.ToString() : item.TimeToEfir.Minutes.ToString();
                                desc = item.Description == null ? item.EventName : item.Description;
                                name = item.EventName;
                                series = item.Series == 0 ? "" : item.Series.ToString();
                                seriesOrPart = desc == "Фильм:" ? series + " часть" : series + " серия";
                            }
                            builtedStr = h + ":" + m + " " + desc + " " + (desc == name ? "" : name) + " " + (item?.Series == 0 ? series : seriesOrPart);


                            fstream.WriteLine(builtedStr);
                        }
                    }

                    fstream.WriteLine("-----------------------------------------------------");

                    for (int i = 0; i < 10; i++)
                    {
                        if (i < 4) continue;
                        if (DateTime.Now.AddDays(i).DayOfWeek.ToString().ToLower() != "Sunday".ToLower()) continue;
                        string possibleDate = DateTime.Now.AddDays(i).ToShortDateString();
                        fstream.WriteLine("Воскресенье" + " " + possibleDate);

                        foreach (var item in context.PrintSundays)
                        {
                            if (item != null)
                            {
                                h = item.TimeToEfir.Hours.ToString().Length == 1 ? "0" + item.TimeToEfir.Hours.ToString() : item.TimeToEfir.Hours.ToString();
                                m = item.TimeToEfir.Minutes.ToString().Length == 1 ? "0" + item.TimeToEfir.Minutes.ToString() : item.TimeToEfir.Minutes.ToString();
                                desc = item.Description == null ? item.EventName : item.Description;
                                name = item.EventName;
                                series = item.Series == 0 ? "" : item.Series.ToString();
                                seriesOrPart = desc == "Фильм:" ? series + " часть" : series + " серия";
                            }
                            builtedStr = h + ":" + m + " " + desc + " " + (desc == name ? "" : name) + " " + (item?.Series == 0 ? series : seriesOrPart);


                            fstream.WriteLine(builtedStr);
                        }
                    }
                }
            }
            CopyContentInDest();
        }

        //запись в текстовый документ если нет файла по указанному пути
        private void ErrorPath(string filename, string? sourcepath)
        {
            string pathCreateFile = @"C:\Users\SKTV-1\Desktop\Эфир\ErorPath.txt";
            using (StreamWriter fstream = new StreamWriter(pathCreateFile, false))
            {
                fstream.WriteLine($@"Файла с таким именем {filename} по такому пути {filename} не найдено");
            }

        }

        // Копирование контента в папки
        private void CopyContentInDest()
        {
            string? sourcePath = "";
            string? nameFolder = "";
            string? fileName = "";
            string destPath = FilePathToSaveEfirTextBox.Text;
            string combainPath = "";
            int orderNumber = 0;

            using (ApplicationContext context = new ApplicationContext())
            {

                nameFolder = "Понедельник";
                combainPath = destPath + "\\" + nameFolder;
                if (!Directory.Exists(combainPath)) Directory.CreateDirectory(combainPath);

                foreach (var item in context.PrintMondays)
                {
                    if (item.Option != null)
                    {
                        sourcePath = item.Option;
                        string[] splitPath = item.Option.Split("\\");
                        fileName = splitPath[splitPath.Length - 1];
                        orderNumber += 1;
                    }

                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, combainPath + "\\" + orderNumber + " " + fileName, true);
                    }
                }
                sourcePath = "";
                nameFolder = "";
                fileName = "";
                combainPath = "";
                orderNumber = 0;

                nameFolder = "Вторник";
                combainPath = destPath + "\\" + nameFolder;
                if (!Directory.Exists(combainPath)) Directory.CreateDirectory(combainPath);

                foreach (var item in context.PrintTuesdays)
                {
                    if (item.Option != null)
                    {
                        sourcePath = item.Option;
                        string[] splitPath = item.Option.Split("\\");
                        fileName = splitPath[splitPath.Length - 1];
                        orderNumber += 1;
                    }

                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, combainPath + "\\" + orderNumber + " " + fileName, true);
                    }

                }
                sourcePath = "";
                nameFolder = "";
                fileName = "";
                combainPath = "";
                orderNumber = 0;


                nameFolder = "Среда";
                combainPath = destPath + "\\" + nameFolder;
                if (!Directory.Exists(combainPath)) Directory.CreateDirectory(combainPath);

                foreach (var item in context.PrintWednesdays)
                {
                    if (item.Option != null)
                    {
                        sourcePath = item.Option;
                        string[] splitPath = item.Option.Split("\\");
                        fileName = splitPath[splitPath.Length - 1];
                        orderNumber += 1;
                    }

                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, combainPath + "\\" + orderNumber + " " + fileName, true);
                    }

                }
                sourcePath = "";
                nameFolder = "";
                fileName = "";
                combainPath = "";
                orderNumber = 0;


                nameFolder = "Четверг";
                combainPath = destPath + "\\" + nameFolder;
                if (!Directory.Exists(combainPath)) Directory.CreateDirectory(combainPath);

                foreach (var item in context.PrintThursdays)
                {
                    if (item.Option != null)
                    {
                        sourcePath = item.Option;
                        string[] splitPath = item.Option.Split("\\");
                        fileName = splitPath[splitPath.Length - 1];
                        orderNumber += 1;
                    }

                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, combainPath + "\\" + orderNumber + " " + fileName, true);
                    }
                }
                sourcePath = "";
                nameFolder = "";
                fileName = "";
                combainPath = "";
                orderNumber = 0;


                nameFolder = "Пятница";
                combainPath = destPath + "\\" + nameFolder;
                if (!Directory.Exists(combainPath)) Directory.CreateDirectory(combainPath);

                foreach (var item in context.PrintFridays)
                {
                    if (item.Option != null)
                    {
                        sourcePath = item.Option;
                        string[] splitPath = item.Option.Split("\\");
                        fileName = splitPath[splitPath.Length - 1];
                        orderNumber += 1;
                    }

                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, combainPath + "\\" + orderNumber + " " + fileName, true);
                    }
                }
                sourcePath = "";
                nameFolder = "";
                fileName = "";
                combainPath = "";
                orderNumber = 0;

                nameFolder = "Суббота";
                combainPath = destPath + "\\" + nameFolder;
                if (!Directory.Exists(combainPath)) Directory.CreateDirectory(combainPath);

                foreach (var item in context.PrintSaturdays)
                {
                    if (item.Option != null)
                    {
                        sourcePath = item.Option;
                        string[] splitPath = item.Option.Split("\\");
                        fileName = splitPath[splitPath.Length - 1];
                        orderNumber += 1;
                    }

                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, combainPath + "\\" + orderNumber + " " + fileName, true);
                    }
                }
                sourcePath = "";
                nameFolder = "";
                fileName = "";
                combainPath = "";
                orderNumber = 0;

                nameFolder = "Воскресенье";
                combainPath = destPath + "\\" + nameFolder;
                if (!Directory.Exists(combainPath)) Directory.CreateDirectory(combainPath);

                foreach (var item in context.PrintSundays)
                {
                    if (item.Option != null)
                    {
                        sourcePath = item.Option;
                        string[] splitPath = item.Option.Split("\\");
                        fileName = splitPath[splitPath.Length - 1];
                        orderNumber += 1;
                    }

                    if (File.Exists(sourcePath))
                    {
                        File.Copy(sourcePath, combainPath + "\\" + orderNumber + " " + fileName, true);
                    }
                }

            }

        }

        #endregion

        // добавляю и сохраняю макет эфира
        private void SaveEfir_Click(object sender, RoutedEventArgs e)
        {
            //TODO ОБЯЗАТЕЛЬНО СДЕЛАТЬ ПРОВЕРКУ ЕСТЬ ЛИ В БАЗЕ КОНТЕНТ!!!
            using (ApplicationContext context = new ApplicationContext())
            {
                MainWindowViewModel model = new MainWindowViewModel();

                if (context.Films.Count() == 0 || context.Serieses.Count() == 0)
                {
                    MessageBox.Show("Проверьте, указаны ли пути к контенту");
                    return;
                }

                #region Отсальные дни заполнить по поенедельнику, если пустые

                if (context.OnTuesday.Count() == 0 && context.OnWednesday.Count() == 0
                && context.OnThursday.Count() == 0 && context.OnFriday.Count() == 0
                && context.OnSaturday.Count() == 0 && context.OnSunday.Count() == 0)
                {
                    foreach (var itemEvent in context.OnMonday)
                    {
                        #region Перезапись для вторника
                        EfirOnTuesday efirTuesday = new EfirOnTuesday();
                        efirTuesday.TimeToEfir = itemEvent.TimeToEfir;
                        efirTuesday.EventName = itemEvent.EventName;
                        efirTuesday.Description = itemEvent.Description;
                        efirTuesday.Option = itemEvent.Option;

                        context.OnTuesday.Add(efirTuesday);
                        context.SaveChanges();
                        foreach (var item in context.OnTuesday.ToList())
                        {
                            model.EventListSourceTuesday.Add(item);
                        }
                        EfirListOnTuesday.ItemsSource = model.EventListSourceMonday;
                        #endregion

                        #region Перезапись для среды
                        EfirOnWednesday efirWednesday = new EfirOnWednesday();
                        efirWednesday.TimeToEfir = itemEvent.TimeToEfir;
                        efirWednesday.EventName = itemEvent.EventName;
                        efirWednesday.Description = itemEvent.Description;
                        efirWednesday.Option = itemEvent.Option;

                        context.OnWednesday.Add(efirWednesday);
                        context.SaveChanges();
                        foreach (var item in context.OnWednesday.ToList())
                        {
                            model.EventListSourceWednesday.Add(item);
                        }
                        EfirListOnWednesday.ItemsSource = model.EventListSourceMonday;
                        #endregion

                        #region Перезапись для четверга
                        EfirOnThursday efirThursday = new EfirOnThursday();
                        efirThursday.TimeToEfir = itemEvent.TimeToEfir;
                        efirThursday.EventName = itemEvent.EventName;
                        efirThursday.Description = itemEvent.Description;
                        efirThursday.Option = itemEvent.Option;

                        context.OnThursday.Add(efirThursday);
                        context.SaveChanges();
                        foreach (var item in context.OnThursday.ToList())
                        {
                            model.EventListSourceThursday.Add(item);
                        }
                        EfirListOnThursday.ItemsSource = model.EventListSourceMonday;
                        #endregion

                        #region Перезапись для пятницы
                        EfirOnFriday efirFriday = new EfirOnFriday();
                        efirFriday.TimeToEfir = itemEvent.TimeToEfir;
                        efirFriday.EventName = itemEvent.EventName;
                        efirFriday.Description = itemEvent.Description;
                        efirFriday.Option = itemEvent.Option;

                        context.OnFriday.Add(efirFriday);
                        context.SaveChanges();
                        foreach (var item in context.OnFriday.ToList())
                        {
                            model.EventListSourceFriday.Add(item);
                        }
                        EfirListOnFriday.ItemsSource = model.EventListSourceMonday;
                        #endregion

                        #region Перезапись для субботы
                        EfirOnSaturday efirSaturday = new EfirOnSaturday();
                        efirSaturday.TimeToEfir = itemEvent.TimeToEfir;
                        efirSaturday.EventName = itemEvent.EventName;
                        efirSaturday.Description = itemEvent.Description;
                        efirSaturday.Option = itemEvent.Option;

                        context.OnSaturday.Add(efirSaturday);
                        context.SaveChanges();
                        foreach (var item in context.OnSaturday.ToList())
                        {
                            model.EventListSourceSaturday.Add(item);
                        }
                        EfirtListOnSaturday.ItemsSource = model.EventListSourceMonday;
                        #endregion

                        #region Перезапись для воскресения
                        EfirOnSunday efirSunday = new EfirOnSunday();
                        efirSunday.TimeToEfir = itemEvent.TimeToEfir;
                        efirSunday.EventName = itemEvent.EventName;
                        efirSunday.Description = itemEvent.Description;
                        efirSunday.Option = itemEvent.Option;

                        context.OnSunday.Add(efirSunday);
                        context.SaveChanges();
                        foreach (var item in context.OnSunday.ToList())
                        {
                            model.EventListSourceSunday.Add(item);
                        }
                        EfirtListOnSunday.ItemsSource = model.EventListSourceMonday;
                        #endregion
                    }
                }
                #endregion

                UpdateModelsView();
            }
        }

        // сохраняю изменения смены порядка эвентов
        private void CapturedReorderEventItem_Drop(object sender, DragEventArgs e)
        {
            //TODO В этом методе надо сделать обновление моделей, если был изменен порядок в ручную(если перетащил событие)
            using (MainWindowViewModel model = new MainWindowViewModel())
            {
                //EfirListOnMonday.UpdateLayout;


                /*  foreach (var item in model.EventListSourceMonday)
                {
                model?.EventListSourceMonday?.Add(item);
                }
                EfirListOnMonday.ItemsSource = model.EventListSourceMonday;*/
            }
        }

        #region Обновление отображения моделей (обновление в базе)
        private void UpdateModelsView()
        {

            //TODO Доделать сортировку отображаемых данных для всех дней
            //TODO Убрать вызов объекта из общего в каждый юзинг
            MainWindowViewModel model = new MainWindowViewModel();

            //Понедельник
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsMonday = context?.OnMonday.ToList();
                var sortedListEventsByTimeMonday = listEventsMonday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeMonday == null) return;
                foreach (var item in sortedListEventsByTimeMonday)
                {
                    model.EventListSourceMonday.Add(item);
                }
                EfirListOnMonday.ItemsSource = model.EventListSourceMonday;
            }


            // Вторник
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsTuesday = context?.OnTuesday.ToList();
                var sortedListEventsByTimeTuesday = listEventsTuesday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeTuesday == null) return;
                foreach (var item in sortedListEventsByTimeTuesday)
                {
                    model.EventListSourceTuesday.Add(item);
                }
                EfirListOnTuesday.ItemsSource = model.EventListSourceTuesday;
            }

            //Среда
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsWednesday = context?.OnWednesday.ToList();
                var sortedListEventsByTimeWednesday = listEventsWednesday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeWednesday == null) return;
                foreach (var item in sortedListEventsByTimeWednesday)
                {
                    model.EventListSourceWednesday.Add(item);
                }
                EfirListOnWednesday.ItemsSource = model.EventListSourceWednesday;
            }

            //четврег
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsThursday = context?.OnThursday.ToList();
                var sortedListEventsByTimeThursday = listEventsThursday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeThursday == null) return;
                foreach (var item in sortedListEventsByTimeThursday)
                {
                    model.EventListSourceThursday.Add(item);
                }
                EfirListOnThursday.ItemsSource = model.EventListSourceThursday;
            }

            //Пятница
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsFriday = context?.OnFriday.ToList();
                var sortedListEventsByTimeFriday = listEventsFriday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeFriday == null) return;
                foreach (var item in sortedListEventsByTimeFriday)
                {
                    model.EventListSourceFriday.Add(item);
                }
                EfirListOnFriday.ItemsSource = model.EventListSourceFriday;
            }

            //Суббота
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsSaturday = context?.OnSaturday.ToList();
                var sortedListEventsByTimeSaturday = listEventsSaturday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeSaturday == null) return;
                foreach (var item in sortedListEventsByTimeSaturday)
                {
                    model.EventListSourceSaturday.Add(item);
                }
                EfirtListOnSaturday.ItemsSource = model.EventListSourceSaturday;
            }

            //Воскресение
            using (ApplicationContext context = new ApplicationContext())
            {
                var listEventsSunday = context?.OnSunday.ToList();
                var sortedListEventsByTimeSunday = listEventsSunday?.OrderBy(x => x.TimeToEfir);

                if (sortedListEventsByTimeSunday == null) return;
                foreach (var item in sortedListEventsByTimeSunday)
                {
                    model.EventListSourceSunday.Add(item);
                }
                EfirtListOnSunday.ItemsSource = model.EventListSourceSunday;
            }
        }
        #endregion

        #region ОБНУЛЕНИЕ МОДЕЛЕЙ
        //Отчистка моделей программы телепередач
        private void ClearPrintModels()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                //TODO Переделать удаление значений в полях использую встроенные методы
                #region Перед созданием эфира отчищаю все модели в базе
                foreach (var item in context.PrintMondays.ToList())
                {
                    context.PrintMondays.Remove(item);
                }
                foreach (var item in context.PrintTuesdays.ToList())
                {
                    context.PrintTuesdays.Remove(item);
                }
                foreach (var item in context.PrintWednesdays.ToList())
                {
                    context.PrintWednesdays.Remove(item);
                }
                foreach (var item in context.PrintThursdays.ToList())
                {
                    context.PrintThursdays.Remove(item);
                }
                foreach (var item in context.PrintFridays.ToList())
                {
                    context.PrintFridays.Remove(item);
                }
                foreach (var item in context.PrintSaturdays.ToList())
                {
                    context.PrintSaturdays.Remove(item);
                }
                foreach (var item in context.PrintSundays.ToList())
                {
                    context.PrintSundays.Remove(item);
                }
                context.SaveChanges();
                #endregion
            }

        }

        // обнуление моделей с контентом
        private void ClearContentModels()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                foreach (var item in context.Films.ToList())
                {
                    context.Films.Remove(item);
                }
                foreach (var item in context.Serieses.ToList())
                {
                    context.Serieses.Remove(item);
                }
                foreach (var item in context.Documentarieses.ToList())
                {
                    context.Documentarieses.Remove(item);
                }
                foreach (var item in context.Preventions.ToList())
                {
                    context.Preventions.Remove(item);
                }
                foreach (var item in context.Entertainments.ToList())
                {
                    context.Entertainments.Remove(item);
                }
                foreach (var item in context.Educationals.ToList())
                {
                    context.Educationals.Remove(item);
                }
                foreach (var item in context.TvShows.ToList())
                {
                    context.TvShows.Remove(item);
                }
                context.SaveChanges();
            }

        }


        #endregion


    }
}


