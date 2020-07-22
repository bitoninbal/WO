using System.Collections.ObjectModel;
using System.Security;
using System.Threading.Tasks;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Library.Api
{
    public interface IClientApi
    {
        #region Methods
        Task DeleteEmployeeAsync(int employeeId);
        Task<int> EmployeeRegisterAsync(string firstName,
                                        string lastName,
                                        string email,
                                        SecureString password,
                                        PermissionsEnum permission,
                                        int directManager);
        Task<ObservableCollection<IPerson>> GetEmployeesAsync(int managerId);
        Task LoginAsync(string email, SecureString password);
        Task UpdateFieldAsync<T>(int personId, T value, string columnName);
        #endregion
    }
}