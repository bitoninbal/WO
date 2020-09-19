using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using WOCommon.Enums;
using WODataAccess.Interfaces;

namespace WODataAccess.DataAccess
{
    public class UpdatesDataAccess: BaseDataAccess, IUpdatesDataAccess
    {
        public async Task AddUpdateAsync(int employeeId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "INSERT INTO Updates(UserId) VALUES(@UserId)";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@UserId", employeeId);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return;
            }
            catch (Exception e)
            {
                return;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }

        public async Task<bool> IsUserHasUpdateAsync(int employeeId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT * FROM Updates WHERE UserId = @EmployeeId";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows) return false;

                    await DeleteUserUpdatesRowsAsync(employeeId);

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }

        public async Task UpdateFieldAsync<T>(int rowId, string columnName, DbTables tableName, T newValue)
        {
            var cnn   = new SqlConnection(ConnectionString);
            var query = $"UPDATE {tableName} SET {columnName} = @NewValue WHERE Id = @Id";
            var cmd   = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@NewValue", newValue);
            cmd.Parameters.AddWithValue("@Id", rowId);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }

        #region Private Methods
        private async Task DeleteUserUpdatesRowsAsync(int employeeId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "DELETE FROM Updates WHERE UserId = @EmployeeId";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception e)
            {
                return;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        #endregion
    }
}
