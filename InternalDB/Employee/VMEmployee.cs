using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InternalDB
{
    public class VMEmployee : INotifyPropertyChanged
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string adress { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string vacationdays { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand AddNewEmployeeCommand { get; private set; }
        public ICommand DeleteEmployeeCommand { get; private set; }

        public VMEmployee()
        {
           AddNewEmployeeCommand = new CommandHandler(AddNewEmployee, CanExecute);
           DeleteEmployeeCommand = new CommandHandler(DeleteEmployee, CanExecute);
        }

        private void AddNewEmployee()
        {
            Employee employee = new Employee();
            employee.firstName = firstName;
            employee.lastName = lastName;
            employee.adress = adress;
            employee.email = email;
            employee.phone = phone;
            employee.username = username;
            employee.password = password;
            DBConnection.AddNewUser(employee);
            firstName = string.Empty;
            RaisePropertyChange(nameof(firstName));
            lastName = string.Empty;
            RaisePropertyChange(nameof(lastName));
            adress = string.Empty;
            RaisePropertyChange(nameof(adress));
            email = string.Empty;
            RaisePropertyChange(nameof(email));
            phone = string.Empty;
            RaisePropertyChange(nameof(phone));
            username = string.Empty;
            RaisePropertyChange(nameof(username));
            password = string.Empty;
            RaisePropertyChange(nameof(password));
        }

        private void DeleteEmployee()
        {
            Employee employee = new Employee();
            employee.username = username;
            DBConnection.DeleteUser(employee);
            username = string.Empty;
            RaisePropertyChange(nameof(username));
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
