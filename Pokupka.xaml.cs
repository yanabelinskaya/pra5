using prakt5.P5DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace prakt5
{
    public partial class Pokupka : Page
    {
        ProductTableAdapter productAdapter = new ProductTableAdapter();
        DataTable selectedProductsTable = new DataTable();
        decimal totalPrice = 0;

        public Pokupka()
        {
            InitializeComponent();
            InitializeSelectedProductsTable();
            ProductsDgr.ItemsSource = productAdapter.GetData();
        }

        private void InitializeSelectedProductsTable()
        {
            selectedProductsTable.Columns.Add("productName", typeof(string));
            selectedProductsTable.Columns.Add("productDescription", typeof(string));
            selectedProductsTable.Columns.Add("productionDate", typeof(DateTime));
            selectedProductsTable.Columns.Add("ShelfLife", typeof(int));
            selectedProductsTable.Columns.Add("Price", typeof(decimal));

            DataDgr.ItemsSource = selectedProductsTable.DefaultView;
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void Plus(object sender, RoutedEventArgs e)
        {
            if (ProductsDgr.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)ProductsDgr.SelectedItem;
                DataRow newRow = selectedProductsTable.NewRow();
                newRow["productName"] = selectedRow["productName"];
                newRow["productDescription"] = selectedRow["productDescription"];
                newRow["productionDate"] = selectedRow["productionDate"];
                newRow["ShelfLife"] = selectedRow["ShelfLife"];
                newRow["Price"] = selectedRow["Price"];
                selectedProductsTable.Rows.Add(newRow);
                totalPrice += (decimal)selectedRow["Price"];
                Money.Text = totalPrice.ToString();
            }
        }

        private void Minus(object sender, RoutedEventArgs e)
        {
            if (DataDgr.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)DataDgr.SelectedItem;
                totalPrice -= (decimal)selectedRow["Price"];
                Money.Text = totalPrice.ToString();
                selectedRow.Delete();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private decimal CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (DataRowView row in DataDgr.Items)
            {
                totalPrice += (decimal)row["Price"];
            }
            return totalPrice;
        }
    }
}