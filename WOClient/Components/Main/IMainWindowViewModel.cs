using MaterialDesignThemes.Wpf;
using WOClient.Library.Models;

namespace WOClient.Components.Main
{
    public interface IMainWindowViewModel
    {
        #region Properties
        static IPerson User { get; protected set; }
        static SnackbarMessageQueue MessageQueue { get; } = new SnackbarMessageQueue();
        #endregion
    }
}
