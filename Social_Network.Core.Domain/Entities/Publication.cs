using Social_Network.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Domain.Entities
{
    public class Publication : AuditableBaseEntity
    {
        public string? Caption { get; set; }
        public string? Picture { get; set; }
        public int UserId { get; set; }
        public string? ImageUser { get; set; }
        public string? UserName { get; set; }
        public ICollection<Comment>? Comments { get; set; }

        public User? Users { get; set; }
    }
}
