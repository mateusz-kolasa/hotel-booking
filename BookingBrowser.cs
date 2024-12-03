using HotelBooking.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{
    public class BookingBrowser
    {
        private const string DATE_FORMAT = "yyyyMMdd";

        private List<Hotel> hotels;
        private List<Booking> bookings;

        public BookingBrowser(List<Hotel> hotels, List<Booking> bookings)
        {
            this.hotels = hotels; 
            this.bookings = bookings;
        }

        private Tuple<Hotel, Tuple<string, string>, String> TryParseQuery(string query)
        {
            if (!query.StartsWith("Availability(") || !query.EndsWith(')'))
            {
                throw new Exception("Invalid command");
            }

            int start = query.IndexOf('(');
            int end = query.IndexOf(')');

            string[] data = query.Substring(start + 1, end - start - 1).Split(',');

            if (data == null || data.Length != 3)
            {
                throw new Exception("Invalid command");
            }

            Hotel hotel = this.hotels.Find((hotel) => hotel.Id == data[0].Trim());

            if (hotel == null)
            {
                throw new Exception("Hotel with given id does not exist");
            }

            if (!hotel.RoomTypes.Any((roomType) => roomType.Code == data[2].Trim()))
            {
                throw new Exception("Hotel does not have given room type");
            }

            
            string[] period = data[1].Trim().Split("-");
            string arrival = period[0].Trim();
            string departure = period.Length == 1 ? arrival : period[1].Trim();

            if (!IsDateValid(arrival) || !IsDateValid(departure))
            {
                throw new Exception("Provided date is not valid");
            }

            if (arrival.CompareTo(departure) > 0)
            {
                throw new Exception("Arrival can not be after departure");
            }

            return new Tuple<Hotel, Tuple<string, string>, string>(hotel, new Tuple<string, string>(arrival, departure), data[2].Trim());
        }

        public int CheckAvailability (string query)
        {
            (Hotel hotel, Tuple<string, string> dateRange, string roomType) = TryParseQuery(query);
            int roomsOfType = hotel.Rooms.Where((room) => room.RoomType == roomType).Count();

            if (roomsOfType == 0)
            {
                return 0;
            }

            string arrival = dateRange.Item1;
            string departure = dateRange.Item2;

            foreach (Booking booking in bookings)
            {
                if (booking.HotelId == hotel.Id && booking.RoomType == roomType)
                {
                    if (booking.Departure.CompareTo(arrival) >= 0 && booking.Arrival.CompareTo(departure) <= 0)
                    {
                        roomsOfType--;
                    }
                }
            }

            return roomsOfType;
        }

        private bool IsDateValid(string dateString)
        {
            return DateTime.TryParseExact(dateString, DATE_FORMAT, CultureInfo.CurrentCulture, DateTimeStyles.None, out _);
        }
    }
}
