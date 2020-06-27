using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;
using WODataAccess.User;
using WOServer.Protos;

namespace WOServer.Services
{
    public class UsersService: Users.UsersBase
    {
        public UsersService(IUserDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #region Fields
        private IUserDataAccess _dataAccess; 
        #endregion

        public override async Task<Empty> Register(RegisterInput request, ServerCallContext context)
        {
            await _dataAccess.RegisterDataAccessAsync(request.FirstName,
                                                      request.LastName,
                                                      request.Email,
                                                      request.Password,
                                                      request.Permission,
                                                      request.DirectManager);

            return new Empty();
        }

        public override async Task<UserData> LoginRequset(LoginInput request, ServerCallContext context)
        {
            var result = await _dataAccess.LoginDataAccessAsync(request.Email, request.Password);

            if (result == null) return new UserData();

            return new UserData
            {
                Id            = result.Id,
                FirstName     = result.FirstName,
                LastName      = result.LastName,
                Email         = result.Email,
                Permission    = result.Permission,
                DirectManager = result.DirectManager,
            };
        }
    }
}
