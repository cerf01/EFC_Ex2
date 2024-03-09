using EFC_Ex2.DAL.Moduls;
using Microsoft.EntityFrameworkCore;

namespace EFC_Ex2.DAL
{
    public class Repository<TEntity> where TEntity : class
    {

        private Context _context;
        public Repository(DbContextOptions<Context> options)
        {
            _context = new Context(options);
        }
        
        public Repository()
        {
            _context = new Context();
        }
     

        public TEntity add(TEntity entity) 
        { 
            var result = _context.Set<TEntity>().Add(entity).Entity; 
            _context.SaveChanges(); 
            return result; 
        }

        public void addRange(List<TEntity> entity)
        {
           _context.Set<TEntity>().AddRange(entity);
            _context.SaveChanges();
   
        }

        public TEntity Get(int id) 
        { 
            return _context.Set<TEntity>().Find(id); 
        }
        public DbSet<TEntity> GetAll() 
        { 
            return _context.Set<TEntity>();
        }
        public void delete(int id) 
        { 
            var entity = _context.Set<TEntity>().Find(id); 
            _context.Set<TEntity>().Remove(entity); 
            _context.SaveChanges();
        }
        public void update(int id, TEntity entity) 
        { 
            var existingEntity = _context.Set<TEntity>().Find(id); 
            _context.Entry(existingEntity).CurrentValues.SetValues(entity); 
            _context.SaveChanges(); 
        }
    }
}


