using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOCommon.Enums;

namespace WOClient.Library.Api.User
{
    internal class DbUpdatesApi: BaseApi
    {
        internal async Task UpdateFieldAsync(GrpcChannel channel, int objectId, string columnName, DbTables tableName, bool value)
        {
            var client = new DbUpdates.DbUpdatesClient(channel);
            var input  = new UpdateBoolFieldInput
            {
                Data = CreateUpdateDataMessage(objectId, columnName, tableName),
                NewValue = value
            };

            await client.UpdateBoolFieldAsync(input);
        }
        internal async Task UpdateFieldAsync(GrpcChannel channel, int objectId, string columnName, DbTables tableName, int value)
        {
            var client = new DbUpdates.DbUpdatesClient(channel);
            var input  = new UpdateIntFieldInput
            {
                Data     = CreateUpdateDataMessage(objectId, columnName, tableName),
                NewValue = value
            };

            await client.UpdateIntFieldAsync(input);
        }
        internal async Task UpdateFieldAsync(GrpcChannel channel, int objectId, string columnName, DbTables tableName, string value)
        {
            var client = new DbUpdates.DbUpdatesClient(channel);
            var input  = new UpdateStringFieldInput
            {
                Data     = CreateUpdateDataMessage(objectId, columnName, tableName),
                NewValue = value
            };

            await client.UpdateStringFieldAsync(input);
        }
        internal async Task UpdateFieldAsync(GrpcChannel channel, int objectId, string columnName, DbTables tableName, DateTime value)
        {
            var client = new DbUpdates.DbUpdatesClient(channel);
            var input  = new UpdateTimestampFieldInput
            {
                Data = CreateUpdateDataMessage(objectId, columnName, tableName),
                NewValue = value.ToTimestamp()
            };

            await client.UpdateTimestampFieldAsync(input);
        }

        #region Private Methods
        private UpdateData CreateUpdateDataMessage(int objectId, string columnName, DbTables tableName)
        {
            return new UpdateData
            {
                RowId      = objectId,
                ColumnName = columnName,
                TableName  = tableName.ToString()
            };
        }
        #endregion
    }
}
