using System;
using System.Collections.Generic;
using System.Text;
using WOClient.Enums;

namespace WOClient.Components.Base
{
    public interface IRequestViewChange
    {
        event EventHandler<ViewsEnum> SwitchViewRequested;
    }
}
