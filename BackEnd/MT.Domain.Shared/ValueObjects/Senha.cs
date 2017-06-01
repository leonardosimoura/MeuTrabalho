using MT.Infra.CrossCutting.Seguranca.Senha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Shared.ValueObjects
{
    public class Senha
    {
        public string CodigoAcesso { get; private set; }

        public Senha(string senha)
        {
            this.CodigoAcesso = senha;
        }
        protected Senha()
        {
            
        }

        public static bool EhValido(Senha senha)
        {
            return EhValido(senha.CodigoAcesso);
        }

        public static bool EhValido(string codigoAcesso)
        {
            return (!string.IsNullOrEmpty(codigoAcesso) && codigoAcesso.Length >= 5);
        }

        public void CriptografarSenha()
        {
            CodigoAcesso = HashSenhas.HashMD5(CodigoAcesso);
        }

        public override string ToString()
        {
            return CodigoAcesso;
        }
    }
}
