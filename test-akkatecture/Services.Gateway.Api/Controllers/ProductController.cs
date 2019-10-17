using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Gateway.Api.Services;
using Services.Gateway.Shared.ViewModels;

namespace Services.Gateway.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<PropertyViewModel> GetProperty(string symbol = "HUBA-1000")
        {
            return await _productService.GetProperty(symbol);
        }

        public async Task<List<PropertyViewModel>> SearchProperties(string searchText = "HUBA")
        {
            return await _productService.SearchProperties(searchText);
        }

        public async Task<List<PropertyViewModel>> GetTrendingProperties(int count = 1)
        {
            return await _productService.GetTrendingProperties(count);
        }

        public async Task<PropertyCollectionsViewModel> GetPropertyCollections(int count = 2)
        {
            return await _productService.GetPropertyCollections(count);
        }

        public async Task<List<PropertyCollectionItemViewModel>> GetPropertyCollection(string id = "1")
        {
            return await _productService.GetPropertyCollection(id);
        }
    }
}
