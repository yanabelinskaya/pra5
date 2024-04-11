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
using System.Windows.Shapes;
using prakt5.P5DataSetTableAdapters;

namespace prakt5
{
    public partial class Director : Window
    {
        public Director()
        {
            InitializeComponent();
        }
        private void Roli_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Poli();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new EmployeesPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new DataAuth();
        }
    }
}
