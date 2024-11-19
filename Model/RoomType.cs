using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBooking.Model
{
    public class RoomType
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("amenities")]
        public List<string> Amenities { get; set; }
        [JsonPropertyName("features")]
        public List<string> Features { get; set; }
    }
}
