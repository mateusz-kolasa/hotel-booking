using HotelBooking.Model;
using System.Text.Json;
using Xunit.Sdk;

namespace HotelBooking;

public class BookingBrowserTest
{
    private BookingBrowser bookingBrowser;

    public BookingBrowserTest() 
    {
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

        string json = File.ReadAllText(projectDirectory + "/Tests/hotels.json");
        List<Hotel> hotels = JsonSerializer.Deserialize<List<Hotel>>(json);

        json = File.ReadAllText(projectDirectory + "/Tests/bookings.json");
        List<Booking> bookings = JsonSerializer.Deserialize<List<Booking>>(json);

        this.bookingBrowser = new BookingBrowser(hotels, bookings);
    }

    [Fact]
    public void ReturnAvailableRooms()
    {

        Assert.Equal(2, this.bookingBrowser.CheckAvailability("Availability(H1, 20240901, SGL)"));
        Assert.Equal(1, this.bookingBrowser.CheckAvailability("Availability(H1, 20240901-20240903, DBL)"));
        Assert.Equal(-1, this.bookingBrowser.CheckAvailability("Availability(H1, 20240901-20240910, DBL)"));
    }

    [Fact]
    public void ThrowsExceptionOnInvalidQuery()
    {
        Action action = () => this.bookingBrowser.CheckAvailability("(H1, 20240901, SGL)");
        Exception exception = Assert.Throws<Exception>(() => action());
        Assert.Equal("Invalid command", exception.Message);

        action = () => this.bookingBrowser.CheckAvailability("Availability(H1, 20240901-20240903)");
        exception = Assert.Throws<Exception>(() => action());
        Assert.Equal("Invalid command", exception.Message);
    }

    [Fact]
    public void ThrowsExcceptionIfHotelDoesntExist()
    {
        Action action = () => this.bookingBrowser.CheckAvailability("Availability(H2, 20240901, SGL)");
        Exception exception = Assert.Throws<Exception>(() => action());
        Assert.Equal("Hotel with given id does not exist", exception.Message);
    }

    [Fact]
    public void ThrowsExceptionIfRoomTypeDoesntExist()
    {
        Action action = () => this.bookingBrowser.CheckAvailability("Availability(H1, 20240901, SGLA)");
        Exception exception = Assert.Throws<Exception>(() => action());
        Assert.Equal("Hotel does not have given room type", exception.Message);
    }
}
