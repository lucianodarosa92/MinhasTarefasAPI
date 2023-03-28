using MinhasTarefasAPI.Models;

namespace MinhasTarefasAPI.Respositories.Interfaces
{
    public interface ITokenRepository
    {
        void Cadastrar(Token token);
        Token Obter(string refreshToken);
        void Atualizar(Token token);
    }
}