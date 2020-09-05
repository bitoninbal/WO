using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;
using WODataAccess.User;
using WOServer.Protos;

namespace WOServer.Services
{
    public class UsersService: Users.UsersBase
    {
        public UsersService(IUserDataAccess userDataAccess, IUpdatesDataAccess updatesDataAccess)
        {
            _updatesDataAccess = updatesDataAccess;
            _userDataAccess    = userDataAccess;
        }

        #region Fields
        private IUpdatesDataAccess _updatesDataAccess;
        private IUserDataAccess _userDataAccess;
        #endregion

        #region Public Methods
        public override async Task<Empty> AddUpdateEvent(Int32Value request, ServerCallContext context)
        {
            await _updatesDataAccess.AddUpdateAsync(request.Value);

            return new Empty();
        }
        public override async Task<Empty> DeleteEmployee(PersonIdInput request, ServerCallContext context)
        {
            await _userDataAccess.DeleteEmployeeDataAccessAsync(request.PersonId);

            return new Empty();
        }
        public override async Task GetEmployees(PersonIdInput request, IServerStreamWriter<UserData> responseStream, ServerCallContext context)
        {
            var result = await _userDataAccess.GetEmployeesDataAccessAsync(request.PersonId);

            if (result is null)
            {
                await responseStream.WriteAsync(new UserData());

                return;
            }

            foreach (var person in result)
            {
                var user = new UserData
                {
                    Id = person.Id,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Permission = person.Permission,
                    DirectManager = person.DirectManager,
                };

                await responseStream.WriteAsync(user);
            }
        }
        public override async Task<UserData> LoginRequset(LoginInput request, ServerCallContext context)
        {
            var result = await _userDataAccess.LoginDataAccessAsync(request.Email, request.Password);

            if (result == null) return new UserData();

            return new UserData
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Permission = result.Permission,
                DirectManager = result.DirectManager,
            };
        }
        public override async Task<Int32Value> Register(RegisterInput request, ServerCallContext context)
        {
            await _userDataAccess.RegisterDataAccessAsync(request.FirstName,
                                                      request.LastName,
                                                      request.Email,
                                                      request.Password,
                                                      request.Permission,
                                                      request.DirectManager);

            var employeeId = await _userDataAccess.GetEmployeeIdAsync(request.Email);

            return new Int32Value
            {
                Value = employeeId
            };
        }
        public override async Task<BoolValue> IsMailExist(StringValue request, ServerCallContext context)
        {
            var result = await _userDataAccess.IsEmployeeEmailExistAsync(request.Value);

            return new BoolValue
            {
                Value = result
            };
        }
        public override async Task<Empty> UpdateIntField(UpdateIntFieldInput request, ServerCallContext context)
        {
            await _userDataAccess.UpdateFieldAsync(request.PersonId, request.NewValue, request.ColumnName);

            return new Empty();
        }
        public override async Task<Empty> UpdateStringField(UpdateStringFieldInput request, ServerCallContext context)
        {
            await _userDataAccess.UpdateFieldAsync(request.PersonId, request.NewValue, request.ColumnName);

            return new Empty();
        }
        public override async Task<BoolValue> RequestUserUpdate(Int32Value request, ServerCallContext context)
        {
            var result = await _updatesDataAccess.IsUserHasUpdateAsync(request.Value);

            return new BoolValue
            {
                Value = result
            };
        }
        #endregion
    }
}
