using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Infrastructure.Persistence.Contexts;
using Social_Network.Infrastructure.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Context

            if (configuration.GetValue<bool>("UseMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(o => o.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("Connection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }

            //services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            //m=>m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPublicationRepository, PublicationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFriendRepository, FriendRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            #endregion
        }
    }
}
