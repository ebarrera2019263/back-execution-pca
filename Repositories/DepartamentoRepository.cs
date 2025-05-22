using ExecutionPca.Api.Data;
using ExecutionPca.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartamentoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Departamento>> GetAllAsync()
        {
            var sql = @"
                SELECT 
                    T7D3F8 AS Empcod,
                    W8TD3F8 AS Depcod,
                    W8TD8TM AS Depdes,
                    W85V3TSN3 AS Bitacora,
                    VA6AX0ZLUCAJAZL AS MisionParticipa
                FROM W4Z8280E4M";

            return await _context.Set<Departamento>().FromSqlRaw(sql).ToListAsync();
        }

        public async Task<Departamento?> GetByIdAsync(int empcod, int depcod)
        {
            var sql = @"
                SELECT 
                    T7D3F8 AS Empcod,
                    W8TD3F8 AS Depcod,
                    W8TD8TM AS Depdes,
                    W85V3TSN3 AS Bitacora,
                    VA6AX0ZLUCAJAZL AS MisionParticipa
                FROM W4Z8280E4M
                WHERE T7D3F8 = @empcod AND W8TD3F8 = @depcod";

            return await _context.Set<Departamento>().FromSqlRaw(sql,
                new SqlParameter("@empcod", empcod),
                new SqlParameter("@depcod", depcod)).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Departamento departamento)
        {
            var sql = @"
                INSERT INTO W4Z8280E4M (T7D3F8, W8TD3F8, W8TD8TM, W85V3TSN3, VA6AX0ZLUCAJAZL)
                VALUES (@Empcod, @Depcod, @Depdes, @Bitacora, @MisionParticipa)";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@Empcod", departamento.Empcod),
                new SqlParameter("@Depcod", departamento.Depcod),
                new SqlParameter("@Depdes", departamento.Depdes),
                new SqlParameter("@Bitacora", (object?)departamento.Bitacora ?? DBNull.Value),
                new SqlParameter("@MisionParticipa", (object?)departamento.MisionParticipa ?? DBNull.Value));
        }

        public async Task UpdateAsync(Departamento departamento)
        {
            var sql = @"
                UPDATE W4Z8280E4M
                SET W8TD8TM = @Depdes,
                    W85V3TSN3 = @Bitacora,
                    VA6AX0ZLUCAJAZL = @MisionParticipa
                WHERE T7D3F8 = @Empcod AND W8TD3F8 = @Depcod";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@Empcod", departamento.Empcod),
                new SqlParameter("@Depcod", departamento.Depcod),
                new SqlParameter("@Depdes", departamento.Depdes),
                new SqlParameter("@Bitacora", (object?)departamento.Bitacora ?? DBNull.Value),
                new SqlParameter("@MisionParticipa", (object?)departamento.MisionParticipa ?? DBNull.Value));
        }

        public async Task DeleteAsync(int empcod, int depcod)
        {
            var sql = "DELETE FROM W4Z8280E4M WHERE T7D3F8 = @Empcod AND W8TD3F8 = @Depcod";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@Empcod", empcod),
                new SqlParameter("@Depcod", depcod));
        }
    }
}
