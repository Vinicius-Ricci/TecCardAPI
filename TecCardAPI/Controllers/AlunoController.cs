﻿using System;
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
    public class AlunoController : ControllerBase
    {
        private BancoContexto _bancoContexto;
        public AlunoController(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
            

        }

        [HttpPost]
        public Aluno Salvar(NovoAluno request)
        {
            var aluno = new Aluno
            {
                Email = request.Email,
                Nome = request.Nome,
                Senha = request.Senha.MakeHash(),
                RM = request.RM,
                QrCode = "fdsf",
                Curso = _bancoContexto.Curso.ToList().First(c => c.Codigo == request.Curso)
            };
            _bancoContexto.Aluno.Add(aluno);
            _bancoContexto.SaveChanges();
            return aluno;
        }
        
        [HttpPost("login")]
        public IActionResult Login(Credencial credencial)
        {
            var aluno =_bancoContexto.Aluno.ToList().Find(a => a.Email == credencial.Email && a.Senha == credencial.Senha.MakeHash());
            if (aluno == default(Aluno))
            {
                return BadRequest(new { message = "Credenciais Invalidas" });
            }
            return Ok(new LoginResponse
            {
                Email = aluno.Email,
                Nome = aluno.Nome,
                Token = aluno.Email.MakeHash()
            });
        }
    }
}
