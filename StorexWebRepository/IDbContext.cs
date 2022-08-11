using Microsoft.EntityFrameworkCore;
using StorexWebCore.Enities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorexWebRepository
{

    public interface IDbContext
    {
        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        DbSet<TEntity> IDbSet<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new();

        long ExecuteStoredProcedure(string commandText, params object[] parameters);
        bool ExecuteProcBool(string cmdTxt, params object[] parameters);
        string ExecuteProcString(string commandText, params object[] parameters);
        object ExecuteStoredProcedureWithOutPutParam(string commandText, SqlParameter outParam, List<SqlParameter> parameters);
    }
}
