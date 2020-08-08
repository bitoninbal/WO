namespace WODataAccess.Models
{
    public class CommentModel
    {
        #region Properties
        public int CommentId { get; set; }
        public int SenderId { get; set; }
        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
        public string Message { get; set; }
        #endregion
    }
}
