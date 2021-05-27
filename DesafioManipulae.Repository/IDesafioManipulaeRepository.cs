using System.Threading.Tasks;

namespace DesafioManipulae.Repository
{
    public interface IDesafioManipulaeRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
    }
}