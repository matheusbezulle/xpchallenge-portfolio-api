using MongoDB.Bson;
using Moq;
using XpChallenge.Portfolio.Application.Commands.ExcluirPortfolio;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Tests.Handlers
{
    public class ExcluirPortfolioCommandHandlerTest
    {
        private readonly Mock<IPortfolioService> _portfolioServiceMock;
        private readonly Mock<INotificator> _notificatorMock;

        private readonly ExcluirPortfolioCommandHandler _handler;

        public ExcluirPortfolioCommandHandlerTest()
        {
            _portfolioServiceMock = new Mock<IPortfolioService>();
            _notificatorMock = new Mock<INotificator>();

            _handler = new(_portfolioServiceMock.Object, _notificatorMock.Object);
        }

        [Fact]
        public async Task ExcluirPortfolio_Sucesso()
        {
            var command = new ExcluirPortfolioCommand(ObjectId.GenerateNewId().ToString());

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            _portfolioServiceMock.Setup(x => x.ExcluirAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Never);
            _portfolioServiceMock.Verify(x => x.ExcluirAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExcluirPortfolio_IdPortfolioInvalido_Falha()
        {
            var command = new ExcluirPortfolioCommand("teste");

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
        }
    }
}
