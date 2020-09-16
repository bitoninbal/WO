using System;
using WOClient.Components.Base;

namespace WOClient.Components.Login
{
    public interface ILoginViewModel: IBaseViewModel, IRequestViewChange
    {
        #region Events
        event EventHandler UserLoggedIn;
        #endregion
    }
}
