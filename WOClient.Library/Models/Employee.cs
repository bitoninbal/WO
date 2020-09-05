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
                        string email):
            base(permission,
                personId,
                managerId,
                firstName,
                lastName,
                email)
        {}

        #region Public Methods

        #endregion
    }
}
