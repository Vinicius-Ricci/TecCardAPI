using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TecCardAPI.Aplicacao.Entidades
{
    public class Usuario
    {
        public int RM { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Foto { get; set; }
        public string QrCode { get; set; }
        public virtual Curso Curso { get; set; }
        public ICollection<Acesso> Acessos { get; set; }
        public ICollection<Status> Situacoes { get; set; }
        public string Tipo { get; set; }
    }
}
