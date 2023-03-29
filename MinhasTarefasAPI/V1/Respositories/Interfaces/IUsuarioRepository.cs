using MinhasTarefasAPI.V1.Models;

namespace MinhasTarefasAPI.V1.Respositories.Interfaces
{
    public interface IUsuarioRepository
    {
        void Cadastrar(ApplicationUser Usuario, string senha);

        ApplicationUser Obter(string email, string senha);

        ApplicationUser Obter(string Id);
    }
}
