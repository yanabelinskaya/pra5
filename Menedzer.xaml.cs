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

namespace prakt5
{
    /// <summary>
    /// Логика взаимодействия для Menedzer.xaml
    /// </summary>
    public partial class Menedzer : Window
    {
        public Menedzer()
        {
            InitializeComponent();
        }

        private void Sklad_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new WareHouse();
        }

        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new SuppliersPage();
        }

        private void Ingredients_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Ingredients();
        }
    }
}
