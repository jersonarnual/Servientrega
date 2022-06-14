using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Servientrega.Data.Context;
using Servientrega.Data.Interface;
using Servientrega.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Servientrega.Data.Repository
{
    public class AvionRepository : IAvionRepository
    {
        #region Members
        private readonly ServientregaContext _context;
        #endregion

        #region Ctor
        public AvionRepository(ServientregaContext context)
        {
            _context = context;
        }
        #endregion
        public bool Delete(Avion entity)
        {
            string sql = "EXEC PR_DeleteAvion @AvionID";
            int rowsAffected;
            List<SqlParameter> parms = new()
            {
                new SqlParameter { ParameterName = "@AvionID", Value = entity.Id }
            };
            rowsAffected = _context.Database.ExecuteSqlRaw(sql, parms.ToArray());
            return rowsAffected > 0;
        }

        public IEnumerable<Avion> GetAll()
        {
            List<Avion> list;
            string sql = "EXEC Pr_GetAvion";
            list = _context.Avion.FromSqlRaw<Avion>(sql).ToList();
            return list;
        }

        public Avion GetById(Guid id)
        {
            List<Avion> list;
            string sql = "EXEC PR_GetAvionbyId @AvionID";

            List<SqlParameter> parms = new()
            {
                new SqlParameter { ParameterName = "@AvionID", Value = id }
            };

            list = _context.Avion.FromSqlRaw<Avion>(sql, parms.ToArray()).ToList();
            return list.FirstOrDefault();
        }

        public bool Insert(Avion entity)
        {
            string sql = "EXEC PR_CreateAvion @Capacidad,@Modelo,@Descripcion";
            int rowsAffected;
            List<SqlParameter> parms = new()
            {
                new SqlParameter { ParameterName = "@Capacidad", Value = entity.Capacidad },
                new SqlParameter { ParameterName = "@Modelo", Value = entity.Modelo },
                new SqlParameter { ParameterName = "@Descripcion", Value = entity.Descripcion}
            };
            rowsAffected = _context.Database.ExecuteSqlRaw(sql, parms.ToArray());
            return rowsAffected > 0;
        }

        public bool Update(Avion entity)
        {
            string sql = "EXEC PR_UpdateAvion @Capacidad,@Modelo,@Descripcion,@AvionId";
            int rowsAffected;
            List<SqlParameter> parms = new()
            {
                new SqlParameter { ParameterName = "@Capacidad", Value = entity.Capacidad },
                new SqlParameter { ParameterName = "@Modelo", Value = entity.Modelo },
                new SqlParameter { ParameterName = "@Descripcion", Value = entity.Descripcion } ,
                new SqlParameter { ParameterName = "@AvionId", Value = entity.Id }
            };
            rowsAffected = _context.Database.ExecuteSqlRaw(sql, parms.ToArray());
            return rowsAffected > 0;
        }
    }
}
