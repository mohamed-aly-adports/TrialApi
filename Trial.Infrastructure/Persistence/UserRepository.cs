using Trial.Application.DTO;
using Trial.Domain.Entities;
using Trial.Infrastructure.Persistence;

namespace Trial.Application.Persistence
{
    public class UserRepository : Repository<UserDTO> 
    {
        public UserRepository(SysContext context) : base(context)
        {
        }
    }
}
