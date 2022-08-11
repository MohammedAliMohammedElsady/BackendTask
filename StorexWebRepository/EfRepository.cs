using StorexWebCore.Enities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StorexWebRepository
{

    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private CustomContext _context;
        private DbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository()
        {

        }

        public void SetContext(CustomContext cntxt)
        {
            _context = cntxt;
        }

        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public virtual bool Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public virtual bool RangeInsert(List<T> entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.AddRange(entity);

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        public virtual T InsertAndReturn(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                int result = this._context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public virtual bool Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                int result = this._context.SaveChanges();

                return true;
            }

            catch (Exception ex)
            {
                //if (null == ex.InnerException || null == ex.InnerException.InnerException)
                //{
                //    throw new Exception(ex.Message);
                //}

                //var sqlEx = ex.InnerException.InnerException
                //                       as System.Data.SqlClient.SqlException;
                //if (sqlEx != null && (sqlEx.Number == 2627 || sqlEx.Number == 2601))
                //{
                //    throw new Exception(ex.Message);
                //    //throw new ArabiaDuplicateException(sqlEx.Message);
                //}

                //else
                //{
                //    throw new Exception(sqlEx.Message);
                //}

                throw new Exception(ex.Message);
            }
        }
        public virtual bool Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                int result = this._context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            //catch (DbEntityValidationException dbEx)
            //{
            //    var msg = string.Empty;

            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //            msg += Environment.NewLine +
            //                   string.Format("Property: {0} Error: {1}", validationError.PropertyName,
            //                       validationError.ErrorMessage);

            //    var fail = new Exception(msg, dbEx);
            //    //Debug.WriteLine(fail.Message, fail);
            //    throw fail;
            //}
            catch (Exception ex)
            {
                //var sqlEx = ex.InnerException.InnerException
                //                       as System.Data.SqlClient.SqlException;
                //if (sqlEx != null && (sqlEx.Number == 547))
                //{
                //    throw new Exception(ex.Message);
                //    //throw new ArabiaDeleteException(sqlEx.Message);
                //}
                throw ex;
            }

        }
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }
        protected DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.IDbSet<T>();
                return _entities;
            }
        }
        //protected virtualIDbSet<T> Entities
        //{
        //get
        //    {
        //        if (_entities == null)
        //            _entities = _context.Set<T>();
        //        return _entities;
        //    }
        //}
        public virtual IQueryable<T> Include<TProperty>(System.Linq.Expressions.Expression<Func<T, TProperty>> path)
        {
            return Entities.Include(path);

        }
        public virtual IQueryable<T> Include(string path)
        {
            return Entities.Include(path);
        }
        public IQueryable<T> Include<TProperty>(IQueryable<T> query, System.Linq.Expressions.Expression<Func<T, TProperty>> path)
        {

            return query.Include(path);


        }
        public IQueryable<T> Include(IQueryable<T> query, string path)
        {

            return query.Include(path);

        }
    }
}
