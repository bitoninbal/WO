using System;
using WOClient.Components.Base;
using WOClient.Enums;

namespace WOClient.Components.NewTask
{
    public class NewTaskViewModel: BaseViewModel, INewTaskViewModel
    {
        #region Events
        public event EventHandler<ViewsEnum> SwitchViewRequested;
        #endregion
    }
}
