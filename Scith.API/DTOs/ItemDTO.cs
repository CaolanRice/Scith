namespace Scith.API.DTOs

//This DTO is used to encapsulate data between the client and server, allows control over which data is exposed by APIs 
{
    public record ItemDTO
    {

        //only allow setting values during init
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }
    }
}