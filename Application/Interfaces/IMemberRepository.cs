using Application.DTOs;
using Application.Helpers;

namespace Application.Interfaces;

public interface IMemberRepository
{
    public Task<PagedList<MemberDto>> GetMembers(UserParams userParams);
    public Task<MemberDto> GetMemberByUsername(string username);
}
