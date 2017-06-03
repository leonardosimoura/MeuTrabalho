using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Shared.ValueObjects
{
   
    public  class Contato
    {

        public Contato(string endereco )
        {
            Endereco = endereco;
        }
        protected Contato()
        {

        }
        public string Endereco { get; protected set; }

        public static bool EhValido(Contato contato)
        {
            return !string.IsNullOrWhiteSpace(contato.Endereco);
        }
    }
}
