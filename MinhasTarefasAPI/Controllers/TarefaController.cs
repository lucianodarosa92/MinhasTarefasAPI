using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MinhasTarefasAPI.Models;
using MinhasTarefasAPI.Respositories.Interfaces;
using System;
using System.Collections.Generic;

namespace MinhasTarefasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TarefaController : Controller
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public TarefaController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        [Authorize]
        public ActionResult Sincronizacao([FromBody] List<Tarefa> tarefas)
        {
            return View();
        }

        [Authorize]
        public ActionResult Restaurar(DateTime data)
        {
            var usuario = _userManager.GetUserAsync(HttpContext.User).Result;

            return Ok(_tarefaRepository.Restauracao(usuario, data));
        }
    }
}