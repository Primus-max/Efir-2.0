using Efir.Model;
using Efir.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Efir.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel
    {
        private string _Title;
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }


        #region Список названий всех событий


        private string _LectionEvent = "ЛЕКЦИИ";
        public string LectionEvent
        {
            get => _LectionEvent;
        }

        private string _FilmsEvent = "ФИЛЬМЫ";
        public string FilmsEvent
        {
            get => _FilmsEvent;
        }

        private string _SeriesEvent = "СЕРИАЛЫ";
        public string SeriesEvent
        {
            get => _SeriesEvent;
        }
        private string _PreventionEvent = "ПРОФИЛАКТИКА";
        public string PreventionEvent
        {
            get => _PreventionEvent;
        }
        private string _TvShowEvent = "ТЕЛЕПЕРЕДАЧИ";
        public string TvShowEvent
        {
            get => _TvShowEvent;
        }
        private string _Break = "ПЕРЕРЫВ";
        public string Break
        {
            get => _Break;
        }
        private string _News = "НОВОСТИ";
        public string News
        {
            get => _News;
        }

        /// <summary>
        /// Получаю список всех событий, имя и булиного значение
        /// </summary>          
        public ObservableCollection<Event> EventList()
        {
            return new ObservableCollection<Event>
          {
              new Event(LectionEvent, 1),
              new Event(FilmsEvent, 1),
              new Event (SeriesEvent, 1),
              new Event(PreventionEvent, 1),
              new Event(TvShowEvent, 1),
              new Event(Break, 1),
              new Event(News, 1),
          };

        }



        #endregion


        #region БЛОК МЕДИА

        #region прогресс бар
        private int _ValueProgressDownlaodingSeries = 10;
        /// <summary>
        /// Индикатор загрузки для сериалов
        /// </summary>
        public int ValueProgressDownlaodingSeries
        {
            get => _ValueProgressDownlaodingSeries;
            set => Set(ref _ValueProgressDownlaodingSeries, value);
        }
        #endregion

        private ObservableCollection<string> _testList = new ObservableCollection<string>();
        public ObservableCollection<string> testList
        {
            get => _testList;
            set => Set(ref _testList, value);
        }



        #region текст боксы для путей


        private string _FilePathToDocumentariesextBox;
        /// <summary>
        /// Хранение пути для текст бокса
        /// </summary>
        public string FilePathToDocumentariesextBox
        {
            get => _FilePathToDocumentariesextBox;
            set => Set(ref _FilePathToDocumentariesextBox, value);
        }
        #endregion

        #endregion


        public MainWindowViewModel()
        {
            for (int i = 0; i < 10; i++)
            {
                testList.Add("ТЕст" + i.ToString());
            }



        }
    }
}
