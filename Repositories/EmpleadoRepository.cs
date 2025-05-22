using ExecutionPca.Api.Data;
using ExecutionPca.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleado>> GetAllAsync()
        {
            var sql = @"
                SELECT 
                    W2GS78DT AS Emplcod,
                    W2GS7FDI AS PrimerNombre,
                    W2GS7FD3 AS SegundoNombre,
                    W2GS7ISI AS PrimerApellido,
                    W2GS7IS3 AS SegundoApellido,
                    QSNDLQEATE AS FechaNacimiento,
                    W5MEFAELLX5ZZ AS Email,
                    W2GS7N2Z AS Sexo,
                    EM475MVQTD AS CodigoReal,
                    VNVIMLD AS UsuarioId,
                    W2GS7LFN AS SiempreN,
                    W85V3TSN3 AS Bitacora,
                    W2GS7IS8 AS ApellidoCasada
                FROM QOTETXQSND";

            return await _context.Empleados.FromSqlRaw(sql).ToListAsync();
        }

        public async Task<Empleado?> GetByCodigoAsync(int codigo)
        {
            var sql = @"
                SELECT 
                    W2GS78DT AS Emplcod,
                    W2GS7FDI AS PrimerNombre,
                    W2GS7FD3 AS SegundoNombre,
                    W2GS7ISI AS PrimerApellido,
                    W2GS7IS3 AS SegundoApellido,
                    QSNDLQEATE AS FechaNacimiento,
                    W5MEFAELLX5ZZ AS Email,
                    W2GS7N2Z AS Sexo,
                    EM475MVQTD AS CodigoReal,
                    VNVIMLD AS UsuarioId,
                    W2GS7LFN AS SiempreN,
                    W85V3TSN3 AS Bitacora,
                    W2GS7IS8 AS ApellidoCasada
                FROM QOTETXQSND
                WHERE W2GS78DT = @codigo";

            var param = new SqlParameter("@codigo", codigo);
            return await _context.Empleados.FromSqlRaw(sql, param).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Empleado empleado)
        {
            var sql = @"
                INSERT INTO QOTETXQSND 
                (W2GS78DT, W2GS7FDI, W2GS7FD3, W2GS7ISI, W2GS7IS3, QSNDLQEATE, W5MEFAELLX5ZZ, W2GS7N2Z, VNVIMLD, EM475MVQTD, W2GS7LFN, W85V3TSN3)
                VALUES 
                (@Emplcod, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @FechaNacimiento, @Email, @Sexo, @UsuarioId, @CodigoReal, @SiempreN, @Bitacora)";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@Emplcod", empleado.Emplcod),
                new SqlParameter("@PrimerNombre", empleado.PrimerNombre),
                new SqlParameter("@SegundoNombre", empleado.SegundoNombre),
                new SqlParameter("@PrimerApellido", empleado.PrimerApellido),
                new SqlParameter("@SegundoApellido", empleado.SegundoApellido),
                new SqlParameter("@FechaNacimiento", (object?)empleado.FechaNacimiento ?? DBNull.Value),
                new SqlParameter("@Email", (object?)empleado.Email ?? DBNull.Value),
                new SqlParameter("@Sexo", empleado.Sexo),
                new SqlParameter("@UsuarioId", empleado.UsuarioId),
                new SqlParameter("@CodigoReal", (object?)empleado.CodigoReal ?? DBNull.Value),
                new SqlParameter("@SiempreN", (object?)empleado.SiempreN ?? DBNull.Value),
                new SqlParameter("@Bitacora", (object?)empleado.Bitacora ?? DBNull.Value)
            );
        }

        public async Task UpdateAsync(Empleado empleado)
        {
            var sql = @"
                UPDATE QOTETXQSND SET
                    W2GS7FDI = @PrimerNombre,
                    W2GS7FD3 = @SegundoNombre,
                    W2GS7ISI = @PrimerApellido,
                    W2GS7IS3 = @SegundoApellido,
                    QSNDLQEATE = @FechaNacimiento,
                    W5MEFAELLX5ZZ = @Email,
                    W2GS7N2Z = @Sexo,
                    EM475MVQTD = @CodigoReal,
                    W85V3TSN3 = @Bitacora
                WHERE W2GS78DT = @Emplcod";

            await _context.Database.ExecuteSqlRawAsync(sql,
                new SqlParameter("@Emplcod", empleado.Emplcod),
                new SqlParameter("@PrimerNombre", empleado.PrimerNombre),
                new SqlParameter("@SegundoNombre", empleado.SegundoNombre),
                new SqlParameter("@PrimerApellido", empleado.PrimerApellido),
                new SqlParameter("@SegundoApellido", empleado.SegundoApellido),
                new SqlParameter("@FechaNacimiento", (object?)empleado.FechaNacimiento ?? DBNull.Value),
                new SqlParameter("@Email", (object?)empleado.Email ?? DBNull.Value),
                new SqlParameter("@Sexo", empleado.Sexo),
                new SqlParameter("@CodigoReal", (object?)empleado.CodigoReal ?? DBNull.Value),
                new SqlParameter("@Bitacora", (object?)empleado.Bitacora ?? DBNull.Value)
            );
        }

        public async Task DeleteAsync(int codigo)
        {
            var sql = "DELETE FROM QOTETXQSND WHERE W2GS78DT = @codigo";
            var param = new SqlParameter("@codigo", codigo);
            await _context.Database.ExecuteSqlRawAsync(sql, param);
        }
    }
}
