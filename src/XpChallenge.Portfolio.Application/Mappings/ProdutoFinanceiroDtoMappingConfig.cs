using Mapster;
using XpChallenge.Portfolio.Application.DataTransfer;
using XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.Application.Mappings
{
    public class ProdutoFinanceiroDtoMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProdutoFinanceiro, ProdutoFinanceiroDto>()
                .Map(dest => dest.IdCategoria, src => (int)src.Categoria)
                .Map(dest => dest.Categoria, src => src.Categoria.ToString());
        }
    }
}
