using System.Linq.Expressions;
using AutoParts.Application.Exceptions;
using AutoParts.Application.Repositories;
using AutoParts.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Infrastructure.Repositories
{
    public abstract class BaseRepository<T, TContext> : IRepository<T>
        where T : IEntity
        where TContext : DbContext
    {
        public BaseRepository(TContext _context)
        {
            context = _context;
            Set = context.Set<T>();
        }

        protected readonly TContext context;
        protected readonly DbSet<T> Set;

        public virtual async Task<T> Create(T model)
        {
            await Set.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity != null)
                Set.Remove(entity);
            else
                throw new NotFoundException("Entity with provided id was not found.");

            await context.SaveChangesAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await Set.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<T> Update(T model)
        {
            Set.Update(model);

            await context.SaveChangesAsync();

            return model;
        }

        public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null!)
        {
            IQueryable<T> query = null!;
            List<T> models = new();

            if (expression != null)
            {
                query = Set.Where(expression);
                models = await query.ToListAsync();
            }
            else
                models = await Set.ToListAsync();

            return models;
        }
    }
}