using Grpc.Net.Client;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;

namespace WOClient.Library.Api.User
{
    public class CommentApi
    {
        internal async Task<int> AddCommentAsync(GrpcChannel channel, int taskId, int personId, string comment)
        {
            var client = new Comments.CommentsClient(channel);
            var input = new CommentInput
            {
                TaskId   = taskId,
                UserId   = personId,
                Comment  = comment
            };
            var response = await client.AddCommentAsync(input);

            return response.Value;
        }
    }
}
