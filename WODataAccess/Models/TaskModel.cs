using System;

namespace WODataAccess.Models
{
    public class TaskModel
    {
        public bool     IsArchive { get; set; }
        public bool     IsCompleted { get; set; }
        public string   Description { get; set; }
        public DateTime FinalDate { get; set; }
        public string   Subject { get; set; }
        public string   Priority { get; set; }
        public int      TaskId { get; set; }
        public int      UserId { get; set; }
    }
}
