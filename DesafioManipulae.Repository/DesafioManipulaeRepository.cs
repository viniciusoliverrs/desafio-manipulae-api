using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DesafioManipulae.Repository
{
    public class DesafioManipulaeRepository : IDesafioManipulaeRepository
    {
        private readonly DesafioManipulaeContext _context;
        public DesafioManipulaeRepository(DesafioManipulaeContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}