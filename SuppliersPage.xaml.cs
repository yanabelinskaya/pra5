using prakt5.P5DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class SuppliersPage : Page
    {
        SuppliersTableAdapter suppliers = new SuppliersTableAdapter();
        IndredientsTableAdapter ingredients = new IndredientsTableAdapter();
        public SuppliersPage()
        {
            InitializeComponent();
            SuppliersDgr.ItemsSource = suppliers.GetData();
            IngredientsTbx.ItemsSource = ingredients.GetData();
            IngredientsTbx.DisplayMemberPath = "IndredientsName";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SuppliersDgr.Columns[1].Header = "Название компании";
            SuppliersDgr.Columns[2].Header = "Телефон";
            SuppliersDgr.Columns[3].Header = "ID ингредиента";
            SuppliersDgr.Columns[0].Header = "ID поставщика";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsTbx.SelectedItem == null || string.IsNullOrWhiteSpace(Phoneee.Text))
            {
                MessageBox.Show("Не все данные заполнены");
                return;
            }

            // Remove any non-numeric characters from the phone number
            string phoneNumber = Phoneee.Text;

            // Check if the resulting string starts with 8 and has a length of 11
            if (!phoneNumber.StartsWith("8") || phoneNumber.Length != 11)
            {
                MessageBox.Show("Номер телефона должен начинаться с 8 и содержать 11 цифр");
                return;
            }

            var Ingredients_id = (int)(IngredientsTbx.SelectedItem as DataRowView).Row[0];
            suppliers.InsertQuery(SuppliersNamee.Text, phoneNumber, Ingredients_id);
            SuppliersDgr.ItemsSource = suppliers.GetData();
            SuppliersDgr.Columns[1].Header = "Название компании";
            SuppliersDgr.Columns[2].Header = "Телефон";
            SuppliersDgr.Columns[3].Header = "ID ингредиента";
            SuppliersDgr.Columns[0].Header = "ID поставщика";
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SuppliersDgr.SelectedItem is DataRowView selectedRow)
            {
                object suppliers_id = selectedRow.Row[0];

                if (IngredientsTbx.SelectedItem == null || string.IsNullOrWhiteSpace(Phoneee.Text))
                {
                    MessageBox.Show("Не все данные заполнены");
                    return;
                }

                // Remove any non-numeric characters from the phone number
                string phoneNumber = Phoneee.Text;

                // Check if the resulting string starts with 8 and has a length of 11
                if (!phoneNumber.StartsWith("8") || phoneNumber.Length != 11)
                {
                    MessageBox.Show("Номер телефона должен начинаться с 8 и содержать 11 цифр");
                    return;
                }
                string phone = Phoneee.Text.ToString();
                var Indredients_id = (int)(IngredientsTbx.SelectedItem as DataRowView).Row[0];
                suppliers.UpdateQuery(SuppliersNamee.Text, phoneNumber, Indredients_id, Convert.ToInt32(suppliers_id));
                SuppliersDgr.ItemsSource = suppliers.GetData();
                SuppliersDgr.Columns[1].Header = "Название компании";
                SuppliersDgr.Columns[2].Header = "Телефон";
                SuppliersDgr.Columns[3].Header = "ID ингредиента";
                SuppliersDgr.Columns[0].Header = "ID поставщика";
            }

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SuppliersDgr.SelectedItem != null)
            {
                object Indredients_id = (SuppliersDgr.SelectedItem as DataRowView).Row[0];
                suppliers.DeleteQuery(Convert.ToInt32(Indredients_id));
                SuppliersDgr.ItemsSource = suppliers.GetData();
                SuppliersDgr.Columns[1].Header = "Название компании";
                SuppliersDgr.Columns[2].Header = "Телефон";
                SuppliersDgr.Columns[3].Header = "ID ингредиента";
                SuppliersDgr.Columns[0].Header = "ID поставщика";
            }
        }
        private void EmployeesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SuppliersDgr.SelectedItem != null)
            {
                DataRowView selectedRow = SuppliersDgr.SelectedItem as DataRowView;
                int Indredients_id = Convert.ToInt32(selectedRow.Row["Indredients_id"]);

                foreach (DataRowView item in IngredientsTbx.Items)
                {
                    if ((int)item.Row["Indredients_id"] == Indredients_id) // Предположим, что в комбо-боксе есть столбец "RoleId", содержащий идентификатор роли
                    {
                        string IngredientsName = item.Row["IndredientsName"].ToString(); // Предположим, что в комбо-боксе есть столбец "RoleName", содержащий названия ролей
                        IngredientsTbx.Text = IngredientsName;
                        break;
                    }
                }
                SuppliersNamee.Text = selectedRow.Row[1].ToString();
                Phoneee.Text = selectedRow.Row[2].ToString();
            }
        }

        private void RoleCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IngredientsTbx.SelectedItem != null)
            {
                var Ingredients_id = (int)(IngredientsTbx.SelectedItem as DataRowView).Row[0];
            }
        }
        private void SuppliersName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SuppliersNamee.Text == "Название компании")
            {
                SuppliersNamee.Text = "";
            }
        }

        private void SuppliersName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SuppliersNamee.Text))
            {
                SuppliersNamee.Text = "Название компании";
            }
        }
        private void Phone_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Phoneee.Text == "Телефон")
            {
                Phoneee.Text = "";
            }
        }

        private void Phone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Phoneee.Text))
            {
                Phoneee.Text = "Телефон";
            }
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
