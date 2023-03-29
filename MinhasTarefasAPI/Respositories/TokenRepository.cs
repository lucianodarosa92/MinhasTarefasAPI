using MinhasTarefasAPI.DataBase;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Respositories.Interfaces;
using System.Linq;

namespace MinhasTarefasAPI.Respositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly MinhasTarefasContext _banco;

        public TokenRepository(MinhasTarefasContext banco)
        {
            _banco = banco;
        }

        public Token Obter(string refreshToken)
        {
            return _banco.Token.FirstOrDefault(a => a.RefreshToken == refreshToken && a.Utilizado == false);
        }

        public void Cadastrar(Token token)
        {
            _banco.Token.Add(token);
            _banco.SaveChanges();
        }

        public void Atualizar(Token token)
        {
            _banco.Token.Update(token);
            _banco.SaveChanges();
        }
    }
}