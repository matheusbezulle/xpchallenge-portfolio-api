﻿using MongoDB.Bson;
using Dominio = XpChallenge.Portfolio.Domain.AggregateRoots;

namespace XpChallenge.Portfolio.Application.Services.Interfaces
{
    public interface IPortfolioService
    {
        Task<string> CriarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken);
        Task AtualizarAsync(Dominio.Portfolio portfolio, CancellationToken cancellationToken);
        Task ExcluirAsync(ObjectId id, CancellationToken cancellationToken);
        Task<Dominio.Portfolio> ObterPorIdAsync(ObjectId id, CancellationToken cancellationToken);
    }
}
