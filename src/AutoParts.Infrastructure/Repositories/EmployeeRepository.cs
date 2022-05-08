using System.Linq.Expressions;
using AutoParts.Application.Repositories;
using AutoParts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoParts.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, ApplicationDbContext>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {

        }

        public override async Task<List<Employee>> GetAll(Expression<Func<Employee, bool>> expression = null!)
        {
            IQueryable<Employee> query = Set.Include(x => x.Image);
            List<Employee> models = new();

            if (expression != null)
                query = Set.Where(expression);

            models = await query.ToListAsync();

            return models;
        }

        public override async Task<Employee?> GetById(int id)
        {
            return await Set.Include(s => s.Image)
                            .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}