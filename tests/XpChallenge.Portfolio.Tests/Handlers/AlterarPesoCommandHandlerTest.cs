using MongoDB.Bson;
using Moq;
using XpChallenge.Portfolio.Application.Commands.AlterarPesoProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.ValueObjects;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Tests.Handlers
{
    public class AlterarPesoCommandHandlerTest
    {
        private readonly Mock<IPortfolioService> _portfolioServiceMock;
        private readonly Mock<INotificator> _notificatorMock;

        private readonly AlterarPesoProdutoFinanceiroCommandHandler _handler;

        public AlterarPesoCommandHandlerTest()
        {
            _portfolioServiceMock = new Mock<IPortfolioService>();
            _notificatorMock = new Mock<INotificator>();

            _handler = new(_portfolioServiceMock.Object, _notificatorMock.Object);
        }

        [Fact]
        public async Task AlterarPeso_Sucesso()
        {
            var command = new AlterarPesoProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4", 10);

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GerarPortfolio());

            _portfolioServiceMock.Setup(x => x.AtualizarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()))
                .Verifiable();

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            var result = await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task AlterarPeso_IdPortfolioInvalido_Falha()
        {
            var command = new AlterarPesoProdutoFinanceiroCommand("", "PETR4", 10);

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            var result = await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task AlterarPeso_PortfolioInexistente_Falha()
        {
            var command = new AlterarPesoProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4", 10);

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Dominio.Portfolio)null!);

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            var result = await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task AlterarPeso_ProdutoFinanceiroInexistente_Falha()
        {
            var command = new AlterarPesoProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4", 10);

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GerarPortfolio(false));

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            var result = await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
        }

        private static Dominio.Portfolio GerarPortfolio(bool incluirItens = true)
        {
            var portfolio = new Dominio.Portfolio("Teste", PortfolioPerfil.Conservador);

            if (incluirItens)
            {
                portfolio.AdicionarProdutoFinanceiro(GerarProdutoFinanceiro());
            }

            return portfolio;
        }

        private static ProdutoFinanceiro GerarProdutoFinanceiro()
        {
            return new ProdutoFinanceiro("PETR4",
                ProdutoCategoria.Ações,
                10,
                DateTime.Now.AddDays(50));
        }
    }
}
