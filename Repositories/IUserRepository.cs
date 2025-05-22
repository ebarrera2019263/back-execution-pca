using ExecutionPca.Api.Models;
using System.Threading.Tasks;

namespace ExecutionPca.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
