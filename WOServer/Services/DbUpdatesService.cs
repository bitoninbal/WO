using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;
using WOCommon.Enums;
using WODataAccess.Interfaces;
using WOServer.Protos;

namespace WOServer.Services
{
    public class DbUpdatesService: DbUpdates.DbUpdatesBase
    {
        public DbUpdatesService(IUpdatesDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #region Fields
        private IUpdatesDataAccess _dataAccess;
        #endregion

        #region Public Methods
        public override async Task<Empty> UpdateBoolField(UpdateBoolFieldInput request, ServerCallContext context)
        {
            await _dataAccess.UpdateFieldAsync(request.Data.RowId,
                                               request.Data.ColumnName,
                                               ConvertDbTablesToString(request.Data.TableName),
                                               request.NewValue);

            return new Empty();
        }
        public override async Task<Empty> UpdateIntField(UpdateIntFieldInput request, ServerCallContext context)
        {
            await _dataAccess.UpdateFieldAsync(request.Data.RowId,
                                               request.Data.ColumnName,
                                               ConvertDbTablesToString(request.Data.TableName),
                                               request.NewValue);

            return new Empty();
        }
        public override async Task<Empty> UpdateStringField(UpdateStringFieldInput request, ServerCallContext context)
        {
            await _dataAccess.UpdateFieldAsync(request.Data.RowId,
                                               request.Data.ColumnName,
                                               ConvertDbTablesToString(request.Data.TableName),
                                               request.NewValue);

            return new Empty();
        }
        public override async Task<Empty> UpdateTimestampField(UpdateTimestampFieldInput request, ServerCallContext context)
        {
            await _dataAccess.UpdateFieldAsync(request.Data.RowId,
                                               request.Data.ColumnName,
                                               ConvertDbTablesToString(request.Data.TableName),
                                               request.NewValue.ToDateTime().ToLocalTime());

            return new Empty();
        }
        #endregion

        #region Private Methods
        private DbTables ConvertDbTablesToString(string table)
        {
            switch (table)
            {
                case "Tasks":
                    return DbTables.Tasks;
                case "Users":
                    return DbTables.Users;
                default:
                    throw new NotSupportedException();
            }
        }
        #endregion
    }
}
