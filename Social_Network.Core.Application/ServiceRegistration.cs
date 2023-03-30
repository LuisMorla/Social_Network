using Microsoft.Extensions.DependencyInjection;
using Social_Network.Core.Application.Interfaces.Services;
using Social_Network.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPublicationService, PublicationService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFriendService, FriendService>();
            #endregion
        }
    }
}
