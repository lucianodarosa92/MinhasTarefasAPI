using MinhasTarefasAPI.V1.Models;

namespace MinhasTarefasAPI.V1.Respositories.Interfaces
{
    public interface ITokenRepository
    {
        void Cadastrar(Token token);
        Token Obter(string refreshToken);
        void Atualizar(Token token);
    }
}