using System.ComponentModel.DataAnnotations;

namespace Store.Service.Services.BasketServices.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
    }
}