using BankEntityClassLibrary;
using Client.Controller;
using Client.Model;
using Client.View;
using System;
using System.Collections.Generic;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AuthView : Window
    {
        private ControllerFormAuth _controller;

        public AuthView()
        {
            InitializeComponent();
            _controller = new ControllerFormAuth(this);

        }

        private async void buttonAuthorize_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;

            if (login == "")
            {
                MessageBox.Show("ERROR, enter login");
                return;
            }

            string password = textBoxPassword.Password;

            if (password == "")
            {
                MessageBox.Show("ERROR, enter password");
                return;
            }

            try
            {
                await _controller.AuthorizeAsync(login, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error. {ex.Message}");
            }
        }
    }
}
