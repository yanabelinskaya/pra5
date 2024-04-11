using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using prakt5.P5DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    public partial class Poli : Page
    {
        RolesTableAdapter roleName = new RolesTableAdapter();

        public Poli()
        {
            InitializeComponent();
            RoleNameDgr.ItemsSource = roleName.GetData();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RoleNameDgr.Columns[1].Header = "Название роли";
            RoleNameDgr.Columns[0].Header = "ID роли";

        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = Role.Text;

            bool containsNonLetters = input.Any(c => !char.IsLetter(c));

            if (containsNonLetters)
            {
                MessageBox.Show("Должны быть введены только буквы.");
                Role.Text = string.Empty;
                return;
            }
            roleName.InsertQuery(Role.Text);
            RoleNameDgr.ItemsSource = roleName.GetData();
            Role.Text = "";
            RoleNameDgr.Columns[1].Header = "Название роли";
            RoleNameDgr.Columns[0].Header = "ID роли";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (RoleNameDgr.SelectedItem != null)
            {
                DataRowView selectedRow = RoleNameDgr.SelectedItem as DataRowView;
                object Role_id = selectedRow.Row[0];
                roleName.UpdateQuery(Role.Text, Convert.ToInt32(Role_id));
                RoleNameDgr.ItemsSource = roleName.GetData();
                Role.Text = "";
                RoleNameDgr.Columns[1].Header = "Название роли";
                RoleNameDgr.Columns[0].Header = "ID роли";
            }
        }

        private void RoleNameDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleNameDgr.SelectedItem != null)
            {
                DataRowView selectedRow = RoleNameDgr.SelectedItem as DataRowView;
                Role.Text = selectedRow.Row[1].ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            object Role_id = (RoleNameDgr.SelectedItem as DataRowView).Row[0];
            roleName.DeleteQuery(Convert.ToInt32(Role_id));
            RoleNameDgr.ItemsSource = roleName.GetData();
            Role.Text = "";
            RoleNameDgr.Columns[1].Header = "Название роли";
            RoleNameDgr.Columns[0].Header = "ID роли";
        }
        private void RoleTbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Role.Text == "Название роли")
            {
                Role.Text = "";
            }
        }

        private void RoleTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Role.Text))
            {
                Role.Text = "Название роли";
            }
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                string jsonText = File.ReadAllText(dialog.FileName);
                List<string> roleNames = JsonConvert.DeserializeObject<List<string>>(jsonText);

                // Получение экземпляра IndredientsTableAdapter
                RolesTableAdapter rolesTableAdapter = new RolesTableAdapter();

                foreach (string roleName in roleNames)
                {
                    // Добавляем каждый объект в таблицу
                    rolesTableAdapter.InsertQuery(roleName);
                }
                RoleNameDgr.ItemsSource = roleName.GetData();
                RoleNameDgr.Columns[1].Header = "Название роли";

                MessageBox.Show("Данные успешно импортированы в таблицу");
            }
        }


    }
}
