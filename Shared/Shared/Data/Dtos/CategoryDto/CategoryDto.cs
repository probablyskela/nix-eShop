namespace Shared.Data.Dtos.CategoryDto;

public record CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}