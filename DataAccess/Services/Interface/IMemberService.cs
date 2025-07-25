using BusinessObject.DTOs;

namespace DataAccess.Services.Interface
{
    public interface IMemberService
    {
        IEnumerable<MemberDto> GetMembers(int page);
        MemberDto? GetMember(int id);
        void CreateMember(MemberDto dto);
        void UpdateMember(MemberDto dto);
        bool DeleteMember(int id);
    }
}
