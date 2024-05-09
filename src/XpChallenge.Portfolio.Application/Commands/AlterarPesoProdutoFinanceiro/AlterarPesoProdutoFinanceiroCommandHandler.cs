using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Commands.AlterarPesoProdutoFinanceiro
{
    public class AlterarPesoProdutoFinanceiroCommandHandler(IPortfolioService portfolioService) : IRequestHandler<AlterarPesoProdutoFinanceiroCommand, AlterarPesoProdutoFinanceiroCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;

        public async Task<AlterarPesoProdutoFinanceiroCommandResponse> Handle(AlterarPesoProdutoFinanceiroCommand request, CancellationToken cancellationToken)
        {
            var response = new AlterarPesoProdutoFinanceiroCommandResponse();

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

            produtoFinanceiro.AtualizarPeso(request.Peso);

            await _portfolioService.AtualizarAsync(portfolio, cancellationToken);

            response.Sucesso = true;
            return response;
        }
    }
}
