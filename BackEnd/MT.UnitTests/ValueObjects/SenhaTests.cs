using MT.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MT.UnitTests.ValueObjects
{
    public class SenhaTests
    {
        [Trait("ValueObjects", "Senha")]
        [Theory(DisplayName = "SenhaValido")]
        [InlineData("12345")]
        [InlineData("aaaaaaaaaa")]
        [InlineData("bbbbbbbbbbbb")]
        [InlineData("cccccccccccccc")]
        [InlineData("eeeeeeeeeeeeee")]
        public void SenhaValido(string password)
        {
            //Arrange

            //Act
            var senha = new Senha(password);
            //Assert
            Assert.Equal(true, Senha.EhValido(senha));
            Assert.Equal(password, senha.CodigoAcesso);
        }

        [Trait("ValueObjects", "Senha")]
        [Theory(DisplayName = "SenhaInValido")]
        [InlineData("123")]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("00")]
        [InlineData("a")]
        public void SenhaInValido(string password)
        {
            //Arrange

            //Act
            var senha = new Senha(password);
            //Assert
            Assert.Equal(false, Senha.EhValido(senha));
            Assert.Equal(password, senha.CodigoAcesso);
        }
    }
}
