namespace Shared.Data.Dtos.ConsumerDtos;

public record ConsumerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}