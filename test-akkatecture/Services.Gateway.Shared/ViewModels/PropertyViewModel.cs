using System;
using System.Collections.Generic;

namespace Services.Gateway.Shared.ViewModels
{
    [Serializable]
    public sealed class PropertyViewModel
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string IconImageUrl { get; set; }

        public string BannerImageUrl { get; set; }

        public List<string> CarouselImageUrls { get; set; }

        public string ArModelUrl { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public string PropertyType { get; set; }

        public int GrossAreaSqm { get; set; }

        public int NetAreaSqm { get; set; }

        public string DividendSchedule { get; set; }

        public DateTime PreviousDividendPayment { get; set; }

        public bool IsTenanted { get; set; }

        public int ContractMonths { get; set; }
    }
}
