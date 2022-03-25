using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace EqtyPMS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDlg = new OpenFileDialog();
                openFileDlg.Title = "Select Positions Data File";
                openFileDlg.Multiselect = false;
                var result = openFileDlg.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured while Loading Input Data file.\n\n" + ex.ToString());
            }
        }
            
        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var svcClient = new PMSReference.ServiceClient();

                DataSet ds = svcClient.GetAccessDBData("Select * From Trades", "Trades");

                dgTransactions.DataContext = ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occured while Get Data.\n\n" + ex.ToString());
            }
        }
    }
}
