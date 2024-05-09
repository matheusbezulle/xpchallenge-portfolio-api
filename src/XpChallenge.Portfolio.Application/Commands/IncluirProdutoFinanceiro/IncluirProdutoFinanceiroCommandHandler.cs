using MapsterMapper;
using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;
using XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro
{
    public class IncluirProdutoFinanceiroCommandHandler(IPortfolioService portfolioService,
        IMapper mapper,
        INotificator notificator) : IRequestHandler<IncluirProdutoFinanceiroCommand, IncluirProdutoFinanceiroCommandResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;
        private readonly IMapper _mapper = mapper;
        private readonly INotificator _notificator = notificator;

        public async Task<IncluirProdutoFinanceiroCommandResponse> Handle(IncluirProdutoFinanceiroCommand request, CancellationToken cancellationToken)
        {
            var response = new IncluirProdutoFinanceiroCommandResponse();

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

            if (portfolio.VerificarProdutoFinanceiroExistente(request.Nome))
            {
                _notificator.AdicionarErroNegocio("O produto financeiro informado já está adicionado ao portfolio.");
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
