using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Application.Interfaces;
using Application.Helpers;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository, IMemberRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsername(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<AppUser> Add(AppUser user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> IsExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username.ToLower());
        }

        public async Task SaveAllAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembers(UserParams userParams)
        {
            var query = _context.Users.Where(x => x.Username != userParams.CurrentUsername)
                                      .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                                      .AsNoTracking();

            if (userParams.Search != null)
            {
                query = query.Where(x => x.KnownAs.ToLower().Contains(userParams.Search));
            }

            query = userParams.OrderBy switch
            {
                "created" => query.OrderByDescending(x => x.Created),
                _ => query.OrderByDescending(x => x.LastActive),
            };

            return await PagedList<MemberDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<MemberDto> GetMemberByUsername(string username)
        {
            return await _context.Users
                            .Where(u => u.Username == username)
                            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                            .SingleOrDefaultAsync();
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
