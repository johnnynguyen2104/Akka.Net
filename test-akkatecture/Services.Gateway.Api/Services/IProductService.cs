using Services.Gateway.Shared.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Gateway.Api.Services
{
    public interface IProductService
    {
        Task<PropertyViewModel> GetProperty(string symbol);

        Task<List<PropertyViewModel>> GetTrendingProperties(int count);

        Task<PropertyCollectionsViewModel> GetPropertyCollections(int count);

        Task<List<PropertyCollectionItemViewModel>> GetPropertyCollection(string id);

        Task<List<PropertyViewModel>> SearchProperties(string searchText);
    }
}
