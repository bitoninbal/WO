﻿using System.Collections.ObjectModel;
using WOClient.Components.Base;
using WOClient.Models;

namespace WOClient.Components.Comments
{
    public interface ICommentsViewModel: IBaseViewModel
    {
        #region Properties
        ObservableCollection<Comment> Comments { get; set; }
        #endregion
    }
}