using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecCardAPI.Aplicacao.Entidades
{
    public class Acesso
    {
        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public string Resultado{ get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}
