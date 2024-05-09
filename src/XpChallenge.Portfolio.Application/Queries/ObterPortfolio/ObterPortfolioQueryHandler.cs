using MapsterMapper;
using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.DataTransfer;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Queries.ObterPortfolio
{
    public class ObterPortfolioQueryHandler(IPortfolioService portfolioService,
        IMapper mapper) : IRequestHandler<ObterPortfolioQuery, ObterPortfolioQueryResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;
        private readonly IMapper _mapper = mapper;

        public async Task<ObterPortfolioQueryResponse> Handle(ObterPortfolioQuery request, CancellationToken cancellationToken)
        {
            var response = new ObterPortfolioQueryResponse();

            if (!ObjectId.TryParse(request.IdPortfolio, out var id))
            {
                //add msg Id inválido
                return response;
            }

            var portfolio = await _portfolioService.ObterPorIdAsync(id, cancellationToken);

            if (portfolio == null)
            {
                //add msg Portfolio nao encontrado
                return response;
            }

            response.Portfolio = _mapper.Map<PortfolioDto>(portfolio);
            return response;
        }
    }
}
