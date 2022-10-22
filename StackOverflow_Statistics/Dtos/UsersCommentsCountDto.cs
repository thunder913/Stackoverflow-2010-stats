namespace StackOverflow_Statistics.Dtos
{
    public class UsersCommentsCountDto
    {
        public int Position { get; set; }
        public int? Id { get; set; }
        public string DisplayName { get; set; }
        public int? CommentCount { get; set; }
        public int? Score { get; set; }
    }
}
