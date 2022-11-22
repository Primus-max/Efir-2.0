using Efir.ViewModels.Base;
using System;
using System.Collections.Generic;
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

        #endregion


    }
}
