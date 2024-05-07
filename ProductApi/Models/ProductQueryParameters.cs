namespace ProductApi.Models
{
    public class ProductQueryParameters : QueryParameters
    {
        public double? MaxPrice { get; set; }
        public double? MinPrice { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

    }
}
