namespace BookStore.Api.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PublishYear { get; set; }
    public decimal Price { get; set; }
    public decimal FinalPrice { get; set; }
    public decimal? DiscountPercent { get; set; }
    public string? DiscountCode { get; set; }
    public double Rating { get; set; }
    public int ReviewsCount { get; set; }
    public string? ImageUrl { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int? PublisherId { get; set; }
    public string? PublisherName { get; set; }
}