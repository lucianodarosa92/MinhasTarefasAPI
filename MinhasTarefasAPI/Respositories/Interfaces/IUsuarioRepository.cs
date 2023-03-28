using MinhasTarefasAPI.Models;

namespace MinhasTarefasAPI.Respositories.Interfaces
{
    public interface IUsuarioRepository
    {
        void Cadastrar(ApplicationUser Usuario, string senha);

        ApplicationUser Obter(string email, string senha);

        ApplicationUser Obter(string Id);
    }
}
