using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace ErrorWpf
{
    public class ItemDataGrid
    {
        public string DataService { get; set; }
        public string Doctor { get; set; }
        public string Clinic { get; set; }
        public string DataBirday { get; set; }
        public string Polic { get; set; }
        public string Pathient { get; set; }
        public int MedService { get; set; }

        public void clearTable(DataGrid dg) {
            dg.ItemsSource = null;
        }

        public void addNameColums(DataGrid dg) {
            DataGridTextColumn textColumn = new DataGridTextColumn();
            DataGridTextColumn textColumn2 = new DataGridTextColumn();
            DataGridTextColumn textColumn3 = new DataGridTextColumn();
            DataGridTextColumn textColumn4 = new DataGridTextColumn();
            DataGridTextColumn textColumn5 = new DataGridTextColumn();
            DataGridTextColumn textColumn6 = new DataGridTextColumn();

            textColumn.Header = "Дата Услуги";            
            textColumn2.Header = "Доктор";
            textColumn3.Header = "Поликлиника";
            textColumn4.Header = "Дата Рождения";
            textColumn5.Header = "Полис";
            textColumn6.Header = "Пациент";

            textColumn.Binding = new Binding("DataService");
            textColumn2.Binding = new Binding("Doctor");
            textColumn3.Binding = new Binding("Clinic");
            textColumn4.Binding = new Binding("DataBirday");
            textColumn5.Binding = new Binding("Polic");
            textColumn6.Binding = new Binding("Pathient");

            dg.Columns.Add(textColumn);
            dg.Columns.Add(textColumn2);
            dg.Columns.Add(textColumn3);
            dg.Columns.Add(textColumn4);
            dg.Columns.Add(textColumn5);
            dg.Columns.Add(textColumn6);
        }
    }
}
