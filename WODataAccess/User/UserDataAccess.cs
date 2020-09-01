using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WODataAccess.Models;

namespace WODataAccess.User
{
    /// <summary>
    /// Data access for registering service.
    /// </summary>
    public class UserDataAccess: BaseDataAccess, IUserDataAccess
    {
        #region Public Methods
        public async Task DeleteEmployeeDataAccessAsync(int personId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "Delete From Users WHERE Id = @PersonId";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@PersonId", personId);
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
        public async Task<IEnumerable<UserModel>> GetEmployeesDataAccessAsync(int managerId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT Id, Email, FirstName, LastName, Permission, DirectManager FROM Users WHERE DirectManager = @DirectManager";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@DirectManager", managerId);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows) return null;

                    var employees = new List<UserModel>();

                    while (await reader.ReadAsync())
                    {
                        employees.Add(new UserModel
                        {
                            Id = await reader.GetFieldValueAsync<int>(0),
                            Email = await reader.GetFieldValueAsync<string>(1),
                            FirstName = await reader.GetFieldValueAsync<string>(2),
                            LastName = await reader.GetFieldValueAsync<string>(3),
                            Permission = await reader.GetFieldValueAsync<string>(4),
                            DirectManager = await reader.GetFieldValueAsync<int>(5),
                        });
                    }

                    return employees;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task<int> GetEmployeeIdAsync(string email)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT Id FROM Users WHERE Email = @Email";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (!reader.HasRows) return 0;

                    await reader.ReadAsync();

                    return await reader.GetFieldValueAsync<int>(0);
                }
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task<bool> IsEmployeeEmailExistAsync(string email)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT Id FROM Users WHERE Email = @Email";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@Email", email);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (!reader.HasRows) return false;

                    await reader.ReadAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task<UserModel> LoginDataAccessAsync(string userName, string hashedPassword)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT Id, Email, FirstName, LastName, Permission, DirectManager FROM Users WHERE Email = @UserName AND Password = @Password";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    if (!reader.HasRows) return null;

                    await reader.ReadAsync();

                    return new UserModel
                    {
                        Id = await reader.GetFieldValueAsync<int>(0),
                        Email = await reader.GetFieldValueAsync<string>(1),
                        FirstName = await reader.GetFieldValueAsync<string>(2),
                        LastName = await reader.GetFieldValueAsync<string>(3),
                        Permission = await reader.GetFieldValueAsync<string>(4),
                        DirectManager = await reader.GetFieldValueAsync<int>(5),
                    };
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task RegisterDataAccessAsync(string FirstName,
                                                  string LastName,
                                                  string Email,
                                                  string Password,
                                                  string Permission,
                                                  int DirectManager)
        {
            var cnn   = new SqlConnection(ConnectionString);
            var query = "INSERT INTO Users(Email, FirstName, LastName, Password, Permission, DirectManager) " +
                        "VALUES(@Email, @FirstName, @LastName, @Password, @Permission, @DirectManager)";
            var cmd   = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@Permission", Permission);
            cmd.Parameters.AddWithValue("@DirectManager", DirectManager);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {

            }
            finally
            {
                await cmd.DisposeAsync();
                await cnn.CloseAsync();
            }
        }
        public async Task UpdateFieldAsync<T>(int id, T newValue, string columnName)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = $"UPDATE Users SET {columnName} = @NewValue WHERE Id = @Id";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@NewValue", newValue);
            cmd.Parameters.AddWithValue("@Id", id);
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
        #endregion
    }
}
