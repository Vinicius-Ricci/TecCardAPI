using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TecCardAPI.Aplicacao.Entidades;
using TecCardAPI.InfraEstrutura.BancoDados;

namespace TecCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {

        private BancoContexto _bancoContexto;

        public CursoController(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;


        }

        [HttpPost]
        public Curso Salvar(Curso curso)
        {
            _bancoContexto.Curso.Add(curso);
            _bancoContexto.SaveChanges();
            return curso;
        }
    }
}
