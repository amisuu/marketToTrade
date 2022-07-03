using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.DTOs
{
    public class AddNewAssetDto
    {
        public string Type { get; set; }
        public string Form { get; set; }
        public string Mass { get; set; }
        public string Fineness { get; set; }
        public int Quantity { get; set; }
        public string Producer { get; set; }
        public string Price { get; set; }
        public int Year { get; set; }
        public string Condition { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public YesOrNoEnum IsOriginalPackage { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public YesOrNoEnum IsReceipt { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}
