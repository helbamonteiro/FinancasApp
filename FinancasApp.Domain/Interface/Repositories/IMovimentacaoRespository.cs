using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Repositories
{
    public interface IMovimentacaoRepository : IBaseRepository<Movimentacao>
    {
        List<Movimentacao> GetAll(DateTime dataMin, DateTime dataMax, Guid usuarioId);
    }
}
