using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interfaces.Repositories
{
    public interface IPerfilRepository : IBaseRepository<Perfil>
    {
        Perfil? Get(string nome);
    }
}
