using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Commands.RemoverProdutoFinanceiro
{
    public class RemoverProdutoFinanceiroCommandHandler(IPortfolioService portfolioService) : IRequestHandler<RemoverProdutoFinanceiroCommand, RemoverProdutoFinanceiroCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;

        public async Task<RemoverProdutoFinanceiroCommandResponse> Handle(RemoverProdutoFinanceiroCommand request, CancellationToken cancellationToken)
        {
            var response = new RemoverProdutoFinanceiroCommandResponse();

            if (!ObjectId.TryParse(request.IdPortfolio, out var id))
            {
                //add msg idPortfolio invalido
                return response;
            }

            var portfolio = await _portfolioService.ObterPorIdAsync(id, cancellationToken);

            if (portfolio == null)
            {
                //add msg portfolio inexistente
                return response;
            }

            var produtoFinanceiro = portfolio.ObterProdutoFinanceiro(request.Nome);

            if (produtoFinanceiro == null)
            {
                //add msg produto inexistente
                return response;
            }

            portfolio.RemoverProdutoFinanceiro(produtoFinanceiro);

            await _portfolioService.AtualizarAsync(portfolio, cancellationToken);

            response.Sucesso = true;
            return response;
        }
    }
}
