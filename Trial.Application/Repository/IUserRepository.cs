using Trial.Application.DTO;

namespace Trial.Application.Repository
{
    public interface IUserRepository : IAsyncRepository<UserDTO>
    {
    }
}
