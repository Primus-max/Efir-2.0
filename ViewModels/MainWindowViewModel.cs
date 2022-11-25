﻿using Efir.ViewModels.Base;
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


        /* private int _CountPartFilm = 1;
        /// <summary>
            /// Индикатор загрузки для сериалов
        /// </summary>
        public int CountPartFilm
        {
get => _CountPartFilm;
set => Set(ref _CountPartFilm, value);
                }
                */


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
