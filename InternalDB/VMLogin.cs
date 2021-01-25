using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace InternalDB
{
    public class VMLogin : INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserState UserState { get; private set; }

        public ICommand LoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public VMLogin()
        {
            LoginCommand = new CommandHandler(Login, CanExecute);
            LogoutCommand = new CommandHandler(Logout, CanExecute);
        }

        private void Login()
        {
            UserState = DBConnection.AuthenticateUser(Username, Password);
        }

        private void Logout()
        {
            UserState = UserState.NotAuthenticated;
            Password = String.Empty;
            RaisePropertyChange(nameof(Password));
        }

        protected void RaisePropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool CanExecute()
        {
            return true;
        }
    }
}
