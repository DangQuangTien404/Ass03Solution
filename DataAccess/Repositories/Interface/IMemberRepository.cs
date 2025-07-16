using BusinessObject;

namespace DataAccess.Repositories.Interface
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetAll();
        IEnumerable<Member> GetPaged(int page, int pageSize);
        Member? GetById(int id);
        Member? GetByEmailAndPassword(string email, string password);
        void Add(Member member);
        void Update(Member member);
        void Delete(Member member);
        bool HasOrders(int memberId);
        void SaveChanges();
    }
}
