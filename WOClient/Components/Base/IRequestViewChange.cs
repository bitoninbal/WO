using System;
using WOClient.Enums;

namespace WOClient.Components.Base
{
    public interface IRequestViewChange
    {
        #region Events
        event EventHandler<ViewsEnum> SwitchViewRequested;
        #endregion
    }
}
