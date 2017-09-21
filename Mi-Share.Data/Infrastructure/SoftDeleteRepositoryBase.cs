using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Mi_Share.Model;

namespace Mi_Share.Data.Infrastructure
{
    public abstract class SoftDeleteRepositoryBase<T> : RepositoryBase<T> where T : class, ISoftDelete
    {
        public SoftDeleteRepositoryBase(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public override void Delete(T entity)
        {
            if (entity is ISoftDelete)
            {
                entity.IsDeleted = true;
                entity.DeletedDate = DateTime.Now;

            }
            else
            {
                dbSet.Remove(entity);
            }
        }

        public override void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                if (obj is ISoftDelete)
                {
                    obj.IsDeleted = true;
                }
                else
                {
                    dbSet.Remove(obj);
                }
            }

            //IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            //foreach (T obj in objects)
            //    dbSet.Remove(obj);
        }

        public override T GetById(int id)
        {
            T obj = dbSet.Find(id);
            if (obj is ISoftDelete)
            {
                if (obj.IsDeleted)
                    return null;
                else
                    return obj;
            }
            else
            {
                return obj;
            }
            //return dbSet.Find(id);
        }

        public override IEnumerable<T> GetAll()
        {
            var queryable = dbSet;
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                var queryableSet = queryable.Where(x => x.IsDeleted == false);
                return queryableSet.ToList();

            }

            return queryable.ToList();
        }

        public override IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                return dbSet.Where(where).Where(x => x.IsDeleted == false).ToList();
            }
            return dbSet.Where(where).ToList();
        }

        public override T Get(Expression<Func<T, bool>> where)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                return dbSet.Where(where).Where(x => x.IsDeleted == false).FirstOrDefault<T>();
            }
            return dbSet.Where(where).FirstOrDefault<T>();
        }
    }
}
