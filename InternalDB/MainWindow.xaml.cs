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

namespace InternalDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VMLogin loginViewModel;
        public MainWindow()
        {
            InitializeComponent();
            loginViewModel = new VMLogin();
            DataContext = loginViewModel;
            LoginTab.IsChecked = true;
            EvaluateTabAccessibility(UserState.NotAuthenticated);
            LoginCredentialsSetup(UserState.NotAuthenticated);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.LoginCommand.Execute(null);
            var state = loginViewModel.UserState;
            EvaluateTabAccessibility(state);
            LoginCredentialsSetup(state);
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            loginViewModel.LogoutCommand.Execute(null);
            LoginCredentialsSetup(loginViewModel.UserState);
            EvaluateTabAccessibility(loginViewModel.UserState);
            LoginCredentialsSetup(loginViewModel.UserState);
        }

        private void EvaluateTabAccessibility(UserState userState)
        {
            if (userState == UserState.AdminAuthenticated)
            {
                RequestLeave.IsEnabled = true;
                PendingVacay.IsEnabled = true;
                UserMng.IsEnabled = true;
                LoginTab.IsEnabled = true;
            }
            else if (userState == UserState.UserAuthenticated)
            {
                LoginTab.IsEnabled = true;

            }
            else
            {
                RequestLeave.IsEnabled = false;
                PendingVacay.IsEnabled =false;
                UserMng.IsEnabled = false;
                LoginTab.IsEnabled = true;
            }
        }

        private void LoginCredentialsSetup(UserState userState)
        {
            if (userState == UserState.NotAuthenticated)
            {
                UsernameLbl.IsEnabled = true;
                UsernameTxtbox.IsEnabled = true;
                PasswordLbl.IsEnabled = true;
                PasswordTxtBox.IsEnabled = true;
                LogoutBtn.IsEnabled = false;
                LoginBtn.IsEnabled = true;
            }
            else if(userState== UserState.AdminAuthenticated || userState == UserState.UserAuthenticated)
            {
                UsernameLbl.IsEnabled = false;
                UsernameTxtbox.IsEnabled = false;
                PasswordLbl.IsEnabled = false;
                PasswordTxtBox.IsEnabled = false;
                LogoutBtn.IsEnabled = true;
                LoginBtn.IsEnabled = false;
            }
        }

     
    }
}
