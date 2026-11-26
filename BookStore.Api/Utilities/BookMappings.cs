using BookStore.Api.DTOs;
using BookStore.Api.Models;

namespace BookStore.Api.Utilities;

public static class BookMappings
{
    public static BookDto ToDto(this Book b) => new()
    {
        Id = b.Id,
        Title = b.Title,
        Description = b.Description,
        Author = b.Author,
        PublishYear = b.PublishYear,
        Price = b.Price,
        FinalPrice = b.GetFinalPrice(),
        DiscountPercent = b.DiscountPercent,
        DiscountCode = b.DiscountCode,
        Rating = b.Rating,
        ReviewsCount = b.ReviewsCount,
        ImageUrl = b.ImageUrl,
        CategoryId = b.CategoryId,
        CategoryName = b.Category?.Name ?? string.Empty,
        PublisherId = b.PublisherId,
        PublisherName = b.Publisher?.Name
    };

    public static void Apply(this Book b, SaveBookDto d)
    {
        b.Title = d.Title;
        b.Description = d.Description;
        b.Author = d.Author;
        b.PublishYear = d.PublishYear;
        b.Price = d.Price;
        b.DiscountPercent = d.DiscountPercent;
        b.DiscountCode = d.DiscountCode;
        b.ImageUrl = d.ImageUrl;
        b.CategoryId = d.CategoryId;
        b.PublisherId = d.PublisherId;
    }
}