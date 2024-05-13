using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Commands.AlterarProdutoFinanceiro
{
    public class AlterarProdutoFinanceiroCommandHandler(IPortfolioService portfolioService,
        INotificator notificator) : IRequestHandler<AlterarProdutoFinanceiroCommand, AlterarProdutoFinanceiroCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;
        private readonly INotificator _notificator = notificator;

        public async Task<AlterarProdutoFinanceiroCommandResponse> Handle(AlterarProdutoFinanceiroCommand request, CancellationToken cancellationToken)
        {
            var response = new AlterarProdutoFinanceiroCommandResponse();

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
            produtoFinanceiro.AtualizarDataVencimento(request.DataVencimento);

            await _portfolioService.AtualizarAsync(portfolio, cancellationToken);

            return response;
        }
    }
}
