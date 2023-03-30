using Social_Network.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Domain.Entities
{
    public class Comment:AuditableBaseEntity
    {
        public string? Caption { get; set; }
        public int UserId { get; set; }
        public int PublicationId { get; set; }

        public Publication? publication { get; set; }
        public User? user { get; set; }


    }
}
