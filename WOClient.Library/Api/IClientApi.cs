using System.Collections.ObjectModel;
using System.Security;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOCommon.Enums;

namespace WOClient.Library.Api
{
    public interface IClientApi
    {
        #region Methods
        Task LoginAsync(string email, SecureString password);

        Task EmployeeRegisterAsync(string firstName,
                                   string lastName,
                                   string email,
                                   SecureString password,
                                   PermissionsEnum permission,
                                   int directManager);

        Task<ObservableCollection<UserData>> GetEmployeesAsync(int managerId);

        Task DeleteEmployeeAsync(int employeeId);
        #endregion
    }
}