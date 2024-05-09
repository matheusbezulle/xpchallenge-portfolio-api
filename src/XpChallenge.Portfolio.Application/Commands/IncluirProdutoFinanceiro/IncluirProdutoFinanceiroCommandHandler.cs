using MapsterMapper;
using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro
{
    public class IncluirProdutoFinanceiroCommandHandler(IPortfolioService portfolioService,
        IMapper mapper) : IRequestHandler<IncluirProdutoFinanceiroCommand, IncluirProdutoFinanceiroCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;
        private readonly IMapper _mapper = mapper;

        public async Task<IncluirProdutoFinanceiroCommandResponse> Handle(IncluirProdutoFinanceiroCommand request, CancellationToken cancellationToken)
        {
            var response = new IncluirProdutoFinanceiroCommandResponse();

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

            if (portfolio.VerificarProdutoFinanceiroExistente(request.Nome))
            {
                //add msg produto já adicionado
                return response;
            }

            var produtoFinanceiro = _mapper.Map<ProdutoFinanceiro>(request);
            portfolio.AdicionarProdutoFinanceiro(produtoFinanceiro);

            await _portfolioService.AtualizarAsync(portfolio, cancellationToken);

            response.Sucesso = true;
            return response;
        }
    }
}
