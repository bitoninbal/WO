using System.Collections.ObjectModel;
using WOClient.Components.Base;
using WOClient.Library.Models;

namespace WOClient.Components.Comments
{
    public class CommentsViewModel: BaseViewModel, ICommentsViewModel
    {
        public CommentsViewModel()
        {
            Comments = new ObservableCollection<Comment>();
        }

        #region Properties
        public ObservableCollection<Comment> Comments { get; set; }
        #endregion
    }
}
