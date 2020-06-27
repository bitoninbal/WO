using System.Security;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOCommon.Enums;

namespace WOClient.Components.NewEmployee
{
    public interface INewEmployeeViewModel : IBaseViewModel
    {
        #region Properties
        string Email { get; }
        SecureString Password { get; }
        string FirstName { get; }
        string LastName { get; }
        PermissionsEnum Permission { get; }
        int DirectManager { get; }
        #endregion

        #region Methods
        Task EmployeeRegisterAsync();
        #endregion
    }
}
