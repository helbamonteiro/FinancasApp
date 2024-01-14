using FinancasApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Interface.Services
{
    public interface IUsuarioDomainService
    {
        void CriarConta(Usuario usuario);
        Usuario Autenticar(string email, string senha);
    }
}
