using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Infra.Data.Repositories
{
    public class MovimentacaoRepository
        : BaseRepository<Movimentacao>, IMovimentacaoRepository
    {
        public List<Movimentacao> GetAll(DateTime dataMin, DateTime dataMax, Guid usuarioId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Movimentacao>()
                    .Include(m => m.Categoria) //JOIN
                    .Where(m => m.Data >= dataMin && m.Data <= dataMax && m.UsuarioId == usuarioId)
                    .OrderBy(m => m.Data)
                    .ToList();
            }
        }
    }
}
