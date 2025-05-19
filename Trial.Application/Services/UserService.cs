using AutoMapper;
using Trial.Application.DTO;
using Trial.Application.Repository;
using Trial.Application.Interfaces;

namespace Trial.Application.Interfaces
{
    public class UserService : IUser
    {
        private IMapper _mapper;
        private readonly IUniteOfWork _unitOfWork;

        public UserService(IMapper mapper,IUniteOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDTO> GetByUserNameAsync(string userName)
        {
            var result = await _unitOfWork.Users.GetByAsync(c => c.UserName == userName);
            return _mapper.Map<UserDTO>(result);
        } 

        public  async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var result = _unitOfWork.Users.GetByIdAsync(id);
            return  _mapper.Map<UserDTO>(result);
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var result = await _unitOfWork.Users.GetAllAsync();
            return _mapper.Map<List<UserDTO>>(result);
        }

        public async Task<bool> AddAsync(UserDTO entity)
        {
            return await _unitOfWork.Users.AddAsync(entity);
        }

        public async Task<UserDTO> UpdateAsync(UserDTO entity)
        {
            var result = await _unitOfWork.Users.UpdateAsync(entity);
            return _mapper.Map<UserDTO>(result);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _unitOfWork.Users.DeleteAsync(id);
        }
    }
}
