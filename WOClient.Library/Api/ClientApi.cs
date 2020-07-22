using Grpc.Net.Client;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using WOClient.Library.Api.User;
using WOClient.Library.Models;
using WOCommon.Enums;
using WOCommon.Extensions;

namespace WOClient.Library.Api
{
    public class ClientApi: IClientApi
    {
        public ClientApi()
        {
            _usersApi = new UsersApi();
        }

        #region Fields
        private readonly UsersApi _usersApi;
        #endregion

        #region Public Methods
        public async Task DeleteEmployeeAsync(int employeeId)
        {
            var channel = GetChannel();

            await _usersApi.DeleteEmployeeAsync(channel, employeeId);
            await channel.ShutdownAsync();
        }
        public async Task<int> EmployeeRegisterAsync(string firstName,
                                                     string lastName,
                                                     string email,
                                                     SecureString password,
                                                     PermissionsEnum permission,
                                                     int directManager)
        {
            var channel        = GetChannel();
            var hashedPassword = password.HashValue();
            var employeeId = await _usersApi.EmployeeRegisterAsync(channel, firstName, lastName, email, hashedPassword, permission, directManager);

            await channel.ShutdownAsync();

            return employeeId;
        }
        public async Task<ObservableCollection<IPerson>> GetEmployeesAsync(int managerId)
        {
            var channel = GetChannel();
            var result = await _usersApi.GetEmployeesAsync(channel, managerId);

            await channel.ShutdownAsync();

            return result;
        }
        public async Task LoginAsync(string userName, SecureString password)
        {
            var channel        = GetChannel();
            var hashedPassword = password.HashValue();

            await _usersApi.LoginAsync(channel, userName, hashedPassword);
            await channel.ShutdownAsync();
        }
        public async Task UpdateFieldAsync<T>(int personId, T value, string columnName)
        {
            string newValue;
            var channel = GetChannel();

            switch (value)
            {
                case PermissionsEnum permission:
                    newValue = permission.ToString();

                    break;
                case string str:
                    newValue = str;

                    break;
                default:
                    newValue = value.ToString();

                    break;
            }

            await _usersApi.UpdateFieldAsync(channel, personId, newValue, columnName);
            await channel.ShutdownAsync();
        }
        #endregion

        #region Private Methods
        private GrpcChannel GetChannel()
        {
            var httpClientHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };
            var httpClient = new HttpClient(httpClientHandler);
            var options = new GrpcChannelOptions { HttpClient = httpClient };

            return GrpcChannel.ForAddress("https://192.168.1.234:5001", options);
        }
        #endregion
    }
}
