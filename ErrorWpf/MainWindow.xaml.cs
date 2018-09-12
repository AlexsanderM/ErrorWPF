using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ErrorWpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            textBoxFind.Text = ofd.FileName.Replace("\\", "\\\\"); 
        }

        private void GetBase_Click(object sender, RoutedEventArgs e)
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textBoxFind.Text + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + "Export-1" + "$]";
                    comm.Connection = conn;
                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        DataSet excelDataSet = new DataSet();
                        da.SelectCommand = comm;
                        da.Fill(excelDataSet);
                        dataGridMed.ItemsSource = excelDataSet.Tables[0].DefaultView;
                    }
                }
            }
        }

        private void OverService_Click(object sender, RoutedEventArgs e)
        {
            String[] dataBirdayArr = new String[dataGridMed.Items.Count];
            String[] polisArr = new String[dataGridMed.Items.Count];
            int[] medServiceArr = new int[dataGridMed.Items.Count];

            for (int i = 0; i < dataGridMed.Items.Count - 1; i++)
            {
                try
                {
                    DataRowView drv = dataGridMed.Items[i] as DataRowView;

                    dataBirdayArr[i] = drv[0].ToString();
                    polisArr[i] = drv[5].ToString();
                    medServiceArr[i] = Convert.ToInt32(drv[18].ToString());

                    //Console.WriteLine(polisArr[i] + dataBirdayArr[i]);
                }
                catch (Exception)
                {                    
                    medServiceArr[i] = i;
                }

                //dataGridMed.DataContext = null;
                //dataGridMed.Items.Clear();                
            }

            for (int z = 0; z < dataGridMed.Items.Count - 1; z++)
            {
                if (medServiceArr[z] == 1001)
                {
                    for (int y = z + 1; y < dataGridMed.Items.Count - 1; y++)
                    {
                        if (medServiceArr[y] == 1001 && String.Compare(polisArr[z], polisArr[y]) == 0)
                        {
                            Console.WriteLine(medServiceArr[z] + " " + polisArr[z] + " " + dataBirdayArr[z]);
                            Console.WriteLine(medServiceArr[y] + " " + polisArr[y] + " " + dataBirdayArr[y]);

                        }
                    }
                }
            }
        }
    }
}
