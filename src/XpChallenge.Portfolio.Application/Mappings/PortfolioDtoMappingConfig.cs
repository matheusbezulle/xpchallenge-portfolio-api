using Mapster;
using XpChallenge.Portfolio.Application.DataTransfer;
using Dominio = XpChallenge.Portfolio.Domain.Entities;

namespace XpChallenge.Portfolio.Application.Mappings
{
    internal class PortfolioDtoMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Dominio.Portfolio, PortfolioDto>()
                .Map(dest => dest.Id, src => src.Id.ToString())
                .Map(dest => dest.IdPerfil, src => (int)src.Perfil)
                .Map(dest => dest.Perfil, src => src.Perfil.ToString());
        }
    }
}
