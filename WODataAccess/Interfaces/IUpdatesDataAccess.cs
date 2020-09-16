using System.Threading.Tasks;
using WOCommon.Enums;

namespace WODataAccess.Interfaces
{
    public interface IUpdatesDataAccess
    {
        Task AddUpdateAsync(int employeeId);
        Task<bool> IsUserHasUpdateAsync(int employeeId);
        Task UpdateFieldAsync<T>(int rowId, string columnName, DbTables tableName, T newValue);
    }
}
