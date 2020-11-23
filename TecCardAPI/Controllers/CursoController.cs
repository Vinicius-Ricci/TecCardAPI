using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TecCardAPI.Aplicacao.Entidades;
using TecCardAPI.InfraEstrutura.BancoDados;
using TecCardAPI.ViewModel;

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

        [HttpGet]
        public IEnumerable<CursoViewModel> Listar()
        {
            return _bancoContexto.Curso.ToList().Select(c => new CursoViewModel(){Id = c.Codigo, Name = c.Nome});
        }
    }
}
