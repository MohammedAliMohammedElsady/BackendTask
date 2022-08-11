using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using StorexWebCore.Enities;
namespace StorexWebRepository
{
    public interface IStorexWebRepository<T> : IRepository<T> where T : BaseEntity
    {
        DatabaseFacade Database();
    }

    public class StorexWebRepository<T> : EfRepository<T>, IStorexWebRepository<T> where T : BaseEntity
    {
        StorexWebContext context = null;
        public DatabaseFacade Database()
        {
            return context.Database;
        }

        public StorexWebRepository() : base()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

                var Configuration = builder.Build();

                string conn = Configuration.GetSection("ApplicationSettings:StorexWebContext").Value.ToString();

                context = new StorexWebContext(conn);
                base.SetContext(context);
            }
            catch (Exception ex)
            {

            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context?.Dispose();
                GC.Collect();
                GC.SuppressFinalize(this);
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

    }
}
