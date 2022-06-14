using Microsoft.EntityFrameworkCore;
using Servientrega.Data.Models;

namespace Servientrega.Data.Context
{
    public class ServientregaContext : DbContext
    {
        #region Ctor
        public ServientregaContext(DbContextOptions<ServientregaContext> options) : base(options)
        {

        }
        public ServientregaContext()
        {

        }
        #endregion

        public virtual DbSet<Avion> Avion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
