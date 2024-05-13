using MapsterMapper;
using MongoDB.Bson;
using Moq;
using XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.ValueObjects;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Tests.Handlers
{
    public class IncluirProdutoFinanceiroCommandHandlerTest
    {
        private readonly Mock<IPortfolioService> _portfolioServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<INotificator> _notificatorMock;

        private readonly IncluirProdutoFinanceiroCommandHandler _handler;

        public IncluirProdutoFinanceiroCommandHandlerTest()
        {
            _portfolioServiceMock = new Mock<IPortfolioService>();
            _mapperMock = new Mock<IMapper>();
            _notificatorMock = new Mock<INotificator>();

            _handler = new(_portfolioServiceMock.Object, _mapperMock.Object, _notificatorMock.Object);
        }

        [Fact]
        public async Task IncluirProdutoFinanceiro_Sucesso()
        {
            var command = new IncluirProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4", 1, 10, DateTime.Now.AddDays(5));

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GerarPortfolio(false));

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Never);
            _portfolioServiceMock.Verify(x => x.AtualizarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task IncluirProdutoFinanceiro_IdPortfolioInvalido_Falha()
        {
            var command = new IncluirProdutoFinanceiroCommand("teste", "PETR4", 1, 10, DateTime.Now.AddDays(5));

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
            _portfolioServiceMock.Verify(x => x.AtualizarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task IncluirProdutoFinanceiro_PortfolioInexistente_Falha()
        {
            var command = new IncluirProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4", 1, 10, DateTime.Now.AddDays(5));

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Dominio.Portfolio)null!);

            _notificatorMock.Setup(x => x.AdicionarErroNegocio(It.IsAny<string>()))
                .Verifiable();

            await _handler.Handle(command, new CancellationToken());

            _notificatorMock.Verify(x => x.AdicionarErroNegocio(It.IsAny<string>()), Times.Once);
            _portfolioServiceMock.Verify(x => x.AtualizarAsync(It.IsAny<Dominio.Portfolio>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task IncluirProdutoFinanceiro_ProdutoFinanceiroExistente_Falha()
        {
            var command = new IncluirProdutoFinanceiroCommand(ObjectId.GenerateNewId().ToString(), "PETR4", 1, 10, DateTime.Now.AddDays(5));

            _portfolioServiceMock.Setup(x => x.ObterPorIdAsync(It.IsAny<ObjectId>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GerarPortfolio());

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
