using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Services
{
    public interface IMovimentacaoDomainService
    {
        void Cadastrar(Movimentacao movimentacao);
        void Atualizar(Movimentacao movimentacao);
        void Excluir(Guid id);
        List<Movimentacao> Consultar(DateTime dataMin, DateTime dataMax, Guid usuarioId);
        Movimentacao ObterPorId(Guid id);
    }
}
