using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class MainWindow : Window
    {
        LoginDataTableAdapter adapter = new LoginDataTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
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


        private void Authorization_Click(object sender, RoutedEventArgs e)
        {
            var allLogins = adapter.GetData().Rows;
            bool isAuthenticated = false;

            for (int i = 0; i < allLogins.Count; i++)
            {
                if (allLogins[i][1].ToString() == LoginTbx.Text && allLogins[i][2].ToString() == Hash(PasswordTbx.Password))
                {
                    int Role_id = (int)allLogins[i][3];

                    switch (Role_id)
                    {
                        case 1:
                            Conditer conditer = new Conditer();
                            conditer.Show();
                            Window.GetWindow(this).Close();
                            return;
                        case 2:
                            Menedzer menedzer = new Menedzer();
                            menedzer.Show();
                            Window.GetWindow(this).Close();
                            return;
                        case 3:
                            Kassir kassir = new Kassir();
                            kassir.Show();
                            Window.GetWindow(this).Close();
                            return;
                        case 4:
                            Director director = new Director();
                            director.Show();
                            Window.GetWindow(this).Close();
                            return;
                    }

                    isAuthenticated = true;
                }
            }

            if (!isAuthenticated)
            {
                NoAuth noAuth = new NoAuth();
                noAuth.ShowDialog();
            }

            LoginTbx.Text = "Логин";
            PasswordTbx.Clear();
            PasswordTbx.Password = "Пароль";
        }
        private void LoginTbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (LoginTbx.Text == "Логин")
            {
                LoginTbx.Text = "";
            }
        }

        private void LoginTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTbx.Text))
            {
                LoginTbx.Text = "Логин";
            }
        }

        private void PasswordTbx_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PasswordTbx.Password == "Пароль")
            {
                PasswordTbx.Password = "";
            }
        }

        private void PasswordTbx_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordTbx.Password))
            {
                PasswordTbx.Password = "Пароль";
            }
        }
    }
}
