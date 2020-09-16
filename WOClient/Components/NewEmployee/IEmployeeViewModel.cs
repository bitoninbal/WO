using System.Security;
using System.Threading.Tasks;
using WOClient.Components.Base;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Components.NewEmployee
{
    public interface IEmployeeViewModel : IBaseViewModel
    {
        #region Properties
        string Email { get; }
        SecureString Password { get; }
        string FirstName { get; }
        string LastName { get; }
        PermissionsEnum Permission { get; }
        #endregion

        #region Methods
        Task RegisterAsync();
        void SetProperties(IPerson employee);
        void Reset();
        #endregion
    }
}
