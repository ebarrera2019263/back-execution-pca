using ExecutionPca.Api.Data;
using ExecutionPca.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var param = new SqlParameter("@username", username);

            var query = await _context.Users
                .FromSqlRaw("SELECT BMB8TM AS Username, W0B0TG3XE AS EncryptedPassword FROM VQDZ0ZTV7M WHERE BMB8TM = @username", param)
                .AsNoTracking()
                .ToListAsync();

            return query.FirstOrDefault();
        }
    }
}
