using MaterialDesignThemes.Wpf;
using System;
using System.Security;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOClient.Library.Api;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Components.NewEmployee
{
    public class NewEmployeeViewModel : BaseViewModel, INewEmployeeViewModel
    {
        public NewEmployeeViewModel(IClientApi api)
        {
            _api = api;

            Permission = PermissionsEnum.Employee;
        }

        #region Fields
        private IClientApi _api;
        private string _email;
        private string _firstName;
        private string _lastName;
        private PermissionsEnum _permission;
        #endregion

        #region Properties
        public string Email
        {
            get => _email;
            set
            {
                if (_email == value) return;

                _email = value;
                NotifyPropertyChanged("Email");
            }
        }
        public SecureString Password { get; set; }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value) return;

                _firstName = value;
                NotifyPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value) return;

                _lastName = value;
                NotifyPropertyChanged("LastName");
            }
        }
        public PermissionsEnum Permission
        {
            get => _permission;
            set
            {
                if (_permission == value) return;

                _permission = value;
                NotifyPropertyChanged("Permission");
            }
        }
        #endregion

        #region Public Methods
        public async Task EmployeeRegisterAsync()
        {
            try
            {
                var employeeId = await _api.EmployeeRegisterAsync(FirstName, LastName, Email, Password.Copy(), Permission, IMainWindowViewModel.User.PersonId);

                AddNewUserToCollection(employeeId);
                SetPropertiesToDefault();

                DialogHost.CloseDialogCommand.Execute(null, null);
            }
            catch (Exception)
            {
                IMainWindowViewModel.MessageQueue.Enqueue("Could not connect to server.", "OK", (obj) => { }, new object(), false, true, TimeSpan.FromSeconds(6));
            }
        }
        #endregion

        #region Private Methods
        private void AddNewUserToCollection(int employeeId)
        {
            var user = (Manager)IMainWindowViewModel.User;
            switch (Permission)
            {
                case PermissionsEnum.Manager:
                    user.MyEmployees.Add(new Manager(Permission, employeeId, user.PersonId, FirstName, LastName, Email));
                    break;
                case PermissionsEnum.Employee:
                    user.MyEmployees.Add(new Employee(Permission, employeeId, user.PersonId, FirstName, LastName, Email));
                    break;
            }
        }
        private void SetPropertiesToDefault()
        {
            Email     = default;
            FirstName = default;
            LastName  = default;
        }
        #endregion
    }
}
