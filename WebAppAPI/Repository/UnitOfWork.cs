using WebAppAPI.Entities;

namespace WebAppAPI.Repository
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private bool disposed = false;
        private ScanMachineContext _context = new ScanMachineContext();

        private GenericRepository<User> _userRepository;
  

        public UnitOfWork(ScanMachineContext _context)
        {
            this._context = _context;

        }

        public IGenericRepository<User> UserRepository
        {
            get
            {
                return _userRepository ??= new GenericRepository<User>(_context);
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
