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
    public partial class MainWindow : Window
    {
        int lengtRowsGrid;
        int[] serviceArr;
        String[] dataServiceArr;
        String[] doctorArr;
        String[] clinicArr;
        String[] dataBirdayArr;
        String[] polisArr;
        String[] pathientArr;
        String[] diagnoseArr;
        String[] moneyArr;
        Char[] sex;
        bool[] flag;
        int[] medServiceArr;

        ItemDataGrid itemDG = new ItemDataGrid();
        DataServiceAll dataServiceAll = new DataServiceAll();        

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
            
            lengtRowsGrid = dataGridMed.Items.Count - 1;
            serviceArr = dataServiceAll.getServiceArr();

            dataServiceArr = new String[lengtRowsGrid];
            doctorArr = new String[lengtRowsGrid];
            clinicArr = new String[lengtRowsGrid];
            dataBirdayArr = new String[lengtRowsGrid];
            polisArr = new String[lengtRowsGrid];
            pathientArr = new String[lengtRowsGrid];
            diagnoseArr = new String[lengtRowsGrid];
            moneyArr = new String[lengtRowsGrid];
            sex = new char[lengtRowsGrid];
            flag = new bool[lengtRowsGrid];
            medServiceArr = new int[lengtRowsGrid];
        }

        private void OverService_Click(object sender, RoutedEventArgs e)
        {
            addArrayItem(lengtRowsGrid);                            
            
            itemDG.clearTable(dataGridMed);
            itemDG.addNameColums(dataGridMed);

            for (int i = 0; i < serviceArr.Length; i++)
            {
                for (int z = 0; z < lengtRowsGrid; z++)
                {
                    if (medServiceArr[z] == serviceArr[i] && flag[z] == true)
                    {
                        for (int y = z + 1; y < lengtRowsGrid; y++)
                        {
                            if (medServiceArr[y] == serviceArr[i] && String.Compare(polisArr[z], polisArr[y]) == 0)
                            {
                                if (String.Compare(dataServiceArr[z], dataServiceArr[y]) == 0)
                                {
                                    if (flag[z] == true)
                                    {
                                        dataGridMed.Items.Add(new ItemDataGrid()
                                        {
                                            DataService = dataServiceArr[z],
                                            Doctor = doctorArr[z],
                                            Clinic = clinicArr[z],
                                            DataBirday = dataBirdayArr[z],
                                            Polic = polisArr[z],
                                            Pathient = pathientArr[z],
                                            Diagnose = diagnoseArr[z],
                                            Sex = sex[z],
                                            MedService = medServiceArr[z]
                                        });

                                        flag[z] = false;
                                    }

                                    dataGridMed.Items.Add(new ItemDataGrid()
                                    {
                                        DataService = dataServiceArr[y],
                                        Doctor = doctorArr[y],
                                        Clinic = clinicArr[y],
                                        DataBirday = dataBirdayArr[y],
                                        Polic = polisArr[y],
                                        Pathient = pathientArr[y],
                                        Diagnose = diagnoseArr[y],
                                        Sex = sex[y],
                                        MedService = medServiceArr[y],
                                        Remove = "Исключить"
                                    });
                                    
                                    flag[y] = false;
                                }
                                else
                                {
                                    if (medServiceArr[y] % 2 != 0)
                                    {
                                        if (flag[z] == true)
                                        {
                                            dataGridMed.Items.Add(new ItemDataGrid()
                                            {
                                                DataService = dataServiceArr[z],
                                                Doctor = doctorArr[z],
                                                Clinic = clinicArr[z],
                                                DataBirday = dataBirdayArr[z],
                                                Polic = polisArr[z],
                                                Pathient = pathientArr[z],
                                                Diagnose = diagnoseArr[z],
                                                Sex = sex[z],
                                                MedService = medServiceArr[z]
                                            });

                                            flag[z] = false;
                                        }

                                        dataGridMed.Items.Add(new ItemDataGrid()
                                        {
                                            DataService = dataServiceArr[y],
                                            Doctor = doctorArr[y],
                                            Clinic = clinicArr[y],
                                            DataBirday = dataBirdayArr[y],
                                            Polic = polisArr[y],
                                            Pathient = pathientArr[y],
                                            Diagnose = diagnoseArr[y],
                                            Sex = sex[y],
                                            MedService = medServiceArr[y],
                                            Remove = Convert.ToString(medServiceArr[y] + 1)
                                        });
                                        
                                        flag[y] = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GetListVDPO_Click(object sender, RoutedEventArgs e)
        {
            addArrayItem(lengtRowsGrid);

            itemDG.clearTable(dataGridMed);
            itemDG.addNameColums(dataGridMed);

            for (int z = 0; z < lengtRowsGrid; z++)
            {
                if (medServiceArr[z] >= 1906 && medServiceArr[z] <= 1920 && flag[z])
                {
                    dataGridMed.Items.Add(new ItemDataGrid()
                    {
                        DataService = dataServiceArr[z],
                        Doctor = doctorArr[z],
                        Clinic = clinicArr[z],
                        DataBirday = dataBirdayArr[z],
                        Polic = polisArr[z],
                        Pathient = pathientArr[z],
                        Diagnose = diagnoseArr[z],
                        Money = moneyArr[z],
                        Sex = sex[z],
                        MedService = medServiceArr[z]
                    });

                    for (int y = 0; y < lengtRowsGrid; y++)
                    {
                        if (String.Compare(polisArr[z], polisArr[y]) == 0 && z != y)
                        {
                            dataGridMed.Items.Add(new ItemDataGrid()
                            {
                                DataService = dataServiceArr[y],
                                Doctor = doctorArr[y],
                                Clinic = clinicArr[y],
                                DataBirday = dataBirdayArr[y],
                                Polic = polisArr[y],
                                Pathient = pathientArr[y],
                                Diagnose = diagnoseArr[y],
                                Money = moneyArr[y],
                                Sex = sex[y],
                                MedService = medServiceArr[y]
                            });

                            if (medServiceArr[y] >= 1906 && medServiceArr[y] <= 1920 && z <= y)
                            {
                                flag[y] = false;
                                Console.WriteLine(medServiceArr[y]);
                            }
                        }
                    }
                }
            }            
        }

        private void addArrayItem(int lenghtArr) {
            for (int i = 0; i < lenghtArr; i++)
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
                    diagnoseArr[i] = drv[21].ToString();
                    moneyArr[i] = drv[16].ToString();
                    sex[i] = Convert.ToChar(drv[6]);
                    medServiceArr[i] = Convert.ToInt32(drv[18]);
                    flag[i] = true;
                }
                catch (Exception)
                {
                    pathientArr[i] = $"i";
                    medServiceArr[i] = i;
                }
            }
        }
    }
}
