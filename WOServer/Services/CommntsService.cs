using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;
using WODataAccess.User;
using WOServer.Protos;

namespace WOServer.Services
{
    public class CommntsService: Comments.CommentsBase
    {
        public CommntsService(ICommentsDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #region Fields
        private ICommentsDataAccess _dataAccess;
        #endregion

        #region Public Methods
        public override async Task<Int32Value> AddComment(CommentInput request, ServerCallContext context)
        {
            var commentId = await _dataAccess.AddCommentDataAccessAsync(request.TaskId,
                                                                        request.UserId,
                                                                        request.Comment);

            return new Int32Value { Value = commentId };
        }
        #endregion
    }
}
