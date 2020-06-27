using Grpc.Net.Client;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Library.Api.User
{
    internal class UsersApi
    {
        #region Public Methods
        public async Task EmployeeRegisterAsync(GrpcChannel channel,
                                                string firstName,
                                                string lastName,
                                                string email,
                                                string hashedPassword,
                                                PermissionsEnum permission,
                                                int directManager)
        {
            var client = new Users.UsersClient(channel);
            var input = new RegisterInput
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = hashedPassword,
                Permission = permission.ToString(),
                DirectManager = directManager
            };

            await client.RegisterAsync(input);
        }
        public async Task LoginAsync(GrpcChannel channel, string userName, string password)
        {
            var client = new Users.UsersClient(channel);
            var input = new LoginInput
            {
                Email = userName,
                Password = password
            };

            var result = await client.LoginRequsetAsync(input);

            if (result.Id == 0) return;

            UserInfo.Instance.Id = result.Id;
            UserInfo.Instance.FirstName = result.FirstName;
            UserInfo.Instance.LastName = result.LastName;
            UserInfo.Instance.Email = result.Email;
            UserInfo.Instance.Permission = result.Permission;
            UserInfo.Instance.DirectManager = result.DirectManager;
        }
        #endregion
    }
}
