using ppedv.GMV.Model;
using ppedv.GMV.Model.Contracts;

namespace ppedv.GMV.Data.EfCore
{
    public class EfRepository : IRepository
    {
        EfContext _context = new EfContext("Server=(localdb)\\mssqllocaldb;Database=GMV;Trusted_Connection=true");

        public void Add<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Remove(entity);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return _context.Set<T>().ToList();
        }

        public T GetbyId<T>(int id) where T : Entity
        {
            return _context.Set<T>().Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Set<T>().Update(entity);
        }
    }
}
