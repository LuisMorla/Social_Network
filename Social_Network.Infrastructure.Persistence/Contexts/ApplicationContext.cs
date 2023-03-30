using Microsoft.EntityFrameworkCore;
using Social_Network.Core.Domain.Common;
using Social_Network.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Publication> publications { get; set; }

        public DbSet<Friend> friends { get; set; }
        public DbSet<Comment> comments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "Generico";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "Generico";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Table
            builder.Entity<User>().ToTable("users");
            builder.Entity<Publication>().ToTable("Publications");
            builder.Entity<Friend>().ToTable("Friends");
            builder.Entity<Comment>().ToTable("Comments");
            #endregion

            #region Keys
            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<Publication>().HasKey(x => x.Id);
            builder.Entity<Friend>().HasKey(x => x.Id);
            builder.Entity<Comment>().HasKey(x => x.Id);
            #endregion

            #region "Relation Ships"
            builder.Entity<Publication>().HasOne(x=>x.Users).WithMany(x=>x.Publications).HasForeignKey(x=>x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Friend>().HasOne(x => x.User1).WithMany(x => x.Friends1).HasForeignKey(x => x.UserFirst).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Friend>().HasOne(x => x.User2).WithMany(x => x.Friends2).HasForeignKey(x => x.UserSecond).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Comment>().HasOne(x => x.user).WithMany(x => x.Comments).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Comment>().HasOne(x => x.publication).WithMany(x => x.Comments).HasForeignKey(x => x.PublicationId).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Configuration
            #endregion

        }

    }
}
