using Moq;
using XpChallenge.Portfolio.Application.Commands.ExcluirAdministrador;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Tests.Handlers
{
    public class ExcluirAdministradorCommandHandlerTest
    {
        private readonly Mock<IAdministradorService> _administradorServiceMock;
        private readonly Mock<INotificator> _notificatorMock;

        private readonly ExcluirAdministradorCommandHandler _handler;

        public ExcluirAdministradorCommandHandlerTest()
        {
            _administradorServiceMock = new Mock<IAdministradorService>();
            _notificatorMock = new Mock<INotificator>();

            _handler = new(_administradorServiceMock.Object, _notificatorMock.Object);
        }

        [Fact]
        public async Task ExcluirAdministrador_Sucesso()
        {
            var command = new ExcluirAdministradorCommand("mock@moq.com");

            _administradorServiceMock.Setup(x => x.ObterPorEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Administrador)null!);

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ExcluirAdministrador_AdministradorExistente_Falha()
        {
            var command = new ExcluirAdministradorCommand("mock@moq.com");

            _administradorServiceMock.Setup(x => x.ObterPorEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Administrador("mock@moq.com"));

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Never);
        }
    }
}
