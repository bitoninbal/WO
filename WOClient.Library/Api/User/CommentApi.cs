using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WOClient.Library.Api.Protos;
using WOClient.Library.Models;

namespace WOClient.Library.Api.User
{
    public class CommentApi
    {
        internal async Task<int> AddCommentAsync(GrpcChannel channel,
                                                 int taskId,
                                                 int senderId,
                                                 int userIdToBeUpdated,
                                                 string comment)
        {
            var client = new Comments.CommentsClient(channel);
            var input  = new CommentInput
            {
                TaskId   = taskId,
                SenderId = senderId,
                Message  = comment
            };
            var response = await client.AddCommentAsync(input);

            await SendUpdateEventAsync(channel, userIdToBeUpdated);

            return response.Value;
        }

        internal async Task<ObservableCollection<Comment>> GetCommentsOfTaskAsync(GrpcChannel channel, int taskId)
        {
            var client = new Comments.CommentsClient(channel);
            var input  = new Int32Value
            {
                Value = taskId
            };

            using var result = client.GetCommentsOfTask(input);
            var commets      = new ObservableCollection<Comment>();

            while (await result.ResponseStream.MoveNext())
            {
                if (result.ResponseStream.Current.CommentId == 0) continue;

                var comment = new Comment
                {
                    CommentId       = result.ResponseStream.Current.CommentId,
                    SenderId        = result.ResponseStream.Current.SenderId,
                    SenderFirstName = result.ResponseStream.Current.SenderFirstName,
                    SenderLastName  = result.ResponseStream.Current.SenderLastName,
                    Message         = result.ResponseStream.Current.Message
                };

                commets.Add(comment);
            }

            return commets;
        }

        internal async Task SendUpdateEventAsync(GrpcChannel channel, int userId)
        {
            var client       = new Users.UsersClient(channel);
            var updatesInput = new Int32Value
            {
                Value = userId
            };

            await client.AddUpdateEventAsync(updatesInput);
        }
    }
}
