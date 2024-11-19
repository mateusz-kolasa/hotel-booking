# hotel-booking

## To Launch

### With .NET installed
```
dotnet restore
dotnet build
dotnet run --bookings <path_to_bookings.json> --hotels <path_to_hotels.json>
```

### With docker
```
docker build -f Dockerfile -t hotel-booking .
docker run -i hotel-booking --bookings <path_to_bookings.json> --hotels <path_to_hotels.json>

For example, using the tests data
docker run -i --mount type=bind,src=$PWD\Tests,target=/Tests hotel-booking --bookings /Tests/bookings.json --hotels /Tests/hotels.json
```

# Usage
After launching succesfully query with following format:
Availability(<hotel_id>, <period>, <room_type>)

