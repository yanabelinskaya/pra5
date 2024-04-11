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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prakt5
{
    /// <summary>
    /// Логика взаимодействия для Check.xaml
    /// </summary>
    public partial class Check : Page
    {
        public Check()
        {
            InitializeComponent();
        }

        public Check(object value, DateTime now, double v, object selectedProducts)
        {
            Value = value;
            Now = now;
            V = v;
            SelectedProducts = selectedProducts;
        }

        public object Value { get; }
        public DateTime Now { get; }
        public double V { get; }
        public object SelectedProducts { get; }
    }
}
