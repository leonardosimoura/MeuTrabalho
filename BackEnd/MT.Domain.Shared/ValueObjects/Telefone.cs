using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Shared.ValueObjects
{
    public class Telefone
    {
        public Telefone(string codigoDDD , string numero)
        {
            SetarCodigoDDD(codigoDDD);
            SetarNumero(numero);
        }

        protected Telefone()
        {

        }

        public string CodigoDDD { get; protected set; }
        public string Numero { get; protected set; }

        public void SetarCodigoDDD(string codigoDDD)
        {
            CodigoDDD = codigoDDD;
        }

        public void SetarNumero(string numero)
        {
            Numero = numero;
        }

        public static bool EhValido(Telefone telefone)
        {
            return !string.IsNullOrWhiteSpace(telefone.CodigoDDD)
                && !string.IsNullOrWhiteSpace(telefone.Numero);
        }

    }
}
