using MT.Domain.Shared.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using MT.Domain.Shared.UoW;
using MT.Domain.Interfaces.Repositories;
using MT.Domain.Models;
using System.Linq.Expressions;

namespace MT.UnitTests.Helpers
{
    public class MockHelper
    {
        public static MockRepository MockRepository => new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Mock };

        public class DomainMock
        {

        }

        public class RepositoryMock
        {
            public static Mock<IContatoUsuarioRepository> IContatoUsuarioRepositoryMock()
            {
                var mock = MockRepository.Create<IContatoUsuarioRepository>();
                var lista = new List<ContatoUsuario>();
                mock.Setup(s => s.Adicionar(It.IsAny<ContatoUsuario>()))
                    .Callback((ContatoUsuario usuario) => lista.Add(usuario));

                mock.Setup(s => s.Atualizar(It.IsAny<ContatoUsuario>()))
                    .Callback((ContatoUsuario usuario) =>
                    {
                        var usu = lista.FirstOrDefault(f => f.UsuarioId == usuario.UsuarioId);
                        if (usu != null)
                        {
                            lista.Remove(usu);
                            lista.Add(usuario);
                        }
                    });

                mock.Setup(s => s.ExcluirAsync(It.IsAny<Expression<Func<ContatoUsuario, bool>>>()))
                    .Callback((Expression<Func<ContatoUsuario, bool>> predicado) =>
                    {
                        foreach (var item in lista.Where(predicado.Compile()))
                        {
                            lista.Remove(item);
                        }
                    });

                mock.Setup(s => s.Selecionar())
                    .Returns(lista.AsQueryable());
                return mock;
            }
                public static Mock<IUsuarioRepository> IUsuarioRepositoryMock()
            {
                var mock = MockRepository.Create<IUsuarioRepository>();
                var lista = new List<Usuario>();
                mock.Setup(s => s.Adicionar(It.IsAny<Usuario>()))
                    .Callback((Usuario usuario) => lista.Add(usuario));

                mock.Setup(s => s.Atualizar(It.IsAny<Usuario>()))
                    .Callback((Usuario usuario) =>
                    {
                        var usu = lista.FirstOrDefault(f => f.UsuarioId == usuario.UsuarioId);
                        if (usu != null)
                        {
                            lista.Remove(usu);
                            lista.Add(usuario);
                        }
                    });

                mock.Setup(s => s.ExcluirAsync(It.IsAny<Expression<Func<Usuario, bool>>>()))
                    .Callback((Expression<Func<Usuario, bool>> predicado) =>
                    {
                        foreach (var item in lista.Where(predicado.Compile()))
                        {
                            lista.Remove(item);
                        }
                    });

                mock.Setup(s => s.Selecionar())
                    .Returns(lista.AsQueryable());

                mock.Setup(s => s.SelecionarPorEmailAsync(It.IsAny<string>()))
                    .Returns((string email) => Task.FromResult(lista.FirstOrDefault(f => f.Email.Endereco == email)));

                mock.Setup(s => s.SelecionarPorEmaileSenhaAsync(It.IsAny<string>(), It.IsAny<string>()))
                   .Returns((string email, string senha) => Task.FromResult(lista.FirstOrDefault(f => f.Email.Endereco == email && f.Senha.CodigoAcesso == senha)));

                return mock;
            }
        }
        
        public class InfraMock
        {
            public static Mock<IDomainNotificationHandler> IDomainNotificationHandlerMock()
            {
                var mock = MockRepository.Create<IDomainNotificationHandler>();
                var lista = new List<DomainNotification>();
                mock.Setup(s => s.Adicionar(It.IsAny<string>(), It.IsAny<string>()))
                    .Callback((string key, string value) =>
                    {
                        lista.Add(new DomainNotification(key, value));
                    });                
                mock.Setup(s => s.GetNotifications())
                    .Returns(lista);
                mock.SetupGet(s => s.HasNotifications)
                   .Returns(() =>
                   {
                       return lista.Count > 0;
                   });
                return mock;
            }

            public static Mock<IUnitOfWork> IUnitOfWorkMock()
            {
                var mock = MockRepository.Create<IUnitOfWork>();
                return mock;
            }
        }

    }
}
