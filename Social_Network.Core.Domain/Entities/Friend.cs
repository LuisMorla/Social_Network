using Social_Network.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Domain.Entities
{
    public class Friend : AuditableBaseEntity
    {
        public int UserFirst { get; set; }
        public int UserSecond { get; set; }
        public User? User1 { get; set; }
        public User? User2 { get; set; }


    }
}
