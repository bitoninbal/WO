using System.Threading.Tasks;

namespace WODataAccess.Interfaces
{
    public interface IUpdatesDataAccess
    {
        Task AddUpdateAsync(int employeeId);
        Task<bool> IsUserHasUpdateAsync(int employeeId);
    }
}
