using AutoMapper;
using Trial.Application.DTO;
using Trial.Application.Repository;

namespace Trial.Application.Interfaces
{
    public class UserService : IUser
    {
        private IUniteOfWork _unit;
        private readonly IUserRepository _userRepo;
        private IMapper _mapper;

        public UserService(IUniteOfWork unit,IUserRepository userRepo , IMapper mapper)
        {
            _unit = unit;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetByUserNameAsync(string userName)
        {
            var result = await _userRepo.GetByAsync(c=>c.UserName == userName);
            return _mapper.Map<UserDTO>(result);
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
