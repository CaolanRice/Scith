namespace Scith.Entities
{
    public record Item{

        //only allow setting values during init
        public Guid Id {get; init;} 
        public string Name {get; init;}
        public decimal Price {get; init;}
        public DateTimeOffset CreatedDate {get; init;}
    }
}