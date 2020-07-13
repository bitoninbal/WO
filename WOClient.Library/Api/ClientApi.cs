using Grpc.Net.Client;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOClient.Library.Api.User;
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
        public async Task EmployeeRegisterAsync(string firstName,
                                                string lastName,
                                                string email,
                                                SecureString password,
                                                PermissionsEnum permission,
                                                int directManager)
        {
            var channel        = GetChannel();
            var hashedPassword = password.HashValue();

            await _usersApi.EmployeeRegisterAsync(channel, firstName, lastName, email, hashedPassword, permission, directManager);
            await channel.ShutdownAsync();
        }
        public async Task LoginAsync(string userName, SecureString password)
        {
            var channel        = GetChannel();
            var hashedPassword = password.HashValue();

            await _usersApi.LoginAsync(channel, userName, hashedPassword);
            await channel.ShutdownAsync();
        }
        public async Task<ObservableCollection<UserData>> GetEmployeesAsync(int managerId)
        {
            var channel = GetChannel();
            var result = await _usersApi.GetEmployeesAsync(channel, managerId);

            await channel.ShutdownAsync();

            return result;
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

            return GrpcChannel.ForAddress("https://127.0.0.1:5001", options);
        }
        #endregion
    }
}
