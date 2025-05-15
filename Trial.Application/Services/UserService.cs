using AutoMapper;
using Trial.Application.DTO;
using Trial.Application.Repository;
using Trial.Domain.Entities;

namespace Trial.Application.Services
{
    public class UserService 
    {
        private readonly IUserRepository _userRepo;
        private readonly SysContext dbContext = new();
        private IMapper _mapper;

        public UserService(IUserRepository userRepo , IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public  async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var result =  await _userRepo.GetByIdAsync(id);
            return  _mapper.Map<UserDTO>(result);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var result = await _userRepo.GetAllAsync();
            return _mapper.Map<List<UserDTO>>(result);
        }

        public async Task<bool> AddAsync(UserDTO entity)
        {
            return await  _userRepo.AddAsync(entity);
        }

        public async Task<UserDTO> UpdateAsync(UserDTO entity)
        {
            var result = await _userRepo.UpdateAsync(entity);
            return _mapper.Map<UserDTO>(result);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _userRepo.DeleteAsync(id);
        }
    }
}
