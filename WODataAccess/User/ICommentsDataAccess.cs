using System.Threading.Tasks;

namespace WODataAccess.User
{
    public interface ICommentsDataAccess
    {
        Task<int> AddCommentDataAccessAsync(int taskId, int userId, string comment);
    }
}
