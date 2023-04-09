using Application.DTOs;
using Domain.Entities;
using AutoMapper;
using Domain.Interfaces;
using Application.Interfaces;
using Application.Helpers;
using Domain.Helpers;

namespace Application.Services
{
    public class UserService : IUserService, IMemberRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IMemberRepository memberRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<MemberDto>> GetAllUsers()
        {
            var users = await _userRepository.GetUsers();

            return _mapper.Map<IEnumerable<MemberDto>>(users);
        }
        public async Task<MemberDto> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task<AppUser> AddUser(AppUser user)
        {
            return await _userRepository.Add(user);
        }

        public async Task<bool> IsUserExists(string username)
        {
            return await _userRepository.IsExists(username);
        }

        public async Task<PagedList<MemberDto>> GetMembers(UserParams userParams)
        {
            var users = await _memberRepository.GetMembers(userParams);

            return users;
        }

        public async Task<MemberDto> GetMemberByUsername(string username)
        {
            var user = await _memberRepository.GetMemberByUsername(username);

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<bool> UpdateCurrentUser(UpdateMemberDto memberDto, string currentUser)
        {
            var user = await _userRepository.GetUserByUsername(currentUser);
            _mapper.Map(memberDto, user);

            _userRepository.Update(user);
            if (await _userRepository.SaveAllAsync())
                return true;

            return false;
        }

        public AppUser MapDtoToEntity(RegisterDto registerDto)
        {
            return _mapper.Map<AppUser>(registerDto);
        }

        public async Task<bool> SaveAllAsync()
        {
            if (await _userRepository.SaveAllAsync())
                return true;

            return false;
        }
    }
}
