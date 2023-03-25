using Microsoft.AspNetCore.Identity;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Respositories.Interfaces;
using System;
using System.Text;

namespace MinhasTarefasAPI.Respositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        ApplicationUser IUsuarioRepository.Obter(string email, string senha)
        {
            var user = _userManager.FindByEmailAsync(email).Result;

            if (_userManager.CheckPasswordAsync(user, senha).Result)
            {
                return user;
            }
            else
            {
                /*
                 * Domain Notification
                 */
                throw new Exception("Usuário não localizado!");
            }
        }

        void IUsuarioRepository.Cadastrar(ApplicationUser Usuario, string senha)
        {
            var result = _userManager.CreateAsync(Usuario, senha).Result;

            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var erro in result.Errors)
                {
                    sb.Append(erro.Description);
                }

                /*
                 * Domain Notification
                 */
                throw new Exception($"Usuário não cadastrado! {sb}");
            }
        }
    }
}