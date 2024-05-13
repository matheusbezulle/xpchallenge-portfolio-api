using MongoDB.Bson;
using Moq;
using XpChallenge.Portfolio.Application.Commands.RemoverProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.ValueObjects;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Tests.Handlers
{
    public class RemoverProdutoFinanceiroCommandHandlerTest
    {
        private readonly Mock<IPortfolioService> _portfolioServiceMock;
        private readonly Mock<INotificator> _notificatorMock;

        private readonly RemoverProdutoFinanceiroCommandHandler _handler;

        public RemoverProdutoFinanceiroCommandHandlerTest()
        {
            _portfolioServiceMock = new Mock<IPortfolioService>();
            _notificatorMock = new Mock<INotificator>();

            _handler = new(_portfolioServiceMock.Object, _notificatorMock.Object);
        }

        [Fact]
        public async Task RemoverProdutoFinanceiro_Sucesso()
        {
            var command = new RemoverProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4");

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GerarPortfolio());

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Never);
            _portfolioServiceMock.Verify(x => x.AtualizarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RemoverProdutoFinanceiro_IdPortfolioInvalido_Falha()
        {
            var command = new RemoverProdutoFinanceiroCommand("teste", "PETR4");

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
            _portfolioServiceMock.Verify(x => x.AtualizarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task RemoverProdutoFinanceiro_PortfolioInexistente_Falha()
        {
            var command = new RemoverProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4");

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Dominio.Portfolio)null!);

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
            _portfolioServiceMock.Verify(x => x.AtualizarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task RemoverProdutoFinanceiro_ProdutoFinanceiroInexistente_Falha()
        {
            var command = new RemoverProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4");

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GerarPortfolio(false));

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
            _portfolioServiceMock.Verify(x => x.AtualizarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()), Times.Never);
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
