using WebAppAPI.Entities;
namespace WebAppAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        void Save();
    }
}
