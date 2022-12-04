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

        private string _StartEfir = "НАЧАЛО ТРАНСЛЯЦИИ";
        public string StartEfir
        {
            get => _StartEfir;
        }

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

        private string _EndEfir = "ЗАВРЕШЕНИЕ ТРАНСЛЯЦИИ";
        public string EndEfir
        {
            get => _EndEfir;
        }

        #region Коллекции для отображения событий в дне
        private ObservableCollection<EfirOnMonday> _EventListSourceMonday = new ObservableCollection<EfirOnMonday>();
        public ObservableCollection<EfirOnMonday> EventListSourceMonday
        {
            get => _EventListSourceMonday;
            set => Set(ref _EventListSourceMonday, value);
        }

        private ObservableCollection<EfirOnTuesday> _EventListSourceTuesday = new ObservableCollection<EfirOnTuesday>();
        public ObservableCollection<EfirOnTuesday> EventListSourceTuesday
        {
            get => _EventListSourceTuesday;
            set => Set(ref _EventListSourceTuesday, value);
        }

        private ObservableCollection<EfirOnWednesday> _EventListSourceWednesday = new ObservableCollection<EfirOnWednesday>();
        public ObservableCollection<EfirOnWednesday> EventListSourceWednesday
        {
            get => _EventListSourceWednesday;
            set => Set(ref _EventListSourceWednesday, value);
        }

        private ObservableCollection<EfirOnThursday> _EventListSourceThursday = new ObservableCollection<EfirOnThursday>();
        public ObservableCollection<EfirOnThursday> EventListSourceThursday
        {
            get => _EventListSourceThursday;
            set => Set(ref _EventListSourceThursday, value);
        }

        private ObservableCollection<EfirOnFriday> _EventListSourceFriday = new ObservableCollection<EfirOnFriday>();
        public ObservableCollection<EfirOnFriday> EventListSourceFriday
        {
            get => _EventListSourceFriday;
            set => Set(ref _EventListSourceFriday, value);
        }

        private ObservableCollection<EfirOnSaturday> _EventListSourceSaturday = new ObservableCollection<EfirOnSaturday>();
        public ObservableCollection<EfirOnSaturday> EventListSourceSaturday
        {
            get => _EventListSourceSaturday;
            set => Set(ref _EventListSourceSaturday, value);
        }

        private ObservableCollection<EfirOnSunday> _EventListSourceSunday = new ObservableCollection<EfirOnSunday>();
        public ObservableCollection<EfirOnSunday> EventListSourceSunday
        {
            get => _EventListSourceSunday;
            set => Set(ref _EventListSourceSunday, value);
        }

        #endregion





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
