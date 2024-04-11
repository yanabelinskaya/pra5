using Microsoft.WindowsAPICodePack.Dialogs;
using prakt5.P5DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
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
using Newtonsoft.Json;

namespace prakt5
{
    public partial class Ingredients : Page
    {
        IndredientsTableAdapter ingredients = new IndredientsTableAdapter();
        public Ingredients()
        {
            InitializeComponent();
            IngredientsDgr.ItemsSource = ingredients.GetData();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            IngredientsDgr.Columns[1].Header = "Название ингредиента";
            IngredientsDgr.Columns[2].Header = "Цена";
            IngredientsDgr.Columns[3].Header = "Количество";
            IngredientsDgr.Columns[0].Header = "ID ингредиента";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IngredientsName.Text) || string.IsNullOrWhiteSpace(Price.Text) || string.IsNullOrWhiteSpace(Quantity.Text))
            {
                MessageBox.Show("Не все данные заполнены");
                return;
            }

            // Проверка цены
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

            // Проверка количества
            if (!int.TryParse(Quantity.Text, out int quantity))
            {
                MessageBox.Show("Количество должно быть целым числом");
                return;
            }

            if (quantity < 1)
            {
                MessageBox.Show("Количество не может быть отрицательным и равным 0");
                return;
            }
        
            ingredients.InsertQuery(IngredientsName.Text, int.Parse(Price.Text), int.Parse(Quantity.Text));
            IngredientsDgr.ItemsSource = ingredients.GetData();
            IngredientsDgr.Columns[1].Header = "Название ингредиента";
            IngredientsDgr.Columns[2].Header = "Цена";
            IngredientsDgr.Columns[3].Header = "Количество";
            IngredientsDgr.Columns[0].Header = "ID ингредиента";
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (IngredientsDgr.SelectedItem is DataRowView selectedRow)
            {
                object indredients_id = selectedRow.Row[0];

                if (string.IsNullOrWhiteSpace(IngredientsName.Text) || string.IsNullOrWhiteSpace(Price.Text) || string.IsNullOrWhiteSpace(Quantity.Text))
                {
                    MessageBox.Show("Не все данные заполнены");
                    return;
                }

                // Проверка цены
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

                // Проверка количества
                if (!int.TryParse(Quantity.Text, out int quantity))
                {
                    MessageBox.Show("Количество должно быть целым числом");
                    return;
                }

                if (quantity < 0)
                {
                    MessageBox.Show("Количество не может быть отрицательным");
                    return;
                }
                ingredients.UpdateQuery(IngredientsName.Text, int.Parse(Price.Text), int.Parse(Quantity.Text), Convert.ToInt32(indredients_id));
                IngredientsDgr.ItemsSource = ingredients.GetData();
                IngredientsDgr.Columns[1].Header = "Название ингредиента";
                IngredientsDgr.Columns[2].Header = "Цена";
                IngredientsDgr.Columns[3].Header = "Количество";
                IngredientsDgr.Columns[0].Header = "ID ингредиента";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (IngredientsDgr.SelectedItem != null)
            {
                object Indredients_id = (IngredientsDgr.SelectedItem as DataRowView).Row[0];
                ingredients.DeleteQuery(Convert.ToInt32(Indredients_id));
                IngredientsDgr.ItemsSource = ingredients.GetData();
                IngredientsDgr.Columns[1].Header = "Название ингредиента";
                IngredientsDgr.Columns[2].Header = "Цена";
                IngredientsDgr.Columns[3].Header = "Количество";
                IngredientsDgr.Columns[0].Header = "ID ингредиента";
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                string jsonText = File.ReadAllText(dialog.FileName);
                List<Indredients> indredientsList = JsonConvert.DeserializeObject<List<Indredients>>(jsonText);

                // Получение экземпляра IndredientsTableAdapter
                IndredientsTableAdapter indredientsTableAdapter = new IndredientsTableAdapter();

                foreach (Indredients indredient in indredientsList)
                {
                    // Добавляем каждый объект в таблицу
                    ingredients.InsertQuery(indredient.IndredientsName, indredient.Price, indredient.Quantity);
                }
                IngredientsDgr.ItemsSource = ingredients.GetData();
                IngredientsDgr.Columns[1].Header = "Название ингредиента";
                IngredientsDgr.Columns[2].Header = "Цена";
                IngredientsDgr.Columns[3].Header = "Количество";
                IngredientsDgr.Columns[0].Header = "ID ингредиента";

                MessageBox.Show("Данные успешно импортированы в таблицу" );
            }
        }
    

        private void EmployeesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IngredientsDgr.SelectedItem != null)
            {
                DataRowView selectedRow = IngredientsDgr.SelectedItem as DataRowView;
                IngredientsName.Text = selectedRow.Row[1].ToString();
                Price.Text = selectedRow.Row[2].ToString();
                Quantity.Text = selectedRow.Row[3].ToString();

            }
        }
        private void IngredientsName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (IngredientsName.Text == "Название ингредиента")
            {
                IngredientsName.Text = "";
            }
        }

        private void IngredientsName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(IngredientsName.Text))
            {
                IngredientsName.Text = "Название ингредиента";
            }
        }
        private void Price_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Price.Text == "Цена")
            {
                Price.Text = "";
            }
        }

        private void Price_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Price.Text))
            {
                Price.Text = "Цена";
            }
        }
        private void Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Quantity.Text == "Количество")
            {
                Quantity.Text = "";
            }
        }

        private void Quantity_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Quantity.Text))
            {
                Quantity.Text = "Количество";
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
