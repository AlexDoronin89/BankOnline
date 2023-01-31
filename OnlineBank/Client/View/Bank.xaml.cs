using BankEntityClassLibrary;
using Client.Controller;
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
using System.Windows.Shapes;

namespace Client.View
{
    /// <summary>
    /// Логика взаимодействия для Bank.xaml
    /// </summary>
    public partial class Bank : Window
    {
        private ControllerFormMain _controller;

        public Bank(User user)
        {
            InitializeComponent();

            ControllerFormMain controller = new ControllerFormMain(this, user);
            UpdateDataGridCardsAsync();
        }

        private async void UpdateDataGridCardsAsync()
        {
            List<Card> cards=await _controller.GetUserCardsByIdAsync();
            CardsDataGrid.ItemsSource = cards;
        }

        private void Window_Closing(object sender,System.ComponentModel.CancelEventArgs e)
        {
            _controller.CloseApp();
        }

       
        private async void AddCardButton_Click(object sender, RoutedEventArgs e)
        {
            await _controller.AddNewCardAsync();
            UpdateDataGridCardsAsync();
        }

        private  void UpdateCardsButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGridCardsAsync();
        }

        private void AddBalanceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendMoneyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
