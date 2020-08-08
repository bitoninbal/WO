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
                                                                        request.SenderId,
                                                                        request.Message);

            return new Int32Value { Value = commentId };
        }
        public override async Task GetCommentsOfTask(Int32Value request, IServerStreamWriter<CommentOutput> responseStream, ServerCallContext context)
        {
            var result = await _dataAccess.GetCommentsOfTasktDataAccessAsync(request.Value);

            if (result is null)
            {
                await responseStream.WriteAsync(new CommentOutput());

                return;
            }

            foreach (var model in result)
            {
                var task = new CommentOutput
                {
                    CommentId       = model.CommentId,
                    SenderId        = model.SenderId,
                    SenderFirstName = model.SenderFirstName,
                    SenderLastName  = model.SenderLastName,
                    Message         = model.Message
                };

                await responseStream.WriteAsync(task);
            }
        }
        #endregion
    }
}
