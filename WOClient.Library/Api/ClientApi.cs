﻿using Grpc.Net.Client;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
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

            return GrpcChannel.ForAddress("https://192.168.1.103:5001", options);
        }
        #endregion
    }
}
