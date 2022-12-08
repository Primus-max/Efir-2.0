using Efir.Data;
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
using System.Windows.Shapes;

namespace Efir
{
    /// <summary>
    /// Interaction logic for EfirTable.xaml
    /// </summary>
    public partial class EfirTable : Window
    {
        public EfirTable()
        {
            InitializeComponent();

            using (ApplicationContext context = new ApplicationContext())
            {
                var query =
                from product in context.PrintMondays
                select new { Time = product.TimeToEfir, Desc = product.Description == null ? product.EventName : product.Description, Name = product.EventName, Series = product.Series.ToString() == "0" ? "" : product.Series.ToString() + " серия" };


                GridTest.ItemsSource = null;
                GridTest.ItemsSource = query.ToList();
                GridTest.HeadersVisibility = DataGridHeadersVisibility.None;

                /* var query2 =
                 from product in context.PrintTuesdays
                 select new { Time = product.TimeToEfir, Desc = product.Description == null ? product.EventName : product.Description, Name = product.EventName, Series = product.Series.ToString() == "0" ? "" : product.Series.ToString() + " серия" };


                 GridTest2.ItemsSource = null;
                 GridTest2.ItemsSource = query.ToList();
                 GridTest2.HeadersVisibility = DataGridHeadersVisibility.None;*/
            }
        }
    }
}
