using CSU.EtapaTecnica.Data.Mappings;
using CSU.EtapaTecnica.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CSU.EtapaTecnica.Data.Context
{
    public class CSUContext : DbContext
    {
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<NotaFiscalItem> NotaFiscalItems { get; set; }

        public CSUContext(DbContextOptions<CSUContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotaFiscalMapping).Assembly);
        }

        public async Task CreateDBMigrateAndSeedData()
        {
            if (!Database.CanConnect())
            {
                Database.Migrate();
                await CreateProcedure();
            }

            var dataInserSQLPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SQL\\dataInsert.sql");
            var dataInsertSQL = await File.ReadAllTextAsync(dataInserSQLPath);
            await Database.ExecuteSqlRawAsync(dataInsertSQL);
            await SaveChangesAsync();
        }

        public async Task CreateProcedure()
        {
            var createProcedureSQLPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SQL\\createProcedure.sql");
            var createProcedureSQL = await File.ReadAllTextAsync(createProcedureSQLPath);
            await Database.ExecuteSqlRawAsync(createProcedureSQL);
        }
    }
}
