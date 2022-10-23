using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow_Statistics.Dtos
{
    public class MostViewedPostsDto
    {
        public long Position { get; set; }
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedAt => CreationDate.ToString("dd/MM/yyyy");
        public bool HasAcceptedAnswer => AcceptedAnswer != null ? true : false;
        public string Creator { get; set; }
        public long Views { get; set; }
        public long? AcceptedAnswer { get; set; }
        public long? CreatorId { get; set; }
    }
}
