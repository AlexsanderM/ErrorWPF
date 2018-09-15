using ErrorWpf.Data;
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
            ItemDataGrid itemDG = new ItemDataGrid();
            DataServiceDay dataServiceDay = new DataServiceDay();

            int lengtRowsGrid = dataGridMed.Items.Count - 1;
            int[] servicePrimaryArr = dataServiceDay.getServiceArr();

            String[] dataServiceArr = new String[lengtRowsGrid];
            String[] doctorArr = new String[lengtRowsGrid];
            String[] clinicArr = new String[lengtRowsGrid];
            String[] dataBirdayArr = new String[lengtRowsGrid];
            String[] polisArr = new String[lengtRowsGrid];
            String[] pathientArr = new String[lengtRowsGrid];
            Char[] sex = new char[lengtRowsGrid];
            int[] medServiceArr = new int[lengtRowsGrid];

            for (int i = 0; i < lengtRowsGrid; i++)
            {
                try
                {
                    DataRowView drv = dataGridMed.Items[i] as DataRowView;

                    dataServiceArr[i] = drv[0].ToString();
                    doctorArr[i] = drv[10].ToString();
                    clinicArr[i] = drv[3].ToString();
                    dataBirdayArr[i] = drv[4].ToString();
                    polisArr[i] = drv[5].ToString();
                    pathientArr[i] = drv[7].ToString();
                    sex[i] = Convert.ToChar(drv[6]);
                    medServiceArr[i] = Convert.ToInt32(drv[18]);
                }
                catch (Exception)
                {
                    pathientArr[i] = $"i";
                    medServiceArr[i] = i;
                }                
            }

            itemDG.clearTable(dataGridMed);
            itemDG.addNameColums(dataGridMed);

            for (int i = 0; i < servicePrimaryArr.Length; i++)
            {
                for (int z = 0; z < lengtRowsGrid; z++)
                {
                    if (medServiceArr[z] == servicePrimaryArr[i])
                    {
                        for (int y = z + 1; y < lengtRowsGrid; y++)
                        {
                            if (medServiceArr[y] == servicePrimaryArr[i] && String.Compare(polisArr[z], polisArr[y]) == 0)
                            {
                                dataGridMed.Items.Add(new ItemDataGrid()
                                {
                                    DataService = dataServiceArr[z],
                                    Doctor = doctorArr[z],
                                    Clinic = clinicArr[z],
                                    DataBirday = dataBirdayArr[z],
                                    Polic = polisArr[z],
                                    Pathient = pathientArr[z],
                                    Sex = sex[z],
                                    MedService = medServiceArr[z]
                                });

                                dataGridMed.Items.Add(new ItemDataGrid()
                                {
                                    DataService = dataServiceArr[y],
                                    Doctor = doctorArr[y],
                                    Clinic = clinicArr[y],
                                    DataBirday = dataBirdayArr[y],
                                    Polic = polisArr[y],
                                    Pathient = pathientArr[y],
                                    Sex = sex[y],
                                    MedService = medServiceArr[y],
                                    Remove = "Исключить"
                                });
                            }
                        }
                    }
                }
            }
        }
    }
}
