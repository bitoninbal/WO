using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using WOClient.Components.Base;

namespace WOClient.Components.Login
{
    public class LoginViewModel: BaseViewModel , ILoginViewModel
    {
        private string _userName;

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName == value) return;
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }
        public SecureString Password { get; set; }
    }
}
