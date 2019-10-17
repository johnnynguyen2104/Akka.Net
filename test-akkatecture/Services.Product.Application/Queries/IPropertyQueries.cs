using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Product.Shared.DataTransferObjects;
using Services.Product.Shared.Queries.DataTransferObjects;

namespace Services.Product.Application.Queries
{
    public interface IPropertyQueries
    {
        Task<PropertyDto> GetPropertyAsync(string symbol);

        Task<List<PropertyDto>> SearchPropertiesAsync(string searchText);

        Task<List<PropertyDto>> GetTrendingProperties(int count);

        Task<PropertyCollectionsDto> GetPropertyCollections(int count);

        Task<List<PropertyCollectionItemDto>> GetPropertyCollection(string id);
    }
}
