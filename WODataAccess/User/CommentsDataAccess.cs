using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace WODataAccess.User
{
    public class CommentsDataAccess: BaseDataAccess, ICommentsDataAccess
    {
        public async Task<int> AddCommentDataAccessAsync(int taskId, int userId, string comment)
        {
            var cnn   = new SqlConnection(ConnectionString);
            var query = "INSERT INTO Comments(TaskId, UserId, Comment) " +
                        "VALUES(@TaskId, @UserId, @Comment); SELECT SCOPE_IDENTITY();";
            var cmd   = new SqlCommand(query, cnn);

            cmd.Parameters.AddWithValue("@TaskId", taskId);
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Comment", comment);
            cmd.CommandType = CommandType.Text;

            try
            {
                await cnn.OpenAsync();

                var result    = await cmd.ExecuteScalarAsync();
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
    }
}
