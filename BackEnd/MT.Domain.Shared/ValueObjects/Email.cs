using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MT.Domain.Shared.ValueObjects
{
    public class Email
    {
        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            Endereco = endereco;
        }
        protected Email()
        {

        }

        public static bool EhValido(Email email)
        {
            return EhValido(email.Endereco);
        }

        public static bool EhValido(string endereco)
        {
            Regex regex = new Regex("^[A-Za-z0-9](([_.-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([.-]?[a-zA-Z0-9]+)*)([.][A-Za-z]{2,4})$");
            return regex.IsMatch(endereco);
        }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
