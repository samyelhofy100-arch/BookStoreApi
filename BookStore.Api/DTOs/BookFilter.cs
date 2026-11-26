namespace BookStore.Api.DTOs;

public class BookFilter
{
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? PublisherId { get; set; }
    public int? Year { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 12;
}