using Grpc.Core;
using Grpc.Net.Client;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Library.Api.User
{
    internal class UsersApi
    {
        #region Public Methods
        internal async Task EmployeeRegisterAsync(GrpcChannel channel,
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
        internal async Task LoginAsync(GrpcChannel channel, string userName, string password)
        {
            var client = new Users.UsersClient(channel);
            var input = new LoginInput
            {
                Email = userName,
                Password = password
            };

            var result = await client.LoginRequsetAsync(input);

            if (result.Id == 0) return;

            LoggedInUser.Instance.Id            = result.Id;
            LoggedInUser.Instance.FirstName = result.FirstName;
            LoggedInUser.Instance.LastName      = result.LastName;
            LoggedInUser.Instance.Email         = result.Email;
            LoggedInUser.Instance.DirectManager = result.DirectManager;

            if (result.Permission.Equals("Employee"))
            {
                LoggedInUser.Instance.Permission = PermissionsEnum.Employee;
            } 
            else
            {
                LoggedInUser.Instance.Permission = PermissionsEnum.Manager;
            }
        }

        internal async Task<ObservableCollection<UserData>> GetEmployeesAsync(GrpcChannel channel, int managerId)
        {
            var client = new Users.UsersClient(channel);
            var input = new ManagerIdInput
            {
                ManagerId = managerId
            };

            using var result = client.GetEmployees(input);
            var employees = new ObservableCollection<UserData>();

            //if (result.ResponseStream.Current is null) return null;

            while (await result.ResponseStream.MoveNext())
            {
                employees.Add(new UserData
                {
                    Id            = result.ResponseStream.Current.Id,
                    FirstName     = result.ResponseStream.Current.FirstName,
                    LastName      = result.ResponseStream.Current.LastName,
                    Email         = result.ResponseStream.Current.Email,
                    Permission    = result.ResponseStream.Current.Permission,
                    DirectManager = result.ResponseStream.Current.DirectManager,
                });
            }

            return employees;
        }
        #endregion
    }
}
