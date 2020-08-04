using System;
using System.Drawing;

namespace WODataAccess.Models
{
    public class TaskModel
    {
        public bool IsCompleted { get; set; }
        public string   Description { get; set; }
        public Color    BgColor { get; set; }
        public DateTime FinalDate { get; set; }
        public string   Subject { get; set; }
        public string   Priority { get; set; }
        public int      TaskId { get; set; }
    }
}
