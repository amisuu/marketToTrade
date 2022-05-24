using Application.DTOs;

namespace Application.Interfaces;

public interface IMemberRepository
{
    public Task<IEnumerable<MemberDto>> GetMembers();
    public Task<MemberDto> GetMemberByUsername(string username);
}
