using Services.Product.Shared.DataTransferObjects;
using Services.Product.Shared.Queries.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Product.Application.Queries
{
    public sealed class PropertyQueries : IPropertyQueries
    {
        private readonly Dictionary<string, PropertyDto> _properties;

        private readonly Dictionary<string, string> _propertyCollections;

        private readonly Dictionary<string, List<PropertyCollectionItemDto>> _propertyCollectionItems;

        public PropertyQueries()
        {
            _properties = new Dictionary<string, PropertyDto>
            {
                { "HUBA-1000", CreateFakeProperty("HUBA-1000", "Baba Beach Club", "Hua Hin") },
                { "HUBA-1001", CreateFakeProperty("HUBA-1001", "Baba Beach Club", "Hua Hin") },
                { "HUBA-1002", CreateFakeProperty("HUBA-1002", "Baba Beach Club", "Hua Hin") },
                { "PHSP-1010", CreateFakeProperty("PHSP-1010", "Sri Panwa Resort", "Phuket") },
                { "PHSP-2010", CreateFakeProperty("PHSP-2010", "Sri Panwa Resort", "Phuket") },
                { "BTTA-1010", CreateFakeProperty("BTTA-1010", "Baan Thew Thalay Aquamarine", "Hua Hin") },
                { "BTTB-1020", CreateFakeProperty("BTTB-1020", "Baan Thew Thalay Blue Sapphire", "Hua Hin") },
                { "HUBC-1000", CreateFakeProperty("HUBC-1000", "Baan Chok", "Hua Hin") },
                { "BKIC-1032", CreateFakeProperty("BKIC-1032", "Issara Collection Sathorn", "Bangkok") },
                { "BKIS-2001", CreateFakeProperty("BKIS-2001", "Issi Condo Suksawat", "Phuket") }
            };

            _propertyCollections = new Dictionary<string, string>();

            _propertyCollections.Add("1", "Top 100");
            _propertyCollections.Add("2", "Bangkok Winners");
            _propertyCollections.Add("3", "Last Weeks Gainers");
            _propertyCollections.Add("4", "Recently Listed");

            _propertyCollectionItems = new Dictionary<string, List<PropertyCollectionItemDto>>();

            var collectionItems = _properties.Take(5).Select(i => new PropertyCollectionItemDto
            {
                Symbol = i.Key,
                Name = i.Value.Name,
                Location = i.Value.Location,
                IconImageUrl = i.Value.IconImageUrl,
            });

            _propertyCollectionItems.Add("1", collectionItems.Take(10).ToList());
            _propertyCollectionItems.Add("2", collectionItems.Take(5).ToList());
            _propertyCollectionItems.Add("3", collectionItems.Take(8).ToList());
            _propertyCollectionItems.Add("4", collectionItems.Take(3).ToList());
        }

        public async Task<PropertyDto> GetPropertyAsync(string symbol)
        {
            if (_properties.TryGetValue(symbol, out var property))
            {
                return await Task.FromResult(property);
            }

            return await Task.FromResult(default(PropertyDto));
        }

        public async Task<List<PropertyCollectionItemDto>> GetPropertyCollection(string id)
        {
            var result = new List<PropertyCollectionItemDto>();

            if (_propertyCollectionItems.TryGetValue(id, out var collectionItems))
            {
                result = collectionItems;
            }

            return await Task.FromResult(result);
        }

        public async Task<PropertyCollectionsDto> GetPropertyCollections(int count)
        {
            var result = _propertyCollections.Take(count);

            var propertyCollections = new PropertyCollectionsDto
            {
                UpdatedTimeStamp = DateTime.UtcNow,
                PropertyCollections = result.ToDictionary(k => k.Key, v => v.Value),
            };

            return await Task.FromResult(propertyCollections);
        }

        public async Task<List<PropertyDto>> GetTrendingProperties(int count)
        {
            var result = _properties.Values.Take(count);

            return await Task.FromResult(result.ToList());
        }

        public async Task<List<PropertyDto>> SearchPropertiesAsync(string searchText)
        {
            var result = new List<PropertyDto>();

            foreach (var property in _properties)
            {
                if (property.Key.IndexOf(searchText, 0, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    result.Add(property.Value);
                    continue;
                }

                if (property.Value.Name.IndexOf(searchText, 0, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    result.Add(property.Value);
                }
            }

            return await Task.FromResult(result);
        }

        private PropertyDto CreateFakeProperty(string symbol, string name, string location)
        {
            var property = new PropertyDto
            {
                Symbol = symbol,
                Name = name,
                Summary = "The latest edition to the beach club collection perfectly embodies the aspects of idyllic luxury and elegance with a relaxed as well as energetic atmosphere.",
                Location = location,
                Description = "A stunning gated 5 bedroom residence offering exceptional living accomodation, extneding over 6,800sq ft, set within 1.78 acres of secluded grounds.",
                ArModelUrl = $"/{symbol}-model.unity3d",
                IconImageUrl = $"images/{symbol.ToUpperInvariant()}/avatar.png",
                CarouselImageUrls = getImageList(symbol, 5),
                BannerImageUrl = $"{symbol}-01.jpg",
                PropertyType = "Villa",
                Bathrooms = 2,
                Bedrooms = 4,
                ContractMonths = 24,
                DividendSchedule = "Monthly",
                PreviousDividendPayment = DateTime.UtcNow.AddDays(-30),
                GrossAreaSqm = 488,
                NetAreaSqm = 420,
                IsTenanted = true,
            };

            List<string> getImageList(string sym, int count)
            {
                var images = new List<string>(count);

                for (var i = 0; i < count; i++)
                {
                    images.Add($"images/{symbol.ToUpperInvariant()}/preview-0{i + 1}.jpg");
                }

                return images;
            }

            return property;
        }
    }
}
