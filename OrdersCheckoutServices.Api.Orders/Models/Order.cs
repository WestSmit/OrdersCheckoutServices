using System.ComponentModel.DataAnnotations;

namespace Order.Api.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderProduct[] Products { get; set; }
    }

    public class OrderRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string UserId { get; set; }

        [Required, MinLength(1)]
        public OrderProduct[] Products { get; set; }
    }

    public class OrderProduct
    {
        [Required(AllowEmptyStrings = false)]
        public string ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
    }
}
