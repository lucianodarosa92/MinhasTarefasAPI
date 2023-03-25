using MinhasTarefasAPI.DataBase;
using MinhasTarefasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinhasTarefasAPI.Respositories.Interfaces
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly MinhasTarefasContext _banco;

        public TarefaRepository(MinhasTarefasContext banco)
        {
            _banco = banco;
        }

        public List<Tarefa> Restauracao(ApplicationUser usuario, DateTime dataUltimaSincronizacao)
        {
            var query = _banco.Tarefas.Where(a => a.UsuarioId == usuario.Id).AsQueryable();

            if (dataUltimaSincronizacao != null)
            {
                query = query.Where(a => a.Criado >= dataUltimaSincronizacao | a.Atualizado >= dataUltimaSincronizacao);
            }

            return query.ToList<Tarefa>();
        }

        public List<Tarefa> Sincronizacao(List<Tarefa> tarefas)
        {
            var tarefasNovas = tarefas.Where(a => a.IdTarefaApi == 0).AsQueryable();

            // cadastrar novos registros
            if (tarefasNovas.Count() > 0)
            {
                foreach (var tarefa in tarefas)
                {
                    _banco.Tarefas.Add(tarefa);
                }
            }

            var tarefasAtualizadasOuExcluidas = tarefas.Where(a => a.IdTarefaApi != 0 | a.Excluido == true).AsQueryable();

            // atualização de registros excluidos
            if (tarefasAtualizadasOuExcluidas.Count() > 0)
            {
                foreach (var tarefa in tarefas)
                {
                    _banco.Tarefas.Update(tarefa);
                }
            }

            _banco.SaveChanges();

            return tarefasNovas.ToList();
        }
    }
}