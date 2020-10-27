using FirstOne.Cadastros.Domain.Interfaces;
using FirstOne.Cadastros.Infra.Data.Context;

namespace FirstOne.Cadastros.Infra.Data.Repository
{
    public abstract class Repository : IRepository
    {
        protected readonly SQLServerContext _context;

        protected Repository(SQLServerContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}
