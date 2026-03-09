using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Entiteter;
//using System.Windows.Documents;


namespace Datalager
{
    public class GenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) // gör så att vi kan filtrera med Lambda-uttryck
        {
            return _dbSet.Where(predicate);
        }

        public IEnumerable<T> GetAll() //hämtar samtliga rader från tabellen och gör om det till en lista. 
        {
            return _dbSet.ToList();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate) //hämtar första matchningen eller retunerar null
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public bool IsEmpty()
        {
            return _dbSet.Count() == 0;
        }

        public int Count()
        {
            return _dbSet.Count();
        }
    }
}
