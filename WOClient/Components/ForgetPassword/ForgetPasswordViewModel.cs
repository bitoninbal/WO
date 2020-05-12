using System;
using System.Collections.Generic;
using System.Text;
using WOClient.Components.Base;
using WOClient.Components.Login;
using WOClient.Enums;

namespace WOClient.Components.ForgetPassword
{
    public class ForgetPasswordViewModel : BaseViewModel, IForgetPasswordViewModel
    {
        public event EventHandler<ViewsEnum> SwitchViewRequested;

        #region Fields
        private string _email;
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
        #endregion

    }
}
