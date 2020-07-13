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

        public override async Task GetEmployees(ManagerIdInput request, IServerStreamWriter<UserData> responseStream, ServerCallContext context)
        {
            var result = await _dataAccess.GetEmployeesDataAccessAsync(request.ManagerId);

            if (result is null)
            {
                await responseStream.WriteAsync(new UserData());

                return;
            }

            foreach (var person in result)
            {
                var user = new UserData
                {
                    Id            = person.Id,
                    FirstName     = person.FirstName,
                    LastName      = person.LastName,
                    Email         = person.Email,
                    Permission    = person.Permission,
                    DirectManager = person.DirectManager,
                };

                await responseStream.WriteAsync(user);
            }
        }
    }
}
