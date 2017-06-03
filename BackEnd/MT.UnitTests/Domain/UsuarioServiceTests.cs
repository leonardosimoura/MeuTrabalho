using MT.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MT.UnitTests.Helpers;
using MT.Domain.Validations;
using MT.Domain.Models;
using Moq;

namespace MT.UnitTests.Domain
{
    public class UsuarioServiceTests
    {
        [Fact]
        [Trait("Domain", "UsuarioService")]
        public async void RegistrarUsuarioValido()
        {
            //Arrange
            var IUsuarioRepositoryMock = MockHelper.RepositoryMock.IUsuarioRepositoryMock();var IDomainNotificationHandlerMock = MockHelper.InfraMock.IDomainNotificationHandlerMock();
            var IContatoUsuarioRepositoryMock = MockHelper.RepositoryMock.IContatoUsuarioRepositoryMock();
            var contatoUsuarioValidation = new ContatoUsuarioValidation(IDomainNotificationHandlerMock.Object);
            var usuarioValidation = new UsuarioValidation(IDomainNotificationHandlerMock.Object, IUsuarioRepositoryMock.Object); // Certo é mockar essa interface tambem
            var usuarioService = new UsuarioService(IUsuarioRepositoryMock.Object, IDomainNotificationHandlerMock.Object, usuarioValidation, IContatoUsuarioRepositoryMock.Object,contatoUsuarioValidation);
            var usuario = new Usuario(Guid.NewGuid(),"Leonardo","leonardosimoura@gmail.com","123456789");
            //Act
            usuarioService.Registrar(usuario);
            //Assert
            Assert.NotEmpty(usuarioService.SelecionarAsync().Result);
            Assert.NotNull((await usuarioService.SelecionarAsync()).FirstOrDefault());
            Assert.Equal(usuario, (await usuarioService.SelecionarAsync()).FirstOrDefault());

            IUsuarioRepositoryMock.Verify(v => v.Adicionar(It.IsAny<Usuario>()), Times.Once(), "Deve chamar o adicionar de IUsuarioRepository");
            IDomainNotificationHandlerMock.Verify(v => v.Adicionar(It.IsAny<string>(), It.IsAny<string>()), Times.Never(), "Não deve Adicionar Notificações");
        }

        [Fact]
        [Trait("Domain", "UsuarioService")]
        public async void RegistrarUsuarioInValido()
        {
            //Arrange
            var IUsuarioRepositoryMock = MockHelper.RepositoryMock.IUsuarioRepositoryMock(); var IDomainNotificationHandlerMock = MockHelper.InfraMock.IDomainNotificationHandlerMock();
            var IContatoUsuarioRepositoryMock = MockHelper.RepositoryMock.IContatoUsuarioRepositoryMock();
            var contatoUsuarioValidation = new ContatoUsuarioValidation(IDomainNotificationHandlerMock.Object);
            var usuarioValidation = new UsuarioValidation(IDomainNotificationHandlerMock.Object, IUsuarioRepositoryMock.Object); // Certo é mockar essa interface tambem
            var usuarioService = new UsuarioService(IUsuarioRepositoryMock.Object, IDomainNotificationHandlerMock.Object, usuarioValidation, IContatoUsuarioRepositoryMock.Object, contatoUsuarioValidation);
            var usuario = new Usuario(Guid.NewGuid(), "Leonardo", "", "");
            //Act
            usuarioService.Registrar(usuario);
            //Assert
            Assert.Empty(usuarioService.SelecionarAsync().Result);
            Assert.Null((await usuarioService.SelecionarAsync()).FirstOrDefault());
            Assert.NotEqual(usuario, (await usuarioService.SelecionarAsync()).FirstOrDefault());
            IUsuarioRepositoryMock.Verify(v => v.Adicionar(It.IsAny<Usuario>()), Times.Never(), "Não deve chamar o adicionar de IUsuarioRepository");
            IDomainNotificationHandlerMock.Verify(v => v.Adicionar(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce(), "Deve Adicionar Notificações");
        }
    }
}
