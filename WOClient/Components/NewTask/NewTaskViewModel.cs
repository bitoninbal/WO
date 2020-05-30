using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WOClient.Components.Base;
using WOClient.Enums;

namespace WOClient.Components.NewTask
{
    public class NewTaskViewModel : BaseViewModel, INewTaskViewModel
    {
        public event EventHandler<ViewsEnum> SwitchViewRequested;
    }
}
