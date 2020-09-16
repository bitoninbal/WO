using System.Threading.Tasks;
using WOCommon.Enums;

namespace WOClient.Library.Models
{
    public class Employee: Person
    {
        public Employee(PermissionsEnum permission,
                        int personId,
                        int managerId,
                        string firstName,
                        string lastName,
                        string email) :
            base(permission,
                personId,
                managerId,
                firstName,
                lastName,
                email)
        {
            Task.Run(InitAsync);
        }

        #region Protected Methods
        protected override async Task InitAsync()
        {
            await InitMyTasksAsync();
        }
        #endregion
    }
}
