using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Commands.AlterarPesoProdutoFinanceiro
{
    public class AlterarPesoProdutoFinanceiroCommandHandler(IPortfolioService portfolioService,
        INotificator notificator) : IRequestHandler<AlterarPesoProdutoFinanceiroCommand, AlterarPesoProdutoFinanceiroCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;
        private readonly INotificator _notificator = notificator;

        public async Task<AlterarPesoProdutoFinanceiroCommandResponse> Handle(AlterarPesoProdutoFinanceiroCommand request, CancellationToken cancellationToken)
        {
            var response = new AlterarPesoProdutoFinanceiroCommandResponse();

            if (!ObjectId.TryParse(request.IdPortfolio, out var id))
            {
                _notificator.AdicionarErroNegocio("O 'IdPortfolio' informado é inválido.");
                return response;
            }

            var portfolio = await _portfolioService.ObterPorIdAsync(id, cancellationToken);

            if (portfolio == null)
            {
                _notificator.AdicionarErroNegocio("Não foi possível encontrar o portfolio informado.");
                return response;
            }

            var produtoFinanceiro = portfolio.ObterProdutoFinanceiro(request.Nome);

            if (produtoFinanceiro == null)
            {
                _notificator.AdicionarErroNegocio("Não foi possível encontrar o produto financeiro informado.");
                return response;
            }

            produtoFinanceiro.AtualizarPeso(request.Peso);
            await _portfolioService.AtualizarAsync(portfolio, cancellationToken);

            return response;
        }
    }
}
