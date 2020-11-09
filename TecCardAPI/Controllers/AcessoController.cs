using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TecCardAPI.Aplicacao.Entidades;
using TecCardAPI.InfraEstrutura.BancoDados;
using TecCardAPI.Util;
using TecCardAPI.ViewModel;

namespace TecCardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessoController : ControllerBase
    {
        private BancoContexto _bancoContexto;
        public AcessoController(BancoContexto bancoContexto)
        {
            _bancoContexto = bancoContexto;
            

        }

        [HttpPost("validate")]
        public IActionResult Validate(AcessoVM acessoVM)
        {
            var aluno =_bancoContexto.Usuario.Include(u => u.Situacoes).ToList().Find(a => a.RM == acessoVM.RM );
            if (aluno == default(Usuario))
            {
                return NotFound(new { message = "Usuario não encontrado!" });
            }
            var status = aluno.Situacoes.FirstOrDefault(s => s.DataInicio < DateTime.Now && s.DataFim > DateTime.Now);
            if (status == default(Status))
            {
                return NotFound(new { message = "Permissão de saida não configurada!" });
            }
            return Ok(new AcessoValidateResponse
            {
              Descricao = status.Descricao
            });

            
        }

        [HttpPost("grant")]
        public IActionResult Grant(GrantViewModel vm){
            var aluno = _bancoContexto.Usuario.FirstOrDefault(u => u.RM == vm.RM);

            if(aluno == default(Usuario)){
                return NotFound(new { message = "Aluno não encontrado" });
            }
            _bancoContexto.Status.ToList().FindAll(s => s.Usuario == aluno).ForEach(s => {
                if(s.DataFim > vm.DataInicio){
                    s.DataFim = vm.DataInicio.AddDays(-1);
                }
            });
            
            var situacao = new Status();
            situacao.DataFim = vm.DataFim;
            situacao.DataInicio = vm.DataInicio;
            situacao.Descricao = vm.Descricao;
            situacao.Usuario = aluno;
            _bancoContexto.Status.Add(situacao);
            _bancoContexto.SaveChanges();

            return Ok(new { message = "Status modificado com sucesso!"});
        }
    }
}
