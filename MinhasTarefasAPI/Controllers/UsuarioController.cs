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

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly SignInManager<ApplicationUser> _signmanager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsuarioController(IUsuarioRepository usuarioRepository, SignInManager<ApplicationUser> signmanager, UserManager<ApplicationUser> userManager)
        {
            _usuarioRepository = usuarioRepository;
            _signmanager = signmanager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UsuarioDTO usuarioDTO)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("ConfirmacaoSenha");

            if (ModelState.IsValid)
            {
                var usuario = _usuarioRepository.Obter(usuarioDTO.Email, usuarioDTO.Senha);

                if (usuario != null)
                {
                    //login no entity
                    _signmanager.SignInAsync(usuario, false);

                    //no futuro retorna o token (JWT)
                    return Ok();
                }
                else
                {
                    return NotFound("Usuario não localizado!");
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        [HttpPost("")]
        public ActionResult Cadastrar([FromBody] UsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                var usuario = new ApplicationUser();
                usuario.FullName = usuarioDTO.Nome;
                usuario.UserName = usuarioDTO.Email;
                usuario.Email = usuarioDTO.Email;

                var resultado = _userManager.CreateAsync(usuario, usuarioDTO.Senha).Result;

                if (!resultado.Succeeded)
                {
                    var erros = new List<String>();
                    foreach (var erro in resultado.Errors)
                    {
                        erros.Add(erro.Description);
                    }
                    return UnprocessableEntity(erros);
                }
                else
                {
                    return Ok(usuario);
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }
    }
}