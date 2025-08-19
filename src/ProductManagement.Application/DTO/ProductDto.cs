namespace ProductManagement.API.Controllers
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageFileName { get; set; }
    }
}
