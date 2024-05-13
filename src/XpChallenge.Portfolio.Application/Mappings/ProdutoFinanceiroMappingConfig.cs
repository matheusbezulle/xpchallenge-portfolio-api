using Mapster;
using XpChallenge.Portfolio.Application.Commands.IncluirProdutoFinanceiro;
using XpChallenge.Portfolio.Domain.ValueObjects;

namespace XpChallenge.Portfolio.Application.Mappings
{
    public class ProdutoFinanceiroMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<IncluirProdutoFinanceiroCommand, ProdutoFinanceiro>()
                .Map(dest => dest.Categoria, src => (ProdutoCategoria)src.IdCategoria);
        }
    }
}
