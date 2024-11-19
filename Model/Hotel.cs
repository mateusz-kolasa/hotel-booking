using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelBooking.Model
{
    public class Hotel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("roomTypes")]
        public List<RoomType> RoomTypes { get; set; }
        [JsonPropertyName("rooms")]
        public List<Room> Rooms { get; set; }
    }
}
