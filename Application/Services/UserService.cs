﻿using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService, IMemberRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly ITokenService _tokenService;

        public UserService(IUserRepository userRepository, IMapper mapper, IMemberRepository memberRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _memberRepository = memberRepository;
            _tokenService = tokenService;
        }

        public async Task<UserDto> RegisterUser(RegisterDto registerDto)
        {
            var user = await GetUserByUsername(registerDto.Username);

            using var hmac = new HMACSHA256();

            user.Username = registerDto.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;

            await _userRepository.Add(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
            };
        }

        public async Task<UserDto> LoginUser(LoginDto loginDto)
        {
            var user = await GetUserByUsername(loginDto.Username);

            using var hmac = new HMACSHA256(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    return null;
                }
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
            };
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

        public async Task<AppUser> GetUserByUsername2(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }

        public async Task<AppUser> AddUser(AppUserDto userDto)
        {
            var user = _mapper.Map<AppUser>(userDto);

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
