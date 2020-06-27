using System.Threading.Tasks;
using WODataAccess.Models;

namespace WODataAccess.User
{
    /// <summary>
    /// Interface for dealing with user data access component.
    /// </summary>
    public interface IUserDataAccess
    {
        #region Methods
        Task<UserModel> LoginDataAccessAsync(string userName, string hashedPassword);
        Task RegisterDataAccessAsync(string FirstName,
                                     string LastName,
                                     string Email,
                                     string Password,
                                     string Permission,
                                     int DirectManager);
        #endregion
    }
}
