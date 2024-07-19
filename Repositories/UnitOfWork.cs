using ApiCatalogo.Context;
using EscalaSeguranca.Repositories;


namespace EscalaSegurancaAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly ILoggerFactory _loggerFactory;
        private IPolicialRepository _policialRepository;
        private IEscalaRepository _escalaRepository;
        private ILocalRepository _localRepository;
        private IMarcacaoEscalaRepository _marcacaoEscalaRepository;

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
        }

        public IPolicialRepository PolicialRepository
        {
            get
            {
                if (_policialRepository == null)
                {
                    _policialRepository = new PolicialRepository
                        (_context, _loggerFactory.CreateLogger<PolicialRepository>());                        
                }
                return _policialRepository;
            }
        }

        public IEscalaRepository EscalaRepository
        {
            get
            {
                if (_escalaRepository == null)
                {
                    _escalaRepository = new EscalaRepository
                        (_context, _loggerFactory.CreateLogger<EscalaRepository>());                        
                }
                return _escalaRepository;
            }
        }

        public ILocalRepository LocalRepository
        {
            get
            {
                if (_localRepository == null)
                {
                    _localRepository = new LocalRepository
                        (_context, _loggerFactory.CreateLogger<LocalRepository>());                        
                }
                return _localRepository;
            }
        }

        public IMarcacaoEscalaRepository MarcacaoEscalaRepository
        {
            get
            {
                if (_marcacaoEscalaRepository == null)
                {
                    _marcacaoEscalaRepository = new MarcacaoEscalaRepository
                        (_context, _loggerFactory.CreateLogger<MarcacaoEscalaRepository>());                        
                }
                return _marcacaoEscalaRepository;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
