namespace ODataPostgresApi.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public double Price { get; set; }
    }
}
