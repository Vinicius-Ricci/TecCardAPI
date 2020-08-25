using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecCardAPI.Aplicacao.Entidades
{
    public class Status
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public virtual Aluno Aluno { get; set; }

    }
}
