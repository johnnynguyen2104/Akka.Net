using Akkatecture.Aggregates;
using Akkatecture.Core;
using Akkatecture.Entities;

namespace Brokerage.Service.Domain.Aggregates.RepositoryAbstractions
{
    /// <summary>
    /// The base Repository.
    /// </summary>
    /// <typeparam name="TAggregateRoot">This is a way to have the code enforce the convention that each repository is related to a single aggregate is to implement a generic repository type</typeparam>
    public interface IRepository<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
    }
}
