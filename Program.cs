using HotelBooking;
using HotelBooking.Model;
using System.Text.Json;

public class Program
{
    static void Main(string[] args)
    {
        (List<Hotel> hotels, List<Booking> bookings) = ReadData(args);
        if (hotels == null || bookings == null)
        {
            return;
        }

        Program.RunBrowser(hotels, bookings); 
    }

    private static Tuple<List<Hotel>, List<Booking>> ReadData(string[] args)
    {
        if (args.Length != 4)
        {
            Console.WriteLine("Invalid start arguments");
            return new Tuple<List<Hotel>, List<Booking>>(null, null);
        }

        List<string> argsList = new List<string>(args);
        if (Math.Abs(argsList.IndexOf("--hotels")) % 2 == 1 || Math.Abs(argsList.IndexOf("--bookings")) % 2 == 1)
        {
            Console.WriteLine("Invalid start arguments");
            return new Tuple<List<Hotel>, List<Booking>>(null, null);
        }

        try
        {
            string json = File.ReadAllText(args[argsList.IndexOf("--hotels") + 1]);
            List<Hotel> hotels = JsonSerializer.Deserialize<List<Hotel>>(json);

            json = File.ReadAllText(argsList[argsList.IndexOf("--bookings") + 1]);
            List<Booking> bookings = JsonSerializer.Deserialize<List<Booking>>(json);
            return new Tuple<List<Hotel>, List<Booking>>(hotels, bookings);
        }
        catch (FileNotFoundException exception)
        {
            Console.WriteLine(exception.Message);
            return new Tuple<List<Hotel>, List<Booking>>(null, null);
        }

    }

    private static void RunBrowser(List<Hotel> hotels, List<Booking> bookings)
    {
        BookingBrowser bookingBrowser = new BookingBrowser(hotels, bookings);

        string query;
        do
        {
            Console.WriteLine("Enter booking query:");
            query = Console.ReadLine();
            if (string.IsNullOrEmpty(query.Trim()))
            {
                break;
            }

            try
            {
                int freeRooms = bookingBrowser.CheckAvailability(query);
                Console.WriteLine($"Available rooms: {freeRooms}");
            }
            catch (Exception expection)
            {
                Console.WriteLine(expection.Message);
            }

        } while (!string.IsNullOrEmpty(query));
    }
}
