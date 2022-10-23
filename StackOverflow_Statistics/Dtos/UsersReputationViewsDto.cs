namespace StackOverflow_Statistics.Dtos
{
    public class UsersReputationViewsDto
    {
        public long Position { get; set; }
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public int Reputation { get; set; }
        public int Views { get; set; }
        public string Location { get; set; }
    }
}
