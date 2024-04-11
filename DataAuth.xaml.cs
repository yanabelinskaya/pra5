using System;
using System.Collections.Generic;
using System.Data;
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
using prakt5.P5DataSetTableAdapters;
using System.Security.Cryptography;

namespace prakt5
{
    public partial class DataAuth : Page
    {
        LoginDataTableAdapter loginData = new LoginDataTableAdapter();
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        public DataAuth()
        {
            InitializeComponent();
            DataDgr.ItemsSource = loginData.GetData();
            EmployeeCbx.ItemsSource = employees.GetData();
            EmployeeCbx.DisplayMemberPath = "SurNameEmployee";
        }

        private static string Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                string sb = "";
                for (int i = 0; i < hash.Length; i++)
                {
                    sb += hash[i].ToString("x2");
                }
                return sb;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataDgr.Columns[1].Header = "Логин";
            DataDgr.Columns[2].Header = "Пароль";
            DataDgr.Columns[3].Header = "ID сотрудника";
            DataDgr.Columns[0].Header = "ID данных";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeCbx.SelectedItem == null || string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Password))
            {
                MessageBox.Show("Не все данные заполнены");
                return;
            }
            var Employee_id = (int)(EmployeeCbx.SelectedItem as DataRowView).Row[0];
            string hashedPassword = Hash(Password.Password);
            loginData.InsertQuery(Login.Text, hashedPassword, Employee_id);
            DataDgr.ItemsSource = loginData.GetData();

            DataDgr.Columns[1].Header = "Логин";
            DataDgr.Columns[2].Header = "Пароль";
            DataDgr.Columns[3].Header = "ID сотрудника";
            DataDgr.Columns[0].Header = "ID данных";
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (DataDgr.SelectedItem is DataRowView selectedRow)
            {
                object loginData_id = selectedRow.Row[0];

                if (EmployeeCbx.SelectedItem == null || string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Password))
                {
                    MessageBox.Show("Не все данные заполнены");
                    return;
                }
                int Employee_id = (int)(EmployeeCbx.SelectedItem as DataRowView).Row[0];
                string hashedPassword = Hash(Password.Password);
                loginData.UpdateQuery(Login.Text, hashedPassword, Employee_id, Convert.ToInt32(loginData_id));
                DataDgr.ItemsSource = loginData.GetData();

                DataDgr.Columns[1].Header = "Логин";
                DataDgr.Columns[2].Header = "Пароль";
                DataDgr.Columns[3].Header = "ID сотрудника";
                DataDgr.Columns[0].Header = "ID данных";
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (DataDgr.SelectedItem != null)
            {
                object LoginData_id = (DataDgr.SelectedItem as DataRowView).Row[0];
                loginData.DeleteQuery(Convert.ToInt32(LoginData_id));
                DataDgr.ItemsSource = loginData.GetData();
                DataDgr.Columns[1].Header = "Логин";
                DataDgr.Columns[2].Header = "Пароль";
                DataDgr.Columns[3].Header = "ID сотрудника";
                DataDgr.Columns[0].Header = "ID данных";
            }
        }

        private void DataDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataDgr.SelectedItem != null)
            {
                DataRowView selectedRow = DataDgr.SelectedItem as DataRowView;
                int employeeId = Convert.ToInt32(selectedRow.Row["employee_id"]);

                foreach (DataRowView item in EmployeeCbx.Items)
                {
                    if ((int)item.Row["employee_id"] == employeeId) // Предположим, что в комбо-боксе есть столбец "RoleId", содержащий идентификатор роли
                    {
                        string SurNameEmployee = item.Row["SurNameEmployee"].ToString(); // Предположим, что в комбо-боксе есть столбец "RoleName", содержащий названия ролей
                        EmployeeCbx.Text = SurNameEmployee;
                        break;
                    }                  
                }
                Login.Text = selectedRow.Row[1].ToString();
                Password.Password = selectedRow.Row[2].ToString();
            }
        }

        private void RoleCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeeCbx.SelectedItem != null)
            {
                var Employee_id = (int)(EmployeeCbx.SelectedItem as DataRowView).Row[0];
            }
        }

        private void LoginTbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "Логин")
            {
                Login.Text = "";
            }
        }

        private void LoginTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Text))
            {
                Login.Text = "Логин";
            }
        }

        private void PasswordTbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Password.Password == "Пароль")
            {
                Password.Password = "";
            }
        }

        private void PasswordTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Password.Password))
            {
                Password.Password = "Пароль";
            }
        }
    }
}
