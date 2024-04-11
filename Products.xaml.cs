using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using prakt5.P5DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
    public partial class Products : Page
    {
        ProductTableAdapter product = new ProductTableAdapter();
        CategoriesTableAdapter categories = new CategoriesTableAdapter();
        public Products()
        {
            InitializeComponent();
            ProductsDgr.ItemsSource = product.GetData();
            CategoryCbx.ItemsSource = categories.GetData();
            CategoryCbx.DisplayMemberPath = "CategoriName";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductsDgr.Columns[1].Header = "Название изделия";
            ProductsDgr.Columns[2].Header = "Описание";
            ProductsDgr.Columns[3].Header = "Дата производства";
            ProductsDgr.Columns[4].Header = "Срок годности";
            ProductsDgr.Columns[5].Header = "Цена";
            ProductsDgr.Columns[6].Header = "ID категории";
            ProductsDgr.Columns[0].Header = "ID изделия";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryCbx.SelectedItem == null || string.IsNullOrWhiteSpace(ProductsDescription.Text) || string.IsNullOrWhiteSpace(ShelfLife.Text) || string.IsNullOrWhiteSpace(Price.Text))
            {
                MessageBox.Show("Не все данные заполнены");
                return;
            }
            if (!DateTime.TryParseExact(ProductsDate.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                MessageBox.Show("Дата и время должны быть в формате dd.MM.yyyy");
                return;
            }
            if (date < DateTime.Today)
            {
                MessageBox.Show("Дата и время не может быть меньше сегодняшней");
                return;
            }
            if (!int.TryParse(ShelfLife.Text, out int shelfLife))
            {
                if (!int.TryParse(ShelfLife.Text, out _))
                    MessageBox.Show("Срок годности должен быть числом");
                return;
            }
            if (shelfLife < 1)
            {
                MessageBox.Show("Срок годности должен быть положительным числом и не 0");
                return;
            }

            if (!double.TryParse(Price.Text, out double price))
            {
                MessageBox.Show("Цена должна быть числом");
                return;
            }

            if (price < 0)
            {
                MessageBox.Show("Цена не может быть отрицательной");
                return;
            }
            if (price > 2000000)
            {
                MessageBox.Show("Цена не может быть выше 2000000)");
                return;
            }
            var Categori_id = (int)(CategoryCbx.SelectedItem as DataRowView).Row[0];
            product.InsertQuery(ProductsName.Text, ProductsDescription.Text, ProductsDate.Text, int.Parse(ShelfLife.Text), decimal.Parse(Price.Text), Categori_id);
            ProductsDgr.ItemsSource = product.GetData();

            ProductsDgr.Columns[1].Header = "Название изделия";
            ProductsDgr.Columns[2].Header = "Описание";
            ProductsDgr.Columns[3].Header = "Дата производства";
            ProductsDgr.Columns[4].Header = "Срок годности";
            ProductsDgr.Columns[5].Header = "Цена";
            ProductsDgr.Columns[6].Header = "Категория";
            ProductsDgr.Columns[0].Header = "ID изделия";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (ProductsDgr.SelectedItem is DataRowView selectedRow)
            {
                object product_id = selectedRow.Row[0];
                if (CategoryCbx.SelectedItem == null || string.IsNullOrWhiteSpace(ProductsDescription.Text) || string.IsNullOrWhiteSpace(ShelfLife.Text) || string.IsNullOrWhiteSpace(Price.Text))
                {
                    MessageBox.Show("Не все данные заполнены");
                    return;
                }
                if (!DateTime.TryParseExact(ProductsDate.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                {
                    MessageBox.Show("Дата и время должны быть в формате dd.MM.yyyy");
                    return;
                }
                if (!int.TryParse(ShelfLife.Text, out int shelfLife))
                {
                    if (!int.TryParse(ShelfLife.Text, out _))
                        MessageBox.Show("Срок годности должен быть числом");
                    return;
                }
                if (shelfLife <= 0)
                {
                    MessageBox.Show("Срок годности должен быть положительным числом");
                    return;
                }

                if (!double.TryParse(Price.Text, out double price))
                {
                    MessageBox.Show("Цена должна быть числом");
                    return;
                }

                if (price < 0)
                {
                    MessageBox.Show("Цена не может быть отрицательной");
                    return;
                }
                if (price > 2000000)
                {
                    MessageBox.Show("Цена не может быть выше 2000000)");
                    return;
                }
                var Categori_id = (int)(CategoryCbx.SelectedItem as DataRowView).Row[0];
                product.UpdateQuery(ProductsName.Text, ProductsDescription.Text, ProductsDate.Text, int.Parse(ShelfLife.Text), decimal.Parse(Price.Text), Categori_id, Convert.ToInt32(product_id));
                ProductsDgr.ItemsSource = product.GetData();

                ProductsDgr.Columns[1].Header = "Название изделия";
                ProductsDgr.Columns[2].Header = "Описание";
                ProductsDgr.Columns[3].Header = "Дата производства";
                ProductsDgr.Columns[4].Header = "Срок годности";
                ProductsDgr.Columns[5].Header = "Цена";
                ProductsDgr.Columns[6].Header = "Категория";
                ProductsDgr.Columns[0].Header = "ID изделия";
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (ProductsDgr.SelectedItem != null)
            {
                object product_id = (ProductsDgr.SelectedItem as DataRowView).Row[0];
                product.DeleteQuery(Convert.ToInt32(product_id));
                ProductsDgr.ItemsSource = product.GetData();
                ProductsDgr.Columns[1].Header = "Название изделия";
                ProductsDgr.Columns[2].Header = "Описание";
                ProductsDgr.Columns[3].Header = "Дата производства";
                ProductsDgr.Columns[4].Header = "Срок годности";
                ProductsDgr.Columns[5].Header = "Цена";
                ProductsDgr.Columns[6].Header = "Категория";
                ProductsDgr.Columns[0].Header = "ID изделия";
            }
        }
        private void EmployeesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsDgr.SelectedItem != null)
            {
                DataRowView selectedRow = ProductsDgr.SelectedItem as DataRowView;
                int CategoriId = Convert.ToInt32(selectedRow.Row["Categori_id"]);

                foreach (DataRowView item in CategoryCbx.Items)
                {
                    if ((int)item.Row["Categori_id"] == CategoriId) // Предположим, что в комбо-боксе есть столбец "RoleId", содержащий идентификатор роли
                    {
                        string CategoriName = item.Row["CategoriName"].ToString(); // Предположим, что в комбо-боксе есть столбец "RoleName", содержащий названия ролей
                        CategoryCbx.Text = CategoriName;
                        break;
                    }
                }
                ProductsName.Text = selectedRow.Row[1].ToString();
                ProductsDescription.Text = selectedRow.Row[2].ToString();
                ProductsDate.Text = selectedRow.Row[3].ToString();
                ShelfLife.Text = selectedRow.Row[4].ToString();
                Price.Text = selectedRow.Row[5].ToString();


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
            if (CategoryCbx.SelectedItem != null)
            {
                var Categori_id = (int)(CategoryCbx.SelectedItem as DataRowView).Row[0];
            }
        }
    }
}
