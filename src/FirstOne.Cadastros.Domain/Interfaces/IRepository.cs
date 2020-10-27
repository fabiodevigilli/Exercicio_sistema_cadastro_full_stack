using System;
using System.Collections.Generic;
using System.Text;

namespace FirstOne.Cadastros.Domain.Interfaces
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
