namespace BookStore.Api.Models;

public class Book : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PublishYear { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }

    public decimal? DiscountPercent { get; set; }
    public string? DiscountCode { get; set; }

    public double Rating { get; set; }
    public int ReviewsCount { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int? PublisherId { get; set; }
    public Publisher? Publisher { get; set; }

    public decimal GetFinalPrice()
    {
        if (DiscountPercent is null or <= 0) return Price;
        return Math.Round(Price - (Price * DiscountPercent.Value / 100), 2);
    }
}