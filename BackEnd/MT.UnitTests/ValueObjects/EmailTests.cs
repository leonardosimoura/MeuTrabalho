using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MT.Domain.Shared.ValueObjects;

namespace MT.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Trait("ValueObjects", "Email")]
        [Theory(DisplayName = "EmailValido")]
        [InlineData("leonardosimoura@gmail.com")]
        [InlineData("leonardosimoura@hotmail.com")]
        [InlineData("leonardosimoura@outlook.com")]
        [InlineData("leonardosimoura@outlook.com.br")]
        [InlineData("leonardo.moura@consinco.com.br")]
        public void EmailValido(string enderecoEmail)
        {
            //Arrange

            //Act
            var email = new Email(enderecoEmail);
            //Assert
            Assert.Equal(true, Email.EhValido(email));
            Assert.Equal(enderecoEmail, email.Endereco);
        }

        [Trait("ValueObjects", "Email")]
        [Theory(DisplayName = "EmailInValido")]
        [InlineData("leonardosimouragmail.com")]
        [InlineData("leonardosimoura@outlookcom")]
        [InlineData("leonardosimoura@1.1")]
        [InlineData("@hotmail.com")]
        [InlineData("l.@hotmail.com")]
        public void EmailInValido(string enderecoEmail)
        {
            //Arrange

            //Act
            var email = new Email(enderecoEmail);
            //Assert
            Assert.Equal(false, Email.EhValido(email));
            //Assert.NotEqual(enderecoEmail, email.Endereco);
        }
    }
}
