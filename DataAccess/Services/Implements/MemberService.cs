using BusinessObject;
using BusinessObject.DTOs;
using DataAccess.Hubs;
using Microsoft.AspNetCore.SignalR;
using DataAccess.Repositories.Interface;
using DataAccess.Services.Interface;

namespace DataAccess.Services.Implements
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;
        private readonly IHubContext<MemberHub>? _hub;

        public MemberService(IMemberRepository repository, IHubContext<MemberHub>? hub = null)
        {
            _repository = repository;
            _hub = hub;
        }

        public IEnumerable<MemberDto> GetMembers() => _repository.GetAll().Select(ToDto);

        public MemberDto? GetMember(int id)
        {
            var member = _repository.GetById(id);
            return member == null ? null : ToDto(member);
        }

        public void CreateMember(MemberDto dto)
        {
            var member = new Member
            {
                Email = dto.Email,
                CompanyName = dto.CompanyName,
                City = dto.City,
                Country = dto.Country,
                Password = dto.Password
            };
            _repository.Add(member);
            _repository.SaveChanges();
            dto.MemberId = member.MemberId;
            _hub?.Clients.All.SendAsync("MemberCreated", ToDto(member));
        }

        public void UpdateMember(MemberDto dto)
        {
            var member = _repository.GetById(dto.MemberId);
            if (member == null) return;
            member.Email = dto.Email;
            member.CompanyName = dto.CompanyName;
            member.City = dto.City;
            member.Country = dto.Country;
            member.Password = dto.Password;
            _repository.Update(member);
            _repository.SaveChanges();
            _hub?.Clients.All.SendAsync("MemberUpdated", ToDto(member));
        }

        public bool DeleteMember(int id)
        {
            if (_repository.HasOrders(id)) return false;
            var member = _repository.GetById(id);
            if (member == null) return false;
            _repository.Delete(member);
            _repository.SaveChanges();
            _hub?.Clients.All.SendAsync("MemberDeleted", id);
            return true;
        }

        private static MemberDto ToDto(Member m) => new()
        {
            MemberId = m.MemberId,
            Email = m.Email,
            CompanyName = m.CompanyName,
            City = m.City,
            Country = m.Country,
            Password = m.Password
        };
    }
}
