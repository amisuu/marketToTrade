﻿using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Helpers;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService, IMemberRepository
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper,
                           IMemberRepository memberRepository,
                           ITokenService tokenService,
                           IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MemberDto>> GetAllUsers()
        {
            var users = await _unitOfWork.UserRepository.GetUsers();

            return _mapper.Map<IEnumerable<MemberDto>>(users);
        }
        public async Task<MemberDto> GetUserById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUserById(id);

            return _mapper.Map<MemberDto>(user);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _unitOfWork.UserRepository.GetUserByUsername(username);
        }

        public async Task<AppUser> GetUserByUsername2(string username)
        {
            return await _unitOfWork.UserRepository.GetUserByUsername(username);
        }

        public async Task<AppUser> AddUser(AppUserDto userDto)
        {
            var user = _mapper.Map<AppUser>(userDto);

            return await _unitOfWork.UserRepository.Add(user);
        }

        public async Task<bool> IsUserExists(string username)
        {
            return await _unitOfWork.UserRepository.IsExists(username);
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
            var user = await _unitOfWork.UserRepository.GetUserByUsername(currentUser);
            _mapper.Map(memberDto, user);

            _unitOfWork.UserRepository.Update(user);
            if (await _unitOfWork.Complete())
                return true;

            return false;
        }

        public AppUser MapDtoToEntity(RegisterDto registerDto)
        {
            return _mapper.Map<AppUser>(registerDto);
        }

        public AppUser RegisterUser(RegisterDto registerDto)
        {
            return _mapper.Map<AppUser>(registerDto);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _unitOfWork.Complete();
        }
    }
}
