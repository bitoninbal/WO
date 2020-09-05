using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;

namespace WOClient.Library.Api.User
{
    internal class BaseApi
    {
        internal async Task SendUpdateEventAsync(GrpcChannel channel, int userId)
        {
            var client = new Users.UsersClient(channel);
            var updatesInput = new Int32Value
            {
                Value = userId
            };

            await client.AddUpdateEventAsync(updatesInput);
        }
    }
}
