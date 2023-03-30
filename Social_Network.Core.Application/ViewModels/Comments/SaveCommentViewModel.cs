using Social_Network.Core.Application.ViewModels.Publications;
using Social_Network.Core.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.ViewModels.Comments
{
    public class SaveCommentViewModel
    {
        public int Id { get; set; }
        public string? Caption { get; set; }
        public int UserId { get; set; }
        public int PublicationId { get; set; }

    }
}
