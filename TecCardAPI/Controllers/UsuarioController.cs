using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TecCardAPI.Aplicacao.Entidades;
using TecCardAPI.InfraEstrutura.BancoDados;
using TecCardAPI.Util;
using TecCardAPI.ViewModel;

namespace TecCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private BancoContexto _bancoContexto;
        public UsuarioController(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
            

        }

        [HttpGet]
        public string Ping()
        {
            return "pong";
        }

        [HttpPost]
        public Usuario Salvar(NovoAluno request)
        {
            var usuario = new Usuario
            {
                Email = request.Email,
                Nome = request.Nome,
                Senha = request.Senha.MakeHash(),
                RM = request.RM,
                QrCode = "fdsf",
                Curso = _bancoContexto.Curso.ToList().First(c => c.Codigo == request.Curso),
                Tipo = "ALUNO"

            };
            _bancoContexto.Usuario.Add(usuario);
            _bancoContexto.SaveChanges();
            return usuario;
        }
        
        [HttpPost("login")]
        public IActionResult Login(Credencial credencial)
        {
            var aluno =_bancoContexto.Usuario.ToList().Find(a => a.Email == credencial.Email && a.Senha == credencial.Senha.MakeHash());
            if (aluno == default(Usuario))
            {
                return BadRequest(new { message = "Credenciais Invalidas" });
            }
            return Ok(new LoginResponse
            {
                Email = aluno.Email,
                Nome = aluno.Nome,
                Token = aluno.Email.MakeHash(),
                Tipo = aluno.Tipo
            });
        }
    }
}
