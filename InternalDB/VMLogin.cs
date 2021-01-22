using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace InternalDB
{
    public class VMLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; private set; }
    

        public VMLogin()
        {
            LoginCommand = new CommandHandler(Login, CanExecute);
         
        }

        private void Login()
        {
            var loginState = DBConnection.AuthenticateUser(Username, Password);

        }

        private bool CanExecute()
        {
            return true;
        }
    }
}
