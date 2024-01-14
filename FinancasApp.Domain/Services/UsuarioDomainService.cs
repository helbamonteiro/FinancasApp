using FinancasApp.Domain.Entities;
using FinancasApp.Domain.Helpers;
using FinancasApp.Domain.Interface.Services;
using FinancasApp.Domain.Interfaces.Repositories;
using FinancasApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancasApp.Domain.Services
{
    public class UsuarioDomainService : IUsuarioDomainService
    {
        //atributos
        private readonly IUsuarioRepository? _usuarioRepository;
        private readonly IPerfilRepository? _perfilRepository;

        //método construtor para injeção de dependência
        public UsuarioDomainService(IUsuarioRepository? usuarioRepository, IPerfilRepository? perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }

        public void CriarConta(Usuario usuario)
        {
            //verificar se o email já está cadastrado
            if (_usuarioRepository?.Get(usuario.Email) != null)
                throw new ApplicationException("O email informado já está cadastrado. Tente outro.");

            //criptografia da senha
            usuario.Senha = SHA1Helper.ComputeSHA1Hash(usuario.Senha);

            //definindo o perfil do usuário
            var perfil = _perfilRepository?.Get("USER_ROLE");
            usuario.PerfilId = perfil.Id;

            //gravando o usuário no banco de dados
            _usuarioRepository?.Add(usuario);
        }

        public Usuario Autenticar(string email, string senha)
        {
            //criptografar a senha do usuário
            senha = SHA1Helper.ComputeSHA1Hash(senha);

            //buscar o usuário no banco de dados
            var usuario = _usuarioRepository?.Get(email, senha);

            //verificar se o usuário foi encontrado
            if (usuario != null)
            {
                //retornando os dados do usuário
                return usuario;
            }
            else
            {
                throw new ApplicationException("Acesso negado. Usuário inválido.");
            }
        }
    }
}
