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
    public class UsuarioRepository
        : BaseRepository<Usuario>, IUsuarioRepository
    {
        public Usuario? Get(string email)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Usuario>()
                    .Include(u => u.Perfil) //JOIN
                    .FirstOrDefault(u => u.Email.Equals(email));
            }
        }

        public Usuario? Get(string email, string senha)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Usuario>()
                    .Include(u => u.Perfil) //JOIN
                    .FirstOrDefault(u => u.Email.Equals(email)
                                      && u.Senha.Equals(senha));
            }
        }
    }
}
