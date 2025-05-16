using Trial.Application.DTO;
using Trial.Domain.Common;
using Trial.Domain.Entities;

namespace Trial.Application.Interfaces
{
    public interface IUser
    {
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<bool> AddAsync(UserDTO entity);
        Task<UserDTO> UpdateAsync(UserDTO entity);
        Task<bool> DeleteAsync(Guid id);
        Task<UserDTO> GetByUserNameAsync(string userName);
    }
}
