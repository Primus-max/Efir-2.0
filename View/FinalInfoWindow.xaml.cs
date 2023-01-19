using Efir.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;
using static Efir.MainWindow;

namespace Efir.View
{
    /// <summary>
    /// Interaction logic for FinalInfoWindow.xaml
    /// </summary>
    public partial class FinalInfoWindow : Window
    {
        public ListView ListViewWrongFiles;


        public FinalInfoWindow()
        {
            InitializeComponent();
            ListViewWrongFiles = ListWrongFiles;
        }

        private void DeleteWrongFiles_CLick(object sender, RoutedEventArgs e)
        {
            WrongFile.DeletWrongFiles();
            SaccessText.Text = "Все файлы успешно удалены";
        }

    }
}
