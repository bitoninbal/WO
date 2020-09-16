using System.Collections.Generic;
using System.Threading.Tasks;
using WODataAccess.Models;

namespace WODataAccess.Interfaces
{
    public interface ICommentsDataAccess
    {
        Task<int> AddCommentDataAccessAsync(int taskId,
                                            int senderId,
                                            string comment);
        Task<IEnumerable<CommentModel>> GetCommentsOfTasktDataAccessAsync(int taskId);
    }
}
