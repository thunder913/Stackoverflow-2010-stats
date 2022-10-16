using System;

namespace StackOverflow_Statistics.Models
{
    public class Badge
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
