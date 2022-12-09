using Efir.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
using Word = Microsoft.Office.Interop.Word;

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


                //GridTest.HeadersVisibility = DataGridHeadersVisibility.None;

                /*var query2 =
                 from product in context.PrintTuesdays
                 select new { Time = product.TimeToEfir, Desc = product.Description == null ? product.EventName : product.Description, Name = product.EventName, Series = product.Series.ToString() == "0" ? "" : product.Series.ToString() + " серия" };


                GridTest2.ItemsSource = null;
                GridTest2.ItemsSource = query.ToList();
                GridTest2.HeadersVisibility = DataGridHeadersVisibility.None;*/


            }
            Teset();

        }

        private void Teset()
        {


            var asdf = GridTest.Items;
            var sdfgsdfg = GridTest.Columns;

            GridTest.SelectAllCells();
            GridTest.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, GridTest);
            GridTest.UnselectAllCells();
            var result = (string)Clipboard.GetData(GridTest.ToString());

            dynamic wordApp = null;
            try
            {

                var sw = new StreamWriter("test.doc");
                sw.WriteLine(result);
                sw.Close();
                //var proc = Process.Start("export.doc");
                Type? wordType = Type.GetTypeFromProgID("Word.Application");
                wordApp = Activator.CreateInstance(wordType);
                wordApp?.Documents.Add(System.AppDomain.CurrentDomain.BaseDirectory + "test.doc");

                wordApp.ActiveDocument.Range.ConvertToTable(1, GridTest.Items.Count, GridTest.Columns.Count);
                wordApp.Visible = true;
            }
            catch (Exception ex)
            {
                if (wordApp != null)
                {
                    wordApp.Quit();
                }
                // ignored
            }
        }


        /* public void Export_Data_To_Word(DataGrid DGV, string filename)
         {
             DGV.Ce
             if (DGV.Rows.Count != 0)
             {
                 int RowCount = DGV.Rows.Count;
                 int ColumnCount = DGV.Columns.Count;
                 Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                 //Добавление строк и ячеек
                 int r = 0;
                 for (int c = 0; c <= ColumnCount - 1; c++)
                 {
                     for (r = 0; r <= RowCount - 1; r++)
                     {
                         DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                     }
                 }

                 Word.Document oDoc = new Word.Document();
                 oDoc.Application.Visible = true;

                 //Ориентация листа
                 oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                 dynamic oRange = oDoc.Content.Application.Selection.Range;
                 string oTemp = "";
                 for (r = 0; r <= RowCount - 1; r++)
                 {
                     for (int c = 0; c <= ColumnCount - 1; c++)
                     {
                         oTemp = oTemp + DataArray[r, c] + "\t";

                     }
                 }

                 //Формат таблицы
                 oRange.Text = oTemp;
                 object oMissing = Missing.Value;
                 object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                 object ApplyBorders = true;
                 object AutoFit = true;
                 object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                 oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                       Type.Missing, Type.Missing, ref ApplyBorders,
                                       Type.Missing, Type.Missing, Type.Missing,
                                       Type.Missing, Type.Missing, Type.Missing,
                                       Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                 oRange.Select();

                 oDoc.Application.Selection.Tables[1].Select();
                 oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                 oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                 oDoc.Application.Selection.Tables[1].Rows[1].Select();
                 oDoc.Application.Selection.InsertRowsAbove(1);
                 oDoc.Application.Selection.Tables[1].Rows[1].Select();

                 //Стиль заголовка таблицы
                 oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 2;
                 oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                 oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                 //add header row manually
                 for (int c = 0; c <= ColumnCount - 1; c++)
                 {
                     oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                 }

                 //Стили таблицы
                 oDoc.Application.Selection.Tables[1].Rows[1].Select();
                 oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                 oDoc.Application.Selection.Tables[1].Borders.Enable = 1;



                 //Текст шапки
                 foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Application.ActiveDocument.Sections)
                 {
                     Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                     headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                     headerRange.Text = "Заявка на закупку картриджа";
                     headerRange.Font.Size = 16;
                     headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                 }

                 //Сохранение файла

                 oDoc.SaveAs(filename, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing);
             }
         }
         private void button1_Click(object sender, EventArgs e)
         {
             SaveFileDialog sfd = new SaveFileDialog();

             sfd.Filter = "Word Documents (*.docx)|*.docx";

             sfd.FileName = "Запрос на покупку.docx";

             if (sfd.ShowDialog() == DialogResult.OK)
             {
                 Export_Data_To_Word(GridTest, sfd.FileName);
             }
         }*/
    }
}
