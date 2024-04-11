using System;
using System.Collections.Generic;
using System.Data;
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
using prakt5.P5DataSetTableAdapters;

namespace prakt5
{
    public partial class WareHouse : Page
    {
        WarehouseTableAdapter wareHouse = new WarehouseTableAdapter();
        SuppliersTableAdapter suppliers = new SuppliersTableAdapter();
        public WareHouse()
        {
            InitializeComponent();
            WareHouseDgr.ItemsSource = wareHouse.GetData();
            SupplierTbx.ItemsSource = suppliers.GetData();
            SupplierTbx.DisplayMemberPath = "SuppliersName";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WareHouseDgr.Columns[3].Visibility = Visibility.Collapsed;
            WareHouseDgr.Columns[5].Visibility = Visibility.Collapsed;
            WareHouseDgr.Columns[1].Header = "Дата поставки";
            WareHouseDgr.Columns[2].Header = "Количество";
            WareHouseDgr.Columns[4].Header = "ID поставщика";
            WareHouseDgr.Columns[0].Header = "ID склада";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SupplierTbx.SelectedItem == null || string.IsNullOrWhiteSpace(DateOfReceipt.Text) || string.IsNullOrWhiteSpace(Quantity.Text))
            {
                MessageBox.Show("Не все данные заполнены");
                return;
            }
            if (!int.TryParse(Quantity.Text, out int quantity) || quantity < 1)

            {
                if (!int.TryParse(Quantity.Text, out _))
                    MessageBox.Show("Количество должна быть числом");
                else
                    MessageBox.Show("Количество не может быть отрицательным и равным 0");
                return;
            }
            if (!DateTime.TryParseExact(DateOfReceipt.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                MessageBox.Show("Дата и время должны быть в формате dd.MM.yyyy");
                return;
            }
            if (date < DateTime.Today)
            {
                MessageBox.Show("Дата и время не может быть меньше сегодняшней");
                return;
            }

            var supplier_id = (int)(SupplierTbx.SelectedItem as DataRowView).Row[0];
            wareHouse.InsertQuery(DateOfReceipt.Text, int.Parse(Quantity.Text), supplier_id);
            WareHouseDgr.ItemsSource = wareHouse.GetData();

            WareHouseDgr.Columns[3].Visibility = Visibility.Collapsed;
            WareHouseDgr.Columns[5].Visibility = Visibility.Collapsed;
            WareHouseDgr.Columns[1].Header = "Дата поставки";
            WareHouseDgr.Columns[2].Header = "Количество";
            WareHouseDgr.Columns[4].Header = "ID поставщика";
            WareHouseDgr.Columns[0].Header = "ID склада";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (WareHouseDgr.SelectedItem is DataRowView selectedRow)
            {
                object warehouse_id = selectedRow.Row[0];

                if (SupplierTbx.SelectedItem == null || string.IsNullOrWhiteSpace(DateOfReceipt.Text) || string.IsNullOrWhiteSpace(Quantity.Text))
                {
                    MessageBox.Show("Не все данные заполнены");
                    return;
                }

                if (!int.TryParse(Quantity.Text, out int salary) || salary < 0)
                {
                    if (!int.TryParse(Quantity.Text, out _))
                        MessageBox.Show("Зарплата должна быть числом");
                    else
                        MessageBox.Show("Зарплата не может быть отрицательной");
                    return;
                }
                if (!DateTime.TryParseExact(DateOfReceipt.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _))
                {
                    MessageBox.Show("Дата и время должны быть в формате dd.MM.yyyy");
                    return;
                }

                var supplier_id = (int)(SupplierTbx.SelectedItem as DataRowView).Row[0];
                wareHouse.UpdateQuery(DateOfReceipt.Text, int.Parse(Quantity.Text), supplier_id, Convert.ToInt32(warehouse_id));
                WareHouseDgr.ItemsSource = wareHouse.GetData();

                WareHouseDgr.Columns[3].Visibility = Visibility.Collapsed;
                WareHouseDgr.Columns[5].Visibility = Visibility.Collapsed;
                WareHouseDgr.Columns[1].Header = "Дата поставки";
                WareHouseDgr.Columns[2].Header = "Количество";
                WareHouseDgr.Columns[4].Header = "ID поставщика";
                WareHouseDgr.Columns[0].Header = "ID склада";
            }       
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (WareHouseDgr.SelectedItem != null)
            {
                object employee_id = (WareHouseDgr.SelectedItem as DataRowView).Row[0];
                wareHouse.DeleteQuery(Convert.ToInt32(employee_id));
                WareHouseDgr.ItemsSource = wareHouse.GetData();
                WareHouseDgr.Columns[3].Visibility = Visibility.Collapsed;
                WareHouseDgr.Columns[5].Visibility = Visibility.Collapsed;
                WareHouseDgr.Columns[1].Header = "Дата поставки";
                WareHouseDgr.Columns[2].Header = "Количество";
                WareHouseDgr.Columns[4].Header = "ID поставщика";
                WareHouseDgr.Columns[0].Header = "ID склада";
            }
        }

        private void EmployeesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WareHouseDgr.SelectedItem != null)
            {
                DataRowView selectedRow = WareHouseDgr.SelectedItem as DataRowView;
                int SuppliersId = Convert.ToInt32(selectedRow.Row["suppliers_id"]);

                foreach (DataRowView item in SupplierTbx.Items)
                {
                    if ((int)item.Row["suppliers_id"] == SuppliersId) // Предположим, что в комбо-боксе есть столбец "RoleId", содержащий идентификатор роли
                    {
                        string SuppliersName = item.Row["SuppliersName"].ToString(); // Предположим, что в комбо-боксе есть столбец "RoleName", содержащий названия ролей
                        SupplierTbx.Text = SuppliersName;
                        break;
                    }
                }
                DateOfReceipt.Text = selectedRow.Row[1].ToString();
                Quantity.Text = selectedRow.Row[2].ToString();
            }
        }

        private void RoleCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SupplierTbx.SelectedItem != null)
            {
                var suppliers_id = (int)(SupplierTbx.SelectedItem as DataRowView).Row[0];
            }
        }
        private void DateOfReceipt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (DateOfReceipt.Text == "Дата поставки")
            {
                DateOfReceipt.Text = "";
            }
        }

        private void DateOfReceipt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DateOfReceipt.Text))
            {
                DateOfReceipt.Text = "Дата поставки";
            }
        }
        private void Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Quantity.Text == "Количество продуктов")
            {
                Quantity.Text = "";
            }
        }

        private void Quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Quantity.Text))
            {
                Quantity.Text = "Количество продуктов";
            }
        }
    }
}
