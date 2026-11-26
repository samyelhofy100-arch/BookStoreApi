using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.DTOs;

public class SaveBookDto
{
    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Author { get; set; } = string.Empty;

    [Range(1000, 2100)]
    public int PublishYear { get; set; }

    [Range(0.01, 1000000)]
    public decimal Price { get; set; }

    [Range(0, 100)]
    public decimal? DiscountPercent { get; set; }

    [StringLength(50)]
    public string? DiscountCode { get; set; }

    public string? ImageUrl { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }

    public int? PublisherId { get; set; }
}