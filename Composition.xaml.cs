using Microsoft.WindowsAPICodePack.Shell;
using prakt5.P5DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
    public partial class Composition : Page
    {
        CompositionTableAdapter composition = new CompositionTableAdapter();
        ProductTableAdapter product = new ProductTableAdapter();
        public Composition()
        {
            InitializeComponent();
            CompositionDgr.ItemsSource = composition.GetData();
            ProductCbx.ItemsSource = product.GetData();
            ProductCbx.DisplayMemberPath = "productName";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CompositionDgr.Columns[1].Header = "Состав продукта";
            CompositionDgr.Columns[0].Header = "ID состава";
            CompositionDgr.Columns[2].Header = "ID продукта";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ProductCbx.SelectedItem != null)
            {
                var product_id = (int)(ProductCbx.SelectedItem as DataRowView).Row[0];
                composition.InsertQuery(CompositionText.Text, product_id);
                CompositionDgr.ItemsSource = composition.GetData();
                CompositionDgr.Columns[1].Header = "Состав продукта";
                CompositionDgr.Columns[0].Header = "ID состава";
                CompositionDgr.Columns[2].Header = "ID продукта";
            }
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (CompositionDgr.SelectedItem is DataRowView selectedRow)
            {
                object Composition_id = selectedRow.Row[0];
                var product_id = (int)(ProductCbx.SelectedItem as DataRowView).Row[0];
                composition.UpdateQuery(CompositionText.Text, product_id, Convert.ToInt32(Composition_id));
                CompositionDgr.ItemsSource = composition.GetData();
                CompositionDgr.Columns[1].Header = "Состав продукта";
                CompositionDgr.Columns[0].Header = "ID состава";
                CompositionDgr.Columns[2].Header = "ID продукта";

            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (CompositionDgr.SelectedItem is DataRowView selectedRow)
            {
                object Composition_id = selectedRow.Row[0];
                composition.DeleteQuery(Convert.ToInt32(Composition_id));
                CompositionDgr.ItemsSource = composition.GetData();
                CompositionDgr.Columns[1].Header = "Состав продукта";
                CompositionDgr.Columns[0].Header = "ID состава";
                CompositionDgr.Columns[2].Header = "ID продукта";

            }
        }
        private void EmployeesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CompositionDgr.SelectedItem != null)
            {
                DataRowView selectedRow = CompositionDgr.SelectedItem as DataRowView;
                int ProductId = Convert.ToInt32(selectedRow.Row["product_id"]);

                foreach (DataRowView item in ProductCbx.Items)
                {
                    if ((int)item.Row["product_id"] == ProductId) // Предположим, что в комбо-боксе есть столбец "RoleId", содержащий идентификатор роли
                    {
                        string productName = item.Row["productName"].ToString(); // Предположим, что в комбо-боксе есть столбец "RoleName", содержащий названия ролей
                        ProductCbx.Text = productName;
                        break;
                    }
                }
                CompositionText.Text = selectedRow.Row[1].ToString();
            }
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void RoleCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductCbx.SelectedItem != null)
            {
                var product_id = (int)(ProductCbx.SelectedItem as DataRowView).Row[0];
            }
        }
    }
}
