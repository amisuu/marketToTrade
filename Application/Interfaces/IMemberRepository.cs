using Application.DTOs;
using Application.Helpers;
using Domain.Helpers;

namespace Application.Interfaces;

public interface IMemberRepository
{
    Task<PagedList<MemberDto>> GetMembers(UserParams userParams);
    Task<MemberDto> GetMemberByUsername(string username);
}
