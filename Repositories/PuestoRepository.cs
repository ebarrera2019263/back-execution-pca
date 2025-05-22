using ExecutionPca.Api.Data;
using ExecutionPca.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Repositories
{
    public class PuestoRepository : IPuestoRepository
    {
        private readonly ApplicationDbContext _context;

        public PuestoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Puesto>> GetAllAsync()
        {
            return await _context.Puestos
                .FromSqlRaw("SELECT CAST(DBT3F8 AS INT) AS Puecod, DBTGF7 AS Puenom FROM EVACATPUE210809")
                .ToListAsync();
        }

        public async Task<Puesto?> GetByCodigoAsync(int codigo)
        {
            var param = new SqlParameter("@codigo", codigo);

            return await _context.Puestos
                .FromSqlRaw("SELECT CAST(DBT3F8 AS INT) AS Puecod, DBTGF7 AS Puenom FROM EVACATPUE210809 WHERE DBT3F8 = @codigo", param)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Puesto puesto)
        {
            var sql = "INSERT INTO EVACATPUE210809 (DBT3F8, DBTGF7) VALUES (@PUECOD, @PUENOM)";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@PUECOD", puesto.Puecod),
                new SqlParameter("@PUENOM", puesto.Puenom));
        }

        public async Task UpdateAsync(Puesto puesto)
        {
            var sql = "UPDATE EVACATPUE210809 SET DBTGF7 = @PUENOM WHERE DBT3F8 = @PUECOD";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@PUECOD", puesto.Puecod),
                new SqlParameter("@PUENOM", puesto.Puenom));
        }

        public async Task DeleteAsync(int codigo)
        {
            var sql = "DELETE FROM EVACATPUE210809 WHERE DBT3F8 = @PUECOD";
            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@PUECOD", codigo));
        }
    }
}
