using Akka.Actor;
using Services.Gateway.Api.Actors;
using Services.Gateway.Api.Actors.Product.Messages;
using Services.Gateway.Shared.ViewModels;
using Services.Product.Shared.DataTransferObjects;
using Services.Product.Shared.Queries.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Gateway.Api.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly IActorRef _productQueryActor;

        public ProductService(ActorSystemService actorSystemService)
        {
            _productQueryActor = actorSystemService.ProductQueryActor;
        }

        public async Task<PropertyViewModel> GetProperty(string symbol)
        {
            var response = await _productQueryActor.Ask<PropertyDto>(new GetPropertyMessage(symbol));

            return MapDto(response);
        }

        public async Task<List<PropertyViewModel>> GetTrendingProperties(int count)
        {
            var response = await _productQueryActor.Ask<List<PropertyDto>>(new GetTrendingPropertiesMessage(count));

            var result = new List<PropertyViewModel>();

            foreach (var dto in response)
            {
                result.Add(MapDto(dto));
            }

            return result;
        }

        public async Task<PropertyCollectionsViewModel> GetPropertyCollections(int count)
        {
            var response = await _productQueryActor.Ask<PropertyCollectionsDto>(new GetPropertyCollectionsMessage(count));

            var result = new PropertyCollectionsViewModel
            {
                UpdatedTimeStamp = response.UpdatedTimeStamp,
                PropertyCollections = response.PropertyCollections,
            };

            return result;
        }

        public async Task<List<PropertyCollectionItemViewModel>> GetPropertyCollection(string id)
        {
            var response = await _productQueryActor.Ask<List<PropertyCollectionItemDto>>(new GetPropertyCollectionMessage(id));

            var result = new List<PropertyCollectionItemViewModel>();

            foreach(var dto in response)
            {
                result.Add(new PropertyCollectionItemViewModel
                {
                    Symbol = dto.Symbol,
                    Name = dto.Name,
                    Location = dto.Location,
                    IconImageUrl = dto.IconImageUrl,
                });
            }

            return result;
        }

        public async Task<List<PropertyViewModel>> SearchProperties(string searchText)
        {
            var response = await _productQueryActor.Ask<List<PropertyDto>>(new SearchPropertiesMessage(searchText));

            var result = new List<PropertyViewModel>();

            foreach (var dto in response)
            {
                result.Add(MapDto(dto));
            }

            return result;
        }

        private static PropertyViewModel MapDto(PropertyDto dto)
        {
            return new PropertyViewModel
            {
                Symbol = dto.Symbol,
                Name = dto.Name,
                Summary = dto.Summary,
                Description = dto.Description,
                Location = dto.Location,
                ArModelUrl = dto.ArModelUrl,
                BannerImageUrl = dto.BannerImageUrl,
                Bathrooms = dto.Bathrooms,
                Bedrooms = dto.Bedrooms,
                CarouselImageUrls = dto.CarouselImageUrls,
                ContractMonths = dto.ContractMonths,
                DividendSchedule = dto.DividendSchedule,
                GrossAreaSqm = dto.GrossAreaSqm,
                IconImageUrl = dto.IconImageUrl,
                IsTenanted = dto.IsTenanted,
                NetAreaSqm = dto.NetAreaSqm,
                PreviousDividendPayment = dto.PreviousDividendPayment,
                PropertyType = dto.PropertyType,
            };
        }
    }
}
