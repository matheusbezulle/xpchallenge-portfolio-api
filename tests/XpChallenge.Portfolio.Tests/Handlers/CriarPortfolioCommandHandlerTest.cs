using MongoDB.Bson;
using Moq;
using XpChallenge.Portfolio.Application.Commands.CriarPortfolio;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Tests.Handlers
{
    public class CriarPortfolioCommandHandlerTest
    {
        private readonly Mock<IPortfolioService> _portfolioServiceMock;
        private readonly Mock<INotificator> _notificatorMock;

        private readonly CriarPortfolioCommandHandler _handler;

        public CriarPortfolioCommandHandlerTest()
        {
            _portfolioServiceMock = new Mock<IPortfolioService>();
            _notificatorMock = new Mock<INotificator>();

            _handler = new(_portfolioServiceMock.Object, _notificatorMock.Object);
        }

        [Fact]
        public async Task CriarPortfolio_Sucesso()
        {
            var command = new CriarPortfolioCommand("Teste", 2);

            _portfolioServiceMock.Setup(x => x.CriarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ObjectId.GenerateNewId().ToString());

            _notificatorMock.Setup(x => x.AdicionarErroAplicacao(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroAplicacao(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CriarPortfolio_IdNulo_Falha()
        {
            var command = new CriarPortfolioCommand("Teste", 2);

            _portfolioServiceMock.Setup(x => x.CriarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string)null!);

            _notificatorMock.Setup(x => x.AdicionarErroAplicacao(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroAplicacao(It.IsAny<string>()), Times.Once);
        }
    }
}
