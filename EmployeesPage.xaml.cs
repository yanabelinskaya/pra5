using prakt5.P5DataSetTableAdapters;
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
using System.Xml.Linq;

namespace prakt5
{
    public partial class EmployeesPage : Page
    {
        EmployeesTableAdapter employees = new EmployeesTableAdapter();
        RolesTableAdapter roleName = new RolesTableAdapter();

        public EmployeesPage()
        {
            InitializeComponent();
            EmployeesNameDgr.ItemsSource = employees.GetData();
            RoleCbx.ItemsSource = roleName.GetData();
            RoleCbx.DisplayMemberPath = "RoleName";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EmployeesNameDgr.Columns[5].Visibility = Visibility.Collapsed;
            EmployeesNameDgr.Columns[6].Visibility = Visibility.Collapsed;
            EmployeesNameDgr.Columns[1].Header = "Фамилия сотрудника";
            EmployeesNameDgr.Columns[2].Header = "Имя сотрудника";
            EmployeesNameDgr.Columns[3].Header = "Отчество сотрудника";
            EmployeesNameDgr.Columns[4].Header = "Зарплата";
            EmployeesNameDgr.Columns[7].Header = "ID роли";
            EmployeesNameDgr.Columns[0].Header = "ID сотрудника";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RoleCbx.SelectedItem == null || string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Salary.Text))
            {
                MessageBox.Show("Не все данные заполнены");
                return;
            }
            if (Surname.Text.Any(c => !char.IsLetter(c)))
            {
                MessageBox.Show("В поле должны быть введены только буквы.");
                Name.Text = string.Empty;
                return;
            }
            if (Name.Text.Any(c => !char.IsLetter(c)))
            {
                MessageBox.Show("В поле должны быть введены только буквы.");
                Name.Text = string.Empty;
                return;
            }
            if (MiddleName.Text.Any(c => !char.IsLetter(c)))
            {
                MessageBox.Show("В поле должны быть введены только буквы.");
                Name.Text = string.Empty;
                return;
            }

            if (!int.TryParse(Salary.Text, out int salary) || salary < 0)
            {
                if (!int.TryParse(Salary.Text, out _))
                    MessageBox.Show("Зарплата должна быть числом");
                else
                    MessageBox.Show("Зарплата не может быть отрицательной");
                return;
            }

            // Если все проверки пройдены успешно, выполняем вставку данных в базу данных
            var Role_id = (int)(RoleCbx.SelectedItem as DataRowView).Row[0];
            employees.InsertQuery(Surname.Text, Name.Text, MiddleName.Text, salary, Role_id);
            EmployeesNameDgr.ItemsSource = employees.GetData();

            EmployeesNameDgr.Columns[5].Visibility = Visibility.Collapsed;
            EmployeesNameDgr.Columns[6].Visibility = Visibility.Collapsed;
            EmployeesNameDgr.Columns[1].Header = "Фамилия сотрудника";
            EmployeesNameDgr.Columns[2].Header = "Имя сотрудника";
            EmployeesNameDgr.Columns[3].Header = "Отчество сотрудника";
            EmployeesNameDgr.Columns[4].Header = "Зарплата";
            EmployeesNameDgr.Columns[7].Header = "ID роли";
            EmployeesNameDgr.Columns[0].Header = "ID сотрудника";
        }
        private void Nazad(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (EmployeesNameDgr.SelectedItem is DataRowView selectedRow)
            {
                object employee_id = selectedRow.Row[0];

                if (RoleCbx.SelectedItem == null || string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Salary.Text))
                {
                    MessageBox.Show("Не все данные заполнены");
                    return;
                }

                if (!int.TryParse(Salary.Text, out int salary) || salary < 0)
                {
                    if (!int.TryParse(Salary.Text, out _))
                        MessageBox.Show("Зарплата должна быть числом");
                    else
                        MessageBox.Show("Зарплата не может быть отрицательной");
                    return;
                }

                int Role_id = (int)(RoleCbx.SelectedItem as DataRowView).Row[0];
                employees.UpdateQuery(Surname.Text, Name.Text, MiddleName.Text, salary, Role_id, Convert.ToInt32(employee_id));
                EmployeesNameDgr.ItemsSource = employees.GetData();

                EmployeesNameDgr.Columns[5].Visibility = Visibility.Collapsed;
                EmployeesNameDgr.Columns[6].Visibility = Visibility.Collapsed;
                EmployeesNameDgr.Columns[1].Header = "Фамилия сотрудника";
                EmployeesNameDgr.Columns[2].Header = "Имя сотрудника";
                EmployeesNameDgr.Columns[3].Header = "Отчество сотрудника";
                EmployeesNameDgr.Columns[4].Header = "Зарплата";
                EmployeesNameDgr.Columns[7].Header = "ID роли";
                EmployeesNameDgr.Columns[0].Header = "ID сотрудника";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (EmployeesNameDgr.SelectedItem != null)
            {
                object employee_id = (EmployeesNameDgr.SelectedItem as DataRowView).Row[0];
                employees.DeleteQuery(Convert.ToInt32(employee_id));
                EmployeesNameDgr.ItemsSource = employees.GetData();
                EmployeesNameDgr.Columns[5].Visibility = Visibility.Collapsed;
                EmployeesNameDgr.Columns[6].Visibility = Visibility.Collapsed;
                EmployeesNameDgr.Columns[1].Header = "Фамилия сотрудника";
                EmployeesNameDgr.Columns[2].Header = "Имя сотрудника";
                EmployeesNameDgr.Columns[3].Header = "Отчество сотрудника";
                EmployeesNameDgr.Columns[4].Header = "Зарплата";
                EmployeesNameDgr.Columns[7].Header = "ID роли";
                EmployeesNameDgr.Columns[0].Header = "ID сотрудника";
            }
        }


        private void EmployeesDgr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EmployeesNameDgr.SelectedItem != null)
            {
                DataRowView selectedRow = EmployeesNameDgr.SelectedItem as DataRowView;
                int roleId = Convert.ToInt32(selectedRow.Row["Role_id"]); 
                
                foreach (DataRowView item in RoleCbx.Items)
                {
                    if ((int)item.Row["Role_id"] == roleId) // Предположим, что в комбо-боксе есть столбец "RoleId", содержащий идентификатор роли
                    {
                        string roleName = item.Row["RoleName"].ToString(); // Предположим, что в комбо-боксе есть столбец "RoleName", содержащий названия ролей
                        RoleCbx.Text = roleName;
                        break;
                    }
                }
                Surname.Text = selectedRow.Row[1].ToString();
                Name.Text = selectedRow.Row[2].ToString();
                MiddleName.Text = selectedRow.Row[3].ToString();
                Salary.Text = selectedRow.Row[4].ToString();
            }
        }

        private void RoleCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleCbx.SelectedItem != null)
            {
                var Role_id = (int)(RoleCbx.SelectedItem as DataRowView).Row[0];
            }
        }

        private void Surname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Surname.Text == "Фамилия сотрудника")
            {
                Surname.Text = "";
            }
        }

        private void Surname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Surname.Text))
            {
                Surname.Text = "Фамилия сотрудника";
            }
        }
        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "Имя сотрудника")
            {
                Name.Text = "";
            }
        }

        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text))
            {
                Name.Text = "Имя сотрудника";
            }
        }
        private void MiddleName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (MiddleName.Text == "Отчество сотрудника")
            {
                MiddleName.Text = "";
            }
        }

        private void MiddleName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MiddleName.Text))
            {
                MiddleName.Text = "Отчество сотрудника";
            }
        }
        private void Salary_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Salary.Text == "Зарплата")
            {
                Salary.Text = "";
            }
        }

        private void Salary_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Salary.Text))
            {
                Salary.Text = "Зарплата";
            }
        }
    }
}