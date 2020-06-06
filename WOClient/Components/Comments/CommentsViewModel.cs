using System.Collections.ObjectModel;
using WOClient.Components.Base;
using WOClient.Models;

namespace WOClient.Components.Comments
{
    public class CommentsViewModel: BaseViewModel, ICommentsViewModel
    {
        public CommentsViewModel()
        {
            Comments = new ObservableCollection<Comment>();

            var task1 = new Comment
            {
                TaskName = "Comment 1",
                EmployeeName = "Amir L",
                EmployeeComment = "I have a comment",
                Reply = "Very good comment. Task is very good."
            };

            var task2 = new Comment
            {
                TaskName = "Comment 2",
                EmployeeName = "Inbal B",
                EmployeeComment = "I have another comment",
                Reply = "Not so Very good comment. comment is very good."
            };

            Comments.Add(task1);
            Comments.Add(task2);
        }

        #region Properties
        public ObservableCollection<Comment> Comments { get; set; }
        #endregion
    }
}
