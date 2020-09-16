using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using WODataAccess.Interfaces;
using WODataAccess.Models;

namespace WODataAccess.DataAccess
{
    public class CommentsDataAccess: BaseDataAccess, ICommentsDataAccess
    {
        public async Task<int> AddCommentDataAccessAsync(int taskId,
                                                         int senderId,
                                                         string comment)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "INSERT INTO Comments(TaskId, UserId, Comment) " +
                        "VALUES(@TaskId, @SenderId, @Comment); SELECT SCOPE_IDENTITY();";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@TaskId", taskId);
            cmd.Parameters.AddWithValue("@SenderId", senderId);
            cmd.Parameters.AddWithValue("@Comment", comment);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                var result = await cmd.ExecuteScalarAsync();
                var commentId = Convert.ToInt32(result);

                return commentId;
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
        public async Task<IEnumerable<CommentModel>> GetCommentsOfTasktDataAccessAsync(int taskId)
        {
            var cnn = new SqlConnection(ConnectionString);
            var query = "SELECT c.Id, c.UserId, c.Comment, u.FirstName, u.LastName " +
                        "FROM Comments c, Users u " +
                        "WHERE c.TaskId = @TaskId AND c.UserId = u.Id";
            var cmd = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@TaskId", taskId);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows) return null;

                    var comments = new List<CommentModel>();

                    while (await reader.ReadAsync())
                    {
                        comments.Add(new CommentModel
                        {
                            CommentId = await reader.GetFieldValueAsync<int>(0),
                            SenderId = await reader.GetFieldValueAsync<int>(1),
                            SenderFirstName = await reader.GetFieldValueAsync<string>(3),
                            SenderLastName = await reader.GetFieldValueAsync<string>(4),
                            Message = await reader.GetFieldValueAsync<string>(2)
                        });
                    }

                    return comments;
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
    }
}
