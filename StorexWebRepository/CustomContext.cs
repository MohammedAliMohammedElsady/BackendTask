using Microsoft.EntityFrameworkCore;
using StorexWebCore.Enities;
using System.Data;


namespace StorexWebRepository
{

    public class CustomContext : DbContext
    {
        public CustomContext()
            : base()
        {

        }
        public CustomContext(DbContextOptions existingConnection, bool contextOwnsConnection)
        : base(existingConnection)
        {





        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }




        public DbSet<TEntity> IDbSet<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
        protected List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {

                obj = Activator.CreateInstance<T>();
                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch
                    {

                    }

                }
                list.Add(obj);


            }
            return list;
        }
    }

}
