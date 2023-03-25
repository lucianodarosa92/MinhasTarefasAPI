using MinhasTarefasAPI.Models;
using System;
using System.Collections.Generic;

namespace MinhasTarefasAPI.Respositories.Interfaces
{
    public interface ITarefaRepository
    {
        List<Tarefa> Restauracao(ApplicationUser usuario, DateTime dataUltimaSincronizacao);

        List<Tarefa> Sincronizacao(List<Tarefa> tarefas);
    }
}