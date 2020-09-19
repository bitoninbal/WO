using System;
using System.Security;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Components.Main;
using WOCommon.Extensions;

namespace WOClient.Components.Profile
{
    public class ProfileViewModel: BaseViewModel, IBaseViewModel
    {

        #region Properties
        public SecureString CurrentPassword { get; set; }
        public SecureString NewPassword { get; set; }
        public SecureString RepeatPassword { get; set; }
        #endregion

        #region Public Methods
        public async Task SaveAsync()
        {
            var hashedNewPassword    = NewPassword.HashValue();
            var hashedRepeatPassword = RepeatPassword.HashValue();

            if (!hashedNewPassword.Equals(hashedRepeatPassword))
            {
                IMainWindowViewModel.MessageQueue.Enqueue("The new password don't much with the repeated one.",
                                                          "OK",
                                                          (obj) => { },
                                                          new object(),
                                                          false,
                                                          true,
                                                          TimeSpan.FromSeconds(6));

                return;
            }

            var result = await IMainWindowViewModel.User.TryChangePasswordAsync(IMainWindowViewModel.User.Email, CurrentPassword, hashedNewPassword);

            if (result)
            {
                IMainWindowViewModel.MessageQueue.Enqueue("Password has changed successfully.",
                                                          "OK",
                                                          (obj) => { },
                                                          new object(),
                                                          false,
                                                          true,
                                                          TimeSpan.FromSeconds(6));
            }
            else
            {
                IMainWindowViewModel.MessageQueue.Enqueue("The old password do not much to one of the records in the server.",
                                                          "OK",
                                                          (obj) => { },
                                                          new object(),
                                                          false,
                                                          true,
                                                          TimeSpan.FromSeconds(6));
            }

            CurrentPassword.Clear();
            NewPassword.Clear();
            RepeatPassword.Clear();
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
