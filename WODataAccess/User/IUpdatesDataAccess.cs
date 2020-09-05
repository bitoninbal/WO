using System.Threading.Tasks;

namespace WODataAccess.User
{
    public interface IUpdatesDataAccess
    {
        Task AddUpdateAsync(int employeeId);
        Task<bool> IsUserHasUpdateAsync(int employeeId);
    }
}
