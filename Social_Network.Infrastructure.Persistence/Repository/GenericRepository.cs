using Microsoft.EntityFrameworkCore;
using Social_Network.Core.Application.Interfaces.Repository;
using Social_Network.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Network.Infrastructure.Persistence.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity:class
    {
        private readonly ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }

        /*ADD*/
        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        /*UPDATE*/
        public virtual async Task UpdateAsync(Entity entity, int id)
        {
            var entry = await _context.Set<Entity>().FindAsync(id);
            _context.Entry(entry).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }


        /*DELETE*/
        public virtual async Task DeleteAsync(Entity entity)
        {
            _context.Set<Entity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        /*GET WITHOUT INCLUDE RELATIONS*/
        public virtual async Task<List<Entity>> GetAllAsyncWithOutInclude()
        {
            return await _context.Set<Entity>().ToListAsync();
        }

        public virtual async Task<List<Entity>> GetAllAsyncWithInclude(List<string> properties)
        {
            var query = _context.Set<Entity>().AsQueryable();
            foreach (string property in properties)
            {
                query = query.Include(property);
            }
            return await query.ToListAsync();
        }


        public virtual async Task<Entity> GetByIdAsync(int id)
        {
            return await _context.Set<Entity>().FindAsync(id);
        }
    }
}
