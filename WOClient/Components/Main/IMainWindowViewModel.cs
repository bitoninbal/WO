using System;
using System.Collections.Generic;
using System.Text;
using WOClient.Components.Base;
using WOClient.Components.Login;

namespace WOClient.Components.Main
{
    public interface IMainWindowViewModel
    {
        IBaseViewModel CurrentVm { get; set; }
        ILoginViewModel LoginVm { get; set; }
    }
}
