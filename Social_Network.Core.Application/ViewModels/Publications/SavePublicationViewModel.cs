using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application.ViewModels.Publications
{
    public class SavePublicationViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Caption { get; set; }
        public string? Picture { get; set; }
        public string? ImageUser { get; set; }
        public string? UserName { get; set; }

        public IFormFile? File { get; set; }

    }
}
