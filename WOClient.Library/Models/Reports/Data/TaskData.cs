using System;
using WOCommon.Enums;

namespace WOClient.Library.Models.Reports.Data
{
    public class TaskData
    {
        #region Properties
        public bool IsArchive { get; set; }
        public bool IsCompleted { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinalDate { get; set; }
        public PriorityEnum Priority { get; set; }
        #endregion
    }
}
