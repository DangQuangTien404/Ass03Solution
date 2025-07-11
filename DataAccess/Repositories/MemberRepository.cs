using BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Member> GetAll() => _context.Members.AsNoTracking().ToList();

        public Member? GetById(int id) => _context.Members.Find(id);

        public void Add(Member member) => _context.Members.Add(member);

        public void Update(Member member) => _context.Members.Update(member);

        public void Delete(Member member) => _context.Members.Remove(member);

        public bool HasOrders(int memberId) => _context.Orders.AsNoTracking().Any(o => o.MemberId == memberId);

        public void SaveChanges() => _context.SaveChanges();
    }
}
