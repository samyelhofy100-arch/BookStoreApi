using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class SaveCategoryDto
{
    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
}