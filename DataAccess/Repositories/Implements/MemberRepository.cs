using BusinessObject.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implements
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Member> GetAll() => _context.Members.AsNoTracking().ToList();

        public IEnumerable<Member> GetPaged(int page, int pageSize) =>
            _context.Members.AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

        public Member? GetById(int id) => _context.Members.Find(id);

        public Member? GetByEmailAndPassword(string email, string password)
        {
            var lowerEmail = email.ToLower();
            return _context.Members.AsNoTracking()
                .SingleOrDefault(m => m.Email.ToLower() == lowerEmail && m.Password == password);
        }

        public void Add(Member member) => _context.Members.Add(member);

        public void Update(Member member) => _context.Members.Update(member);

        public void Delete(Member member) => _context.Members.Remove(member);

        public bool HasOrders(int memberId) => _context.Orders.AsNoTracking().Any(o => o.MemberId == memberId);

        public void SaveChanges() => _context.SaveChanges();
    }
}
