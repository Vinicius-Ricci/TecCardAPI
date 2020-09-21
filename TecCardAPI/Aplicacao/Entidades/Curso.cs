using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecCardAPI.Aplicacao.Entidades
{
    public class Curso
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
