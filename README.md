# hotel-booking

## To Launch
```
dotnet restore
dotnet build
dotnet run --bookings <path_to_bookings.json> --hotels <path_to_hotels.json>
```

Then query with following format:
Availability(<hotel_id>, <period>, <room_type>)

