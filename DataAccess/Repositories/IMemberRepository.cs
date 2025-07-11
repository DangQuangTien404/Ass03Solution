using BusinessObject;

namespace DataAccess.Repositories
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        Member? GetById(int id);
        void Add(Member member);
        void Update(Member member);
        void Delete(Member member);
        bool HasOrders(int memberId);
        void SaveChanges();
    }
}
