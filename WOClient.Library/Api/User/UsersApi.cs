using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOClient.Library.Models;
using WOCommon.Enums;

namespace WOClient.Library.Api.User
{
    internal class UsersApi: BaseApi
    {
        #region Public Methods
        internal async Task AddUpdateEventAsync(GrpcChannel channel, int employeeId)
        {
            var client = new Users.UsersClient(channel);
            var input  = new Int32Value
            {
                Value = employeeId
            };

            await client.AddUpdateEventAsync(input);
        }
        internal async Task DeleteEmployeeAsync(GrpcChannel channel, int employeeId)
        {
            var client = new Users.UsersClient(channel);
            var input  = new PersonIdInput
            {
                PersonId = employeeId
            };

            await client.DeleteEmployeeAsync(input);
        }
        internal async Task<bool> IsMailExistAsync(GrpcChannel channel, string email)
        {
            var client = new Users.UsersClient(channel);
            var input  = new StringValue
            {
                Value = email
            };
            var result = await client.IsMailExistAsync(input);

            return result.Value;
        }
        internal async Task<int> EmployeeRegisterAsync(GrpcChannel channel,
                                                       string firstName,
                                                       string lastName,
                                                       string email,
                                                       string hashedPassword,
                                                       PermissionsEnum permission,
                                                       int directManager)
        {
            var client = new Users.UsersClient(channel);
            var input  = new RegisterInput
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = hashedPassword,
                Permission = permission.ToString(),
                DirectManager = directManager
            };
            var result = await client.RegisterAsync(input);

            return result.Value;
        }
        internal async Task<ObservableCollection<IPerson>> GetEmployeesAsync(GrpcChannel channel, int managerId)
        {
            var client = new Users.UsersClient(channel);
            var input  = new PersonIdInput
            {
                PersonId = managerId
            };

            using var result = client.GetEmployees(input);
            var employees    = new ObservableCollection<IPerson>();

            while (await result.ResponseStream.MoveNext())
            {
                if (result.ResponseStream.Current.Id == 0) return null;

                var permission = ConvertStringToPermissionsEnum(result.ResponseStream.Current.Permission);

                switch (permission)
                {
                    case PermissionsEnum.Manager:
                        employees.Add(new Manager(permission,
                                                  result.ResponseStream.Current.Id,
                                                  result.ResponseStream.Current.DirectManager,
                                                  result.ResponseStream.Current.FirstName,
                                                  result.ResponseStream.Current.LastName,
                                                  result.ResponseStream.Current.Email));
                        break;
                    case PermissionsEnum.Employee:
                        employees.Add(new Employee(permission,
                                                   result.ResponseStream.Current.Id,
                                                   result.ResponseStream.Current.DirectManager,
                                                   result.ResponseStream.Current.FirstName,
                                                   result.ResponseStream.Current.LastName,
                                                   result.ResponseStream.Current.Email));
                        break;
                }
            }

            return employees;
        }
        internal async Task LoginAsync(GrpcChannel channel, string userName, string password)
        {
            var client = new Users.UsersClient(channel);
            var input  = new LoginInput
            {
                Email    = userName,
                Password = password
            };
            var result = await client.LoginRequsetAsync(input);

            if (result.Id == 0) return;

            LoggedInUser.Instance.Id = result.Id;
            LoggedInUser.Instance.FirstName = result.FirstName;
            LoggedInUser.Instance.LastName = result.LastName;
            LoggedInUser.Instance.Email = result.Email;
            LoggedInUser.Instance.DirectManager = result.DirectManager;

            if (result.Permission.Equals("Employee"))
                LoggedInUser.Instance.Permission = PermissionsEnum.Employee;
            else
                LoggedInUser.Instance.Permission = PermissionsEnum.Manager;
        }
        internal async Task<bool> RequestUserUpdateAsync(GrpcChannel channel, int userId)
        {
            var client = new Users.UsersClient(channel);
            var input  = new Int32Value
            {
                Value = userId
            };
            var result = await client.RequestUserUpdateAsync(input);

            return result.Value;
        }
        #endregion

        #region Private Methods
        private PermissionsEnum ConvertStringToPermissionsEnum(string permissionString)
        {
            switch (permissionString)
            {
                case "Employee":
                    return PermissionsEnum.Employee;
                case "Manager":
                    return PermissionsEnum.Manager;
                default:
                    return PermissionsEnum.Employee;
            }
        }
        #endregion
    }
}
