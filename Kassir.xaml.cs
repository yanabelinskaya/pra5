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
    /// Логика взаимодействия для Kassir.xaml
    /// </summary>
    public partial class Kassir : Window
    {
        public Kassir()
        {
            InitializeComponent();
        }
        private void Roli_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Pokupka();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Check();
        }
    }
}
