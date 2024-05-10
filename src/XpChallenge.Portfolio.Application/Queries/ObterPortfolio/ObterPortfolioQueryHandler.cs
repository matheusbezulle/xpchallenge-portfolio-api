using MapsterMapper;
using MediatR;
using MongoDB.Bson;
using XpChallenge.Portfolio.Application.DataTransfer;
using XpChallenge.Portfolio.Application.Notifications;
using XpChallenge.Portfolio.Application.Services.Interfaces;

namespace XpChallenge.Portfolio.Application.Queries.ObterPortfolio
{
    public class ObterPortfolioQueryHandler(IPortfolioService portfolioService,
        IMapper mapper,
        INotificator notificator) : IRequestHandler<ObterPortfolioQuery, ObterPortfolioQueryResponse>
    {
        private readonly IPortfolioService _portfolioService = portfolioService;
        private readonly IMapper _mapper = mapper;
        private readonly INotificator _notificator = notificator;

        public async Task<ObterPortfolioQueryResponse> Handle(ObterPortfolioQuery request, CancellationToken cancellationToken)
        {
            var response = new ObterPortfolioQueryResponse();

            if (!ObjectId.TryParse(request.IdPortfolio, out var id))
            {
                _notificator.AdicionarErroNegocio("O 'IdPortfolio' informado é inválido.");
                return response;
            }

            var portfolio = await _portfolioService.ObterPorIdAsync(id, cancellationToken);

            if (portfolio is null)
                return response;

            response.Portfolio = _mapper.Map<PortfolioDto>(portfolio);
            TotalizarPorcentagens(response.Portfolio);

            response.Mensagens = _notificator.ObterMensagens();
            return response;
        }

        private static void TotalizarPorcentagens(PortfolioDto portfolioDto)
        {
            int somaTotal = portfolioDto.ProdutosFinanceiros.Sum(obj => obj.Peso);

            CalcularPorcentagemObjetos(portfolioDto.ProdutosFinanceiros, somaTotal);
            CalcularPorcentagemCategorias(portfolioDto, somaTotal);
        }

        private static void CalcularPorcentagemObjetos(List<ProdutoFinanceiroDto> produtosFinanceiros, int somaTotal)
        {
            double somaPorcentagens = 0;

            for (int i = 0; i < produtosFinanceiros.Count; i++)
            {
                if (i == produtosFinanceiros.Count - 1)
                    produtosFinanceiros[i].Porcentagem = 100.0 - somaPorcentagens;
                else
                {
                    double porcentagem = Math.Round((double)produtosFinanceiros[i].Peso / somaTotal * 100, 2);
                    somaPorcentagens += porcentagem;
                    produtosFinanceiros[i].Porcentagem = porcentagem;
                }
            }
        }

        private static void CalcularPorcentagemCategorias(PortfolioDto portfolioDto, int somaTotal)
        {
            var categorias = portfolioDto.ProdutosFinanceiros.GroupBy(obj => obj.Categoria).ToList();
            double somaPorcentagens = 0;

            for (int i = 0; i < categorias.Count; i++)
            {
                if (i == categorias.Count - 1)
                    portfolioDto.Totalizadores.Add(new(categorias[i].Key, Math.Round(100.0 - somaPorcentagens, 2)));
                else
                {
                    int somaCategoria = categorias[i].Sum(obj => obj.Peso);
                    double porcentagemCategoria = Math.Round((double)somaCategoria / somaTotal * 100, 2);
                    somaPorcentagens += porcentagemCategoria;

                    portfolioDto.Totalizadores.Add(new(categorias[i].Key, porcentagemCategoria));
                }
            }
        }
    }
}
