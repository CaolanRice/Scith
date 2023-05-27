namespace Scith.API.Entities{
    public record Item{

        //only allow setting values during init
        public Guid Id {get; init;} 
        public string Name {get; init;}
        public double Price {get; init;}
        public DateTimeOffset CreatedDate {get; init;}
    }
}