using Social_Network.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Domain.Entities
{
    public class User : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUser { get; set; }
        public string UserName { get; set; }

        public bool IsVerified { get; set; }

        public ICollection<Publication>? Publications { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Friend>? Friends1 { get; set; }
        public ICollection<Friend>? Friends2 { get; set; }



    }
}
