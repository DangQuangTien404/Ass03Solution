using BusinessObject.DTOs;

namespace DataAccess.Services
{
    public interface IMemberService
    {
        IEnumerable<MemberDto> GetMembers();
        MemberDto? GetMember(int id);
        void CreateMember(MemberDto dto);
        void UpdateMember(MemberDto dto);
        bool DeleteMember(int id);
    }
}
